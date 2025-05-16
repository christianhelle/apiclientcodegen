import * as assert from 'assert';
import * as vscode from 'vscode';

suite('Extension Test Suite', () => {
  vscode.window.showInformationMessage('Starting tests.');

  test('Extension should be present', () => {
    assert.strictEqual(
      vscode.extensions.getExtension('ChristianResmaHelle.rest-api-client-code-generator') !== undefined,
      true
    );
  });

  test('Command registration', async () => {
    const commands = await vscode.commands.getCommands(true);
    
    // Check if our commands are registered
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.nswag'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.refitter'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.openapi'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.kiota'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.swagger'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.autoRest'), true);
  });
});
