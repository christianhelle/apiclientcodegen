package com.github.christianhelle.apiclientcodegen.actions

import com.intellij.openapi.project.Project
import com.intellij.openapi.vfs.VirtualFile

class RefitterSettingsAction : BaseCodeGeneratorAction() {
    override fun generateCode(project: Project, file: VirtualFile) {
        generateRefitterOutput(project, file)
    }
    
    override fun isFileSupported(file: VirtualFile): Boolean = isRefitterFile(file)
}