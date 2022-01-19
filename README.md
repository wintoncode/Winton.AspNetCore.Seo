# Winton.AspNetCore.Seo

Add SEO metadata to your ASP.NET Core website.

[![NuGet Badge](https://buildstats.info/nuget/Winton.AspNetCore.Seo)](https://www.nuget.org/packages/Winton.AspNetCore.Seo/)

[![Build history](https://buildstats.info/github/chart/wintoncode/Winton.AspNetCore.Seo?branch=master)](https://github.com/wintoncode/Winton.AspNetCore.Seo/actions)

## What does it do

It makes it easy to add search engine metadata to your ASP.NET Core website, allowing you to focus on writing the code you want to write.
It provides three things:

- a robots.txt file,
- a sitemap.xml file, and
- HTML `<meta>` tags

You no longer have to look up the format of each of these separate protocols, or pollute your HTML with tons of `<meta>` tags.

It also gives you the flexibility to define the behaviour of how these files are generated at runtime.
This is useful because it means you can do things such as inform search engine bots not to crawl your development and staging sites, but allow them to index the production version, without having to write and maintain three separate files.

## How do I use it

### Installation

Add the package `Winton.AspNetCore.Seo` to the `.csproj` file of your project or install using nuget package manager:

```sh
Install-Package Winton.AspNetCore.Seo
```

### Minimal setup

1. Add required services in `ConfigureServices` in `Startup.cs`. For instance to use the default robots.txt settings and without defining a sitemap:

    ```csharp
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSeoWithDefaultRobots();
    }
    ```

1. Add the appropriate Open Graph TagHelper to your HTML files.
   For instance in `_Layout.cshtml`:

    ```html
    @using System;
    @using Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
    @addTagHelper *, Winton.AspNetCore.Seo

    <!DOCTYPE html>
    <html>
    <head>
        <open-graph-article
            audio="@{ new Audio("http://example.com") }"
            authors="@{ new List<string> { "http://example.com/choc13" } }"
            description="Test description"
            images="
            @{
                new List<Image>
                {
                    new Image("http://example.com/img1.jpg") { SecureUrl = "https://example.com/img1.jpg", Height = 250 },
                    new Image("http://example.com/img2.jpg") { SecureUrl = "https://example.com/img2.jpg", Height = 300, Width = 500 }
                };
            }"
            published-time="@{ DateTime.UtcNow }"
            title="Test"
        >
        </open-graph-article>
    </head>
    <body>
    </body>
    </html>
    ```

### Defining your sitemap

Your sitemap can be defined in various ways when calling either `AddSeoWithDefaultRobots` or `AddSeo`:

1. Using a delegate.
   For example:

    ```csharp
    services.AddSeoWithDefaultRobots(
        options => options.Urls = new List<SitemapUrlOptions>
        {
            new SitemapUrlOptions
            {
                Priority = 0.9M,
                RelativeUrl = "test"
            }
        });
    ```

    This is useful when you want to define your site map using code and keep the default robots.txt settings (see below for more details on customising robots.txt options).
    There is also a version of the delegate that has access to `IWebHostEnvironment` for when the configuration is dependant on the environment.

1. From config. For example:

    ```csharp
    services.AddSeo(_configuration);
    ```

    This is useful when you want to put your sitemap into config, such as `appsettings.json`.

    The config must contain a section with the key `"Seo"`, for example:

    ```json
    {
        "Seo": {
            "Sitemap": {
                "Urls": [
                    {
                        "Priority": 0.9,
                        "RelativeUrl": "/login"
                    },
                    {
                        "Priority": 0.8,
                        "RelativeUrl": "/about"
                    }
                ]
            }
        }
    }
    ```

    _Note that this method does not set any default robots.txt options, so you need to also configure the robots.txt as required by your application._

### Customising the robots.txt file

The robots.txt file can be configured in the same manner as the sitemap.
If `AddSeoWithDefaultRobots` is called the default robots.txt options will be used which applies the following policy:

- The URL of the sitemap is specified in all environments.
- In the production environment all user agents are allowed to index any part of the site.
- In all other environments, all user agents are disallowed from indexing any part of the site.

_Note: It's always worth remembering that malicious bots can just ignore a robots.txt file, so it should not be relied on to hide any sensitive parts of the site._

Calling `AddSeo` instead allows you to specify the options. There are several ways they can be specified:

1. Using a delegate.
   For example:

    ```csharp
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSeo(
            options =>
            {
                options.RobotsTxt.AddSitemapUrl = false;
                options.RobotsTxt.UserAgentRecords = new List<UserAgentRecord>
                {
                    new UserAgentRecord
                    {
                        Disallow = new List<string> { "/login" },
                        UserAgent = "bing"
                    }
                };
            });
    }
    ```

    This is useful when you want to define your the robots.txt using code.
    You can also configure the sitemap in the same delegate.
    There is also a version of the delegate that has access to `IWebHostEnvironment` for when the configuration is dependant on the environment.

1. From config.
   For example:

    ```csharp
    services.AddSeo(_configuration);
    ```

    This is useful when you want to define the robots.txt in config, such as `appsettings.json`.

    The config must contain a section with the key `"Seo"`, for example:

    ```json
    {
        "Seo": {
            "RobotsTxt": {
                "AddSitemapUrl": false,
                "UserAgentRecords": [
                    {
                        "UserAgent": "bing",
                        "Disallow": [ "/login" ]
                    },
                    {
                        "UserAgent": "google",
                        "Disallow": [ "*" ]
                    }
                ]
            }
        }
    }
    ```

    You should also configure the sitemap via config if you require a sitemap.

The `RobotsTxtOptions` has the following properties:

- `AddSitemapUrl` - Determines whether the URL to the sitemap should be included in the robots.txt file
- `UserAgentRecords` - A collection of user agent records. Use this to define the parts of the site that you want each particular robot to ignore. You can define a record using the wildcard user agent to apply the same rules to all robots by using `*`.

### Available Open Graph tag helpers

The followings tag helpers are available from this library when the `@addTagHelper *, Winton.AspNetCore.Seo` import statement is used:

- `open-graph-article`
- `open-graph-book`
- `open-graph-profile`
- `open-graph-website`
- `open-graph-music-album`
- `open-graph-music-playlist`
- `open-graph-music-radio-station`
- `open-graph-music-song`
- `open-graph-video-episode`
- `open-graph-video-movie`
- `open-graph-video-other`
- `open-graph-video-tv-show`

In order to assign some of the properties that have types defined by this library, such as `Audio` and `Image`, you will need to also add `@using Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph` to the top of your cshtml file.

## How do I check that it's working

Start up your website and you should be able to see the files at the following URLs in a browser:

- /robots.txt
- /sitemap.xml

There should also be several `<meta>` tags in the head of the HTML pages of your site if you have added any Open Graph tag helpers.

## Extending the library

### Add tag helpers for other Open Graph objects

All of the Open Graph tag helpers listed above extend the `OpenGraphTagHelper` class.
To add your own Open Graph objects you can also extend this class.
The base class will take care of turning the properties in your tag helper into the correct Open Graph meta tags.
You can also fine tune the behvaiour of your object by using the `OpenGraphPropertyAttribute` and `OpenGraphNamespaceAttribute` in this library.
See below for examples.

#### Specifying a different Open Graph namespace

By default the `OpenGraphTagHelper` specifies the `og` namespace for all tags.
To override this for properties on any custom Open Graph objects just add the `OpenGraphNamespaceAttribute` to your class.
For instance: `[OpenGraphNamespaceAttribute("example", "http://example.com/ns#")]`.

#### An example custom Open Graph object

Below is a brief of example of a custom Open Graph tag helper:

```cs
[OpenGraphNamespaceAttribute("baz", "http://baz.com/ns#")]
public sealed class OpenGraphFooTagHelper : OpenGraphTagHelper
{
    public OpenGraphFooTagHelper()
        : base("foo") // Call the base constructor with the og:type of the custom object
    {
    }

    // Property will render as <meta content="value assigned" property="baz:bar">
    public string Bar { get; set; }

    // Property will render as <meta content="value assigned" property="baz:foo_bar"> for each entry
    // The OpenGraphPropetyAttribute can be used to specify the property name explicitly
    [OpenGraphProperty(Name = "foo_bar")]
    public IEnumerable<string> FooBars { get; }
}
```

For more examples see the src for how the Open Graph objects provided by this library have been implemented.
