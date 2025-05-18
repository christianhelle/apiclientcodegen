package com.christianhelle.apiclientcodegen

import com.intellij.openapi.actionSystem.AnAction
import com.intellij.openapi.actionSystem.AnActionEvent
import com.intellij.openapi.actionSystem.CommonDataKeys

class RefitterGeneratorAction : AnAction("Refitter") {
    override fun actionPerformed(e: AnActionEvent) {
        val file = e.getData(CommonDataKeys.VIRTUAL_FILE) ?: return
        val filePath = file.path
        val outputFile = file.parent.path + "/RefitterOutput.cs"
        val namespace = "GeneratedCode"
        val command = "rapicgen csharp refitter \"$filePath\" $namespace \"$outputFile\""
        runRapicgen(command, e)
    }
}