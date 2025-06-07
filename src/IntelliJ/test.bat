@echo off
REM Test script for the IntelliJ plugin
REM This script runs tests and quality checks

echo Testing REST API Client Code Generator IntelliJ Plugin...

REM Set up Java environment
set JAVA_HOME=c:\projects\christianhelle\apiclientcodegen\java\jdk21
set PATH=%JAVA_HOME%\bin;%PATH%

REM Check if Java is available
java -version >nul 2>&1
if %errorlevel% neq 0 (
    echo Error: Java is not installed or not in PATH
    exit /b 1
)

echo.
echo 🧪 Running verification tasks...
gradlew.bat verifyPlugin

if %errorlevel% equ 0 (
    echo.
    echo ✅ Verification passed!
) else (
    echo.
    echo ❌ Verification failed!
    exit /b 1
)

echo.
echo 🔍 Running code quality checks...
gradlew.bat check -x test

if %errorlevel% equ 0 (
    echo.
    echo ✅ Code quality checks passed!
) else (
    echo.
    echo ⚠️  Code quality checks had issues
)

echo.
echo 📊 Test report location: build\reports\tests\test\index.html
echo 🔍 Check report location: build\reports\

echo.
echo ✨ Testing completed!
pause
