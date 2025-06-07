package com.christianhelle.apiclientcodegen.actions.typescript

import com.christianhelle.apiclientcodegen.actions.BaseGeneratorAction

class RxJSAction : BaseGeneratorAction() {
    override val generatorCommand = "rxjs"
    override val generatorDisplayName = "RxJS"
    override val requiresJava = true
    override val isTypeScript = true
}
