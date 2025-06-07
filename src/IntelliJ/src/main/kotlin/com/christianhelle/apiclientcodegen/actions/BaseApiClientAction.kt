package com.christianhelle.apiclientcodegen.actions

import com.christianhelle.apiclientcodegen.services.RapicgenService
import com.christianhelle.apiclientcodegen.settings.ApiClientCodeGenSettings
import com.intellij.openapi.actionSystem.AnAction
import com.intellij.openapi.actionSystem.AnActionEvent
import com.intellij.openapi.actionSystem.CommonDataKeys
import com.intellij.openapi.diagnostic.thisLogger
import com.intellij.openapi.vfs.VirtualFile
import java.io.File

abstract class BaseApiClientAction(private val generator: String, private val isTypeScript: Boolean = false) : AnAction() {
    
    companion object {
        private val LOG = thisLogger()
    }
    
    override fun actionPerformed(e: AnActionEvent) {
        val project = e.project ?: return
        val file = e.getData(CommonDataKeys.VIRTUAL_FILE) ?: return
        
        if (!isValidFile(file)) {
            return
        }
        
        val settings = ApiClientCodeGenSettings.getInstance()
        val namespace = settings.state.namespace
        val outputDirectory = settings.state.outputDirectory
        
        val specificationFile = file.path
        val outputPath = generateOutputPath(specificationFile, outputDirectory, isTypeScript)
        
        val rapicgenService = RapicgenService.getInstance()
        rapicgenService.executeRapicgenCommand(
            project, 
            generator, 
            specificationFile, 
            namespace, 
            outputPath, 
            isTypeScript
        )
    }
    
    override fun update(e: AnActionEvent) {
        val file = e.getData(CommonDataKeys.VIRTUAL_FILE)
        val visible = file != null && isValidFile(file)
        e.presentation.isEnabledAndVisible = visible
    }
    
    private fun isValidFile(file: VirtualFile): Boolean {
        val extension = file.extension?.lowercase()
        return extension in listOf("json", "yaml", "yml")
    }
    
    private fun generateOutputPath(specificationFile: String, outputDirectory: String, isTypeScript: Boolean): String {
        val specFile = File(specificationFile)
        val baseDir = if (outputDirectory.isNotEmpty()) {
            File(specFile.parentFile, outputDirectory)
        } else {
            specFile.parentFile
        }
        
        return if (isTypeScript) {
            // For TypeScript, return a directory path
            File(baseDir, "${specFile.nameWithoutExtension}-typescript-$generator").absolutePath
        } else {
            // For C#, return a specific file path
            val filename = "${specFile.nameWithoutExtension}-$generator.cs"
            File(baseDir, filename).absolutePath
        }
    }
}