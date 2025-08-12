package com.christianhelle.apiclientcodegen.actions

import com.intellij.openapi.actionSystem.AnAction
import com.intellij.openapi.actionSystem.AnActionEvent
import com.intellij.openapi.actionSystem.CommonDataKeys
import com.intellij.openapi.vfs.VfsUtil
import java.nio.file.Paths

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
        val namespace = prompt(project, "C# Namespace", "Enter namespace", "GeneratedCode") ?: return
        val outputFile = file.nameWithoutExtension + ".cs"
        val cmd = listOf(rapicgen, "csharp", "nswag", file.path, namespace, outputFile)
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
