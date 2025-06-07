package com.christianhelle.apiclientcodegen.actions.csharp

import com.christianhelle.apiclientcodegen.actions.BaseGeneratorAction

class SwaggerAction : BaseGeneratorAction() {
    override val generatorCommand = "swagger"
    override val generatorDisplayName = "Swagger Codegen CLI"
    override val requiresJava = true
}
