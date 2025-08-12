package com.github.christianhelle.apiclientcodegen.services

import com.github.christianhelle.apiclientcodegen.settings.SettingsState
import com.intellij.notification.NotificationGroupManager
import com.intellij.notification.NotificationType
import com.intellij.openapi.project.Project
import com.intellij.openapi.vfs.VirtualFile
import java.io.File
import java.nio.file.Path
import java.nio.file.Paths

class CodeGeneratorService {
    
    fun generateCSharpCode(project: Project, specFile: VirtualFile, generator: String) {
        val settings = SettingsState.getInstance()
        val namespace = if (settings.namespace.isBlank()) "GeneratedCode" else settings.namespace
        val outputFile = getOutputFilePath(specFile, generator, settings.outputDirectory)
        
        // Try global rapicgen tool first, then fall back to project-relative CLI
        val command = try {
            // Check if rapicgen is available globally
            val globalCheck = ProcessBuilder("rapicgen", "--help").start()
            if (globalCheck.waitFor() == 0) {
                listOf("rapicgen", "csharp", generator, specFile.path, namespace, outputFile)
            } else {
                throw Exception("Global rapicgen not available")
            }
        } catch (e: Exception) {
            // Fall back to project CLI
            listOf(
                "dotnet", "run", "--project", 
                getCliProjectPath(),
                "--",
                "csharp", generator, 
                specFile.path, 
                namespace, 
                outputFile
            )
        }
        
        executeCommand(project, command, "C# $generator", outputFile)
    }
    
    fun generateTypeScriptCode(project: Project, specFile: VirtualFile, generator: String) {
        val settings = SettingsState.getInstance()
        val outputDir = getTypeScriptOutputDirectory(specFile, settings.outputDirectory)
        
        // Try global rapicgen tool first, then fall back to project-relative CLI
        val command = try {
            // Check if rapicgen is available globally
            val globalCheck = ProcessBuilder("rapicgen", "--help").start()
            if (globalCheck.waitFor() == 0) {
                listOf("rapicgen", "typescript", generator, specFile.path, outputDir)
            } else {
                throw Exception("Global rapicgen not available")
            }
        } catch (e: Exception) {
            // Fall back to project CLI
            listOf(
                "dotnet", "run", "--project", 
                getCliProjectPath(),
                "--",
                "typescript", generator, 
                specFile.path, 
                outputDir
            )
        }
        
        executeCommand(project, command, "TypeScript $generator", outputDir)
    }
    
    fun generateRefitterOutput(project: Project, refitterFile: VirtualFile) {
        // Try global rapicgen tool first, then fall back to project-relative CLI
        val command = try {
            // Check if rapicgen is available globally
            val globalCheck = ProcessBuilder("rapicgen", "--help").start()
            if (globalCheck.waitFor() == 0) {
                listOf("rapicgen", "refitter", refitterFile.path)
            } else {
                throw Exception("Global rapicgen not available")
            }
        } catch (e: Exception) {
            // Fall back to project CLI
            listOf(
                "dotnet", "run", "--project", 
                getCliProjectPath(),
                "--",
                "refitter", 
                refitterFile.path
            )
        }
        
        executeCommand(project, command, "Refitter", refitterFile.parent.path)
    }
    
    private fun getCliProjectPath(): String {
        // Find the CLI project path relative to current project
        val currentPath = System.getProperty("user.dir")
        return findCliProject(currentPath) ?: throw IllegalStateException("Could not find CLI project. Please ensure rapicgen .NET tool is installed globally or the CLI project is accessible.")
    }
    
    private fun findCliProject(startPath: String): String? {
        // Try to find in the repository structure
        val cliPath = Paths.get(startPath, "src", "CLI", "ApiClientCodeGen.CLI", "ApiClientCodeGen.CLI.csproj")
        if (cliPath.toFile().exists()) {
            return cliPath.toString()
        }
        
        // Try to find relative to the project root
        val workspaceRoot = System.getenv("GITHUB_WORKSPACE") ?: System.getProperty("user.dir")
        val workspaceCli = Paths.get(workspaceRoot, "src", "CLI", "ApiClientCodeGen.CLI", "ApiClientCodeGen.CLI.csproj")
        if (workspaceCli.toFile().exists()) {
            return workspaceCli.toString()
        }
        
        // Try different relative paths that might be common
        val parentPaths = listOf(
            Paths.get(startPath, "..", "..", "src", "CLI", "ApiClientCodeGen.CLI", "ApiClientCodeGen.CLI.csproj"),
            Paths.get(startPath, "..", "..", "..", "src", "CLI", "ApiClientCodeGen.CLI", "ApiClientCodeGen.CLI.csproj"),
            Paths.get(startPath, "..", "..", "..", "..", "src", "CLI", "ApiClientCodeGen.CLI", "ApiClientCodeGen.CLI.csproj")
        )
        
        for (path in parentPaths) {
            if (path.toFile().exists()) {
                return path.toString()
            }
        }
        
        return null
    }
    
    private fun getOutputFilePath(specFile: VirtualFile, generator: String, outputDirectory: String): String {
        val dir = if (outputDirectory.isBlank()) {
            specFile.parent.path
        } else {
            if (File(outputDirectory).isAbsolute) {
                outputDirectory
            } else {
                Paths.get(specFile.parent.path, outputDirectory).toString()
            }
        }
        
        val extension = when (generator) {
            "nswag", "refitter", "openapi", "kiota", "swagger", "autorest" -> "cs"
            else -> "cs"
        }
        
        val filename = "${specFile.nameWithoutExtension}-$generator.$extension"
        return Paths.get(dir, filename).toString()
    }
    
    private fun getTypeScriptOutputDirectory(specFile: VirtualFile, outputDirectory: String): String {
        return if (outputDirectory.isBlank()) {
            specFile.parent.path
        } else {
            if (File(outputDirectory).isAbsolute) {
                outputDirectory
            } else {
                Paths.get(specFile.parent.path, outputDirectory).toString()
            }
        }
    }
    
    private fun executeCommand(project: Project, command: List<String>, generatorName: String, outputPath: String) {
        try {
            val processBuilder = ProcessBuilder(command)
            processBuilder.redirectErrorStream(true)
            
            val process = processBuilder.start()
            val output = process.inputStream.bufferedReader().readText()
            val exitCode = process.waitFor()
            
            if (exitCode == 0) {
                showNotification(
                    project,
                    "Code Generation Successful",
                    "$generatorName code generated successfully at: $outputPath",
                    NotificationType.INFORMATION
                )
            } else {
                showNotification(
                    project,
                    "Code Generation Failed",
                    "$generatorName code generation failed: $output",
                    NotificationType.ERROR
                )
            }
        } catch (e: Exception) {
            showNotification(
                project,
                "Code Generation Error",
                "Error executing $generatorName: ${e.message}",
                NotificationType.ERROR
            )
        }
    }
    
    private fun showNotification(project: Project, title: String, content: String, type: NotificationType) {
        NotificationGroupManager.getInstance()
            .getNotificationGroup("REST API Client Code Generator")
            .createNotification(title, content, type)
            .notify(project)
    }
}