package com.christianhelle.apiclientcodegen.actions

import com.intellij.openapi.project.Project
import com.intellij.openapi.ui.Messages
import com.intellij.openapi.vfs.VirtualFile
import java.io.BufferedReader
import java.io.InputStreamReader

internal fun findRapicgen(): String? {
    val path = System.getenv("PATH") ?: return null
    val isWindows = System.getProperty("os.name").lowercase().contains("win")
    val exe = if (isWindows) "rapicgen.exe" else "rapicgen"
    return path.split(if (isWindows) ';' else ':')
        .map { java.nio.file.Paths.get(it, exe).toFile() }
        .firstOrNull { it.exists() }?.absolutePath
}

internal fun runProcess(command: List<String>, workingDir: java.io.File, onStdOut: (String)->Unit, onStdErr: (String)->Unit): Int {
    val pb = ProcessBuilder(command).directory(workingDir).redirectErrorStream(false)
    val process = pb.start()
    BufferedReader(InputStreamReader(process.inputStream)).use { r -> r.lines().forEach(onStdOut) }
    val stderrThread = Thread {
        BufferedReader(InputStreamReader(process.errorStream)).use { r -> r.lines().forEach(onStdErr) }
    }
    stderrThread.start()
    BufferedReader(InputStreamReader(process.inputStream)).use { r -> r.lines().forEach(onStdOut) }
    stderrThread.join()
    return process.waitFor()
}

internal fun prompt(project: Project, title: String, message: String, default: String = ""): String? =
    Messages.showInputDialog(project, message, title, Messages.getQuestionIcon(), default, null)

internal fun showInfo(project: Project, message: String) =
    Messages.showInfoMessage(project, message, "REST API Client Code Generator")

internal fun showError(project: Project, message: String) =
    Messages.showErrorDialog(project, message, "REST API Client Code Generator")

internal fun VirtualFile.isOpenApiSpec(): Boolean = name.endsWith(".json", true) || name.endsWith(".yaml", true) || name.endsWith(".yml", true)
internal fun VirtualFile.isRefitterConfig(): Boolean = name.endsWith(".refitter", true)
