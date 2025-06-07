package com.christianhelle.apiclientcodegen.actions.csharp

import com.christianhelle.apiclientcodegen.actions.BaseGeneratorAction

class AutoRestAction : BaseGeneratorAction() {
    override val generatorCommand = "autorest"
    override val generatorDisplayName = "AutoREST"
    override val requiresJava = false
}
