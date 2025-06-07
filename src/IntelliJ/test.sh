#!/bin/bash

# Test script for IntelliJ plugin

set -e

echo "Testing REST API Client Code Generator IntelliJ Plugin..."

# Ensure we're in the IntelliJ directory
cd "$(dirname "$0")"

# Run verification
echo "Running verification..."
./gradlew verifyPlugin

# Run tests (if any exist)
echo "Running tests..."
./gradlew test

# Run the plugin in a sandbox environment for testing
echo "To test the plugin interactively, run:"
echo "./gradlew runIde"

echo "✅ Plugin verification completed!"