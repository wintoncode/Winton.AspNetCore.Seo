// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    public class OpenGraphPropertyTests
    {
        public sealed class Create : OpenGraphPropertyTests
        {
            [Theory]
            [InlineData(nameof(TestOpenGraphTagHelper.BogStandardProperty), "test:bog_standard_property")]
            [InlineData(nameof(TestOpenGraphTagHelper.PrimaryProperty), "test")]
            [InlineData(nameof(TestOpenGraphTagHelper.PropertyWithOverriddenName), "test:overridden")]
            private void ShouldCreateWithCorrectFullName(string propertyName, string expected)
            {
                var openGraphTagHelper = new TestOpenGraphTagHelper();
                PropertyInfo propertyInfo = openGraphTagHelper.GetType().GetProperty(propertyName) ??
                                            throw new ArgumentNullException(nameof(propertyInfo));

                OpenGraphProperty openGraphProperty = OpenGraphProperty.Create(propertyInfo, openGraphTagHelper);

                openGraphProperty.FullName.Should().Be(expected);
            }

            [Theory]
            [InlineData(nameof(TestOpenGraphTagHelper.BogStandardProperty), false)]
            [InlineData(nameof(TestOpenGraphTagHelper.PrimaryProperty), true)]
            [InlineData(nameof(TestOpenGraphTagHelper.PropertyWithOverriddenName), false)]
            private void ShouldCreateWithCorrectIsPrimaryValue(string propertyName, bool expected)
            {
                var openGraphTagHelper = new TestOpenGraphTagHelper();
                PropertyInfo propertyInfo = openGraphTagHelper.GetType().GetProperty(propertyName) ??
                                            throw new ArgumentNullException(nameof(propertyInfo));

                OpenGraphProperty openGraphProperty = OpenGraphProperty.Create(propertyInfo, openGraphTagHelper);

                openGraphProperty.IsPrimary.Should().Be(expected);
            }

            [Theory]
            [InlineData(nameof(TestOpenGraphTagHelper.BogStandardProperty), "bog standard value")]
            [InlineData(nameof(TestOpenGraphTagHelper.PrimaryProperty), "primary value")]
            [InlineData(nameof(TestOpenGraphTagHelper.PropertyWithOverriddenName), "overidden value")]
            private void ShouldCreateWithCorrectValue(string propertyName, string expected)
            {
                var openGraphTagHelper = new TestOpenGraphTagHelper
                {
                    BogStandardProperty = "bog standard value",
                    PrimaryProperty = "primary value",
                    PropertyWithOverriddenName = "overidden value"
                };
                PropertyInfo propertyInfo = openGraphTagHelper.GetType().GetProperty(propertyName)
                                            ?? throw new ArgumentNullException(nameof(propertyInfo));

                OpenGraphProperty openGraphProperty = OpenGraphProperty.Create(propertyInfo, openGraphTagHelper);

                openGraphProperty.Value.Should().Be(expected);
            }
        }

        public sealed class Name : OpenGraphPropertyTests
        {
            [Fact]
            private void ShouldReturnFinalSegmentOfFullName()
            {
                var openGraphTagHelper = new TestOpenGraphTagHelper();
                PropertyInfo propertyInfo =
                    openGraphTagHelper.GetType().GetProperty(nameof(TestOpenGraphTagHelper.BogStandardProperty)) ??
                    throw new ArgumentNullException(nameof(propertyInfo));
                OpenGraphProperty openGraphProperty = OpenGraphProperty.Create(propertyInfo, openGraphTagHelper);

                string name = openGraphProperty.Name;

                name.Should().Be("bog_standard_property");
            }
        }

        [OpenGraphNamespace("test", "http://example.com/test")]
        private sealed class TestOpenGraphTagHelper : OpenGraphTagHelper
        {
            public TestOpenGraphTagHelper()
                : base("test")
            {
            }

            public string? BogStandardProperty { get; set; }

            [OpenGraphProperty(IsPrimary = true)]
            public string? PrimaryProperty { get; set; }

            [OpenGraphProperty(Name = "overridden")]
            public string? PropertyWithOverriddenName { get; set; }
        }
    }
}