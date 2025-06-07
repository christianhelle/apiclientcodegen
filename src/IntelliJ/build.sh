#!/bin/bash

# Build script for the IntelliJ plugin
# This script builds the plugin and runs basic validation

echo "Building REST API Client Code Generator IntelliJ Plugin..."

# Check if Java is available
if ! command -v java &> /dev/null; then
    echo "Error: Java is not installed or not in PATH"
    exit 1
fi

# Check Java version
JAVA_VERSION=$(java -version 2>&1 | grep -oP 'version "?(1\.)?\K\d+' | head -1)
if [ "$JAVA_VERSION" -lt 17 ]; then
    echo "Error: Java 17 or higher is required (found Java $JAVA_VERSION)"
    exit 1
fi

echo "Using Java version: $JAVA_VERSION"

# Make gradlew executable
chmod +x ./gradlew

# Clean and build the plugin
echo "Cleaning previous build..."
./gradlew clean

echo "Building plugin..."
./gradlew build

if [ $? -eq 0 ]; then
    echo "✅ Plugin built successfully!"
    echo "📦 Plugin archive location: build/distributions/"
    ls -la build/distributions/
else
    echo "❌ Build failed!"
    exit 1
fi

echo "🔍 Running verification tasks..."
./gradlew verifyPlugin

if [ $? -eq 0 ]; then
    echo "✅ Plugin verification passed!"
else
    echo "⚠️  Plugin verification had warnings/errors"
fi

echo "✨ Build process completed!"
