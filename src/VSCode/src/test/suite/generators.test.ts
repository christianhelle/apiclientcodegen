import * as assert from 'assert';
import { generators, generatorRequiresJava } from '../../services/generators';

suite('Generators Configuration Test Suite', () => {
  test('Removed generator is absent from generators list', () => {
    const autorestGenerator = generators.find(g => g.command === 'autorest');
    assert.strictEqual(autorestGenerator, undefined);
  });

  test('All generators have required fields', () => {
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
    const commands = generators.map(g => g.command);
    const uniqueCommands = new Set(commands);

    assert.strictEqual(commands.length, uniqueCommands.size, 'All generator commands must be unique');
  });

  test('Removed generator command is not treated as requiring Java', () => {
    assert.strictEqual(generatorRequiresJava('autorest', false), false);
  });
});
