package com.christianhelle.apiclientcodegen.utils

import com.intellij.openapi.project.Project
import com.intellij.openapi.vfs.VirtualFile
import com.christianhelle.apiclientcodegen.settings.RestApiClientCodeGeneratorSettings
import java.nio.file.Paths

object FileUtils {
    
    fun isOpenApiSpecificationFile(file: VirtualFile): Boolean {
        val extension = file.extension?.lowercase()
        return extension in listOf("json", "yaml", "yml")
    }
    
    fun isRefitterSettingsFile(file: VirtualFile): Boolean {
        return file.extension?.lowercase() == "refitter"
    }
    
    fun getNamespace(): String {
        return RestApiClientCodeGeneratorSettings.getInstance().namespace
    }
    
    fun getOutputDirectory(project: Project, specificationFile: String): String {
        val settings = RestApiClientCodeGeneratorSettings.getInstance()
        val configuredOutputDir = settings.outputDirectory
        
        return if (configuredOutputDir.isEmpty()) {
            // Use the directory of the specification file
            Paths.get(specificationFile).parent?.toString() ?: ""
        } else {
            // Use the configured output directory relative to project root
            val projectBasePath = project.basePath ?: ""
            Paths.get(projectBasePath, configuredOutputDir).toString()
        }
    }
    
    fun getTypeScriptOutputDirectory(project: Project, specificationFile: String): String {
        val settings = RestApiClientCodeGeneratorSettings.getInstance()
        val configuredOutputDir = settings.outputDirectory
        
        return if (configuredOutputDir.isEmpty()) {
            // Use the directory of the specification file
            Paths.get(specificationFile).parent?.toString() ?: ""
        } else {
            // Use the configured output directory relative to project root
            val projectBasePath = project.basePath ?: ""
            Paths.get(projectBasePath, configuredOutputDir).toString()
        }
    }
    
    fun getOutputFilePath(specificationFile: String, extension: String = ".cs"): String {
        val specPath = Paths.get(specificationFile)
        val fileName = specPath.fileName.toString()
        val nameWithoutExtension = fileName.substringBeforeLast('.')
        return specPath.parent.resolve("$nameWithoutExtension$extension").toString()
    }
    
    fun validateSpecificationFile(file: VirtualFile): Boolean {
        if (!file.exists() || file.isDirectory) {
            return false
        }
        
        return isOpenApiSpecificationFile(file)
    }
}
