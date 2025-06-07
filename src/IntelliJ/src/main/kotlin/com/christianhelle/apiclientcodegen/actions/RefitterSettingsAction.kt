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
import com.intellij.openapi.vfs.VirtualFileManager
import com.christianhelle.apiclientcodegen.services.GeneratorService
import com.christianhelle.apiclientcodegen.utils.FileUtils
import com.christianhelle.apiclientcodegen.utils.SystemUtils

class RefitterSettingsAction : AnAction() {
    
    companion object {
        private val LOG = Logger.getInstance(RefitterSettingsAction::class.java)
        private val NOTIFICATION_GROUP = NotificationGroupManager.getInstance()
            .getNotificationGroup("REST API Client Code Generator")
    }
    
    override fun getActionUpdateThread(): ActionUpdateThread = ActionUpdateThread.BGT
    
    override fun update(e: AnActionEvent) {
        val virtualFile = e.getData(CommonDataKeys.VIRTUAL_FILE)
        val project = e.project
        
        e.presentation.isEnabledAndVisible = project != null && 
                virtualFile != null && 
                FileUtils.isRefitterSettingsFile(virtualFile)
    }
    
    override fun actionPerformed(e: AnActionEvent) {
        val project = e.project ?: return
        val virtualFile = e.getData(CommonDataKeys.VIRTUAL_FILE) ?: return
        
        if (!FileUtils.isRefitterSettingsFile(virtualFile)) {
            showErrorNotification(project, "Invalid settings file", 
                "The selected file is not a valid Refitter settings file.")
            return
        }
        
        // Validate system dependencies
        val validationResult = SystemUtils.validateDependencies(false)
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
        val settingsFile = virtualFile.path
        
        ProgressManager.getInstance().run(object : Task.Backgroundable(project, "Generating Refitter code...", true) {
            override fun run(indicator: ProgressIndicator) {
                indicator.text = "Generating code using Refitter settings..."
                indicator.isIndeterminate = true
                
                try {
                    val result = GeneratorService.executeRapicgenRefitterSettings(project, settingsFile)
                    
                    ApplicationManager.getApplication().invokeLater {
                        if (result.exitCode == 0) {
                            showSuccessNotification(project, "Code Generation Complete", 
                                "Code generated successfully using Refitter settings")
                              // Try to refresh the project view to show generated files
                            VirtualFileManager.getInstance().asyncRefresh(null)
                        } else {
                            LOG.warn("Code generation failed with exit code ${result.exitCode}")
                            LOG.warn("STDOUT: ${result.stdout}")
                            LOG.warn("STDERR: ${result.stderr}")
                            
                            showErrorNotification(project, "Code Generation Failed", 
                                "Failed to generate code using Refitter settings: ${result.stderr}")
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
    
    private fun showSuccessNotification(project: Project, title: String, content: String) {
        NOTIFICATION_GROUP.createNotification(title, content, NotificationType.INFORMATION)
            .notify(project)
    }
    
    private fun showInfoNotification(project: Project, title: String, content: String) {
        NOTIFICATION_GROUP.createNotification(title, content, NotificationType.INFORMATION)
            .notify(project)
    }
    
    private fun showErrorNotification(project: Project, title: String, content: String) {
        NOTIFICATION_GROUP.createNotification(title, content, NotificationType.ERROR)
            .notify(project)
    }
}
