package com.christianhelle.apiclientcodegen.actions.csharp

import com.christianhelle.apiclientcodegen.actions.BaseGeneratorAction

class NSwagAction : BaseGeneratorAction() {
    override val generatorCommand = "nswag"
    override val generatorDisplayName = "NSwag"
    override val requiresJava = false
}
