package com.christianhelle.apiclientcodegen.actions.typescript

import com.christianhelle.apiclientcodegen.actions.BaseGeneratorAction

class ReduxQueryAction : BaseGeneratorAction() {
    override val generatorCommand = "reduxquery"
    override val generatorDisplayName = "Redux Query"
    override val requiresJava = true
    override val isTypeScript = true
}
