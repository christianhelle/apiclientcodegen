package com.christianhelle.apiclientcodegen.actions.typescript

import com.christianhelle.apiclientcodegen.actions.BaseGeneratorAction

class JQueryAction : BaseGeneratorAction() {
    override val generatorCommand = "jquery"
    override val generatorDisplayName = "jQuery"
    override val requiresJava = true
    override val isTypeScript = true
}
