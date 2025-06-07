package com.christianhelle.apiclientcodegen.actions.typescript

import com.christianhelle.apiclientcodegen.actions.BaseGeneratorAction

class AngularAction : BaseGeneratorAction() {
    override val generatorCommand = "angular"
    override val generatorDisplayName = "Angular"
    override val requiresJava = true
    override val isTypeScript = true
}
