package com.christianhelle.apiclientcodegen.settings

import com.intellij.openapi.options.Configurable
import com.intellij.openapi.ui.DialogPanel
import com.intellij.ui.dsl.builder.*
import javax.swing.JComponent

class RestApiClientCodeGeneratorConfigurable : Configurable {
    
    private var panel: DialogPanel? = null
    private val settings = RestApiClientCodeGeneratorSettings.getInstance()
    
    override fun getDisplayName(): String = "REST API Client Code Generator"
    
    override fun createComponent(): JComponent {
        panel = panel {
            group("General Settings") {
                row("Default Namespace:") {
                    textField()
                        .bindText(settings::namespace)
                        .comment("Default namespace to use in the generated code")
                        .columns(COLUMNS_MEDIUM)
                }
                
                row("Output Directory:") {
                    textField()
                        .bindText(settings::outputDirectory)
                        .comment("Output directory relative to the project root. If empty, the code will be generated in the same directory as the specification file")
                        .columns(COLUMNS_MEDIUM)
                }
            }
            
            group("Requirements") {
                row {
                    text("The following tools are required for code generation:")
                }
                row {
                    text("• .NET SDK 6.0 or higher")
                }
                row {
                    text("• Java Runtime Environment (for OpenAPI Generator and Swagger Codegen CLI)")
                }
                row {
                    text("• NPM (for AutoREST and NSwag)")
                }
                row {
                    text("• rapicgen .NET tool (will be installed automatically if not present)")
                }
            }
        }
        
        return panel!!
    }
    
    override fun isModified(): Boolean {
        return panel?.isModified() ?: false
    }
    
    override fun apply() {
        panel?.apply()
    }
    
    override fun reset() {
        panel?.reset()
    }
    
    override fun disposeUIResources() {
        panel = null
    }
}
