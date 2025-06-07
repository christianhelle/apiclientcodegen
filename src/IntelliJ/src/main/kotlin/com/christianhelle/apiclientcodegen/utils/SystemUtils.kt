package com.christianhelle.apiclientcodegen.utils

import com.intellij.execution.configurations.GeneralCommandLine
import com.intellij.execution.process.CapturingProcessHandler
import com.intellij.openapi.diagnostic.Logger
import com.intellij.openapi.util.SystemInfo

object SystemUtils {
    private val LOG = Logger.getInstance(SystemUtils::class.java)
    
    fun isDotNetInstalled(): Boolean {
        return try {
            val commandLine = GeneralCommandLine("dotnet", "--version")
            val processHandler = CapturingProcessHandler(commandLine)
            val output = processHandler.runProcess(10000)
            
            output.exitCode == 0
        } catch (e: Exception) {
            LOG.warn("Error checking .NET installation", e)
            false
        }
    }
    
    fun isJavaInstalled(): Boolean {
        return try {
            val commandLine = GeneralCommandLine("java", "-version")
            val processHandler = CapturingProcessHandler(commandLine)
            val output = processHandler.runProcess(10000)
            
            output.exitCode == 0
        } catch (e: Exception) {
            LOG.warn("Error checking Java installation", e)
            false
        }
    }
    
    fun isNpmInstalled(): Boolean {
        return try {
            val commandLine = GeneralCommandLine("npm", "--version")
            val processHandler = CapturingProcessHandler(commandLine)
            val output = processHandler.runProcess(10000)
            
            output.exitCode == 0
        } catch (e: Exception) {
            LOG.warn("Error checking NPM installation", e)
            false
        }
    }
    
    fun validateDependencies(requiresJava: Boolean = false): ValidationResult {
        val missingDependencies = mutableListOf<String>()
        
        if (!isDotNetInstalled()) {
            missingDependencies.add(".NET SDK 6.0 or higher")
        }
        
        if (requiresJava && !isJavaInstalled()) {
            missingDependencies.add("Java Runtime Environment")
        }
        
        return ValidationResult(
            isValid = missingDependencies.isEmpty(),
            missingDependencies = missingDependencies
        )
    }
    
    data class ValidationResult(
        val isValid: Boolean,
        val missingDependencies: List<String>
    )
}
