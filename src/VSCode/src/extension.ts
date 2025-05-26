import * as vscode from 'vscode';
import { promptForFile } from './utils/file-utils';
import { generators, typescriptGenerators, executeRapicgen, executeRapicgenTypeScript, executeRapicgenRefitterSettings } from './services/generators';

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
          const selectedFileUri = await promptForFile(
            '**/*.{json,yaml,yml}',
            'Select a Swagger/OpenAPI specification file',
            'No Swagger/OpenAPI specification files found in the workspace'
          );
          
          if (!selectedFileUri) {
            return;
          }
          fileUri = selectedFileUri;
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
          const selectedFileUri = await promptForFile(
            '**/*.{json,yaml,yml}',
            'Select a Swagger/OpenAPI specification file',
            'No Swagger/OpenAPI specification files found in the workspace'
          );
          
          if (!selectedFileUri) {
            return;
          }
          fileUri = selectedFileUri;
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
        const selectedFileUri = await promptForFile(
          '**/*.refitter',
          'Select a .refitter settings file',
          'No .refitter settings files found in the workspace'
        );
        
        if (!selectedFileUri) {
          return;
        }
        fileUri = selectedFileUri;
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
