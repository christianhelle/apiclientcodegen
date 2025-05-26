import * as vscode from 'vscode';
import * as path from 'path';
import * as fs from 'fs';
import { IFileQuickPickItem } from '../types/types';

/**
 * Prompt the user to select a file from the workspace
 * @param filePattern The glob pattern to search for files
 * @param placeHolder The placeholder text for the quick pick menu
 * @param errorMessage The error message to show if no files are found
 * @returns The selected file URI or undefined if canceled
 */
export async function promptForFile(
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
 * Gets the namespace to use for generated code
 * @returns The namespace from configuration or a default value
 */
export function getNamespace(): string {
  const config = vscode.workspace.getConfiguration('restApiClientCodeGenerator');
  return config.get<string>('namespace', 'GeneratedCode');
}

/**
 * Gets the output directory to use for generated code
 * @param specificationFile The path to the specification file
 * @returns The output directory path
 */
export function getOutputDirectory(specificationFile: string): string {
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
export function getOutputFilePath(specificationFile: string, generator: string): string {
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
export function getTypeScriptOutputDirectory(specificationFile: string, generator: string): string {
  const baseOutputDir = getOutputDirectory(specificationFile);
  const fileName = path.basename(specificationFile, path.extname(specificationFile));
  return path.join(baseOutputDir, `${fileName}-${generator}-typescript`);
}

/**
 * Validates that a specification file exists and is readable
 * @param specificationFilePath The path to the OpenAPI/Swagger specification file
 * @returns true if file is valid, false otherwise
 */
export function validateSpecificationFile(specificationFilePath: string): boolean {
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