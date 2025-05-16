import * as vscode from 'vscode';
import * as path from 'path';
import * as fs from 'fs';
import { execSync } from 'child_process';
import { platform } from 'os';

/**
 * Checks if the .NET SDK is installed and available
 * @returns true if the .NET SDK is installed, false otherwise
 */
function isDotNetSdkInstalled(): boolean {
  try {
    execSync('dotnet --version', { encoding: 'utf-8' });
    return true;
  } catch (error) {
    console.error('Error checking .NET SDK:', error);
    return false;
  }
}

/**
 * Checks if the Rapicgen .NET tool is installed globally
 * @returns true if the tool is installed, false otherwise
 */
function isRapicgenInstalled(): boolean {
  try {
    // Different command for Windows vs. Unix platforms
    const command = platform() === 'win32' 
      ? 'dotnet tool list --global | findstr rapicgen' 
      : 'dotnet tool list --global | grep rapicgen';
      
    execSync(command, { encoding: 'utf-8' });
    return true;
  } catch (error) {
    return false;
  }
}

/**
 * Installs the Rapicgen .NET tool globally
 * @returns true if installation was successful
 */
async function installRapicgen(): Promise<boolean> {
  try {
    const installResult = await vscode.window.withProgress({
      location: vscode.ProgressLocation.Notification,
      title: "Installing Rapicgen tool...",
      cancellable: false
    }, async () => {
      try {
        execSync('dotnet tool install --global rapicgen', { encoding: 'utf-8' });
        return true;
      } catch (error) {
        console.error('Failed to install rapicgen tool:', error);
        return false;
      }
    });
    
    return installResult;
  } catch (error) {
    console.error('Error during installation process:', error);
    return false;
  }
}

/**
 * Gets the namespace to use for generated code
 * @returns The namespace from configuration or a default value
 */
function getNamespace(): string {
  const config = vscode.workspace.getConfiguration('restApiClientCodeGenerator');
  return config.get<string>('namespace', 'GeneratedCode');
}

/**
 * Gets the output directory to use for generated code
 * @param specificationFile The path to the specification file
 * @returns The output directory path
 */
function getOutputDirectory(specificationFile: string): string {
  const config = vscode.workspace.getConfiguration('restApiClientCodeGenerator');
  const outputDir = config.get<string>('outputDirectory', '');
  
  if (outputDir === '') {
    // Use the directory of the specification file
    return path.dirname(specificationFile);
  } else {
    // Use the configured output directory
    const workspaceFolder = vscode.workspace.getWorkspaceFolder(vscode.Uri.file(specificationFile));
    if (workspaceFolder) {
      return path.join(workspaceFolder.uri.fsPath, outputDir);
    } else {
      return path.dirname(specificationFile);
    }
  }
}

/**
 * Generates the output file path
 * @param specificationFile The path to the specification file
 * @param generator The generator name (for the file name)
 * @returns The full path for the output file
 */
function getOutputFilePath(specificationFile: string, generator: string): string {
  const outputDir = getOutputDirectory(specificationFile);
  const fileName = path.basename(specificationFile, path.extname(specificationFile));
  return path.join(outputDir, `${fileName}.${generator}.cs`);
}

/**
 * Executes the Rapicgen tool with the given generator and parameters
 * @param generator The generator to use (nswag, refitter, etc.)
 * @param specificationFilePath The path to the OpenAPI/Swagger specification file
 */
async function executeRapicgen(generator: string, specificationFilePath: string): Promise<void> {
  // Validate that the file exists
  if (!fs.existsSync(specificationFilePath)) {
    vscode.window.showErrorMessage(`File not found: ${specificationFilePath}`);
    return;
  }
  
  // Validate that the file is readable
  try {
    fs.accessSync(specificationFilePath, fs.constants.R_OK);
  } catch (err) {
    vscode.window.showErrorMessage(`Cannot read file: ${specificationFilePath}`);
    return;
  }
  
  // Check if .NET SDK is installed
  if (!isDotNetSdkInstalled()) {
    vscode.window.showErrorMessage(
      '.NET SDK not found. Please install .NET SDK 6.0 or higher to use this extension. Visit https://dotnet.microsoft.com/download/dotnet to download and install.'
    );
    return;
  }
  
  // Check if the Rapicgen tool is installed
  if (!isRapicgenInstalled()) {
    const shouldInstall = await vscode.window.showInformationMessage(
      'The Rapicgen .NET tool is not installed. Would you like to install it?',
      'Yes', 'No'
    );
    
    if (shouldInstall === 'Yes') {
      const installSuccess = await installRapicgen();
      if (!installSuccess) {
        vscode.window.showErrorMessage('Failed to install the Rapicgen .NET tool. Please install it manually with "dotnet tool install --global rapicgen".');
        return;
      }
    } else {
      return;
    }
  }

  const namespace = getNamespace();
  const outputFile = getOutputFilePath(specificationFilePath, generator);
  
  // Ensure output directory exists
  const outputDir = path.dirname(outputFile);
  if (!fs.existsSync(outputDir)) {
    try {
      fs.mkdirSync(outputDir, { recursive: true });
    } catch (err) {
      vscode.window.showErrorMessage(`Failed to create output directory: ${outputDir}`);
      return;
    }
  }
  
  const command = `rapicgen csharp ${generator} "${specificationFilePath}" "${namespace}" "${outputFile}"`;
  
  try {    // Show progress while generating
    await vscode.window.withProgress({
      location: vscode.ProgressLocation.Notification,
      title: `Generating code with ${generator}...`,
      cancellable: false
    }, async () => {
      try {
        // Run with higher timeout since code generation can take time
        const output = execSync(command, { 
          encoding: 'utf-8',
          timeout: 120000 // 2 minute timeout
        });
        
        // Log output for debugging
        console.log(`Rapicgen output: ${output}`);
        
        if (!fs.existsSync(outputFile)) {
          vscode.window.showErrorMessage(`Failed to generate output file: ${outputFile}`);
          return;
        }
        
        // Open the generated file
        const document = await vscode.workspace.openTextDocument(outputFile);
        await vscode.window.showTextDocument(document);
        
        vscode.window.showInformationMessage(`Successfully generated ${generator} client code at ${outputFile}`);      } catch (error: unknown) {
        let errorMessage = `Error generating code with ${generator}`;
        
        const err = error as { message?: string; stderr?: string };
        if (err.message) {
          errorMessage += `: ${err.message}`;
        }
        
        if (err.stderr) {
          errorMessage += `\n${err.stderr}`;
          console.error('Command stderr:', err.stderr);
        }
        
        vscode.window.showErrorMessage(errorMessage);
        console.error('Command execution error:', error);
      }
    });
  } catch (error) {
    console.error('Error during code generation process:', error);
  }
}

/**
 * Activates the extension
 * @param context The extension context
 */
export function activate(context: vscode.ExtensionContext) {
  console.log('Rest API Client Code Generator extension is now active');

  // Register commands for each generator
  const generators = [
    { command: 'nswag', displayName: 'NSwag' },
    { command: 'refitter', displayName: 'Refitter' },
    { command: 'openapi', displayName: 'OpenAPI Generator' },
    { command: 'kiota', displayName: 'Microsoft Kiota' },
    { command: 'swagger', displayName: 'Swagger Codegen CLI' },
    { command: 'autorest', displayName: 'AutoREST' }
  ];

  for (const generator of generators) {
    const disposable = vscode.commands.registerCommand(
      `restApiClientCodeGenerator.${generator.command}`, 
      async (fileUri: vscode.Uri) => {
        if (!fileUri) {
          // If command was triggered from command palette, ask for file
          const files = await vscode.workspace.findFiles('**/*.{json,yaml,yml}');
          if (files.length === 0) {
            vscode.window.showErrorMessage('No Swagger/OpenAPI specification files found in the workspace');
            return;
          }

          const fileItems = files.map(file => ({
            label: path.basename(file.fsPath),
            description: vscode.workspace.asRelativePath(file),
            path: file.fsPath
          }));

          const selectedFile = await vscode.window.showQuickPick(fileItems, {
            placeHolder: 'Select a Swagger/OpenAPI specification file'
          });

          if (!selectedFile) {
            return;
          }

          fileUri = vscode.Uri.file(selectedFile.path);
        }

        executeRapicgen(generator.command, fileUri.fsPath);
      }
    );

    context.subscriptions.push(disposable);
  }
}

/**
 * Deactivates the extension
 * Nothing to clean up in this extension
 */
export function deactivate(): void {
  // Nothing to clean up
}
