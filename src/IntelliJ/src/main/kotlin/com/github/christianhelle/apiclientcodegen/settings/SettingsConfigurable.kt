package com.github.christianhelle.apiclientcodegen.settings

import com.intellij.openapi.options.Configurable
import javax.swing.JComponent

class SettingsConfigurable : Configurable {
    private var settingsComponent: SettingsComponent? = null

    override fun getDisplayName(): String = "REST API Client Code Generator"

    override fun getPreferredFocusedComponent(): JComponent? = settingsComponent?.preferredFocusedComponent

    override fun createComponent(): JComponent {
        settingsComponent = SettingsComponent()
        return settingsComponent!!.panel
    }

    override fun isModified(): Boolean {
        val settings = SettingsState.getInstance()
        return settingsComponent?.namespace != settings.namespace ||
                settingsComponent?.outputDirectory != settings.outputDirectory
    }

    override fun apply() {
        val settings = SettingsState.getInstance()
        settingsComponent?.let {
            settings.namespace = it.namespace
            settings.outputDirectory = it.outputDirectory
        }
    }

    override fun reset() {
        val settings = SettingsState.getInstance()
        settingsComponent?.let {
            it.namespace = settings.namespace
            it.outputDirectory = settings.outputDirectory
        }
    }

    override fun disposeUIResources() {
        settingsComponent = null
    }
}