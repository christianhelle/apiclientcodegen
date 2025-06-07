package com.christianhelle.apiclientcodegen.actions.csharp

import com.christianhelle.apiclientcodegen.actions.BaseGeneratorAction

class OpenApiAction : BaseGeneratorAction() {
    override val generatorCommand = "openapi"
    override val generatorDisplayName = "OpenAPI Generator"
    override val requiresJava = true
}
