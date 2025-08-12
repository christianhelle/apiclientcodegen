package com.github.christianhelle.apiclientcodegen.actions

import com.intellij.openapi.project.Project
import com.intellij.openapi.vfs.VirtualFile

class CSharpNSwagAction : BaseCodeGeneratorAction() {
    override fun generateCode(project: Project, file: VirtualFile) {
        generateCSharpCode(project, file, "nswag")
    }
    
    override fun isFileSupported(file: VirtualFile): Boolean = isSwaggerOrOpenApiFile(file)
}

class CSharpRefitterAction : BaseCodeGeneratorAction() {
    override fun generateCode(project: Project, file: VirtualFile) {
        generateCSharpCode(project, file, "refitter")
    }
    
    override fun isFileSupported(file: VirtualFile): Boolean = isSwaggerOrOpenApiFile(file)
}

class CSharpOpenApiAction : BaseCodeGeneratorAction() {
    override fun generateCode(project: Project, file: VirtualFile) {
        generateCSharpCode(project, file, "openapi")
    }
    
    override fun isFileSupported(file: VirtualFile): Boolean = isSwaggerOrOpenApiFile(file)
}

class CSharpKiotaAction : BaseCodeGeneratorAction() {
    override fun generateCode(project: Project, file: VirtualFile) {
        generateCSharpCode(project, file, "kiota")
    }
    
    override fun isFileSupported(file: VirtualFile): Boolean = isSwaggerOrOpenApiFile(file)
}

class CSharpSwaggerAction : BaseCodeGeneratorAction() {
    override fun generateCode(project: Project, file: VirtualFile) {
        generateCSharpCode(project, file, "swagger")
    }
    
    override fun isFileSupported(file: VirtualFile): Boolean = isSwaggerOrOpenApiFile(file)
}

class CSharpAutoRestAction : BaseCodeGeneratorAction() {
    override fun generateCode(project: Project, file: VirtualFile) {
        generateCSharpCode(project, file, "autorest")
    }
    
    override fun isFileSupported(file: VirtualFile): Boolean = isSwaggerOrOpenApiFile(file)
}