package com.christianhelle.apiclientcodegen.services

import com.intellij.execution.configurations.GeneralCommandLine
import com.intellij.execution.process.ProcessHandler
import com.intellij.execution.process.ProcessHandlerFactory
import com.intellij.execution.process.ProcessTerminatedListener
import com.intellij.execution.util.ExecUtil
import com.intellij.notification.NotificationGroupManager
import com.intellij.notification.NotificationType
import com.intellij.openapi.application.ApplicationManager
import com.intellij.openapi.diagnostic.thisLogger
import com.intellij.openapi.fileEditor.FileEditorManager
import com.intellij.openapi.progress.ProgressIndicator
import com.intellij.openapi.progress.ProgressManager
import com.intellij.openapi.progress.Task
import com.intellij.openapi.project.Project
import com.intellij.openapi.util.SystemInfo
import com.intellij.openapi.vfs.LocalFileSystem
import com.intellij.openapi.vfs.VirtualFile
import java.io.File
import java.util.concurrent.TimeUnit

class RapicgenService {
    
    companion object {
        private val LOG = thisLogger()
        
        fun getInstance(): RapicgenService {
            return ApplicationManager.getApplication().getService(RapicgenService::class.java)
        }
    }
    
    fun executeRapicgenCommand(
        project: Project,
        generator: String,
        specificationFile: String,
        namespace: String,
        outputFile: String,
        isTypeScript: Boolean = false,
        isRefitterSettings: Boolean = false
    ) {
        ProgressManager.getInstance().run(object : Task.Backgroundable(project, "Generating API Client Code", true) {
            override fun run(indicator: ProgressIndicator) {
                try {
                    indicator.text = "Checking rapicgen tool availability..."
                    
                    if (!ensureRapicgenInstalled(project, indicator)) {
                        return
                    }
                    
                    indicator.text = "Generating code with $generator..."
                    
                    val command = buildCommand(generator, specificationFile, namespace, outputFile, isTypeScript, isRefitterSettings)
                    LOG.info("Executing command: ${command.commandLineString}")
                    
                    val result = ExecUtil.execAndGetOutput(command, 300000) // 5 minutes timeout
                    
                    if (result.exitCode == 0) {
                        ApplicationManager.getApplication().invokeLater {
                            showSuccessNotification(project, generator, outputFile, isTypeScript)
                            openGeneratedFile(project, outputFile, isTypeScript)
                        }
                    } else {
                        ApplicationManager.getApplication().invokeLater {
                            showErrorNotification(project, generator, result.stderr)
                        }
                    }
                } catch (e: Exception) {
                    LOG.error("Error executing rapicgen command", e)
                    ApplicationManager.getApplication().invokeLater {
                        showErrorNotification(project, generator, e.message ?: "Unknown error")
                    }
                }
            }
        })
    }
    
    private fun buildCommand(
        generator: String, 
        specificationFile: String, 
        namespace: String, 
        outputFile: String,
        isTypeScript: Boolean,
        isRefitterSettings: Boolean
    ): GeneralCommandLine {
        val command = GeneralCommandLine()
        
        if (SystemInfo.isWindows) {
            command.exePath = "cmd"
            command.addParameter("/c")
            command.addParameter("rapicgen")
        } else {
            command.exePath = "rapicgen"
        }
        
        when {
            isRefitterSettings -> {
                command.addParameters("csharp", "refitter", "--settings-file", specificationFile)
            }
            isTypeScript -> {
                command.addParameters("typescript", generator, specificationFile, outputFile)
            }
            else -> {
                command.addParameters("csharp", generator, specificationFile, namespace, outputFile)
            }
        }
        
        return command
    }
    
    private fun ensureRapicgenInstalled(project: Project, indicator: ProgressIndicator): Boolean {
        try {
            // Check if rapicgen is available
            val checkCommand = GeneralCommandLine()
            if (SystemInfo.isWindows) {
                checkCommand.exePath = "cmd"
                checkCommand.addParameter("/c")
                checkCommand.addParameter("rapicgen")
            } else {
                checkCommand.exePath = "rapicgen"
            }
            checkCommand.addParameter("--help")
            
            val result = ExecUtil.execAndGetOutput(checkCommand, 30000)
            
            if (result.exitCode == 0) {
                return true
            }
            
            // If not available, try to install it
            indicator.text = "Installing rapicgen tool..."
            
            val installCommand = GeneralCommandLine()
            if (SystemInfo.isWindows) {
                installCommand.exePath = "cmd"
                installCommand.addParameter("/c")
                installCommand.addParameter("dotnet")
            } else {
                installCommand.exePath = "dotnet"
            }
            installCommand.addParameters("tool", "install", "--global", "rapicgen")
            
            val installResult = ExecUtil.execAndGetOutput(installCommand, 120000) // 2 minutes
            
            if (installResult.exitCode == 0) {
                ApplicationManager.getApplication().invokeLater {
                    showInfoNotification(project, "rapicgen tool installed successfully")
                }
                return true
            } else {
                ApplicationManager.getApplication().invokeLater {
                    showErrorNotification(project, "Installation", 
                        "Failed to install rapicgen tool. Please install .NET SDK and run 'dotnet tool install --global rapicgen' manually.")
                }
                return false
            }
        } catch (e: Exception) {
            LOG.error("Error checking/installing rapicgen", e)
            ApplicationManager.getApplication().invokeLater {
                showErrorNotification(project, "Installation", 
                    "Error checking rapicgen availability: ${e.message}")
            }
            return false
        }
    }
    
    private fun openGeneratedFile(project: Project, outputPath: String, isTypeScript: Boolean) {
        val file = if (isTypeScript) {
            // For TypeScript, try to find the main generated file in the directory
            val dir = File(outputPath)
            if (dir.isDirectory) {
                dir.listFiles()?.firstOrNull { it.name.endsWith(".ts") }
            } else {
                File(outputPath)
            }
        } else {
            File(outputPath)
        }
        
        file?.let { f ->
            if (f.exists()) {
                val virtualFile: VirtualFile? = LocalFileSystem.getInstance().refreshAndFindFileByIoFile(f)
                virtualFile?.let {
                    FileEditorManager.getInstance(project).openFile(it, true)
                }
            }
        }
    }
    
    private fun showSuccessNotification(project: Project, generator: String, outputPath: String, isTypeScript: Boolean) {
        val message = if (isTypeScript) {
            "TypeScript client generated successfully with $generator in $outputPath"
        } else {
            "C# client generated successfully with $generator: $outputPath"
        }
        
        NotificationGroupManager.getInstance()
            .getNotificationGroup("REST API Client Code Generator")
            .createNotification(message, NotificationType.INFORMATION)
            .notify(project)
    }
    
    private fun showErrorNotification(project: Project, generator: String, error: String) {
        val message = "Failed to generate code with $generator: $error"
        
        NotificationGroupManager.getInstance()
            .getNotificationGroup("REST API Client Code Generator")
            .createNotification(message, NotificationType.ERROR)
            .notify(project)
    }
    
    private fun showInfoNotification(project: Project, message: String) {
        NotificationGroupManager.getInstance()
            .getNotificationGroup("REST API Client Code Generator")
            .createNotification(message, NotificationType.INFORMATION)
            .notify(project)
    }
}