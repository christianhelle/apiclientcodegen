package com.christianhelle.apiclientcodegen.actions

import com.intellij.openapi.actionSystem.AnAction
import com.intellij.openapi.actionSystem.AnActionEvent
import com.intellij.openapi.actionSystem.CommonDataKeys
import com.intellij.openapi.vfs.VfsUtil
import java.nio.file.Paths
import com.intellij.openapi.ui.Messages

class GenerateCSharpClientAction : AnAction() {
    override fun update(e: AnActionEvent) {
        val file = e.getData(CommonDataKeys.VIRTUAL_FILE)
        e.presentation.isEnabledAndVisible = file?.isOpenApiSpec() == true
    }

    override fun actionPerformed(e: AnActionEvent) {
        val project = e.project ?: return
        val file = e.getData(CommonDataKeys.VIRTUAL_FILE) ?: return
        val rapicgen = findRapicgen() ?: run {
            showError(project, "rapicgen tool not found. Install with: dotnet tool install --global rapicgen")
            return
        }
    val generators = arrayOf("nswag", "refitter", "openapi", "kiota", "swagger", "autorest")
    val choice = Messages.showChooseDialog(project, "Select C# generator", "C# Generator", generators, generators[0], Messages.getQuestionIcon()) ?: return
    val namespace = prompt(project, "C# Namespace", "Enter namespace", "GeneratedCode") ?: return
    val outputFile = file.nameWithoutExtension + "-${choice}.cs"
    val cmd = listOf(rapicgen, "csharp", choice, file.path, namespace, outputFile)
        val log = StringBuilder()
        val code = runProcess(cmd, java.io.File(file.parent.path), { log.appendLine(it) }, { log.appendLine(it) })
        if (code == 0) {
            VfsUtil.findFile(Paths.get(file.parent.path, outputFile), true)?.refresh(true, false)
            showInfo(project, "Generated $outputFile")
        } else {
            showError(project, "Generation failed. See log:\n$log")
        }
    }
}
