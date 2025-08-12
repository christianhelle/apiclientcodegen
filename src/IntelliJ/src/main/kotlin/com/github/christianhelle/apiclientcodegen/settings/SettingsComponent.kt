package com.github.christianhelle.apiclientcodegen.settings

import com.intellij.ui.components.JBLabel
import com.intellij.ui.components.JBTextField
import com.intellij.util.ui.FormBuilder
import javax.swing.JComponent
import javax.swing.JPanel

class SettingsComponent {
    val panel: JPanel
    private val namespaceText = JBTextField()
    private val outputDirectoryText = JBTextField()

    init {
        panel = FormBuilder.createFormBuilder()
            .addLabeledComponent(JBLabel("Default namespace:"), namespaceText, 1, false)
            .addLabeledComponent(JBLabel("Output directory:"), outputDirectoryText, 1, false)
            .addComponentFillVertically(JPanel(), 0)
            .panel
    }

    val preferredFocusedComponent: JComponent
        get() = namespaceText

    var namespace: String
        get() = namespaceText.text
        set(value) {
            namespaceText.text = value
        }

    var outputDirectory: String
        get() = outputDirectoryText.text
        set(value) {
            outputDirectoryText.text = value
        }
}