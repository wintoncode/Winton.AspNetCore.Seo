# Winton.AspNetCore.Seo

## What Does It Do?
It adds a default robots.txt and sitemap.xml to the web app.

## How Do I Use It?
1. Install the package:
```
Install-Package Winton.AspNetCore.Seo
```

2. Add services in `Startup.cs`
```
public void ConfigureServices(IServiceCollection services)
{
	// Define the urls you want to show in the sitemap.xml
	var sitemapConfig = new SitemapConfig 
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

3. Add SEO routes to MVC. For instance in `Startup.cs`:
```
public void Configure(IApplicationBuilder app)
{
    app.UseMvc(routeBuilder => routeBuilder.MapSeoRoutes());
}
```