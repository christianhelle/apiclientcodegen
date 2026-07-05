using System;
using System.IO;
using FluentAssertions;
using Xunit;

namespace Rapicgen.CLI.Tests.Command
{
    [Trait("Category", "Integration")]
    public class CliHelpTests
    {
        [Fact]
        public void AutoRest_Command_Is_Not_Registered()
        {
            var testDirectory = AppContext.BaseDirectory;
            var cliProjectPath = Path.GetFullPath(Path.Combine(testDirectory, "..", "..", "..", "..", "ApiClientCodeGen.CLI", "Program.cs"));
            var programContent = File.ReadAllText(cliProjectPath);

            programContent.Should().NotContain("autorest",
                "the CLI help should no longer advertise a removed AutoRest command");
        }

        [Fact]
        public void Program_Does_Not_Register_AutoRest_Dependencies()
        {
            var testDirectory = AppContext.BaseDirectory;
            var programPath = Path.GetFullPath(Path.Combine(testDirectory, "..", "..", "..", "..", "ApiClientCodeGen.CLI", "Program.cs"));
            var programContent = File.ReadAllText(programPath);

            programContent.Should().NotContain("IAutoRestOptions");
            programContent.Should().NotContain("IAutoRestCodeGeneratorFactory");
            programContent.Should().NotContain("#pragma warning disable CS0618");
        }
    }
}
