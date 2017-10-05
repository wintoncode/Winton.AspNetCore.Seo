#! /bin/bash
set -e

dotnet restore
dotnet build --configuration Release
dotnet test test/Winton.AspNetCore.Seo.Tests/ --no-build --configuration Release --framework netcoreapp2.0
