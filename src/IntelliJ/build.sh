#!/bin/bash

# Build script for IntelliJ plugin

echo "Building IntelliJ Plugin for REST API Client Code Generator..."

# Check if gradlew exists
if [ ! -f "./gradlew" ]; then
    echo "Error: gradlew not found. Please ensure you're in the IntelliJ plugin directory."
    exit 1
fi

# Check Java version
if ! command -v java &> /dev/null; then
    echo "Error: Java is not installed or not in PATH."
    exit 1
fi

java_version=$(java -version 2>&1 | head -1 | cut -d'"' -f2 | sed '/^1\./s///' | cut -d'.' -f1)
if [ "$java_version" -lt 17 ]; then
    echo "Error: Java 17 or higher is required. Found Java $java_version"
    exit 1
fi

echo "Java version check passed: Java $java_version"

# Build the plugin
echo "Running Gradle build..."
./gradlew clean build

if [ $? -eq 0 ]; then
    echo "Build successful!"
    echo "Plugin JAR can be found in build/distributions/"
    ls -la build/distributions/ 2>/dev/null || echo "Distribution directory not yet created"
else
    echo "Build failed!"
    exit 1
fi