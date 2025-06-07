package com.christianhelle.apiclientcodegen.actions.typescript

import com.christianhelle.apiclientcodegen.actions.BaseGeneratorAction

class FetchAction : BaseGeneratorAction() {
    override val generatorCommand = "fetch"
    override val generatorDisplayName = "Fetch"
    override val requiresJava = true
    override val isTypeScript = true
}
