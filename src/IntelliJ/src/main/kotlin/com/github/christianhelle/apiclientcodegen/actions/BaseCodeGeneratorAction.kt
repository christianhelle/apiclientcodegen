package com.github.christianhelle.apiclientcodegen.actions

import com.github.christianhelle.apiclientcodegen.services.CodeGeneratorService
import com.intellij.openapi.actionSystem.AnAction
import com.intellij.openapi.actionSystem.AnActionEvent
import com.intellij.openapi.actionSystem.CommonDataKeys
import com.intellij.openapi.project.Project
import com.intellij.openapi.vfs.VirtualFile

abstract class BaseCodeGeneratorAction : AnAction() {
    
    private val codeGeneratorService = CodeGeneratorService()
    
    override fun actionPerformed(e: AnActionEvent) {
        val project = e.project ?: return
        val file = e.getData(CommonDataKeys.VIRTUAL_FILE) ?: return
        
        generateCode(project, file)
    }
    
    override fun update(e: AnActionEvent) {
        val file = e.getData(CommonDataKeys.VIRTUAL_FILE)
        e.presentation.isEnabledAndVisible = file != null && isFileSupported(file)
    }
    
    protected abstract fun generateCode(project: Project, file: VirtualFile)
    
    protected abstract fun isFileSupported(file: VirtualFile): Boolean
    
    protected fun isSwaggerOrOpenApiFile(file: VirtualFile): Boolean {
        val extension = file.extension?.lowercase()
        return extension in listOf("json", "yaml", "yml")
    }
    
    protected fun isRefitterFile(file: VirtualFile): Boolean {
        return file.extension?.lowercase() == "refitter"
    }
    
    protected fun generateCSharpCode(project: Project, file: VirtualFile, generator: String) {
        codeGeneratorService.generateCSharpCode(project, file, generator)
    }
    
    protected fun generateTypeScriptCode(project: Project, file: VirtualFile, generator: String) {
        codeGeneratorService.generateTypeScriptCode(project, file, generator)
    }
    
    protected fun generateRefitterOutput(project: Project, file: VirtualFile) {
        codeGeneratorService.generateRefitterOutput(project, file)
    }
}