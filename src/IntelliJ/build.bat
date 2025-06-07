@echo off
REM Build script for IntelliJ plugin (Windows)

echo Building REST API Client Code Generator IntelliJ Plugin...

REM Ensure we're in the IntelliJ directory
cd /d "%~dp0"

REM Clean previous builds
echo Cleaning previous builds...
gradlew.bat clean

REM Build the plugin
echo Building plugin...
gradlew.bat build

REM Check if build succeeded
if %errorlevel% equ 0 (
    echo ✅ Plugin built successfully!
    echo Plugin location: build\distributions\
    dir build\distributions\
) else (
    echo ❌ Plugin build failed!
    exit /b 1
)