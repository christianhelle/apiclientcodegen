import * as assert from 'assert';
import * as vscode from 'vscode';

suite('Extension Test Suite', () => {
  vscode.window.showInformationMessage('Starting tests.');

  test('Extension should be present', async () => {
    const extension = vscode.extensions.getExtension('ChristianResmaHelle.apiclientcodegen');
    assert.ok(extension, 'Extension should be found');
    
    // Activate the extension if it's not active
    if (!extension.isActive) {
      await extension.activate();
    }
    
    assert.ok(extension.isActive, 'Extension should be active');
  });
  test('Command registration', async () => {
    const extension = vscode.extensions.getExtension('ChristianResmaHelle.apiclientcodegen');
    assert.ok(extension, 'Extension should be found');
    
    // Activate the extension if it's not active
    if (!extension.isActive) {
      await extension.activate();
    }
    
    const commands = await vscode.commands.getCommands(true);
    
    // Check if our C# commands are registered
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.nswag'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.refitter'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.openapi'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.kiota'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.swagger'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.autorest'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.refitterSettings'), true);
  });

  test('TypeScript command registration', async () => {
    const extension = vscode.extensions.getExtension('ChristianResmaHelle.apiclientcodegen');
    assert.ok(extension, 'Extension should be found');
    
    // Activate the extension if it's not active
    if (!extension.isActive) {
      await extension.activate();
    }
    
    const commands = await vscode.commands.getCommands(true);
    
    // Check if our TypeScript commands are registered
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.typescript.angular'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.typescript.aurelia'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.typescript.axios'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.typescript.fetch'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.typescript.inversify'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.typescript.jquery'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.typescript.nestjs'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.typescript.node'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.typescript.reduxquery'), true);
    assert.strictEqual(commands.includes('restApiClientCodeGenerator.typescript.rxjs'), true);
  });

  test('Configuration validation', () => {
    const config = vscode.workspace.getConfiguration('restApiClientCodeGenerator');
    
    // Check if configuration properties exist
    assert.ok(config.has('namespace'));
    assert.ok(config.has('outputDirectory'));
    
    // Check default values
    assert.strictEqual(config.get('namespace'), 'GeneratedCode');
    assert.strictEqual(config.get('outputDirectory'), '');
  });
  test('TypeScript generators array validation', async () => {
    // This test ensures the TypeScript generators array contains the expected generators
    const expectedGenerators = [
      'angular', 'aurelia', 'axios', 'fetch', 'inversify', 
      'jquery', 'nestjs', 'node', 'reduxquery', 'rxjs'
    ];
    
    const extension = vscode.extensions.getExtension('ChristianResmaHelle.apiclientcodegen');
    if (extension && !extension.isActive) {
      await extension.activate();
    }
    
    const commands = await vscode.commands.getCommands(true);
    expectedGenerators.forEach((generator) => {
      assert.strictEqual(
        commands.includes(`restApiClientCodeGenerator.typescript.${generator}`), 
        true,
        `TypeScript generator command for ${generator} should be registered`
      );
    });
  });

  test('C# generators array validation', async () => {
    // This test ensures the C# generators array contains the expected generators
    const expectedGenerators = [
      'nswag', 'refitter', 'openapi', 'kiota', 'swagger', 'autorest'
    ];
    
    const extension = vscode.extensions.getExtension('ChristianResmaHelle.apiclientcodegen');
    if (extension && !extension.isActive) {
      await extension.activate();
    }
    
    const commands = await vscode.commands.getCommands(true);
    expectedGenerators.forEach((generator) => {
      assert.strictEqual(
        commands.includes(`restApiClientCodeGenerator.${generator}`), 
        true,
        `C# generator command for ${generator} should be registered`
      );
    });
  });
});
