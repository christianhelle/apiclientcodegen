import * as vscode from 'vscode';
import * as path from 'path';
import * as fs from 'fs';
import { IGenerator } from '../types/types';
import { validateSpecificationFile, getNamespace, getOutputFilePath, getTypeScriptOutputDirectory } from '../utils/file-utils';
import { validateDependencies } from '../utils/system-utils';
import { executeRapicgenCommand, ensureRapicgenToolAvailable } from './rapicgen-tool';

/**
 * Available code generators with their command names and display names
 */
export const generators: IGenerator[] = [
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
export const typescriptGenerators: IGenerator[] = [
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
 * Available Visual Basic generators with their command names and display names
 */
export const vbGenerators: IGenerator[] = [
  { command: 'nswag', displayName: 'NSwag', requiresJava: false },
  { command: 'refitter', displayName: 'Refitter', requiresJava: false },
  { command: 'openapi', displayName: 'OpenAPI Generator', requiresJava: true },
  { command: 'kiota', displayName: 'Microsoft Kiota', requiresJava: false },
  { command: 'swagger', displayName: 'Swagger Codegen CLI', requiresJava: true },
  { command: 'autorest', displayName: 'AutoREST', requiresJava: false }
];

/**
 * Checks if a generator requires Java runtime
 * @param generator The generator command name
 * @param isTypeScript Whether the generator is for TypeScript
 * @param isVb Whether the generator is for Visual Basic
 * @returns true if the generator requires Java, false otherwise
 */
export function generatorRequiresJava(generator: string, isTypeScript = false, isVb = false): boolean {
  if (isTypeScript) {
    const typescriptGenerator = typescriptGenerators.find(g => g.command === generator);
    return typescriptGenerator?.requiresJava ?? false;
  } else if (isVb) {
    const vbGenerator = vbGenerators.find(g => g.command === generator);
    return vbGenerator?.requiresJava ?? false;
  } else {
    const csharpGenerator = generators.find(g => g.command === generator);
    return csharpGenerator?.requiresJava ?? false;
  }
}

/**
 * Executes the Rapicgen tool to generate C# client code
 * @param generator The generator to use (nswag, openapi, etc.)
 * @param specificationFilePath The path to the OpenAPI/Swagger specification file
 * @param context The extension context
 */
export async function executeRapicgen(generator: string, specificationFilePath: string, context: vscode.ExtensionContext): Promise<void> {
  // Validate the specification file
  if (!validateSpecificationFile(specificationFilePath)) {
    return;
  }
  
  // Validate dependencies
  if (!validateDependencies(generator, generatorRequiresJava)) {
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
export async function executeRapicgenTypeScript(generator: string, specificationFilePath: string, context: vscode.ExtensionContext): Promise<void> {
  // Validate the specification file
  if (!validateSpecificationFile(specificationFilePath)) {
    return;
  }
  
  // Validate dependencies
  if (!validateDependencies(generator, generatorRequiresJava, true)) {
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
export async function executeRapicgenRefitterSettings(settingsFilePath: string, context: vscode.ExtensionContext): Promise<void> {
  // Validate the settings file
  if (!validateSpecificationFile(settingsFilePath)) {
    return;
  }
  
  // Validate dependencies
  if (!validateDependencies('refitter', generatorRequiresJava)) {
    return;
  }
  
  // Ensure the Rapicgen tool is installed and up-to-date
  const rapicgenAvailable = await ensureRapicgenToolAvailable(context);
  if (!rapicgenAvailable) {
    return;
  }

  const command = `rapicgen csharp refitter . --settings-file "${settingsFilePath}"`;
  
  await executeRapicgenCommand(command, 'Refitter', settingsFilePath, false, true);
}

/**
 * Executes the Rapicgen tool to generate Visual Basic client code
 * @param generator The generator to use (nswag, openapi, etc.)
 * @param specificationFilePath The path to the OpenAPI/Swagger specification file
 * @param context The extension context
 */
export async function executeRapicgenVisualBasic(generator: string, specificationFilePath: string, context: vscode.ExtensionContext): Promise<void> {
  // Validate the specification file
  if (!validateSpecificationFile(specificationFilePath)) {
    return;
  }
  
  // Validate dependencies
  if (!validateDependencies(generator, (gen) => generatorRequiresJava(gen, false, true))) {
    return;
  }
  
  // Ensure the Rapicgen tool is installed and up-to-date
  const rapicgenAvailable = await ensureRapicgenToolAvailable(context);
  if (!rapicgenAvailable) {
    return;
  }

  const namespace = getNamespace();
  const outputFile = getOutputFilePath(specificationFilePath, generator, 'vb');
  
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
  
  const command = `rapicgen vb ${generator} "${specificationFilePath}" "${namespace}" "${outputFile}"`;
  const generatorDisplayName = vbGenerators.find(g => g.command === generator)?.displayName || generator;
  
  await executeRapicgenCommand(command, `${generatorDisplayName} (VB.NET)`, outputFile);
}