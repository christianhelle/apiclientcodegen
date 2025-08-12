import org.jetbrains.intellij.platform.gradle.tasks.RunIdeTask

plugins {
    id("java")
    id("org.jetbrains.intellij.platform") version "2.0.1"
    kotlin("jvm") version "2.0.0"
}

val pluginGroup: String by project
val pluginName: String by project
val pluginVersion: String by project
val pluginSinceBuild: String by project
val pluginUntilBuild: String by project
val platformVersion: String by project
val javaVersion: String by project

group = pluginGroup
version = pluginVersion

repositories {
    mavenCentral()
    intellijPlatform { defaultRepositories() }
}

intellijPlatform {
    platform { version.set(platformVersion) }
    pluginConfiguration {
        name.set(pluginName)
        version.set(pluginVersion)
        ideaVersion.sinceBuild.set(pluginSinceBuild)
        ideaVersion.untilBuild.set(pluginUntilBuild)
    }
}

dependencies {
    intellijPlatform { intellijIdeaCommunity(platformVersion) }
    implementation(kotlin("stdlib"))
}

tasks {
    withType<JavaCompile> { sourceCompatibility = javaVersion; targetCompatibility = javaVersion }
    processResources {
        from("../../images/icon.png") { rename { "pluginIcon.png" } }
    }
    register<RunIdeTask>("runRiderLike") { autoReloadPlugins.set(true) }
}
