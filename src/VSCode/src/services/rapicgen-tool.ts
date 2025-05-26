import * as vscode from 'vscode';
import * as fs from 'fs';
import { execSync } from 'child_process';
import { platform } from 'os';
import { IRapicgenVersionStatus } from '../types/types';
import { getExtensionVersion, compareVersions } from '../utils/version-utils';
import { handleExecutionError } from '../utils/error-utils';

/**
 * Gets the installed Rapicgen .NET tool version
 * @returns The installed version as a string, or null if not installed or version cannot be determined
 */
export function getInstalledRapicgenVersion(): string | null {
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
 * Checks if the Rapicgen .NET tool is installed globally
 * @returns true if the tool is installed, false otherwise
 */
export function isRapicgenInstalled(): boolean {
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
export function checkRapicgenVersionStatus(context: vscode.ExtensionContext): IRapicgenVersionStatus {
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
export async function installRapicgen(context: vscode.ExtensionContext): Promise<boolean> {
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
export async function ensureRapicgenToolAvailable(context: vscode.ExtensionContext): Promise<boolean> {
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
 * Executes the Rapicgen tool with specified parameters
 * @param command The full command to execute
 * @param generatorName Display name of the generator
 * @param outputPath Path to the output file or directory
 * @param isTypeScript Whether this is a TypeScript generator
 * @param isRefitterSettings Whether this is a Refitter settings execution
 */
export async function executeRapicgenCommand(
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