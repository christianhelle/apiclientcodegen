package com.christianhelle.apiclientcodegen.actions.typescript

import com.christianhelle.apiclientcodegen.actions.BaseGeneratorAction

class NodeAction : BaseGeneratorAction() {
    override val generatorCommand = "node"
    override val generatorDisplayName = "Node"
    override val requiresJava = true
    override val isTypeScript = true
}
