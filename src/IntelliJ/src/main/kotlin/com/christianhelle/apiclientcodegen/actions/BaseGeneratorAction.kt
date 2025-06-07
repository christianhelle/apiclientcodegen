package com.christianhelle.apiclientcodegen.actions

import com.intellij.notification.NotificationGroupManager
import com.intellij.notification.NotificationType
import com.intellij.openapi.actionSystem.ActionUpdateThread
import com.intellij.openapi.actionSystem.AnAction
import com.intellij.openapi.actionSystem.AnActionEvent
import com.intellij.openapi.actionSystem.CommonDataKeys
import com.intellij.openapi.application.ApplicationManager
import com.intellij.openapi.diagnostic.Logger
import com.intellij.openapi.fileEditor.FileEditorManager
import com.intellij.openapi.progress.ProgressIndicator
import com.intellij.openapi.progress.ProgressManager
import com.intellij.openapi.progress.Task
import com.intellij.openapi.project.Project
import com.intellij.openapi.vfs.LocalFileSystem
import com.intellij.openapi.vfs.VirtualFile
import com.christianhelle.apiclientcodegen.services.GeneratorService
import com.christianhelle.apiclientcodegen.utils.FileUtils
import com.christianhelle.apiclientcodegen.utils.SystemUtils

abstract class BaseGeneratorAction : AnAction() {
    
    protected abstract val generatorCommand: String
    protected abstract val generatorDisplayName: String
    protected open val requiresJava: Boolean = false
    protected open val isTypeScript: Boolean = false
    
    companion object {
        private val LOG = Logger.getInstance(BaseGeneratorAction::class.java)
        private val NOTIFICATION_GROUP = NotificationGroupManager.getInstance()
            .getNotificationGroup("REST API Client Code Generator")
    }
    
    override fun getActionUpdateThread(): ActionUpdateThread = ActionUpdateThread.BGT
    
    override fun update(e: AnActionEvent) {
        val virtualFile = e.getData(CommonDataKeys.VIRTUAL_FILE)
        val project = e.project
        
        e.presentation.isEnabledAndVisible = project != null && 
                virtualFile != null && 
                FileUtils.isOpenApiSpecificationFile(virtualFile)
    }
    
    override fun actionPerformed(e: AnActionEvent) {
        val project = e.project ?: return
        val virtualFile = e.getData(CommonDataKeys.VIRTUAL_FILE) ?: return
        
        if (!FileUtils.validateSpecificationFile(virtualFile)) {
            showErrorNotification(project, "Invalid specification file", 
                "The selected file is not a valid OpenAPI/Swagger specification file.")
            return
        }
        
        // Validate system dependencies
        val validationResult = SystemUtils.validateDependencies(requiresJava)
        if (!validationResult.isValid) {
            showErrorNotification(project, "Missing Dependencies", 
                "The following dependencies are required: ${validationResult.missingDependencies.joinToString(", ")}")
            return
        }
        
        // Check if rapicgen is installed
        if (!GeneratorService.isRapicgenInstalled()) {
            installRapicgenAndGenerate(project, virtualFile)
        } else {
            generateCode(project, virtualFile)
        }
    }
    
    private fun installRapicgenAndGenerate(project: Project, virtualFile: VirtualFile) {
        ProgressManager.getInstance().run(object : Task.Backgroundable(project, "Installing rapicgen tool...", true) {
            override fun run(indicator: ProgressIndicator) {
                indicator.text = "Installing rapicgen .NET tool..."
                indicator.isIndeterminate = true
                
                val installResult = GeneratorService.installRapicgen()
                
                ApplicationManager.getApplication().invokeLater {
                    if (installResult.exitCode == 0) {
                        showInfoNotification(project, "Installation Complete", 
                            "rapicgen tool installed successfully. Generating code...")
                        generateCode(project, virtualFile)
                    } else {
                        showErrorNotification(project, "Installation Failed", 
                            "Failed to install rapicgen tool: ${installResult.stderr}")
                    }
                }
            }
        })
    }
    
    private fun generateCode(project: Project, virtualFile: VirtualFile) {
        val specificationFile = virtualFile.path
        val namespace = FileUtils.getNamespace()
        val outputDirectory = if (isTypeScript) {
            FileUtils.getTypeScriptOutputDirectory(project, specificationFile)
        } else {
            FileUtils.getOutputDirectory(project, specificationFile)
        }
        
        ProgressManager.getInstance().run(object : Task.Backgroundable(project, "Generating $generatorDisplayName code...", true) {
            override fun run(indicator: ProgressIndicator) {
                indicator.text = "Generating code using $generatorDisplayName..."
                indicator.isIndeterminate = true
                
                try {
                    val result = GeneratorService.executeRapicgen(
                        project, generatorCommand, specificationFile, namespace, outputDirectory, isTypeScript
                    )
                    
                    ApplicationManager.getApplication().invokeLater {
                        if (result.exitCode == 0) {
                            showSuccessNotification(project, "Code Generation Complete", 
                                "Code generated successfully using $generatorDisplayName")
                            
                            // Try to open the generated file
                            openGeneratedFile(project, specificationFile, outputDirectory)
                        } else {
                            LOG.warn("Code generation failed with exit code ${result.exitCode}")
                            LOG.warn("STDOUT: ${result.stdout}")
                            LOG.warn("STDERR: ${result.stderr}")
                            
                            showErrorNotification(project, "Code Generation Failed", 
                                "Failed to generate code using $generatorDisplayName: ${result.stderr}")
                        }
                    }
                } catch (e: Exception) {
                    LOG.error("Error during code generation", e)
                    ApplicationManager.getApplication().invokeLater {
                        showErrorNotification(project, "Code Generation Error", 
                            "An error occurred during code generation: ${e.message}")
                    }
                }
            }
        })
    }
    
    private fun openGeneratedFile(project: Project, specificationFile: String, outputDirectory: String) {
        try {
            val extension = if (isTypeScript) ".ts" else ".cs"
            val outputFile = FileUtils.getOutputFilePath(specificationFile, extension)
            val virtualFile = LocalFileSystem.getInstance().findFileByPath(outputFile)
            
            if (virtualFile != null) {
                FileEditorManager.getInstance(project).openFile(virtualFile, true)
            }
        } catch (e: Exception) {
            LOG.warn("Could not open generated file", e)
        }
    }
    
    protected fun showSuccessNotification(project: Project, title: String, content: String) {
        NOTIFICATION_GROUP.createNotification(title, content, NotificationType.INFORMATION)
            .notify(project)
    }
    
    protected fun showInfoNotification(project: Project, title: String, content: String) {
        NOTIFICATION_GROUP.createNotification(title, content, NotificationType.INFORMATION)
            .notify(project)
    }
    
    protected fun showErrorNotification(project: Project, title: String, content: String) {
        NOTIFICATION_GROUP.createNotification(title, content, NotificationType.ERROR)
            .notify(project)
    }
}
