using System;
using FluentAssertions;
using Rapicgen.Core;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators
{
    /// <summary>
    /// Safety tests to ensure AutoRest generators remain routable during deprecation period.
    /// These tests prevent accidental removal of AutoRest code paths before Phase 3.
    /// </summary>
    #pragma warning disable CS0618 // Type or member is obsolete - These tests intentionally validate deprecated AutoRest during deprecation period
    [Trait("Category", "Unit")]
    public class AutoRestEnumRoutingSafetyTests
    {
        [Fact]
        public void SupportedCodeGenerator_AutoRest_Enum_Value_Exists()
        {
            // CRITICAL: This test prevents accidental removal during deprecation
            // AutoRest must remain functional until Phase 3 (~Jan 2027)
            var result = Enum.IsDefined(typeof(SupportedCodeGenerator), SupportedCodeGenerator.AutoRest);
            
            result.Should().BeTrue(
                "AutoRest enum value must exist during deprecation period. " +
                "Removal is only allowed in Phase 3.");
        }

        [Fact]
        public void SupportedCodeGenerator_AutoRestV3_Enum_Value_Exists()
        {
            // CRITICAL: This test prevents accidental removal during deprecation
            // AutoRestV3 must remain functional until Phase 3 (~Jan 2027)
            var result = Enum.IsDefined(typeof(SupportedCodeGenerator), SupportedCodeGenerator.AutoRestV3);
            
            result.Should().BeTrue(
                "AutoRestV3 enum value must exist during deprecation period. " +
                "Removal is only allowed in Phase 3.");
        }

        [Fact]
        public void AutoRest_Enum_Can_Be_Parsed_From_String()
        {
            // Validates that enum parsing for AutoRest works correctly
            var parseResult = Enum.TryParse<SupportedCodeGenerator>("AutoRest", out var parsedValue);
            
            parseResult.Should().BeTrue("AutoRest must be parseable from string");
            parsedValue.Should().Be(SupportedCodeGenerator.AutoRest);
        }

        [Fact]
        public void AutoRestV3_Enum_Can_Be_Parsed_From_String()
        {
            // Validates that enum parsing for AutoRestV3 works correctly
            var parseResult = Enum.TryParse<SupportedCodeGenerator>("AutoRestV3", out var parsedValue);
            
            parseResult.Should().BeTrue("AutoRestV3 must be parseable from string");
            parsedValue.Should().Be(SupportedCodeGenerator.AutoRestV3);
        }

        [Theory]
        [InlineData(SupportedCodeGenerator.AutoRest)]
        [InlineData(SupportedCodeGenerator.AutoRestV3)]
        public void AutoRest_Enum_Values_Convert_To_String_Correctly(SupportedCodeGenerator generator)
        {
            // Validates ToString() behavior for AutoRest enums
            var result = generator.ToString();
            
            result.Should().NotBeNullOrEmpty();
            result.Should().BeOneOf("AutoRest", "AutoRestV3");
        }

        [Fact]
        public void AutoRest_Enum_Values_Are_Marked_As_Obsolete()
        {
            // The enum VALUES are marked [Obsolete] to provide compile-time warnings
            // This is intentional for this project's deprecation strategy
            var autoRestField = typeof(SupportedCodeGenerator).GetField(nameof(SupportedCodeGenerator.AutoRest));
            var autoRestV3Field = typeof(SupportedCodeGenerator).GetField(nameof(SupportedCodeGenerator.AutoRestV3));
            
            var autoRestObsolete = Attribute.GetCustomAttribute(autoRestField!, typeof(ObsoleteAttribute)) as ObsoleteAttribute;
            var autoRestV3Obsolete = Attribute.GetCustomAttribute(autoRestV3Field!, typeof(ObsoleteAttribute)) as ObsoleteAttribute;
            
            autoRestObsolete.Should().NotBeNull(
                "AutoRest enum value should be marked [Obsolete] to provide compile-time warnings");
            autoRestV3Obsolete.Should().NotBeNull(
                "AutoRestV3 enum value should be marked [Obsolete] to provide compile-time warnings");
            
            autoRestObsolete!.IsError.Should().BeFalse(
                "Obsolete attribute should be a warning, not an error, during deprecation period");
            autoRestV3Obsolete!.IsError.Should().BeFalse(
                "Obsolete attribute should be a warning, not an error, during deprecation period");
        }

        [Fact]
        public void Both_AutoRest_Enums_Present_In_GetValues()
        {
            // Validates that both AutoRest enum values appear in Enum.GetValues()
            var allValues = Enum.GetValues(typeof(SupportedCodeGenerator));
            
            var list = new System.Collections.Generic.List<SupportedCodeGenerator>();
            foreach (var value in allValues)
            {
                list.Add((SupportedCodeGenerator)value);
            }
            
            list.Should().Contain(SupportedCodeGenerator.AutoRest, 
                "AutoRest must be in GetValues() during deprecation");
            list.Should().Contain(SupportedCodeGenerator.AutoRestV3,
                "AutoRestV3 must be in GetValues() during deprecation");
        }
    }
    #pragma warning restore CS0618
}
