@echo off
REM Build script for the IntelliJ plugin
REM This script builds the plugin and runs basic validation

echo Building REST API Client Code Generator IntelliJ Plugin...

REM Set up Java environment
set JAVA_HOME=c:\projects\christianhelle\apiclientcodegen\java\jdk-21.0.3+9
set PATH=%JAVA_HOME%\bin;%PATH%

REM Check if Java is available
java -version >nul 2>&1
if %errorlevel% neq 0 (
    echo Error: Java is not installed or not in PATH
    exit /b 1
)

echo Using Java:
java -version

echo.
echo Cleaning previous build...
gradlew.bat clean

echo.
echo Building plugin...
gradlew.bat buildPlugin

if %errorlevel% equ 0 (
    echo.
    echo ✅ Plugin built successfully!
    echo 📦 Plugin archive location: build\distributions\
    dir build\distributions\
) else (
    echo.
    echo ❌ Build failed!
    exit /b 1
)

echo.
echo 🔍 Running verification tasks...
gradlew.bat verifyPlugin

if %errorlevel% equ 0 (
    echo.
    echo ✅ Plugin verification passed!
) else (
    echo.
    echo ⚠️  Plugin verification had warnings/errors
)

echo.
echo ✨ Build process completed!
pause
