package com.christianhelle.apiclientcodegen.actions.typescript

import com.christianhelle.apiclientcodegen.actions.BaseGeneratorAction

class AureliaAction : BaseGeneratorAction() {
    override val generatorCommand = "aurelia"
    override val generatorDisplayName = "Aurelia"
    override val requiresJava = true
    override val isTypeScript = true
}
