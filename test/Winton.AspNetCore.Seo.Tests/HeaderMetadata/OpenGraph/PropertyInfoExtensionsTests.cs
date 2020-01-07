using System;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    public class PropertyInfoExtensionsTests
    {
        public sealed class GetOpenGraphNamespace : PropertyInfoExtensionsTests
        {
            [Fact]
            private void ShouldGetAttribute()
            {
                PropertyInfo propertyInfo = typeof(ClassWithNamespaceAttribute).Properties().Single();

                OpenGraphNamespaceAttribute attribute = propertyInfo.GetOpenGraphNamespace();

                attribute.Should().Be(new OpenGraphNamespaceAttribute("base", "http://example.com/base"));
            }

            [Fact]
            private void ShouldGetNamespaceFromAttributeOnBaseClassIfNotRespecified()
            {
                PropertyInfo? propertyInfo = typeof(DerivedClassWithoutNamespaceAttribute)
                    .GetProperty(nameof(DerivedClassWithoutNamespaceAttribute.Property));

                OpenGraphNamespaceAttribute? attribute = propertyInfo?.GetOpenGraphNamespace();

                attribute.Should().Be(new OpenGraphNamespaceAttribute("base", "http://example.com/base"));
            }

            [Fact]
            private void ShouldGetNamespaceFromAttributeOnMostDerivedClass()
            {
                PropertyInfo? propertyInfo = typeof(DerivedClassWithNamespaceAttribute)
                    .GetProperty(nameof(DerivedClassWithNamespaceAttribute.Property));

                OpenGraphNamespaceAttribute? attribute = propertyInfo?.GetOpenGraphNamespace();

                attribute.Should().Be(new OpenGraphNamespaceAttribute("derived", "http://example.com/derived"));
            }

            [Fact]
            private void ShouldThrowWhenNoAttributeOnDeclaringClass()
            {
                PropertyInfo propertyInfo = typeof(ClassWithoutNamespaceAttribute).Properties().Single();

                Action gettingNamespace = () => propertyInfo.GetOpenGraphNamespace();

                gettingNamespace
                    .Should().Throw<Exception>()
                    .WithMessage(
                        "The type ClassWithoutNamespaceAttribute that declares the property Property is missing the required OpenGraphNamespaceAttribute");
            }
        }

        [OpenGraphNamespace("base", "http://example.com/base")]
        private class ClassWithNamespaceAttribute
        {
            public string? InheritedProperty { get; set; }
        }

        private sealed class ClassWithoutNamespaceAttribute
        {
            public string? Property { get; set; }
        }

        [OpenGraphNamespace("derived", "http://example.com/derived")]
        private class DerivedClassWithNamespaceAttribute : ClassWithNamespaceAttribute
        {
            public string? Property { get; set; }
        }

        private class DerivedClassWithoutNamespaceAttribute : ClassWithNamespaceAttribute
        {
            public string? Property { get; set; }
        }
    }
}