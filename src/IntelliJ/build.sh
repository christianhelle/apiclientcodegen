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
echo "Note: First build may take several minutes to download dependencies..."

# Try a simple task first to validate the setup
echo "Validating Gradle setup..."
if timeout 120 ./gradlew tasks --console=plain > /dev/null 2>&1; then
    echo "Gradle setup validation successful"
    
    echo "Building plugin..."
    if timeout 300 ./gradlew build --console=plain; then
        echo "Build successful!"
        echo "Plugin distribution files:"
        find build/distributions -name "*.zip" 2>/dev/null || echo "Distribution directory not yet created"
    else
        echo "Build failed! Please check the Gradle configuration."
        echo "You can try running './gradlew build --stacktrace' for detailed error information."
        exit 1
    fi
else
    echo "Gradle setup validation failed!"
    echo "Please ensure Java 17+ is properly installed and JAVA_HOME is set correctly."
    echo "You can try running './gradlew tasks --stacktrace' for detailed error information."
    exit 1
fi