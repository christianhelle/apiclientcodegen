package com.christianhelle.apiclientcodegen.actions.typescript

import com.christianhelle.apiclientcodegen.actions.BaseGeneratorAction

class InversifyAction : BaseGeneratorAction() {
    override val generatorCommand = "inversify"
    override val generatorDisplayName = "Inversify"
    override val requiresJava = true
    override val isTypeScript = true
}
