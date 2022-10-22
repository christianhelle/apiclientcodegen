using System.IO;
using System.Threading.Tasks;
using ApiClientCodeGen.VSMac.Commands.NSwagStudio;
using Rapicgen.Core;
using MonoDevelop.Ide;
using MonoDevelop.Projects;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class AddNewNSwagStudioCommandHandler : AddNewCommandHandler
    {
        protected override string GeneratorName => null;

        private readonly GenerateNSwagStudioCommand command;

        public AddNewNSwagStudioCommandHandler()
        {
            command = Container.Instance.Resolve<GenerateNSwagStudioCommand>();
        }

        protected override SupportedCodeGenerator CodeGeneratorType
            => SupportedCodeGenerator.NSwagStudio;

        protected override async Task AddFile(string itemPath, string url)
        {
            var filename = Path.Combine(itemPath, "Swagger.nswag");
            var swaggerJson = await DownloadTextAsync(url);
            var contents = await NSwagStudioFileHelper.CreateNSwagStudioFileAsync(
                swaggerJson,
                url);
            
            File.WriteAllText(filename, contents);
            IdeApp.ProjectOperations.CurrentSelectedProject.AddFile(filename, BuildAction.None);
            command.Run(filename);
        }
    }
}