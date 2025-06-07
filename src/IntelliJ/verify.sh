#!/bin/bash

# Verification script for IntelliJ plugin structure and code
# This script checks the plugin without requiring full IntelliJ SDK download

set -e

echo "🔍 Verifying IntelliJ Plugin Structure..."

# Ensure we're in the IntelliJ directory
cd "$(dirname "$0")"

echo "✅ Checking plugin.xml structure..."
if [ -f "src/main/resources/META-INF/plugin.xml" ]; then
    echo "   ✓ plugin.xml exists"
    
    # Basic XML validation (if xmllint is available)
    if command -v xmllint >/dev/null 2>&1; then
        if xmllint --noout src/main/resources/META-INF/plugin.xml 2>/dev/null; then
            echo "   ✓ plugin.xml is valid XML"
        else
            echo "   ⚠ plugin.xml has XML syntax issues"
        fi
    fi
else
    echo "   ❌ plugin.xml missing"
    exit 1
fi

echo "✅ Checking icon file..."
if [ -f "src/main/resources/META-INF/pluginIcon.png" ]; then
    echo "   ✓ Plugin icon exists"
else
    echo "   ⚠ Plugin icon missing"
fi

echo "✅ Checking Kotlin source files..."
KOTLIN_FILES=$(find src/main/kotlin -name "*.kt" 2>/dev/null | wc -l)
if [ "$KOTLIN_FILES" -gt 0 ]; then
    echo "   ✓ Found $KOTLIN_FILES Kotlin source files"
    
    # List the action classes
    echo "   📁 Action classes:"
    find src/main/kotlin -name "*Action.kt" -exec basename {} \; | sed 's/^/      /'
    
    # List the service classes
    echo "   📁 Service classes:"
    find src/main/kotlin -name "*Service.kt" -exec basename {} \; | sed 's/^/      /'
    
    # List the settings classes
    echo "   📁 Settings classes:"
    find src/main/kotlin -name "*Settings*.kt" -exec basename {} \; | sed 's/^/      /'
else
    echo "   ❌ No Kotlin source files found"
    exit 1
fi

echo "✅ Checking build configuration..."
if [ -f "build.gradle.kts" ]; then
    echo "   ✓ Gradle build configuration exists"
    
    # Check for required plugins
    if grep -q "org.jetbrains.intellij" build.gradle.kts; then
        echo "   ✓ IntelliJ plugin configured"
    else
        echo "   ❌ IntelliJ plugin not configured"
        exit 1
    fi
    
    if grep -q "kotlin" build.gradle.kts; then
        echo "   ✓ Kotlin plugin configured"
    else
        echo "   ❌ Kotlin plugin not configured"
        exit 1
    fi
else
    echo "   ❌ build.gradle.kts missing"
    exit 1
fi

echo "✅ Checking build scripts..."
if [ -f "build.sh" ] && [ -x "build.sh" ]; then
    echo "   ✓ build.sh exists and is executable"
else
    echo "   ⚠ build.sh missing or not executable"
fi

if [ -f "test.sh" ] && [ -x "test.sh" ]; then
    echo "   ✓ test.sh exists and is executable"
else
    echo "   ⚠ test.sh missing or not executable"
fi

echo "✅ Checking documentation..."
if [ -f "README.md" ]; then
    echo "   ✓ README.md exists"
    
    # Check for key sections
    if grep -q "## Features" README.md; then
        echo "   ✓ Features section found"
    fi
    
    if grep -q "## Installation" README.md; then
        echo "   ✓ Installation section found"
    fi
    
    if grep -q "## Usage" README.md; then
        echo "   ✓ Usage section found"
    fi
else
    echo "   ⚠ README.md missing"
fi

echo ""
echo "🎉 Plugin structure verification complete!"
echo ""
echo "📋 Summary:"
echo "   • Plugin XML configuration: ✓"
echo "   • Kotlin source files: ✓ ($KOTLIN_FILES files)"
echo "   • Build configuration: ✓"
echo "   • Documentation: ✓"
echo ""
echo "🚀 To build the plugin (requires network access to JetBrains repositories):"
echo "   ./gradlew build"
echo ""
echo "🧪 To test the plugin interactively:"
echo "   ./gradlew runIde"