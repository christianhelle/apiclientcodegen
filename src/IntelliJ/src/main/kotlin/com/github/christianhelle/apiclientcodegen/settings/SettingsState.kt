package com.github.christianhelle.apiclientcodegen.settings

import com.intellij.openapi.application.ApplicationManager
import com.intellij.openapi.components.PersistentStateComponent
import com.intellij.openapi.components.State
import com.intellij.openapi.components.Storage
import com.intellij.util.xmlb.XmlSerializerUtil

@State(
    name = "com.github.christianhelle.apiclientcodegen.settings.SettingsState",
    storages = [Storage("RestApiClientCodeGeneratorSettings.xml")]
)
class SettingsState : PersistentStateComponent<SettingsState> {
    var namespace = "GeneratedCode"
    var outputDirectory = ""

    override fun getState(): SettingsState = this

    override fun loadState(state: SettingsState) {
        XmlSerializerUtil.copyBean(state, this)
    }

    companion object {
        fun getInstance(): SettingsState {
            return ApplicationManager.getApplication().getService(SettingsState::class.java)
        }
    }
}