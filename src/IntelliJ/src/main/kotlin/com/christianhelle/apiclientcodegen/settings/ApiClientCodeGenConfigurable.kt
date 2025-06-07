package com.christianhelle.apiclientcodegen.settings

import com.intellij.openapi.options.Configurable
import com.intellij.openapi.ui.DialogWrapper.CANCEL_EXIT_CODE
import com.intellij.openapi.ui.DialogWrapper.OK_EXIT_CODE
import com.intellij.ui.components.JBLabel
import com.intellij.ui.components.JBTextField
import com.intellij.util.ui.FormBuilder
import org.jetbrains.annotations.Nls
import javax.swing.JComponent
import javax.swing.JPanel

class ApiClientCodeGenConfigurable : Configurable {
    
    private var namespaceField: JBTextField? = null
    private var outputDirectoryField: JBTextField? = null
    private var panel: JPanel? = null
    
    @Nls(capitalization = Nls.Capitalization.Title)
    override fun getDisplayName(): String = "REST API Client Code Generator"
    
    override fun getPreferredFocusedComponent(): JComponent? = namespaceField
    
    override fun createComponent(): JComponent? {
        namespaceField = JBTextField()
        outputDirectoryField = JBTextField()
        
        panel = FormBuilder.createFormBuilder()
            .addLabeledComponent(
                JBLabel("Default namespace:"), 
                namespaceField!!, 
                1, 
                false
            )
            .addLabeledComponent(
                JBLabel("Output directory (relative to project root):"), 
                outputDirectoryField!!, 
                1, 
                false
            )
            .addComponentFillVertically(JPanel(), 0)
            .panel
            
        return panel
    }
    
    override fun isModified(): Boolean {
        val settings = ApiClientCodeGenSettings.getInstance()
        return namespaceField?.text != settings.state.namespace ||
               outputDirectoryField?.text != settings.state.outputDirectory
    }
    
    override fun apply() {
        val settings = ApiClientCodeGenSettings.getInstance()
        settings.state.namespace = namespaceField?.text ?: "GeneratedCode"
        settings.state.outputDirectory = outputDirectoryField?.text ?: ""
    }
    
    override fun reset() {
        val settings = ApiClientCodeGenSettings.getInstance()
        namespaceField?.text = settings.state.namespace
        outputDirectoryField?.text = settings.state.outputDirectory
    }
    
    override fun disposeUIResources() {
        namespaceField = null
        outputDirectoryField = null
        panel = null
    }
}