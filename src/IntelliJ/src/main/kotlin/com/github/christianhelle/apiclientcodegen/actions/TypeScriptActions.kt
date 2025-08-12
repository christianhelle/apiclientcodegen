package com.github.christianhelle.apiclientcodegen.actions

import com.intellij.openapi.project.Project
import com.intellij.openapi.vfs.VirtualFile

class TypeScriptAngularAction : BaseCodeGeneratorAction() {
    override fun generateCode(project: Project, file: VirtualFile) {
        generateTypeScriptCode(project, file, "angular")
    }
    
    override fun isFileSupported(file: VirtualFile): Boolean = isSwaggerOrOpenApiFile(file)
}

class TypeScriptAureliaAction : BaseCodeGeneratorAction() {
    override fun generateCode(project: Project, file: VirtualFile) {
        generateTypeScriptCode(project, file, "aurelia")
    }
    
    override fun isFileSupported(file: VirtualFile): Boolean = isSwaggerOrOpenApiFile(file)
}

class TypeScriptAxiosAction : BaseCodeGeneratorAction() {
    override fun generateCode(project: Project, file: VirtualFile) {
        generateTypeScriptCode(project, file, "axios")
    }
    
    override fun isFileSupported(file: VirtualFile): Boolean = isSwaggerOrOpenApiFile(file)
}

class TypeScriptFetchAction : BaseCodeGeneratorAction() {
    override fun generateCode(project: Project, file: VirtualFile) {
        generateTypeScriptCode(project, file, "fetch")
    }
    
    override fun isFileSupported(file: VirtualFile): Boolean = isSwaggerOrOpenApiFile(file)
}

class TypeScriptInversifyAction : BaseCodeGeneratorAction() {
    override fun generateCode(project: Project, file: VirtualFile) {
        generateTypeScriptCode(project, file, "inversify")
    }
    
    override fun isFileSupported(file: VirtualFile): Boolean = isSwaggerOrOpenApiFile(file)
}

class TypeScriptJQueryAction : BaseCodeGeneratorAction() {
    override fun generateCode(project: Project, file: VirtualFile) {
        generateTypeScriptCode(project, file, "jquery")
    }
    
    override fun isFileSupported(file: VirtualFile): Boolean = isSwaggerOrOpenApiFile(file)
}

class TypeScriptNestJSAction : BaseCodeGeneratorAction() {
    override fun generateCode(project: Project, file: VirtualFile) {
        generateTypeScriptCode(project, file, "nestjs")
    }
    
    override fun isFileSupported(file: VirtualFile): Boolean = isSwaggerOrOpenApiFile(file)
}

class TypeScriptNodeAction : BaseCodeGeneratorAction() {
    override fun generateCode(project: Project, file: VirtualFile) {
        generateTypeScriptCode(project, file, "node")
    }
    
    override fun isFileSupported(file: VirtualFile): Boolean = isSwaggerOrOpenApiFile(file)
}

class TypeScriptReduxQueryAction : BaseCodeGeneratorAction() {
    override fun generateCode(project: Project, file: VirtualFile) {
        generateTypeScriptCode(project, file, "reduxquery")
    }
    
    override fun isFileSupported(file: VirtualFile): Boolean = isSwaggerOrOpenApiFile(file)
}

class TypeScriptRxJSAction : BaseCodeGeneratorAction() {
    override fun generateCode(project: Project, file: VirtualFile) {
        generateTypeScriptCode(project, file, "rxjs")
    }
    
    override fun isFileSupported(file: VirtualFile): Boolean = isSwaggerOrOpenApiFile(file)
}