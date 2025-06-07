package com.christianhelle.apiclientcodegen.actions.typescript

import com.christianhelle.apiclientcodegen.actions.BaseGeneratorAction

class AxiosAction : BaseGeneratorAction() {
    override val generatorCommand = "axios"
    override val generatorDisplayName = "Axios"
    override val requiresJava = true
    override val isTypeScript = true
}
