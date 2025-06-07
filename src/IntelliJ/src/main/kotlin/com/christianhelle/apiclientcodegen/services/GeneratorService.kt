package com.christianhelle.apiclientcodegen.services

import com.intellij.execution.configurations.GeneralCommandLine
import com.intellij.execution.process.CapturingProcessHandler
import com.intellij.execution.process.ProcessOutput
import com.intellij.openapi.diagnostic.Logger
import com.intellij.openapi.project.Project
import com.intellij.openapi.util.SystemInfo
import java.io.File

data class Generator(
    val command: String,
    val displayName: String,
    val requiresJava: Boolean = false
)

object GeneratorService {
    private val LOG = Logger.getInstance(GeneratorService::class.java)
    
    val csharpGenerators = listOf(
        Generator("nswag", "NSwag", false),
        Generator("refitter", "Refitter", false),
        Generator("openapi", "OpenAPI Generator", true),
        Generator("kiota", "Microsoft Kiota", false),
        Generator("swagger", "Swagger Codegen CLI", true),
        Generator("autorest", "AutoREST", false)
    )
    
    val typescriptGenerators = listOf(
        Generator("angular", "Angular", true),
        Generator("aurelia", "Aurelia", true),
        Generator("axios", "Axios", true),
        Generator("fetch", "Fetch", true),
        Generator("inversify", "Inversify", true),
        Generator("jquery", "jQuery", true),
        Generator("nestjs", "NestJS", true),
        Generator("node", "Node", true),
        Generator("reduxquery", "Redux Query", true),
        Generator("rxjs", "RxJS", true)
    )
    
    fun isRapicgenInstalled(): Boolean {
        return try {
            val command = if (SystemInfo.isWindows) {
                "cmd /c dotnet tool list --global | findstr rapicgen"
            } else {
                "dotnet tool list --global | grep rapicgen"
            }
            
            val commandLine = GeneralCommandLine.createFromCommand(command)
            val processHandler = CapturingProcessHandler(commandLine)
            val output = processHandler.runProcess(10000)
            
            output.exitCode == 0 && output.stdout.contains("rapicgen")
        } catch (e: Exception) {
            LOG.warn("Error checking rapicgen installation", e)
            false
        }
    }
    
    fun installRapicgen(): ProcessOutput {
        val commandLine = GeneralCommandLine("dotnet", "tool", "install", "--global", "rapicgen")
        val processHandler = CapturingProcessHandler(commandLine)
        return processHandler.runProcess(60000)
    }
    
    fun updateRapicgen(): ProcessOutput {
        val commandLine = GeneralCommandLine("dotnet", "tool", "update", "--global", "rapicgen")
        val processHandler = CapturingProcessHandler(commandLine)
        return processHandler.runProcess(60000)
    }
    
    fun executeRapicgen(
        project: Project,
        command: String,
        specificationFile: String,
        namespace: String,
        outputDirectory: String,
        isTypeScript: Boolean = false
    ): ProcessOutput {
        val workingDirectory = File(project.basePath ?: "")
        
        val commandLine = GeneralCommandLine().apply {
            exePath = "rapicgen"
            
            if (isTypeScript) {
                addParameter("typescript")
                addParameter(command)
            } else {
                addParameter(command)
            }
            
            addParameter(specificationFile)
            addParameter(namespace)
            
            if (outputDirectory.isNotEmpty()) {
                addParameter("--output")
                addParameter(outputDirectory)
            }
            
            setWorkDirectory(workingDirectory)
        }
        
        LOG.info("Executing command: ${commandLine.commandLineString}")
        
        val processHandler = CapturingProcessHandler(commandLine)
        return processHandler.runProcess(120000) // 2 minutes timeout
    }
    
    fun executeRapicgenRefitterSettings(
        project: Project,
        settingsFile: String
    ): ProcessOutput {
        val workingDirectory = File(project.basePath ?: "")
        
        val commandLine = GeneralCommandLine().apply {
            exePath = "rapicgen"
            addParameter("refitter")
            addParameter("--settings-file")
            addParameter(settingsFile)
            setWorkDirectory(workingDirectory)
        }
        
        LOG.info("Executing command: ${commandLine.commandLineString}")
        
        val processHandler = CapturingProcessHandler(commandLine)
        return processHandler.runProcess(120000) // 2 minutes timeout
    }
}
