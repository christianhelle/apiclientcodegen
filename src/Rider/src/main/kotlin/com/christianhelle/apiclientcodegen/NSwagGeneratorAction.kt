package com.christianhelle.apiclientcodegen

import com.intellij.openapi.actionSystem.AnAction
import com.intellij.openapi.actionSystem.AnActionEvent
import com.intellij.openapi.actionSystem.CommonDataKeys
import com.intellij.openapi.ui.Messages
import java.io.File

class NSwagGeneratorAction : AnAction("NSwag") {
    override fun actionPerformed(e: AnActionEvent) {
        val file = e.getData(CommonDataKeys.VIRTUAL_FILE) ?: return
        val filePath = file.path
        val outputFile = file.parent.path + "/NSwagOutput.cs"
        val namespace = "GeneratedCode"
        val command = "rapicgen csharp nswag \"$filePath\" $namespace \"$outputFile\""
        runRapicgen(command, e)
    }
}

fun runRapicgen(command: String, e: AnActionEvent) {
    try {
        val process = ProcessBuilder("/bin/sh", "-c", command)
            .redirectErrorStream(true)
            .start()
        val output = process.inputStream.bufferedReader().readText()
        process.waitFor()
        Messages.showInfoMessage(e.project, output, "REST API Client Generator")
    } catch (ex: Exception) {
        Messages.showErrorDialog(e.project, ex.message ?: "Unknown error", "REST API Client Generator Error")
    }
}