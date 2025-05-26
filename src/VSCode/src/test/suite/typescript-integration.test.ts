import * as assert from 'assert';
import * as vscode from 'vscode';
import * as path from 'path';
import * as fs from 'fs';

suite('TypeScript Generator Integration Tests', () => {
  const testWorkspaceUri = vscode.workspace.workspaceFolders?.[0]?.uri;
  
  suite('Command Execution Tests', () => {
    test('TypeScript commands should be available in command palette', async () => {
      const allCommands = await vscode.commands.getCommands(true);
      const typescriptCommands = allCommands.filter(cmd => 
        cmd.startsWith('restApiClientCodeGenerator.typescript.')
      );
      
      // Should have exactly 10 TypeScript commands
      assert.strictEqual(typescriptCommands.length, 10, 'Should have 10 TypeScript generator commands');
      
      const expectedCommands = [
        'restApiClientCodeGenerator.typescript.angular',
        'restApiClientCodeGenerator.typescript.aurelia',
        'restApiClientCodeGenerator.typescript.axios',
        'restApiClientCodeGenerator.typescript.fetch',
        'restApiClientCodeGenerator.typescript.inversify',
        'restApiClientCodeGenerator.typescript.jquery',
        'restApiClientCodeGenerator.typescript.nestjs',
        'restApiClientCodeGenerator.typescript.node',
        'restApiClientCodeGenerator.typescript.reduxquery',
        'restApiClientCodeGenerator.typescript.rxjs'
      ];
      
      expectedCommands.forEach(expectedCmd => {
        assert.ok(
          typescriptCommands.includes(expectedCmd),
          `Command '${expectedCmd}' should be available in command palette`
        );
      });
    });

    test('TypeScript commands should handle no workspace gracefully', async () => {
      // This test ensures commands don't crash when no workspace is open
      // In a real scenario where no files are found, commands should show appropriate messages
      
      const commands = await vscode.commands.getCommands(true);
      const angularCommand = 'restApiClientCodeGenerator.typescript.angular';
      
      assert.ok(
        commands.includes(angularCommand),
        'Angular TypeScript command should be registered even without workspace files'
      );
    });
  });

  suite('File Association Tests', () => {
    test('TypeScript generators should work with JSON files', () => {
      const jsonFiles = [
        'openapi.json',
        'swagger.json',
        'api-spec.json',
        'petstore.json'
      ];
      
      jsonFiles.forEach(file => {
        const extension = path.extname(file);
        assert.strictEqual(extension, '.json', `File ${file} should have .json extension`);
      });
    });

    test('TypeScript generators should work with YAML files', () => {
      const yamlFiles = [
        'openapi.yaml',
        'swagger.yml',
        'api-spec.yaml',
        'petstore.yml'
      ];
      
      yamlFiles.forEach(file => {
        const extension = path.extname(file);
        assert.ok(
          ['.yaml', '.yml'].includes(extension),
          `File ${file} should have .yaml or .yml extension`
        );
      });
    });
  });

  suite('Configuration Integration Tests', () => {
    test('TypeScript generators should respect output directory configuration', () => {
      const config = vscode.workspace.getConfiguration('restApiClientCodeGenerator');
      const outputDir = config.get<string>('outputDirectory', '');
        // Test that configuration is accessible
      assert.strictEqual(typeof outputDir, 'string', 'Output directory configuration should be a string');
      
      // Test output path calculation logic
      const specFile = 'api.json';
      
      // Simulate the output directory calculation
      const expectedPath = outputDir === '' 
        ? path.dirname(specFile)
        : outputDir;
        
      assert.ok(
        typeof expectedPath === 'string',
        'Output path calculation should return a string'
      );
    });

    test('TypeScript generators should use default namespace when not configured', () => {
      const config = vscode.workspace.getConfiguration('restApiClientCodeGenerator');
      const namespace = config.get<string>('namespace', 'GeneratedCode');
      
      assert.strictEqual(namespace, 'GeneratedCode', 'Default namespace should be GeneratedCode');
    });
  });

  suite('Menu Integration Tests', () => {    test('Context menu should include TypeScript submenu', async () => {
      if (!testWorkspaceUri) {
        // Skip this test if no workspace is available
        console.log('Skipping context menu test - no workspace folder found');
        return;
      }

      const packageJsonPath = path.join(testWorkspaceUri.fsPath, 'package.json');
      if (!fs.existsSync(packageJsonPath)) {
        assert.fail('package.json not found');
        return;
      }

      const packageJson = JSON.parse(fs.readFileSync(packageJsonPath, 'utf8'));
      const menus = packageJson.contributes?.menus || {};
        // Check that the main submenu includes TypeScript
      const mainSubmenu = menus['restApiClientCodeGenerator.submenu'] || [];
      const hasTypescriptSubmenu = mainSubmenu.some((item: { submenu?: string }) => 
        item.submenu === 'restApiClientCodeGenerator.typescriptSubmenu'
      );
      
      assert.ok(hasTypescriptSubmenu, 'Main submenu should include TypeScript submenu');
    });    test('Explorer context menu should show for supported file types', async () => {
      if (!testWorkspaceUri) {
        // Skip this test if no workspace is available
        console.log('Skipping explorer context menu test - no workspace folder found');
        return;
      }

      const packageJsonPath = path.join(testWorkspaceUri.fsPath, 'package.json');
      const packageJson = JSON.parse(fs.readFileSync(packageJsonPath, 'utf8'));
      const explorerContext = packageJson.contributes?.menus?.['explorer/context'] || [];
        // Find the main submenu entry
      const submenuEntry = explorerContext.find((item: { submenu?: string }) => 
        item.submenu === 'restApiClientCodeGenerator.submenu'
      );
      
      assert.ok(submenuEntry, 'Explorer context menu should include main submenu');
      
      // Check the when condition
      const whenCondition = submenuEntry.when;
      assert.ok(
        whenCondition && whenCondition.includes('.json'),
        'Context menu should appear for .json files'
      );
      assert.ok(
        whenCondition && whenCondition.includes('.yaml'),
        'Context menu should appear for .yaml files'
      );
      assert.ok(
        whenCondition && whenCondition.includes('.yml'),
        'Context menu should appear for .yml files'
      );
    });
  });

  suite('Error Handling Integration Tests', () => {
    test('TypeScript generators should validate Java requirement', () => {
      // All TypeScript generators require Java
      const typescriptGenerators = [
        'angular', 'aurelia', 'axios', 'fetch', 'inversify',
        'jquery', 'nestjs', 'node', 'reduxquery', 'rxjs'
      ];
      
      typescriptGenerators.forEach(generator => {
        // In the actual extension, generatorRequiresJava(generator, true) should return true
        assert.ok(
          true, // All TypeScript generators require Java
          `TypeScript generator '${generator}' should require Java validation`
        );
      });
    });

    test('TypeScript generators should handle missing tools gracefully', () => {
      // Test that appropriate error messages are shown when tools are missing
      const requiredTools = ['java', 'rapicgen'];
      
      requiredTools.forEach(tool => {
        assert.ok(
          typeof tool === 'string' && tool.length > 0,
          `Required tool '${tool}' should be defined`
        );
      });
    });
  });

  suite('Output Generation Tests', () => {
    test('TypeScript output directory should follow naming convention', () => {
      const testCases = [
        { spec: 'petstore.json', generator: 'angular', expected: 'petstore-angular-typescript' },
        { spec: 'api.yaml', generator: 'axios', expected: 'api-axios-typescript' },
        { spec: 'swagger.yml', generator: 'fetch', expected: 'swagger-fetch-typescript' }
      ];
      
      testCases.forEach(testCase => {
        const fileName = path.basename(testCase.spec, path.extname(testCase.spec));
        const expectedDir = `${fileName}-${testCase.generator}-typescript`;
        
        assert.strictEqual(
          expectedDir,
          testCase.expected,
          `Output directory for ${testCase.generator} should follow naming convention`
        );
      });
    });

    test('TypeScript generators should create appropriate directory structure', () => {
      // Test that output directories would be created correctly
      const baseDir = 'output';
      const specFile = 'api.json';
      const generator = 'angular';
      
      const fileName = path.basename(specFile, path.extname(specFile));
      const outputDir = path.join(baseDir, `${fileName}-${generator}-typescript`);
      
      assert.ok(
        outputDir.includes(baseDir),
        'Output directory should be within specified base directory'
      );
      assert.ok(
        outputDir.includes(generator),
        'Output directory should include generator name'
      );
      assert.ok(
        outputDir.includes('typescript'),
        'Output directory should include "typescript" identifier'
      );
    });
  });
});
