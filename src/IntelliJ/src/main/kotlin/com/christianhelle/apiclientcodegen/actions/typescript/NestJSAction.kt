package com.christianhelle.apiclientcodegen.actions.typescript

import com.christianhelle.apiclientcodegen.actions.BaseGeneratorAction

class NestJSAction : BaseGeneratorAction() {
    override val generatorCommand = "nestjs"
    override val generatorDisplayName = "NestJS"
    override val requiresJava = true
    override val isTypeScript = true
}
