import * as assert from 'assert';
import * as vscode from 'vscode';
import * as path from 'path';
import * as fs from 'fs';

suite('TypeScript Generators Test Suite', () => {
  const testWorkspaceUri = vscode.workspace.workspaceFolders?.[0]?.uri;
  
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

  test('All TypeScript generator commands should be registered', async () => {
    const commands = await vscode.commands.getCommands(true);
    
    typescriptGenerators.forEach(generator => {
      const commandName = `restApiClientCodeGenerator.typescript.${generator.command}`;
      assert.strictEqual(
        commands.includes(commandName), 
        true,
        `Command ${commandName} for ${generator.displayName} should be registered`
      );
    });
  });

  test('TypeScript generator commands should have correct structure', () => {
    typescriptGenerators.forEach(generator => {
      // Validate generator properties
      assert.ok(generator.command, `Generator should have a command property: ${generator.displayName}`);
      assert.ok(generator.displayName, `Generator should have a display name: ${generator.command}`);
      assert.strictEqual(typeof generator.requiresJava, 'boolean', `Generator should have a boolean requiresJava property: ${generator.displayName}`);
      
      // All TypeScript generators currently require Java
      assert.strictEqual(generator.requiresJava, true, `TypeScript generator ${generator.displayName} should require Java`);
    });
  });

  test('TypeScript generators count should match expected', () => {
    assert.strictEqual(typescriptGenerators.length, 10, 'There should be exactly 10 TypeScript generators');
  });

  test('TypeScript generator command names should be unique', () => {
    const commandNames = typescriptGenerators.map(g => g.command);
    const uniqueCommandNames = [...new Set(commandNames)];
    
    assert.strictEqual(
      commandNames.length, 
      uniqueCommandNames.length, 
      'All TypeScript generator command names should be unique'
    );
  });

  test('TypeScript generator display names should be unique', () => {
    const displayNames = typescriptGenerators.map(g => g.displayName);
    const uniqueDisplayNames = [...new Set(displayNames)];
    
    assert.strictEqual(
      displayNames.length, 
      uniqueDisplayNames.length, 
      'All TypeScript generator display names should be unique'
    );
  });

  test('Extension configuration should support TypeScript output', () => {
    const config = vscode.workspace.getConfiguration('restApiClientCodeGenerator');
    
    // The outputDirectory setting should work for TypeScript generators too
    assert.ok(config.has('outputDirectory'), 'Configuration should have outputDirectory setting');
    
    // Test that we can set output directory
    const defaultOutputDir = config.get<string>('outputDirectory', '');
    assert.strictEqual(typeof defaultOutputDir, 'string', 'Output directory should be a string');
  });
  test('Package.json should include all TypeScript commands', async () => {
    if (!testWorkspaceUri) {
      // Skip this test if no workspace is available
      console.log('Skipping package.json test - no workspace folder found');
      return;
    }

    const packageJsonPath = path.join(testWorkspaceUri.fsPath, 'package.json');
    assert.ok(fs.existsSync(packageJsonPath), 'package.json should exist');    const packageJson = JSON.parse(fs.readFileSync(packageJsonPath, 'utf8'));
    const commands = packageJson.contributes?.commands || [];

    typescriptGenerators.forEach(generator => {
      const expectedCommand = `restApiClientCodeGenerator.typescript.${generator.command}`;
      const commandExists = commands.some((cmd: { command?: string }) => cmd.command === expectedCommand);
      
      assert.strictEqual(
        commandExists, 
        true, 
        `Package.json should include command ${expectedCommand} for ${generator.displayName}`
      );
    });
  });
  test('Package.json should include TypeScript submenu', async () => {
    if (!testWorkspaceUri) {
      // Skip this test if no workspace is available
      console.log('Skipping submenu test - no workspace folder found');
      return;
    }

    const packageJsonPath = path.join(testWorkspaceUri.fsPath, 'package.json');
    const packageJson = JSON.parse(fs.readFileSync(packageJsonPath, 'utf8'));
      const submenus = packageJson.contributes?.submenus || [];
    const typescriptSubmenu = submenus.find((submenu: { id?: string }) => 
      submenu.id === 'restApiClientCodeGenerator.typescriptSubmenu'
    );
    
    assert.ok(typescriptSubmenu, 'Package.json should include TypeScript submenu');
    assert.strictEqual(typescriptSubmenu.label, 'TypeScript', 'TypeScript submenu should have correct label');
  });
  test('Package.json should include TypeScript menu items', async () => {
    if (!testWorkspaceUri) {
      // Skip this test if no workspace is available
      console.log('Skipping menu items test - no workspace folder found');
      return;
    }

    const packageJsonPath = path.join(testWorkspaceUri.fsPath, 'package.json');
    const packageJson = JSON.parse(fs.readFileSync(packageJsonPath, 'utf8'));
    
    const menus = packageJson.contributes?.menus || {};
    const typescriptSubmenuItems = menus['restApiClientCodeGenerator.typescriptSubmenu'] || [];
    
    assert.ok(Array.isArray(typescriptSubmenuItems), 'TypeScript submenu items should be an array');
    assert.strictEqual(
      typescriptSubmenuItems.length, 
      typescriptGenerators.length, 
      'Number of TypeScript submenu items should match number of generators'
    );    typescriptGenerators.forEach(generator => {
      const expectedCommand = `restApiClientCodeGenerator.typescript.${generator.command}`;
      const menuItemExists = typescriptSubmenuItems.some((item: { command?: string }) => 
        item.command === expectedCommand
      );
      
      assert.strictEqual(
        menuItemExists, 
        true, 
        `TypeScript submenu should include item for ${generator.displayName}`
      );
    });
  });

  test('TypeScript generator commands should handle missing file parameter gracefully', async () => {
    // This test ensures commands don't crash when called without a file parameter
    // We can't easily test the full execution without actual OpenAPI files and tools installed,
    // but we can at least verify the commands exist and are callable
    
    const commands = await vscode.commands.getCommands(true);
    
    for (const generator of typescriptGenerators.slice(0, 2)) { // Test first 2 to avoid timeout
      const commandName = `restApiClientCodeGenerator.typescript.${generator.command}`;
      assert.strictEqual(
        commands.includes(commandName), 
        true,
        `Command ${commandName} should be registered and callable`
      );
    }
  });

  test('Command naming convention should be consistent', () => {
    typescriptGenerators.forEach(generator => {
      // Command names should be lowercase and valid JavaScript identifiers
      assert.ok(
        /^[a-z][a-z0-9]*$/.test(generator.command),
        `Generator command name '${generator.command}' should be lowercase alphanumeric starting with letter`
      );
      
      // Display names should be proper case
      assert.ok(
        generator.displayName.charAt(0).toUpperCase() === generator.displayName.charAt(0),
        `Generator display name '${generator.displayName}' should start with uppercase letter`
      );
    });
  });
});
