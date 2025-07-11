package com.christianhelle.apiclientcodegen

import com.intellij.openapi.actionSystem.AnAction
import com.intellij.openapi.actionSystem.AnActionEvent
import com.intellij.openapi.actionSystem.CommonDataKeys
import com.intellij.openapi.fileChooser.FileChooser
import com.intellij.openapi.fileChooser.FileChooserDescriptorFactory
import com.intellij.openapi.ui.Messages
import com.intellij.openapi.vfs.VirtualFile
import com.intellij.notification.NotificationGroupManager
import com.intellij.notification.NotificationType
import com.intellij.openapi.project.Project
import java.io.File

class GenerateRefitterSettingsAction : AnAction() {
    override fun actionPerformed(e: AnActionEvent) {
        val project = e.project ?: return
        val virtualFile = e.getData(CommonDataKeys.VIRTUAL_FILE)

        val settingsFile: VirtualFile? = if (virtualFile != null && isRefitterFile(virtualFile)) {
            virtualFile
        } else {
            promptForRefitterFile(project)
        }

        if (settingsFile == null) {
            notify(project, "No .refitter settings file selected.", NotificationType.WARNING)
            return
        }

        val settingsFilePath = settingsFile.path

        val command = "rapicgen csharp refitter --settings-file "$settingsFilePath""
        executeRapicgenCommand(project, command, "Refitter Settings")
    }

    private fun isRefitterFile(file: VirtualFile): Boolean {
        return file.extension?.toLowerCase() == "refitter"
    }

    private fun promptForRefitterFile(project: Project): VirtualFile? {
        val descriptor = FileChooserDescriptorFactory.createSingleFileDescriptor()
            .withFileFilter { file -> isRefitterFile(file) }
            .withTitle("Select a .refitter settings file")

        return FileChooser.chooseFile(descriptor, project, null)
    }

    private fun executeRapicgenCommand(project: Project, command: String, generatorName: String) {
        try {
            val process = Runtime.getRuntime().exec(command, null, File(project.basePath!!))
            val exitCode = process.waitFor()

            val output = process.inputStream.bufferedReader().readText()
            val error = process.errorStream.bufferedReader().readText()

            if (exitCode == 0) {
                notify(project, "$generatorName client code generated successfully!
$output", NotificationType.INFORMATION)
            } else {
                notify(project, "Failed to generate $generatorName client code.
Error: $error
Output: $output", NotificationType.ERROR)
            }
        } catch (e: Exception) {
            notify(project, "Error executing rapicgen: ${e.message}", NotificationType.ERROR)
        }
    }

    private fun notify(project: Project, content: String, type: NotificationType) {
        NotificationGroupManager.getInstance()
            .getNotificationGroup("ApiClientCodeGen.Notifications")
            .createNotification("API Client Code Generator", content, type)
            .notify(project)
    }
}
