# Winton.AspNetCore.Seo

[![Build status](https://ci.appveyor.com/api/projects/status/722rlfhiy67qcwxb/branch/master?svg=true)](https://ci.appveyor.com/project/wintoncode/winton-aspnetcore-seo/branch/master)
[![Travis Build Status](https://travis-ci.org/wintoncode/Winton.AspNetCore.Seo.svg?branch=master)](https://travis-ci.org/wintoncode/Winton.AspNetCore.Seo)
[![NuGet version](https://img.shields.io/nuget/v/Winton.AspNetCore.Seo.svg)](https://www.nuget.org/packages/Winton.AspNetCore.Seo)
[![NuGet version](https://img.shields.io/nuget/vpre/Winton.AspNetCore.Seo.svg)](https://www.nuget.org/packages/Winton.AspNetCore.Seo)

## What does it do

It makes it easy to add search engine metadata to your ASP.NET Core website, allowing you to focus on writing the code you want to write. It provides three things:

* a robots.txt file,
* a sitemap.xml file, and
* HTML `<meta>` tags

You no longer have to look up the format of each of these separate protocols, or pollute your HTML with tons of `<meta>` tags.

It also gives you the flexibility to define the behaviour of how these files are generated at runtime. This is useful because it means you can do things such as inform search engine bots not to crawl your development and staging sites, but allow them to index the production version, without having to write and maintain three separate files.

## How do I use it

### Installation

Add the package `Winton.AspNetCore.Seo` to the `.csproj` file of your project or install using nuget package manager:

```sh
Install-Package Winton.AspNetCore.Seo
```

### Minimal setup

1. Configure services. For instance in `Startup.cs`:

    ```csharp
    public void ConfigureServices(IServiceCollection services)
    {
        // Define the URLs you want to show in the sitemap.xml
        var sitemapConfig = new MyImplementationOfISitemapConfig 
        {
            new SitemapConfigUrl
            {
                Priority = 0.9M,
                RelativeUrl = "/login"
            }
        }
        services.AddSeo(sitemapConfig);
    }
    ```

1. Add SEO routes to MVC. For instance in `Startup.cs`:

    ```csharp
    public void Configure(IApplicationBuilder app)
    {
        app.UseMvc(routeBuilder => routeBuilder.MapSeoRoutes());
    }
    ```
1. Add the appropriate Open Graph TagHelper to your HTML files. For instance in `_Layout.cshtml`:

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

In order to define your sitemap the `ISitemapConfig` interface needs to be implemented. Given an implementation there are two ways by which it can then be passed to the `IServiceCollection`:

1. As an instance. For example: 
    ```csharp
    services.AddSeo(sitemapConfig);
    ```
    This is useful when you want to define your sitemap in config and deserialise it to a config class. Such as reading it in from `appsettings.json`.

1. As a type. For example:
    ```csharp
    services.AddSeo<TSitemapConfig>();
    ```
    where `TSitemapConfig` is an implementation of `ISitemapConfig`. This is useful when you want to have dynamic behaviour. `TSitemapConfig` will be registered with the service collection and will therefore be instantiated by the dependency injection container. This allows it to depend on other services. If you wanted to load the sitemap from a database or a REST endpoint this would be the one to choose.

### Customising the robots.txt file

The output of the robots.txt is controlled by the implementation of the `IRobotsTxtOptions` interface. When `AddSeo` is called a default implementation of this interface will be used, unless one has already been registered beforehand. The default options apply the following policy:

* The URL of the sitemap is specified in all environments.
* In the production environment all user agents are allowed to index any part of the site.
* In all other environments, all user agents are disallowed from indexing any part of the site.

*Note: It's always worth remembering that malicious bots can just ignore a robots.txt file, so it should not be relied on to hide any sensitive parts of the site.*

To provide your own implementation you can do the following:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Register your implementation of IRobotsTxtOptions first
    services.AddSingleton<IRobotsTxtOptions, YourRobotsTxtOptions>();
    services.AddSeo(sitemapConfig);
}
```

The `IRobotsTxtOptions` interface has the following properties:

* `AddSitemapUrl` - Determines whether the URL to the sitemap should be included in the robots.txt file
* `UserAgentRecords` - A collection of user agent records. Use this define the parts of the site that you want each particular robot to ignore. You can define a record using the wildcard user agent to apply the same rules to all robots by using `UserAgent.Any`.

### Available Open Graph tag helpers

The followings tag helpers are available from this library when the `@addTagHelper *, Winton.AspNetCore.Seo` import statement is used:

* `open-graph-article`
* `open-graph-book`
* `open-graph-profile`
* `open-graph-website`
* `open-graph-music-album`
* `open-graph-music-playlist`
* `open-graph-music-radio-station`
* `open-graph-music-song`
* `open-graph-video-episode`
* `open-graph-video-movie`
* `open-graph-video-other`
* `open-graph-video-tv-show`

In order to assign some of the properties that have types defined by this library, such as `Audio` and `Image`, you will need to also add `@using Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph` to the top of your cshtml file.

## How do I check that it's working

Start up your website and you should be able to see the files at the following URLs in a browser:

* /robots.txt
* /sitemap.xml

There should also be several `<meta>` tags in the head of the HTML pages of your site if you have added any Open Graph tag helpers.

## Extending the library

### Add tag helpers for other Open Graph objects

All of the Open Graph tag helpers listed above extend the `OpenGraphTagHelper` class. To add your own Open Graph objects you can also extend this class. The base class will take care of turning the properties in your tag helper into the correct Open Graph meta tags. You can also fine tune the behvaiour of your object by using the `OpenGraphPropertyAttribute` and `OpenGraphNamespaceAttribute` in this library. See below for examples.

#### Specifying a different Open Graph namespace

By default the `OpenGraphTagHelper` specifies the `og` namespace for all tags. To override this for properties on any custom Open Graph objects just add the `OpenGraphNamespaceAttribute` to your class. For instance: `[OpenGraphNamespaceAttribute("example", "http://example.com/ns#")]`.

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
