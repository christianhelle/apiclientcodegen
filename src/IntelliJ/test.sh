#!/bin/bash

# Test script for the IntelliJ plugin
# This script runs tests and quality checks

echo "Testing REST API Client Code Generator IntelliJ Plugin..."

# Check if Java is available
if ! command -v java &> /dev/null; then
    echo "Error: Java is not installed or not in PATH"
    exit 1
fi

# Make gradlew executable
chmod +x ./gradlew

echo "🧪 Running unit tests..."
./gradlew test

if [ $? -eq 0 ]; then
    echo "✅ Tests passed!"
else
    echo "❌ Tests failed!"
    exit 1
fi

echo "🔍 Running code quality checks..."
./gradlew check

if [ $? -eq 0 ]; then
    echo "✅ Code quality checks passed!"
else
    echo "⚠️  Code quality checks had issues"
fi

echo "📊 Test report location: build/reports/tests/test/index.html"
echo "🔍 Check report location: build/reports/

echo "✨ Testing completed!"
