import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

plugins {
    id("org.jetbrains.intellij") version "1.17.3"
    kotlin("jvm") version "1.9.22"
}

java {
    sourceCompatibility = JavaVersion.VERSION_17
    targetCompatibility = JavaVersion.VERSION_17
}

tasks.withType<KotlinCompile> {
    kotlinOptions.jvmTarget = "17"
}

intellij {
    type = "RD"
    version = "2023.3"
    
    // No plugin XML configuration here, moved to patchPluginXml task
}

group = "com.christianhelle.apiclientcodegen"
version = "1.0.0"

repositories {
    mavenCentral()
}

tasks {
    patchPluginXml {
        changeNotes.set("Initial version. Adds REST API Client Generator context menu.")
        version.set(project.version.toString())
        pluginDescription.set("""
            <idea-plugin>
                <extensions defaultExtensionNs="com.intellij">
                    <rider.excludeFromHostAssemblies>
                        <assembly>JetBrains.Platform.UIInteractive.Shell.Common</assembly>
                        <assembly>System.ValueTuple</assembly>
                    </rider.excludeFromHostAssemblies>
                </extensions>
            </idea-plugin>
        """.trimIndent())
    }
}
