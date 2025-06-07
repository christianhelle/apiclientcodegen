package com.christianhelle.apiclientcodegen.actions.csharp

import com.christianhelle.apiclientcodegen.actions.BaseGeneratorAction

class RefitterAction : BaseGeneratorAction() {
    override val generatorCommand = "refitter"
    override val generatorDisplayName = "Refitter"
    override val requiresJava = false
}
