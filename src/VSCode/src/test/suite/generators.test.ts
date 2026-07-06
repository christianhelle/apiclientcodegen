import * as assert from 'assert';
import { generators } from '../../services/generators';

suite('Generators Configuration Test Suite', () => {
  test('All generators have required fields', () => {
    // Validates generator structure integrity
    generators.forEach(generator => {
      assert.notStrictEqual(generator.command, undefined, `Generator ${generator.displayName} missing command`);
      assert.notStrictEqual(generator.displayName, undefined, `Generator ${generator.command} missing displayName`);
      assert.notStrictEqual(generator.requiresJava, undefined, `Generator ${generator.command} missing requiresJava`);
      
      assert.strictEqual(typeof generator.command, 'string', `Generator ${generator.displayName} command must be string`);
      assert.strictEqual(typeof generator.displayName, 'string', `Generator ${generator.command} displayName must be string`);
      assert.strictEqual(typeof generator.requiresJava, 'boolean', `Generator ${generator.command} requiresJava must be boolean`);
    });
  });

  test('Generator commands are unique', () => {
    // Ensures no duplicate command names exist
    const commands = generators.map(g => g.command);
    const uniqueCommands = new Set(commands);
    
    assert.strictEqual(
      commands.length,
      uniqueCommands.size,
      'All generator commands must be unique'
    );
  });
});
