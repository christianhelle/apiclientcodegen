#!/bin/bash

# Build script for IntelliJ plugin

set -e

echo "Building REST API Client Code Generator IntelliJ Plugin..."

# Ensure we're in the IntelliJ directory
cd "$(dirname "$0")"

# Clean previous builds
echo "Cleaning previous builds..."
./gradlew clean

# Build the plugin
echo "Building plugin..."
./gradlew build

# Check if build succeeded
if [ $? -eq 0 ]; then
    echo "✅ Plugin built successfully!"
    echo "Plugin location: build/distributions/"
    ls -la build/distributions/
else
    echo "❌ Plugin build failed!"
    exit 1
fi