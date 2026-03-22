import * as assert from 'assert';
import { generators, generatorRequiresJava } from '../../services/generators';

suite('Generators Configuration Test Suite', () => {
  test('AutoRest generator is present in generators list', () => {
    // CRITICAL: Prevents accidental removal during deprecation period
    // AutoRest must remain in the list until Phase 3 (~Jan 2027)
    const autorestGenerator = generators.find(g => g.command === 'autorest');
    
    assert.notStrictEqual(
      autorestGenerator,
      undefined,
      'AutoRest must be present in generators list during deprecation period'
    );
  });

  test('AutoRest displayName contains Deprecated label', () => {
    // Validates the canonical "AutoRest (Deprecated)" label format
    const autorestGenerator = generators.find(g => g.command === 'autorest');
    
    assert.strictEqual(
      autorestGenerator?.displayName,
      'AutoRest (Deprecated)',
      'AutoRest displayName must use canonical "AutoRest (Deprecated)" format'
    );
  });

  test('AutoRest command is lowercase', () => {
    const autorestGenerator = generators.find(g => g.command === 'autorest');
    
    assert.strictEqual(
      autorestGenerator?.command,
      'autorest',
      'AutoRest command must be lowercase'
    );
  });

  test('AutoRest does not require Java', () => {
    // AutoRest is an NPM-based tool and does not require Java runtime
    const autorestGenerator = generators.find(g => g.command === 'autorest');
    
    assert.strictEqual(
      autorestGenerator?.requiresJava,
      false,
      'AutoRest should not require Java runtime'
    );
  });

  test('generatorRequiresJava returns false for AutoRest', () => {
    // Validates the helper function correctly identifies AutoRest as non-Java
    const requiresJava = generatorRequiresJava('autorest', false);
    
    assert.strictEqual(
      requiresJava,
      false,
      'generatorRequiresJava should return false for AutoRest'
    );
  });

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

  test('AutoRest is positioned last in generators array', () => {
    // Per UX convention, deprecated items should appear last
    const lastGenerator = generators[generators.length - 1];
    
    assert.strictEqual(
      lastGenerator.command,
      'autorest',
      'AutoRest should be positioned last in generators array (deprecated items go last)'
    );
  });
});
