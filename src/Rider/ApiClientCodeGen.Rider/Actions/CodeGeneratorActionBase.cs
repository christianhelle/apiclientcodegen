using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using JetBrains.Application.DataContext;
using JetBrains.Application.Progress;
using JetBrains.Application.UI.Actions;
using JetBrains.Application.UI.ActionsRevised.Menu;
using JetBrains.Application.UI.Controls.BulbMenu.Items;
using JetBrains.Diagnostics;
using JetBrains.IDE;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.ProjectModel.DataContext;
using JetBrains.ReSharper.Resources.Shell;
using JetBrains.Rider.Model.UIAutomation;
using JetBrains.Util;
using Rapicgen.Rider.Generators;

namespace Rapicgen.Rider.Actions
{
    public abstract class CodeGeneratorActionBase : IExecutableAction
    {
        private readonly string _generatorName;
        private readonly string _displayName;
        protected readonly ILogger Logger;
        
        protected CodeGeneratorActionBase(string generatorName, string displayName, ILogger logger)
        {
            _generatorName = generatorName;
            _displayName = displayName;
            Logger = logger;
        }
        
        public bool Update(IDataContext context, ActionPresentation presentation, DelegateUpdate nextUpdate)
        {
            var projectModelElement = context.GetData(ProjectModelDataConstants.PROJECT_MODEL_ELEMENT);
            if (projectModelElement == null)
                return false;

            // Only enable this action for json and yaml files
            var isEnabledForFile = false;
            
            if (projectModelElement is IProjectFile file)
            {
                var extension = Path.GetExtension(file.Name);
                isEnabledForFile = extension.EndsWith(".json", StringComparison.OrdinalIgnoreCase) ||
                                  extension.EndsWith(".yaml", StringComparison.OrdinalIgnoreCase) ||
                                  extension.EndsWith(".yml", StringComparison.OrdinalIgnoreCase);
            }
            
            return isEnabledForFile && nextUpdate();
        }

        public void Execute(IDataContext context, DelegateExecute nextExecute)
        {
            var projectModelElement = context.GetData(ProjectModelDataConstants.PROJECT_MODEL_ELEMENT);
            if (projectModelElement == null)
                return;

            if (projectModelElement is IProjectFile file)
            {
                try
                {
                    ExecuteForFileAsync(file).FireAndForget();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Error generating code with {_displayName}: {ex.Message}",
                        "API Client Code Generator Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    
                    Logger.Error($"Error generating code with {_displayName}", ex);
                }
            }
            
            nextExecute();
        }

        private async Task ExecuteForFileAsync(IProjectFile file)
        {
            var specificationPath = file.Location.FullPath;
            var directoryPath = Path.GetDirectoryName(specificationPath);
            var fileName = Path.GetFileNameWithoutExtension(specificationPath);
            var outputPath = Path.Combine(directoryPath!, $"{fileName}.{_generatorName}.cs");
            
            Logger.Info($"Generating code for {specificationPath} with {_displayName}");
            
            var runner = new RapicgenToolRunner(Logger);
            
            if (!runner.IsToolInstalled())
            {
                var result = MessageBox.Show(
                    "The Rapicgen .NET tool is not installed. Would you like to install it now?", 
                    "Rapicgen Tool Not Found",
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    if (!runner.InstallTool())
                    {
                        MessageBox.Show(
                            "Failed to install the Rapicgen .NET tool. Please install it manually using: dotnet tool install --global rapicgen", 
                            "Installation Failed",
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            string defaultNamespace = DetermineDefaultNamespace(file);
            
            using var progress = new ProgressIndicator(
                Lifetime.Eternal, 
                new Lifetimes.SequentialLifetimes(Lifetime.Eternal), 
                $"Generating code with {_displayName}...");
            
            var output = await runner.GenerateCode(_generatorName, specificationPath, defaultNamespace, outputPath);
            
            Logger.Info($"Generated code at {outputPath}");
            
            MessageBox.Show(
                $"Successfully generated {_displayName} client code at {outputPath}", 
                "Code Generation Successful",
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information);
            
            // Open generated file
            await OpenFileAsync(outputPath);
        }

        private static string DetermineDefaultNamespace(IProjectFile file)
        {
            var project = file.GetProject();
            if (project == null)
                return "GeneratedCode";

            // Try to get default namespace from project
            var defaultNamespace = project.GetProperty("RootNamespace")?.Value;
            if (string.IsNullOrEmpty(defaultNamespace))
                defaultNamespace = project.Name;
            
            return defaultNamespace;
        }

        private async Task OpenFileAsync(string filePath)
        {
            await Task.Delay(500); // Short delay to ensure file is ready to be opened
            
            using (ReadLockCookie.Create())
            {
                var solution = Shell.Instance.GetComponent<ISolutionManager>().CurrentSolution;
                if (solution == null)
                    return;
                
                var projectFile = solution.FindProjectItemsByLocation(FileSystemPath.Parse(filePath)).FirstOrDefault() as IProjectFile;
                if (projectFile != null)
                {
                    Shell.Instance.GetComponent<IEditorManager>().OpenProjectFile(projectFile);
                }
            }
        }
    }
}
