package com.christianhelle.apiclientcodegen

import com.intellij.openapi.actionSystem.*
import com.intellij.openapi.vfs.VirtualFile

class RestApiClientGeneratorGroup : ActionGroup() {
    override fun getChildren(e: AnActionEvent?): Array<AnAction> {
        return arrayOf(
            NSwagGeneratorAction(),
            RefitterGeneratorAction(),
            OpenApiGeneratorAction(),
            KiotaGeneratorAction(),
            SwaggerGeneratorAction(),
            AutoRestGeneratorAction()
        )
    }

    override fun update(e: AnActionEvent) {
        val file = e.getData(CommonDataKeys.VIRTUAL_FILE)
        e.presentation.isVisible = file != null && isOpenApiSpecFile(file)
    }

    private fun isOpenApiSpecFile(file: VirtualFile): Boolean {
        val ext = file.extension?.lowercase()
        return ext == "json" || ext == "yaml" || ext == "yml"
    }
}