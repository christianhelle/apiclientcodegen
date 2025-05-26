import * as vscode from 'vscode';
import { execSync } from 'child_process';

/**
 * Checks if the .NET SDK is installed and available
 * @returns true if the .NET SDK is installed, false otherwise
 */
export function isDotNetSdkInstalled(): boolean {
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
export function isJavaRuntimeInstalled(): boolean {
  try {
    execSync('java -version', { encoding: 'utf-8' });
    return true;
  } catch (error) {
    console.error('Error checking Java runtime:', error);
    return false;
  }
}

/**
 * Validates required dependencies for a generator
 * @param generator The generator command name
 * @param generatorsList List of generators to check against
 * @param isTypeScript Whether the generator is for TypeScript
 * @returns true if all requirements are met, false otherwise
 */
export function validateDependencies(
  generator: string, 
  generatorRequiresJava: (generator: string, isTypeScript: boolean) => boolean,
  isTypeScript = false
): boolean {
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