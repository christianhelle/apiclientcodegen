plugins {
    id("java")
    id("org.jetbrains.kotlin.jvm") version "1.9.10"
    id("org.jetbrains.intellij") version "1.17.3"
}

group = "com.christianhelle.apiclientcodegen"
version = "1.0.0"

repositories {
    mavenCentral()
}

dependencies {
    implementation("org.jetbrains.kotlin:kotlin-stdlib")
    testImplementation("junit:junit:4.13.2")
    testImplementation("org.mockito:mockito-core:5.5.0")
}

intellij {
    version.set("2025.1.2")
    type.set("RD") // Rider
    
    plugins.set(listOf(
        "com.intellij.platform.debug",
        "org.jetbrains.plugins.terminal"
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
        untilBuild.set("251.*")
        
        changeNotes.set("""
            <h3>1.0.0</h3>
            <ul>
                <li>Initial release of REST API Client Code Generator for JetBrains Rider</li>
                <li>Support for generating C# REST API clients using NSwag, Refitter, OpenAPI Generator, Microsoft Kiota, Swagger Codegen CLI, and AutoREST</li>
                <li>Support for generating TypeScript REST API clients for Angular, Aurelia, Axios, Fetch, Inversify, jQuery, NestJS, Node, Redux Query, and RxJS</li>
                <li>Support for Refitter settings files</li>
                <li>Context menu integration for .json, .yaml, .yml, and .refitter files</li>
                <li>Configurable namespace and output directory settings</li>
            </ul>
        """.trimIndent())
    }

    signPlugin {
        certificateChain.set(System.getenv("CERTIFICATE_CHAIN"))
        privateKey.set(System.getenv("PRIVATE_KEY"))
        password.set(System.getenv("PRIVATE_KEY_PASSWORD"))
    }

    publishPlugin {
        token.set(System.getenv("PUBLISH_TOKEN"))
    }
}
