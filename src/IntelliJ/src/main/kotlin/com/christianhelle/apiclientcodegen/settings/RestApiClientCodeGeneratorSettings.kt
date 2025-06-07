package com.christianhelle.apiclientcodegen.settings

import com.intellij.openapi.application.ApplicationManager
import com.intellij.openapi.components.*

@Service(Service.Level.APP)
@State(
    name = "RestApiClientCodeGeneratorSettings",
    storages = [Storage("restApiClientCodeGenerator.xml")]
)
class RestApiClientCodeGeneratorSettings : PersistentStateComponent<RestApiClientCodeGeneratorSettings.State> {
    
    data class State(
        var namespace: String = "GeneratedCode",
        var outputDirectory: String = ""
    )
    
    private var myState = State()
    
    override fun getState(): State = myState
    
    override fun loadState(state: State) {
        myState = state
    }
    
    var namespace: String
        get() = myState.namespace
        set(value) {
            myState.namespace = value
        }
    
    var outputDirectory: String
        get() = myState.outputDirectory
        set(value) {
            myState.outputDirectory = value
        }
    
    companion object {
        fun getInstance(): RestApiClientCodeGeneratorSettings {
            return ApplicationManager.getApplication().getService(RestApiClientCodeGeneratorSettings::class.java)
        }
    }
}
