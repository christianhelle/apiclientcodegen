plugins {
    id("org.jetbrains.intellij") version "1.17.4"
    kotlin("jvm") version "1.9.20"
}

group = "com.christianhelle.apiclientcodegen"
version = "1.0.0"

repositories {
    mavenCentral()
}

intellij {
    version.set("2025.1.2")
    type.set("RD") // Rider
    
    plugins.set(listOf(
        // Required for Rider compatibility
    ))
}

tasks {
    withType<JavaCompile> {
        sourceCompatibility = "17"
        targetCompatibility = "17"
    }
    
    withType<org.jetbrains.kotlin.gradle.tasks.KotlinCompile> {
        kotlinOptions.jvmTarget = "17"
    }

    patchPluginXml {
        sinceBuild.set("251")
        untilBuild.set("252.*")
        
        changeNotes.set("""
            <![CDATA[
            REST API Client Code Generator for IntelliJ/JetBrains Rider<br>
            <ul>
            <li>Generate C# REST API clients from OpenAPI/Swagger specifications</li>
            <li>Generate TypeScript REST API clients from OpenAPI/Swagger specifications</li>
            <li>Support for multiple generators: NSwag, Refitter, OpenAPI Generator, Microsoft Kiota, Swagger Codegen CLI, AutoREST</li>
            <li>Context menu integration for JSON/YAML files</li>
            <li>Support for .refitter settings files</li>
            </ul>
            ]]>
        """)
    }

    signPlugin {
        certificateChain.set(System.getenv("CERTIFICATE_CHAIN"))
        privateKey.set(System.getenv("PRIVATE_KEY"))
        password.set(System.getenv("PRIVATE_KEY_PASSWORD"))
    }

    publishPlugin {
        token.set(System.getenv("PUBLISH_TOKEN"))
    }

    buildSearchableOptions {
        enabled = false
    }
}

dependencies {
    implementation(kotlin("stdlib"))
}