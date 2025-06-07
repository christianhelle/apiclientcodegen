package com.christianhelle.apiclientcodegen.settings

import org.junit.Assert.*
import org.junit.Test

class RestApiClientCodeGeneratorSettingsTest {
    
    @Test
    fun testDefaultValues() {
        val settings = RestApiClientCodeGeneratorSettings()
        val state = settings.state
        
        assertEquals("GeneratedCode", state.namespace)
        assertEquals("", state.outputDirectory)
    }
    
    @Test
    fun testStateModification() {
        val settings = RestApiClientCodeGeneratorSettings()
        
        settings.namespace = "TestNamespace"
        settings.outputDirectory = "output"
        
        assertEquals("TestNamespace", settings.namespace)
        assertEquals("output", settings.outputDirectory)
        
        val state = settings.state
        assertEquals("TestNamespace", state.namespace)
        assertEquals("output", state.outputDirectory)
    }
    
    @Test
    fun testLoadState() {
        val settings = RestApiClientCodeGeneratorSettings()
        val newState = RestApiClientCodeGeneratorSettings.State(
            namespace = "LoadedNamespace",
            outputDirectory = "loaded/output"
        )
        
        settings.loadState(newState)
        
        assertEquals("LoadedNamespace", settings.namespace)
        assertEquals("loaded/output", settings.outputDirectory)
    }
}
