package com.christianhelle.apiclientcodegen.settings

import com.intellij.openapi.application.ApplicationManager
import com.intellij.openapi.components.*

@State(
    name = "ApiClientCodeGenSettings",
    storages = [Storage("apiClientCodeGen.xml")]
)
class ApiClientCodeGenSettings : PersistentStateComponent<ApiClientCodeGenSettings.State> {
    
    data class State(
        var namespace: String = "GeneratedCode",
        var outputDirectory: String = ""
    )
    
    private var myState = State()
    
    override fun getState(): State = myState
    
    override fun loadState(state: State) {
        myState = state
    }
    
    companion object {
        fun getInstance(): ApiClientCodeGenSettings {
            return ApplicationManager.getApplication().getService(ApiClientCodeGenSettings::class.java)
        }
    }
}