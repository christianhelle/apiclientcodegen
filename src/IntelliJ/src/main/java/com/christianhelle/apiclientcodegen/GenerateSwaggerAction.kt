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

class GenerateSwaggerAction : AnAction() {
    override fun actionPerformed(e: AnActionEvent) {
        val project = e.project ?: return
        val virtualFile = e.getData(CommonDataKeys.VIRTUAL_FILE)

        val specificationFile: VirtualFile? = if (virtualFile != null && isSwaggerOrOpenApiFile(virtualFile)) {
            virtualFile
        } else {
            promptForSwaggerOrOpenApiFile(project)
        }

        if (specificationFile == null) {
            notify(project, "No Swagger/OpenAPI specification file selected.", NotificationType.WARNING)
            return
        }

        val specificationFilePath = specificationFile.path
        val namespace = Messages.showInputDialog(project, "Enter namespace:", "Namespace", Messages.getQuestionIcon(), "GeneratedCode", null)
        if (namespace == null) {
            return // User cancelled
        }

        val outputDirectory = Messages.showInputDialog(project, "Enter output directory (relative to project root):", "Output Directory", Messages.getQuestionIcon(), "", null)
        if (outputDirectory == null) {
            return // User cancelled
        }

        val outputFilePath = if (outputDirectory.isBlank()) {
            File(specificationFilePath).parent + File.separator + "SwaggerOutput.cs"
        } else {
            project.basePath + File.separator + outputDirectory + File.separator + "SwaggerOutput.cs"
        }

        // Ensure output directory exists
        val outputFileDir = File(outputFilePath).parentFile
        if (!outputFileDir.exists()) {
            outputFileDir.mkdirs()
        }

        val command = "rapicgen csharp swagger "$specificationFilePath" "$namespace" "$outputFilePath""
        executeRapicgenCommand(project, command, "Swagger Codegen CLI")
    }

    private fun isSwaggerOrOpenApiFile(file: VirtualFile): Boolean {
        val extension = file.extension?.toLowerCase()
        return extension == "json" || extension == "yaml" || extension == "yml"
    }

    private fun promptForSwaggerOrOpenApiFile(project: Project): VirtualFile? {
        val descriptor = FileChooserDescriptorFactory.createSingleFileDescriptor()
            .withFileFilter { file -> isSwaggerOrOpenApiFile(file) }
            .withTitle("Select a Swagger/OpenAPI specification file")

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
