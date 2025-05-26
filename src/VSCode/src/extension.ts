import * as vscode from 'vscode';
import * as path from 'path';
import * as fs from 'fs';
import { execSync } from 'child_process';
import { platform } from 'os';

/**
 * Interface representing a file item in quick pick menu
 */
interface IFileQuickPickItem extends vscode.QuickPickItem {
  path: string;
}

/**
 * Prompt the user to select a file from the workspace
 * @param filePattern The glob pattern to search for files
 * @param placeHolder The placeholder text for the quick pick menu
 * @param errorMessage The error message to show if no files are found
 * @returns The selected file URI or undefined if canceled
 */
async function promptForFile(
  filePattern: string, 
  placeHolder: string,
  errorMessage: string
): Promise<vscode.Uri | undefined> {
  const files = await vscode.workspace.findFiles(filePattern);
  if (files.length === 0) {
    vscode.window.showErrorMessage(errorMessage);
    return undefined;
  }

  const fileItems: IFileQuickPickItem[] = files.map(file => ({
    label: path.basename(file.fsPath),
    description: vscode.workspace.asRelativePath(file),
    path: file.fsPath
  }));

  const selectedFile = await vscode.window.showQuickPick(fileItems, {
    placeHolder: placeHolder
  });

  if (!selectedFile) {
    return undefined;
  }
  
  return vscode.Uri.file(selectedFile.path);
}

/**
 * Validates required dependencies for a generator
 * @param generator The generator command name
 * @param isTypeScript Whether the generator is for TypeScript
 * @returns true if all requirements are met, false otherwise
 */
function validateDependencies(generator: string, isTypeScript = false): boolean {
  // Check if .NET SDK is installed
  if (!isDotNetSdkInstalled()) {
    vscode.window.showErrorMessage(
      '.NET SDK not found. Please install .NET SDK 6.0 or higher to use this extension. Visit https://dotnet.microsoft.com/download/dotnet to download and install.'
    );
    return false;
  }
  
  // Check if Java is required and installed
  if (generatorRequiresJava(generator, isTypeScript) && !isJavaRuntimeInstalled()) {
    vscode.window.showErrorMessage(
      'Java Runtime Environment (JRE) not found. The selected generator requires Java to be installed. ' + 
      'Please install the latest version of Java from https://adoptium.net/ or https://www.oracle.com/java/technologies/downloads/'
    );
    return false;
  }
  
  return true;
}

/**
 * Handles command execution errors
 * @param error The error that occurred
 * @param generatorName The name of the generator that failed
 * @param isTypeScript Whether the generator is for TypeScript 
 */
function handleExecutionError(error: unknown, generatorName: string, isTypeScript = false): void {
  let errorMessage = `Error generating ${isTypeScript ? 'TypeScript ' : ''}code with ${generatorName}`;
  
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

/**
 * Gets the extension version from package.json
 * @param context The extension context
 * @returns The version string or undefined if not found
 */
function getExtensionVersion(context: vscode.ExtensionContext): string | undefined {
  try {
    // Get the extension's package.json path
    const packageJsonPath = path.join(context.extensionPath, 'package.json');
    if (fs.existsSync(packageJsonPath)) {
      const packageJson = JSON.parse(fs.readFileSync(packageJsonPath, 'utf8'));
      return packageJson.version;
    }
  } catch (error) {
    console.error('Error reading extension version:', error);
  }
  return undefined;
}

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
 * Checks if Java runtime is installed and available
 * @returns true if Java is installed, false otherwise
 */
function isJavaRuntimeInstalled(): boolean {
  try {
    execSync('java -version', { encoding: 'utf-8' });
    return true;
  } catch (error) {
    console.error('Error checking Java runtime:', error);
    return false;
  }
}

/**
 * Gets the installed Rapicgen .NET tool version
 * @returns The installed version as a string, or null if not installed or version cannot be determined
 */
function getInstalledRapicgenVersion(): string | null {
  try {
    // Different command for Windows vs. Unix platforms
    const command = platform() === 'win32'
      ? 'dotnet tool list --global | findstr rapicgen'
      : 'dotnet tool list --global | grep rapicgen';
      
    const output = execSync(command, { encoding: 'utf-8' });
    
    // Parse the version from the command output
    // The output format is usually: package-id  version  commands
    const match = output.match(/rapicgen\s+(\d+\.\d+\.\d+)/i);
    return match && match[1] ? match[1] : null;
  } catch (error) {
    console.error('Error getting Rapicgen version:', error);
    return null;
  }
}

/**
 * Compares two version strings
 * @param version1 First version string
 * @param version2 Second version string
 * @returns 1 if version1 > version2, -1 if version1 < version2, 0 if equal
 */
function compareVersions(version1: string, version2: string): number {
  const parts1 = version1.split('.').map(Number);
  const parts2 = version2.split('.').map(Number);
  
  for (let i = 0; i < Math.max(parts1.length, parts2.length); i++) {
    const p1 = i < parts1.length ? parts1[i] : 0;
    const p2 = i < parts2.length ? parts2[i] : 0;
    
    if (p1 > p2) {
      return 1;
    }
    if (p1 < p2) {
      return -1;
    }
  }
  
  return 0;
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
 * Checks if the Rapicgen tool needs to be updated based on extension version
 * @param context The extension context
 * @returns An object indicating if update is needed and current/target versions
 */
function checkRapicgenVersionStatus(context: vscode.ExtensionContext): { 
  needsUpdate: boolean; 
  currentVersion: string | null;
  targetVersion: string | null;
} {
  const extensionVersion = getExtensionVersion(context);
  const installedVersion = getInstalledRapicgenVersion();
  
  // Default result
  const result = {
    needsUpdate: false,
    currentVersion: installedVersion,
    targetVersion: extensionVersion || null
  };
  
  // If extension is dev version (0.1.0) or we can't get versions, no update needed
  if (!extensionVersion || extensionVersion === '0.1.0' || !installedVersion) {
    return result;
  }
  
  // Compare versions and determine if update is needed
  const comparison = compareVersions(installedVersion, extensionVersion);
  
  // Only update if the installed version is older (comparison < 0)
  result.needsUpdate = comparison < 0;
  
  return result;
}

/**
 * Installs or updates the Rapicgen .NET tool globally
 * @param context The extension context
 * @returns true if installation/update was successful
 */
async function installRapicgen(context: vscode.ExtensionContext): Promise<boolean> {
  try {
    const version = getExtensionVersion(context);
    const isUpdate = isRapicgenInstalled();
    const versionStatus = isUpdate ? checkRapicgenVersionStatus(context) : null;
    
    // If tool is already installed and up-to-date (or newer), no need to update
    if (isUpdate && versionStatus && !versionStatus.needsUpdate) {
      console.log(`Rapicgen is already installed with version ${versionStatus.currentVersion}, which is greater than or equal to extension version ${versionStatus.targetVersion}. No update needed.`);
      return true;
    }
    
    // Set the appropriate title for the progress notification
    const title = isUpdate && versionStatus?.needsUpdate
      ? `Updating Rapicgen tool from v${versionStatus.currentVersion} to v${versionStatus.targetVersion}...`
      : "Installing Rapicgen tool...";
    
    const installResult = await vscode.window.withProgress({
      location: vscode.ProgressLocation.Notification,
      title: title,
      cancellable: false
    }, async () => {
      try {
        // Build the installation command
        let command = isUpdate && versionStatus?.needsUpdate
          ? 'dotnet tool update --global rapicgen'
          : 'dotnet tool install --global rapicgen';
        
        // Only specify version if we're in a release build (detected by version from package.json)
        // Local development builds won't specify a version
        if (version && version !== '0.1.0') {
          command += ` --version ${version}`;
        }
        
        execSync(command, { encoding: 'utf-8' });
        return true;
      } catch (error) {
        console.error('Failed to install/update rapicgen tool:', error);
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
 * Ensures that the Rapicgen tool is installed and up-to-date
 * @param context The extension context
 * @returns true if the tool is available and ready to use, false otherwise
 */
async function ensureRapicgenToolAvailable(context: vscode.ExtensionContext): Promise<boolean> {
  // Check if the Rapicgen tool is installed
  if (!isRapicgenInstalled()) {
    const shouldInstall = await vscode.window.showInformationMessage(
      'The Rapicgen .NET tool is not installed. Would you like to install it?',
      'Yes', 'No'
    );
    
    if (shouldInstall === 'Yes') {
      const installSuccess = await installRapicgen(context);
      if (!installSuccess) {
        vscode.window.showErrorMessage('Failed to install the Rapicgen .NET tool. Please install it manually with "dotnet tool install --global rapicgen".');
        return false;
      }
      return true;
    } else {
      return false;
    }
  } else {
    // Check if update is needed
    const versionStatus = checkRapicgenVersionStatus(context);
    if (versionStatus.needsUpdate) {
      const shouldUpdate = await vscode.window.showInformationMessage(
        `A newer version of the Rapicgen tool is available (current: v${versionStatus.currentVersion}, available: v${versionStatus.targetVersion}). Would you like to update?`,
        'Yes', 'No'
      );
      
      if (shouldUpdate === 'Yes') {
        const updateSuccess = await installRapicgen(context);
        if (!updateSuccess) {
          vscode.window.showWarningMessage(`Failed to update the Rapicgen tool. Continuing with existing version ${versionStatus.currentVersion}.`);
        }
      }
    }
    return true;
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
 * Gets the output directory for generated TypeScript code
 * @param specificationFile The path to the specification file
 * @param generator The generator name (for the directory name)
 * @returns The full path for the output directory
 */
function getTypeScriptOutputDirectory(specificationFile: string, generator: string): string {
  const baseOutputDir = getOutputDirectory(specificationFile);
  const fileName = path.basename(specificationFile, path.extname(specificationFile));
  return path.join(baseOutputDir, `${fileName}-${generator}-typescript`);
}

/**
 * Available code generators with their command names and display names
 */
const generators = [
  { command: 'nswag', displayName: 'NSwag', requiresJava: false },
  { command: 'refitter', displayName: 'Refitter', requiresJava: false },
  { command: 'openapi', displayName: 'OpenAPI Generator', requiresJava: true },
  { command: 'kiota', displayName: 'Microsoft Kiota', requiresJava: false },
  { command: 'swagger', displayName: 'Swagger Codegen CLI', requiresJava: true },
  { command: 'autorest', displayName: 'AutoREST', requiresJava: false }
];

/**
 * Available TypeScript generators with their command names and display names
 */
const typescriptGenerators = [
  { command: 'angular', displayName: 'Angular', requiresJava: true },
  { command: 'aurelia', displayName: 'Aurelia', requiresJava: true },
  { command: 'axios', displayName: 'Axios', requiresJava: true },
  { command: 'fetch', displayName: 'Fetch', requiresJava: true },
  { command: 'inversify', displayName: 'Inversify', requiresJava: true },
  { command: 'jquery', displayName: 'JQuery', requiresJava: true },
  { command: 'nestjs', displayName: 'NestJS', requiresJava: true },
  { command: 'node', displayName: 'Node', requiresJava: true },
  { command: 'reduxquery', displayName: 'Redux Query', requiresJava: true },
  { command: 'rxjs', displayName: 'RxJS', requiresJava: true }
];

/**
 * Checks if a generator requires Java runtime
 * @param generator The generator command name
 * @param isTypeScript Whether the generator is for TypeScript
 * @returns true if the generator requires Java, false otherwise
 */
function generatorRequiresJava(generator: string, isTypeScript = false): boolean {
  if (isTypeScript) {
    const typescriptGenerator = typescriptGenerators.find(g => g.command === generator);
    return typescriptGenerator?.requiresJava ?? false;
  } else {
    const csharpGenerator = generators.find(g => g.command === generator);
    return csharpGenerator?.requiresJava ?? false;
  }
}

/**
 * Executes the Rapicgen tool with specified parameters
 * @param command The full command to execute
 * @param generatorName Display name of the generator
 * @param outputPath Path to the output file or directory
 * @param isTypeScript Whether this is a TypeScript generator
 * @param isRefitterSettings Whether this is a Refitter settings execution
 */
async function executeRapicgenCommand(
  command: string,
  generatorName: string,
  outputPath: string,
  isTypeScript = false,
  isRefitterSettings = false
): Promise<void> {
  try {
    // Determine the progress notification title
    let title: string;
    if (isRefitterSettings) {
      title = 'Generating Refitter output from settings file...';
    } else if (isTypeScript) {
      title = `Generating TypeScript code with ${generatorName}...`;
    } else {
      title = `Generating code with ${generatorName}...`;
    }
      
    await vscode.window.withProgress({
      location: vscode.ProgressLocation.Notification,
      title: title,
      cancellable: false
    }, async () => {
      try {
        // Run with higher timeout since code generation can take time
        const output = execSync(command, { 
          encoding: 'utf-8',
          timeout: 120000 // 2 minute timeout
        });
        
        // Log output for debugging
        console.log(`Rapicgen${isTypeScript ? ' TypeScript' : ''}${isRefitterSettings ? ' Refitter settings' : ''} output: ${output}`);
        
        // Handle success based on the generator type
        if (isRefitterSettings) {
          vscode.window.showInformationMessage('Successfully generated Refitter output from settings file');
          return;
        }
        
        if (isTypeScript) {
          if (!fs.existsSync(outputPath) || fs.readdirSync(outputPath).length === 0) {
            vscode.window.showErrorMessage(`Failed to generate output in directory: ${outputPath}`);
            return;
          }
          vscode.window.showInformationMessage(`Successfully generated ${generatorName} TypeScript code in ${outputPath}`);
        } else {
          if (!fs.existsSync(outputPath)) {
            vscode.window.showErrorMessage(`Failed to generate output file: ${outputPath}`);
            return;
          }
          
          // Open the generated file for non-TypeScript outputs
          const document = await vscode.workspace.openTextDocument(outputPath);
          await vscode.window.showTextDocument(document);
          
          vscode.window.showInformationMessage(`Successfully generated ${generatorName} client code at ${outputPath}`);
        }
      } catch (error) {
        handleExecutionError(error, generatorName, isTypeScript);
      }
    });
  } catch (error) {
    console.error(`Error during ${isTypeScript ? 'TypeScript ' : ''}${isRefitterSettings ? 'Refitter settings ' : ''}code generation process:`, error);
  }
}
async function executeRapicgen(generator: string, specificationFilePath: string, context: vscode.ExtensionContext): Promise<void> {
  // Validate the specification file
  if (!validateSpecificationFile(specificationFilePath)) {
    return;
  }
  
  // Validate dependencies
  if (!validateDependencies(generator)) {
    return;
  }
  
  // Ensure the Rapicgen tool is installed and up-to-date
  const rapicgenAvailable = await ensureRapicgenToolAvailable(context);
  if (!rapicgenAvailable) {
    return;
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
  const generatorDisplayName = generators.find(g => g.command === generator)?.displayName || generator;
  
  await executeRapicgenCommand(command, generatorDisplayName, outputFile);
}

/**
 * Executes the Rapicgen tool to generate TypeScript client code
 * @param generator The TypeScript generator to use (angular, axios, etc.)
 * @param specificationFilePath The path to the OpenAPI/Swagger specification file
 * @param context The extension context
 */
async function executeRapicgenTypeScript(generator: string, specificationFilePath: string, context: vscode.ExtensionContext): Promise<void> {
  // Validate the specification file
  if (!validateSpecificationFile(specificationFilePath)) {
    return;
  }
  
  // Validate dependencies
  if (!validateDependencies(generator, true)) {
    return;
  }
  
  // Ensure the Rapicgen tool is installed and up-to-date
  const rapicgenAvailable = await ensureRapicgenToolAvailable(context);
  if (!rapicgenAvailable) {
    return;
  }

  // For TypeScript, we get an output directory rather than a single file
  const outputDir = getTypeScriptOutputDirectory(specificationFilePath, generator);
  
  // Ensure output directory exists
  if (!fs.existsSync(outputDir)) {
    try {
      fs.mkdirSync(outputDir, { recursive: true });
    } catch (err) {
      vscode.window.showErrorMessage(`Failed to create output directory: ${outputDir}`);
      return;
    }
  }
  
  const command = `rapicgen typescript ${generator} "${specificationFilePath}" "${outputDir}"`;
  const generatorDisplayName = typescriptGenerators.find(g => g.command === generator)?.displayName || generator;
  
  await executeRapicgenCommand(command, generatorDisplayName, outputDir, true);
}

/**
 * Executes the Rapicgen tool with Refitter using a settings file
 * @param settingsFilePath The path to the .refitter settings file
 * @param context The extension context
 */
async function executeRapicgenRefitterSettings(settingsFilePath: string, context: vscode.ExtensionContext): Promise<void> {
  // Validate the settings file
  if (!validateSpecificationFile(settingsFilePath)) {
    return;
  }
  
  // Validate dependencies
  if (!validateDependencies('refitter')) {
    return;
  }
  
  // Ensure the Rapicgen tool is installed and up-to-date
  const rapicgenAvailable = await ensureRapicgenToolAvailable(context);
  if (!rapicgenAvailable) {
    return;
  }

  const command = `rapicgen csharp refitter --settings-file "${settingsFilePath}"`;
  
  await executeRapicgenCommand(command, 'Refitter', settingsFilePath, false, true);
}

/**
 * Validates that a specification file exists and is readable
 * @param specificationFilePath The path to the OpenAPI/Swagger specification file
 * @returns true if file is valid, false otherwise
 */
function validateSpecificationFile(specificationFilePath: string): boolean {
  // Validate that the file exists
  if (!fs.existsSync(specificationFilePath)) {
    vscode.window.showErrorMessage(`File not found: ${specificationFilePath}`);
    return false;
  }
  
  // Validate that the file is readable
  try {
    fs.accessSync(specificationFilePath, fs.constants.R_OK);
    return true;
  } catch (err) {
    vscode.window.showErrorMessage(`Cannot read file: ${specificationFilePath}`);
    return false;
  }
}

/**
 * Activates the extension
 * @param context The extension context
 */
export function activate(context: vscode.ExtensionContext) {
  console.log('Rest API Client Code Generator extension is now active');

  // Register commands for each C# generator
  for (const generator of generators) {
    const disposable = vscode.commands.registerCommand(
      `restApiClientCodeGenerator.${generator.command}`, 
      async (fileUri: vscode.Uri) => {
        if (!fileUri) {
          // If command was triggered from command palette, ask for file
          fileUri = await promptForFile(
            '**/*.{json,yaml,yml}',
            'Select a Swagger/OpenAPI specification file',
            'No Swagger/OpenAPI specification files found in the workspace'
          );
          
          if (!fileUri) {
            return;
          }
        }

        executeRapicgen(generator.command, fileUri.fsPath, context);
      }
    );

    context.subscriptions.push(disposable);
  }
  
  // Register commands for each TypeScript generator
  for (const generator of typescriptGenerators) {
    const disposable = vscode.commands.registerCommand(
      `restApiClientCodeGenerator.typescript.${generator.command}`, 
      async (fileUri: vscode.Uri) => {
        if (!fileUri) {
          // If command was triggered from command palette, ask for file
          fileUri = await promptForFile(
            '**/*.{json,yaml,yml}',
            'Select a Swagger/OpenAPI specification file',
            'No Swagger/OpenAPI specification files found in the workspace'
          );
          
          if (!fileUri) {
            return;
          }
        }

        executeRapicgenTypeScript(generator.command, fileUri.fsPath, context);
      }
    );
    
    context.subscriptions.push(disposable);
  }
  
  // Register command for Refitter settings
  const refitterSettingsDisposable = vscode.commands.registerCommand(
    'restApiClientCodeGenerator.refitterSettings',
    async (fileUri: vscode.Uri) => {
      if (!fileUri) {
        // If command was triggered from command palette, ask for file
        fileUri = await promptForFile(
          '**/*.refitter',
          'Select a .refitter settings file',
          'No .refitter settings files found in the workspace'
        );
        
        if (!fileUri) {
          return;
        }
      }

      executeRapicgenRefitterSettings(fileUri.fsPath, context);
    }
  );

  context.subscriptions.push(refitterSettingsDisposable);
}

/**
 * Deactivates the extension
 */
export function deactivate() {
  // Nothing to do here
}
