package com.christianhelle.apiclientcodegen.actions.csharp

import com.christianhelle.apiclientcodegen.actions.BaseGeneratorAction

class KiotaAction : BaseGeneratorAction() {
    override val generatorCommand = "kiota"
    override val generatorDisplayName = "Microsoft Kiota"
    override val requiresJava = false
}
