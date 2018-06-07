# Changelog

## 2.0.0 (08/06/2018)

- BREAKING CHANGE: Removed `HeaderMetadataViewComponent` and replaced with `OpenGraphTagHelper`. However there is no replacement for `<meta name="robots" content="none"/>`, which was added by `HeaderMetadataViewComponent` in non-production environments. If you still require this you will need to add it manually to each page that you want to stop robots from indexing or following links on. Alternatively set it in the `robots.txt` file using this library.
- BREAKING CHANGE: Updated target framework to netstandard2.0
- Added several tag helpers to make it easy to add Open Graph metadata to cshtml pages.

## 1.3.0 (13/12/2017)

- Added the ability to set the `Noindex` directive for each user agent in `robots.txt` file.

## 1.2.0 (06/10/2017)

- First open source release.