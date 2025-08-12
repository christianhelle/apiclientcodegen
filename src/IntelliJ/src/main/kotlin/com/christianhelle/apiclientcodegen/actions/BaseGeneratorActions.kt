package com.christianhelle.apiclientcodegen.actions

import com.intellij.openapi.actionSystem.AnAction
import com.intellij.openapi.actionSystem.AnActionEvent
import com.intellij.openapi.actionSystem.CommonDataKeys
import com.intellij.openapi.vfs.LocalFileSystem
import com.intellij.openapi.vfs.VfsUtil
import java.nio.file.Paths

abstract class CSharpGeneratorAction(private val generator: String): AnAction() {
    override fun update(e: AnActionEvent) { e.presentation.isEnabledAndVisible = e.getData(CommonDataKeys.VIRTUAL_FILE)?.isOpenApiSpec() == true }
    override fun actionPerformed(e: AnActionEvent) {
        val project = e.project ?: return
        val file = e.getData(CommonDataKeys.VIRTUAL_FILE) ?: return
        val rapicgen = findRapicgen() ?: return showError(project, "rapicgen tool not found. Install with: dotnet tool install --global rapicgen")
        val ns = prompt(project, "C# Namespace", "Enter namespace", "GeneratedCode") ?: return
        val outputFile = file.nameWithoutExtension + "-${generator}.cs"
        val cmd = listOf(rapicgen, "csharp", generator, file.path, ns, outputFile)
        val log = StringBuilder()
        val code = runProcess(cmd, java.io.File(file.parent.path), { log.appendLine(it) }, { log.appendLine(it) })
        if (code == 0) {
            VfsUtil.findFile(Paths.get(file.parent.path, outputFile), true)?.refresh(true, false)
            showInfo(project, "Generated $outputFile")
        } else showError(project, "Generation failed.\n$log")
    }
}

abstract class TypeScriptGeneratorAction(private val generator: String): AnAction() {
    override fun update(e: AnActionEvent) { e.presentation.isEnabledAndVisible = e.getData(CommonDataKeys.VIRTUAL_FILE)?.isOpenApiSpec() == true }
    override fun actionPerformed(e: AnActionEvent) {
        val project = e.project ?: return
        val file = e.getData(CommonDataKeys.VIRTUAL_FILE) ?: return
        val rapicgen = findRapicgen() ?: return showError(project, "rapicgen tool not found. Install with: dotnet tool install --global rapicgen")
        val defaultDir = "typescript-${generator.lowercase()}-client"
        val outputDir = prompt(project, "TypeScript Output", "Enter output directory", defaultDir) ?: return
        val cmd = listOf(rapicgen, "typescript", generator, file.path, outputDir)
        val log = StringBuilder()
        val code = runProcess(cmd, java.io.File(file.parent.path), { log.appendLine(it) }, { log.appendLine(it) })
        if (code == 0) {
            LocalFileSystem.getInstance().refreshAndFindFileByPath(Paths.get(file.parent.path, outputDir).toString())?.refresh(true, true)
            showInfo(project, "Generated $generator client in $outputDir")
        } else showError(project, "Generation failed.\n$log")
    }
}

class CSharpGeneratorNSwagAction: CSharpGeneratorAction("nswag")
class CSharpGeneratorRefitterAction: CSharpGeneratorAction("refitter")
class CSharpGeneratorOpenApiAction: CSharpGeneratorAction("openapi")
class CSharpGeneratorKiotaAction: CSharpGeneratorAction("kiota")
class CSharpGeneratorSwaggerAction: CSharpGeneratorAction("swagger")
class CSharpGeneratorAutoRestAction: CSharpGeneratorAction("autorest")

class TypeScriptGeneratorAngularAction: TypeScriptGeneratorAction("Angular")
class TypeScriptGeneratorAureliaAction: TypeScriptGeneratorAction("Aurelia")
class TypeScriptGeneratorAxiosAction: TypeScriptGeneratorAction("Axios")
class TypeScriptGeneratorFetchAction: TypeScriptGeneratorAction("Fetch")
class TypeScriptGeneratorInversifyAction: TypeScriptGeneratorAction("Inversify")
class TypeScriptGeneratorJQueryAction: TypeScriptGeneratorAction("JQuery")
class TypeScriptGeneratorNestJSAction: TypeScriptGeneratorAction("NestJS")
class TypeScriptGeneratorNodeAction: TypeScriptGeneratorAction("Node")
class TypeScriptGeneratorReduxQueryAction: TypeScriptGeneratorAction("ReduxQuery")
class TypeScriptGeneratorRxjsAction: TypeScriptGeneratorAction("Rxjs")
