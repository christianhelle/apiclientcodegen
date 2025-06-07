@echo off
REM Test script for IntelliJ plugin (Windows)

echo Testing REST API Client Code Generator IntelliJ Plugin...

REM Ensure we're in the IntelliJ directory
cd /d "%~dp0"

REM Run verification
echo Running verification...
gradlew.bat verifyPlugin

REM Run tests (if any exist)
echo Running tests...
gradlew.bat test

REM Instructions for interactive testing
echo To test the plugin interactively, run:
echo gradlew.bat runIde

echo ✅ Plugin verification completed!