import * as assert from 'assert';
import * as vscode from 'vscode';

suite('Extension Test Suite', () => {
  vscode.window.showInformationMessage('Starting tests.');

  test('Extension should be present', () => {
    assert.strictEqual(
      vscode.extensions.getExtension('ChristianResmaHelle.apiclientcodegen') !== undefined,
      true
    );
  });

  test('Command registration', async () => {
    const commands = await vscode.commands.getCommands(true);

    assert.strictEqual(commands.includes('restApiClientCodeGenerator.nswag'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.refitter'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.openapi'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.kiota'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.swagger'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.autorest'), false);
  });
});
