# Changelog

## 2.0.0 (Unreleased)

- BREAKING CHANGE: Removed `HeaderMetadataViewComponent` and replaced with `OpenGraphMetadataTagHelper`. However there is no replacement for `<meta name="robots" content="none"/>`, which was added by `HeaderMetadataViewComponent` in non-production environments. If you still require this you will need to add it manually to each page that you want to stop robots from indexing or following links on. Alternatively set it in the `robots.txt` file using this library.

## 1.3.0 (13/12/2017)

- Added the ability to set the `Noindex` directive for each user agent in `robots.txt` file.

## 1.2.0 (06/10/2017)

- First open source release.