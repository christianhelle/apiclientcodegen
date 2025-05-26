import * as vscode from 'vscode';

/**
 * Handles command execution errors
 * @param error The error that occurred
 * @param generatorName The name of the generator that failed
 * @param isTypeScript Whether the generator is for TypeScript 
 */
export function handleExecutionError(error: unknown, generatorName: string, isTypeScript = false): void {
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