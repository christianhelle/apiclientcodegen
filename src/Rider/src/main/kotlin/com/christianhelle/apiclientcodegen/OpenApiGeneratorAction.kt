package com.christianhelle.apiclientcodegen

import com.intellij.openapi.actionSystem.AnAction
import com.intellij.openapi.actionSystem.AnActionEvent
import com.intellij.openapi.actionSystem.CommonDataKeys

class OpenApiGeneratorAction : AnAction("OpenAPI Generator") {
    override fun actionPerformed(e: AnActionEvent) {
        val file = e.getData(CommonDataKeys.VIRTUAL_FILE) ?: return
        val filePath = file.path
        val outputFile = file.parent.path + "/OpenApiOutput.cs"
        val namespace = "GeneratedCode"
        val command = "rapicgen csharp openapi \"$filePath\" $namespace \"$outputFile\""
        runRapicgen(command, e)
    }
}