package com.christianhelle.apiclientcodegen.actions

import com.christianhelle.apiclientcodegen.services.RapicgenService
import com.intellij.openapi.actionSystem.AnAction
import com.intellij.openapi.actionSystem.AnActionEvent
import com.intellij.openapi.actionSystem.CommonDataKeys
import com.intellij.openapi.vfs.VirtualFile

class RefitterSettingsAction : AnAction() {
    
    override fun actionPerformed(e: AnActionEvent) {
        val project = e.project ?: return
        val file = e.getData(CommonDataKeys.VIRTUAL_FILE) ?: return
        
        if (!isRefitterFile(file)) {
            return
        }
        
        val rapicgenService = RapicgenService.getInstance()
        rapicgenService.executeRapicgenCommand(
            project, 
            "refitter", 
            file.path, 
            "", // namespace not used for refitter settings
            "", // output file determined by settings
            isTypeScript = false,
            isRefitterSettings = true
        )
    }
    
    override fun update(e: AnActionEvent) {
        val file = e.getData(CommonDataKeys.VIRTUAL_FILE)
        val visible = file != null && isRefitterFile(file)
        e.presentation.isEnabledAndVisible = visible
    }
    
    private fun isRefitterFile(file: VirtualFile): Boolean {
        return file.extension?.lowercase() == "refitter" || file.name == ".refitter"
    }
}