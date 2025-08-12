#!/bin/bash

# Test script for IntelliJ plugin

echo "Testing IntelliJ Plugin structure..."

# Check plugin.xml exists
if [ -f "src/main/resources/META-INF/plugin.xml" ]; then
    echo "✓ plugin.xml found"
else
    echo "✗ plugin.xml not found"
    exit 1
fi

# Check Kotlin sources exist
if [ -d "src/main/kotlin" ]; then
    echo "✓ Kotlin source directory found"
    kotlin_files=$(find src/main/kotlin -name "*.kt" | wc -l)
    echo "  Found $kotlin_files Kotlin files"
else
    echo "✗ Kotlin source directory not found"
    exit 1
fi

# Check build.gradle.kts exists
if [ -f "build.gradle.kts" ]; then
    echo "✓ build.gradle.kts found"
else
    echo "✗ build.gradle.kts not found"
    exit 1
fi

# Check gradle wrapper exists
if [ -f "gradlew" ] && [ -f "gradle/wrapper/gradle-wrapper.jar" ]; then
    echo "✓ Gradle wrapper found"
else
    echo "✗ Gradle wrapper not found"
    exit 1
fi

# Check plugin icon
if [ -f "src/main/resources/META-INF/pluginIcon.png" ]; then
    echo "✓ Plugin icon found"
else
    echo "✗ Plugin icon not found"
fi

echo ""
echo "Plugin structure validation complete!"
echo ""
echo "Directory structure:"
find . -type f -name "*.kt" -o -name "*.xml" -o -name "*.kts" -o -name "*.png" | sort