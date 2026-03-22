using System;
using System.IO;
using FluentAssertions;
using Xunit;

namespace Rapicgen.CLI.Tests.Command
{
    /// <summary>
    /// Tests for CLI help output and deprecation messaging.
    /// Validates that AutoRest deprecation notices are properly displayed to users.
    /// </summary>
    [Trait("Category", "Integration")]
    public class CliHelpTests
    {
        /// <summary>
        /// Validates that the AutoRest command description in Program.cs contains the canonical deprecated label.
        /// This is the string shown when users run 'rapicgen csharp --help'.
        /// </summary>
        [Fact]
        public void AutoRest_Help_Description_Contains_Deprecated_Label()
        {
            // Arrange - The canonical description from Program.cs line 44
            const string expectedDescription = "AutoRest (Deprecated - v3.0.0-beta.20210504.2)";
            
            // Act - Read the Program.cs file to verify the actual registered description
            // Navigate from test assembly location to CLI project
            var testDirectory = AppContext.BaseDirectory;
            var cliProjectPath = Path.GetFullPath(Path.Combine(testDirectory, "..", "..", "..", "..", "ApiClientCodeGen.CLI", "Program.cs"));
            
            if (!File.Exists(cliProjectPath))
            {
                throw new InvalidOperationException($"Program.cs file not found at: {cliProjectPath}");
            }
            
            var programContent = File.ReadAllText(cliProjectPath);
            
            // Assert - Verify the canonical deprecated label is present
            programContent.Should().Contain(expectedDescription, 
                "the CLI help must show the deprecated label to warn users");
        }
        
        /// <summary>
        /// Validates that the AutoRestCommand emits a runtime deprecation warning.
        /// This is the warning shown when users actually execute 'rapicgen csharp autorest'.
        /// </summary>
        [Fact]
        public void AutoRest_Command_Contains_Runtime_Warning()
        {
            // Arrange - The canonical runtime warning message
            const string expectedWarning = "WARNING: AutoRest is deprecated by Microsoft and will be retired on July 1, 2026. AutoRest support will be removed from this tool in a future major version. Use NSwag, Refitter, or Kiota instead.";
            
            // Act - Read the AutoRestCommand.cs file to verify the runtime warning
            var testDirectory = AppContext.BaseDirectory;
            var commandPath = Path.GetFullPath(Path.Combine(testDirectory, "..", "..", "..", "..", "ApiClientCodeGen.CLI", "Commands", "CSharp", "AutoRestCommand.cs"));
            
            var commandContent = File.ReadAllText(commandPath);
            
            // Assert - Verify the runtime warning is emitted
            commandContent.Should().Contain(expectedWarning, 
                "the command must emit a deprecation warning on every execution");
            
            // Verify it contains Console.Error.WriteLine for stderr output
            commandContent.Should().Contain("Console.Error.WriteLine", 
                "warnings should be written to stderr, not stdout");
            
            // Verify warning prefix
            commandContent.Should().Contain("WARNING:", 
                "deprecation messages must be clearly marked as warnings");
        }
        
        /// <summary>
        /// Validates that the AutoRestCommand class has the [Obsolete] attribute.
        /// This provides compile-time warnings to NuGet package consumers.
        /// </summary>
        [Fact]
        public void AutoRest_Command_Has_Obsolete_Attribute()
        {
            // Arrange - The expected Obsolete attribute message
            const string expectedObsoleteMessage = "AutoRest is deprecated by Microsoft and will be retired on July 1, 2026. AutoRest support will be removed from this tool in a future major version. Use NSwag, Refitter, or Kiota instead.";
            
            // Act - Read the AutoRestCommand.cs file to verify the [Obsolete] attribute
            var testDirectory = AppContext.BaseDirectory;
            var commandPath = Path.GetFullPath(Path.Combine(testDirectory, "..", "..", "..", "..", "ApiClientCodeGen.CLI", "Commands", "CSharp", "AutoRestCommand.cs"));
            
            var commandContent = File.ReadAllText(commandPath);
            
            // Assert - Verify the [Obsolete] attribute is present
            commandContent.Should().Contain("[Obsolete(", 
                "the command class must be marked with [Obsolete] for compile-time warnings");
            
            commandContent.Should().Contain(expectedObsoleteMessage, 
                "the [Obsolete] attribute must contain the canonical deprecation message");
            
            // Verify it's set as non-error (false parameter)
            commandContent.Should().Contain("false)]", 
                "the [Obsolete] attribute should be a warning, not an error, during Phase 1");
        }
        
        /// <summary>
        /// Validates that the CLI Program.cs suppresses CS0618 warnings when using AutoRest types.
        /// This is necessary because AutoRest types are marked [Obsolete] but still need to be registered.
        /// </summary>
        [Fact]
        public void Program_Suppresses_Obsolete_Warnings_For_AutoRest_DI_Registration()
        {
            // Arrange
            var testDirectory = AppContext.BaseDirectory;
            var programPath = Path.GetFullPath(Path.Combine(testDirectory, "..", "..", "..", "..", "ApiClientCodeGen.CLI", "Program.cs"));
            
            var programContent = File.ReadAllText(programPath);
            
            // Assert - Verify CS0618 is suppressed around AutoRest service registration
            programContent.Should().Contain("#pragma warning disable CS0618", 
                "Program.cs must suppress obsolete warnings when registering deprecated AutoRest services");
            
            programContent.Should().Contain("#pragma warning restore CS0618", 
                "suppression should be scoped to minimize warning suppression");
            
            // Verify AutoRest DI registrations are present
            programContent.Should().Contain("IAutoRestOptions", 
                "AutoRest options must still be registered during deprecation period");
            
            programContent.Should().Contain("IAutoRestCodeGeneratorFactory", 
                "AutoRest factory must still be registered during deprecation period");
        }
    }
}
