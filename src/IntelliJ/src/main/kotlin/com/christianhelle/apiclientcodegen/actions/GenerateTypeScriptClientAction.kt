package com.christianhelle.apiclientcodegen.actions

import com.intellij.openapi.actionSystem.AnAction
import com.intellij.openapi.actionSystem.AnActionEvent
import com.intellij.openapi.actionSystem.CommonDataKeys
import com.intellij.openapi.vfs.LocalFileSystem
import java.nio.file.Paths
import com.intellij.openapi.ui.Messages

class GenerateTypeScriptClientAction : AnAction() {
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
    val generators = arrayOf("Angular", "Aurelia", "Axios", "Fetch", "Inversify", "JQuery", "NestJS", "Node", "ReduxQuery", "Rxjs")
    val choice = Messages.showChooseDialog(project, "Select TypeScript generator", "TypeScript Generator", generators, generators[0], Messages.getQuestionIcon()) ?: return
    val outputDir = prompt(project, "TypeScript Output", "Enter output directory", "typescript-${choice.toLowerCase()}-client") ?: return
    val cmd = listOf(rapicgen, "typescript", choice, file.path, outputDir)
        val log = StringBuilder()
        val code = runProcess(cmd, java.io.File(file.parent.path), { log.appendLine(it) }, { log.appendLine(it) })
        if (code == 0) {
            LocalFileSystem.getInstance().refreshAndFindFileByPath(Paths.get(file.parent.path, outputDir).toString())?.refresh(true, true)
            showInfo(project, "Generated TypeScript client in $outputDir")
        } else {
            showError(project, "Generation failed. See log:\n$log")
        }
    }
}
