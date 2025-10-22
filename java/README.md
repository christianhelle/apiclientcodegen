# Java Runtime Environment (JRE)

This folder contains the OpenJDK Java Runtime Environment used by Java-based code generators in this project.

## Bundled JRE

- **Distribution**: Eclipse Adoptium Temurin
- **Version**: 21.0.3+9-LTS (JRE)
- **Build Date**: 2024-04-16
- **Platform**: Windows x86_64

## OpenJDK Dependency

This project uses Java-based tools for generating REST API client code from OpenAPI/Swagger specifications. The following code generators require a Java Runtime Environment:

### 1. OpenAPI Generator

**OpenAPI Generator** is a Java-based code generation tool that creates client libraries, server stubs, and API documentation from OpenAPI specifications.

- **Current Version**: 7.16.0 (latest supported version)
- **Distribution**: JAR file (`openapi-generator-cli-{version}.jar`)
- **Download Location**: Maven Central Repository
- **Repository**: https://github.com/OpenAPITools/openapi-generator
- **Java Requirement**: Java 11 or higher

**Usage in this project:**
```bash
java -jar openapi-generator-cli-7.16.0.jar generate \
  --generator-name csharp \
  --input-spec [swagger file] \
  --output [output directory] \
  --package-name [namespace] \
  --global-property apiTests=false \
  --global-property modelTests=false
```

The tool is downloaded on-demand when first used and cached locally for subsequent use.

### 2. Swagger Codegen CLI

**Swagger Codegen CLI** is the predecessor to OpenAPI Generator and provides similar functionality for generating client code from Swagger/OpenAPI specifications.

- **Current Version**: 3.0.34
- **Distribution**: JAR file (`swagger-codegen-cli.jar`)
- **Download Location**: Maven Central Repository
- **Repository**: https://github.com/swagger-api/swagger-codegen
- **Java Requirement**: Java 8 or higher

**Usage in this project:**
```bash
java -jar swagger-codegen-cli.jar generate \
  -l csharp \
  --input-spec [swagger file] \
  --output [output directory] \
  -DpackageName=[namespace] \
  -DapiTests=false \
  -DmodelTests=false
```

Like OpenAPI Generator, Swagger Codegen CLI is also downloaded on-demand when first used.

## Java Path Configuration

By default, the code generators look for Java in the following order:

1. **Custom Path**: If configured in the tool options (Tools → REST API Client Code Generator → General)
2. **JAVA_HOME Environment Variable**: The standard location for Java installations
3. **System PATH**: Falls back to searching for `java` in the system PATH

If no Java installation is found, the bundled JRE in this folder may be used on Windows systems.

## Alternative Java Distributions

While this project bundles Eclipse Adoptium Temurin, the code generators are compatible with various Java distributions:

- Oracle JDK
- OpenJDK
- Eclipse Adoptium Temurin
- Amazon Corretto
- Azul Zulu
- Microsoft Build of OpenJDK

Any Java 11+ runtime should work for OpenAPI Generator, and Java 8+ for Swagger Codegen CLI.

## Installation Notes

### For End Users

The Java-based code generators (JAR files) are downloaded automatically when first used. The bundled JRE ensures the generators work out-of-the-box on Windows without requiring a separate Java installation.

### For Developers

If you're developing or extending this tool:

1. Ensure Java 11+ is installed on your development machine
2. Set the `JAVA_HOME` environment variable to your JDK installation path
3. The build and test processes may require a full JDK (not just JRE)

## References

- [OpenAPI Generator Documentation](https://openapi-generator.tech/)
- [Swagger Codegen Documentation](https://github.com/swagger-api/swagger-codegen/wiki)
- [Eclipse Adoptium](https://adoptium.net/)
- [OpenAPI Specification](https://www.openapis.org/)
