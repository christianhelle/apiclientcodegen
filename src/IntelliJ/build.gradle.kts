plugins {
    id("java")
    id("org.jetbrains.intellij.platform") version "2.13.1"
    kotlin("jvm") version "2.3.20"
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
    intellijPlatform {
        defaultRepositories()
        localPlatformArtifacts()
    }
}

intellijPlatform {
    pluginConfiguration {
        name.set(pluginName)
        version.set(pluginVersion)
        ideaVersion.sinceBuild.set(pluginSinceBuild)
        if (pluginUntilBuild.isNotBlank()) {
            ideaVersion.untilBuild.set(pluginUntilBuild)
        }
    }
    pluginVerification {
        ides {
            recommended()
        }
    }
}

dependencies {
    intellijPlatform { intellijIdeaCommunity(platformVersion) }
}

tasks {
    withType<JavaCompile> { sourceCompatibility = javaVersion; targetCompatibility = javaVersion }
    withType<org.jetbrains.kotlin.gradle.tasks.KotlinCompile> {
        kotlinOptions { jvmTarget = javaVersion }
    }
    buildPlugin {
        // JARs inside the plugin ZIP must be STORED (not compressed) so IntelliJ's
        // classloader can load classes directly from nested archives.
        entryCompression = ZipEntryCompression.STORED
    }
}

kotlin {
    jvmToolchain(javaVersion.toInt())
}

java {
    toolchain {
        languageVersion.set(JavaLanguageVersion.of(javaVersion))
    }
}
