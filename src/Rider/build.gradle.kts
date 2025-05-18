plugins {
    id("org.jetbrains.intellij") version "1.17.3"
    kotlin("jvm") version "1.9.22"
}

intellij {
    type = "RD"
    version = "2025.1"
    plugins = listOf()
}

group = "com.christianhelle.apiclientcodegen"
version = "1.0.0"

repositories {
    mavenCentral()
}

dependencies {
    implementation(kotlin("stdlib"))
}

tasks {
    patchPluginXml {
        changeNotes = "Initial version. Adds REST API Client Generator context menu."
    }
}