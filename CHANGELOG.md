# Changelog

## 3.0.0 (14/01/2020)

Updated to work with ASP.NET Core 3.0.

### Breaking Changes

- Consolidated setup into a single call to either `AddSeo` or `AddSeoWithDefaultRobots`, there is no `MapSeoRoutes` extension method for the `IMvcBuilder` anymore.
- Switched to the Options pattern for configuring Robots.txt and Sitemap.xml via the aforementioned `IServiceCollection` extension methods. See the README for more details.

## 2.0.0 (08/06/2018)

### Breaking Changes

- Removed `HeaderMetadataViewComponent` and replaced with `OpenGraphTagHelper`. However there is no replacement for `<meta name="robots" content="none"/>`, which was added by `HeaderMetadataViewComponent` in non-production environments. If you still require this you will need to add it manually to each page that you want to stop robots from indexing or following links on. Alternatively set it in the `robots.txt` file using this library.
- Updated target framework to netstandard2.0

### New Features

- Added several tag helpers to make it easy to add Open Graph metadata to cshtml pages.

## 1.3.0 (13/12/2017)

- Added the ability to set the `Noindex` directive for each user agent in `robots.txt` file.

## 1.2.0 (06/10/2017)

- First open source release.