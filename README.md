# Winton.AspNetCore.Seo

## What Does It Do?
It makes it easy to add search engine metadata to your ASP.NET Core website, allowing you to focus on writing the code you want to write. It provides three things:

* robots.txt file,
* sitemap.xml file, and
* HTML `<meta>` tags

You no longer have to look up the format of each of these separate protocols, or pollute your HTML with tons of `<meta>` tags.

It also gives you the flexibility to define the behaviour of how these files are generated at runtime. This is useful because it means you can do things such as inform search engine bots not to crawl your development and staging sites, but allow them to index the production version, without having to write and maintain three separate files.

## How Do I Use It?
### Installation
Add the package `Winton.AspNetCore.Seo` to the `.csproj` file of your project or install using nuget package manager:

```sh
Install-Package Winton.AspNetCore.Seo
```

### Minimal Setup
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
1. Add the header metadata to your HTML files. For instance in `_Layout.cshtml`:

    ```csharp
    @using Winton.AspNetCore.Seo.HeaderMetadata

    <!DOCTYPE html>
    <html>
    <head>
        @await Component.InvokeAsync("Winton.AspNetCore.Seo.HeaderMetadata", new HeaderMetadataViewModel { Title = "Your Site's Title", Description = "The description of your website", Image = "/url/to/site/image" })
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
* In the production environment all user agents are allowed to indexing any part of the site.
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

* `AddSitemapUrl` - Determines whether the url to the sitemap should be included in the robots.txt file
* `UserAgentRecords` - A collection of user agent records. Use this define the parts of the site that you want each particular robot to ignore. You can define a record using the wildcard user agent to apply the same rules to all robots by using `UserAgent.Any`.

### Defining the HTML header metadata
The header metadata is used to set things like the [Open Graph protocol](http://ogp.me/) `<meta>` tags. These are used by social networks to decide how a link to your site is displayed when it is shared. The `HeaderMetadataViewModel` contains all the options that can be set in order to configure these `<meta>` tags.

## How Do I Check It's Working?
Start up your website and you should be able to see the files at the following URLs in a browser:
* /robots.txt
* /sitemap.xml

There should also be several `<meta>` tags in the HTML pages of your site that you have added the header metadata to.
