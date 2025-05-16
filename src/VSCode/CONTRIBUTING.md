# Contributing to REST API Client Code Generator

Thank you for your interest in contributing to the REST API Client Code Generator VS Code extension!

## Development Setup

1. Clone the repository
   ```
   git clone https://github.com/christianhelle/apiclientcodegen.git
   ```

2. Navigate to the VS Code extension directory
   ```
   cd apiclientcodegen/src/VSCode
   ```

3. Install dependencies
   ```
   npm install
   ```

4. Open in VS Code
   ```
   code .
   ```

5. Press F5 to start debugging (launches the Extension Development Host)

## Build Process

The extension uses webpack for bundling. The main build scripts are:

- `npm run compile` - Compiles TypeScript to JavaScript
- `npm run watch` - Compiles and watches for changes
- `npm run package` - Creates a VSIX package
- `npm run lint` - Runs ESLint

## Testing

Run tests with:
```
npm test
```

## Pull Request Guidelines

1. Update the README.md with details of significant changes
2. Update the CHANGELOG.md with the version number and details
3. Make sure all tests pass
4. Your PR will be merged once approved

## Code of Conduct

Please be respectful and inclusive in all interactions related to this project.
