
import org.jetbrains.changelog.Changelog
import org.jetbrains.changelog.markdownToHTML
import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

fun properties(key: String) = project.findProperty(key).toString()

plugins {
    id("java")
    id("org.jetbrains.kotlin.jvm") version "1.9.22"
    id("org.jetbrains.intellij") version "1.17.2"
    id("org.jetbrains.changelog") version "2.2.0"
    id("org.jmailen.kotlinter") version "4.1.1"
}

group = properties("pluginGroup")
version = properties("pluginVersion")

repositories {
    mavenCentral()
}

sourceSets.main {
    java.srcDirs("src/main/java")
    resources.srcDirs("src/main/resources")
}

intellij {
    pluginName.set(properties("pluginName"))
    version.set(properties("platformVersion"))
    type.set(properties("platformType"))

    plugins.set(properties("platformPlugins").split(',').map { it.trim() }.filter { it.isNotEmpty() })
}

tasks {
    withType<JavaCompile> {
        sourceCompatibility = "17"
        targetCompatibility = "17"
    }
    withType<KotlinCompile> {
        kotlinOptions.jvmTarget = "17"
    }

    wrapper {
        gradleVersion = properties("gradleVersion")
    }

    patchPluginXml {
        version.set(properties("pluginVersion"))
        sinceBuild.set(properties("pluginSinceBuild"))
        untilBuild.set(properties("pluginUntilBuild"))

        val changelog = project.changelog
        changeNotes.set(provider {
            changelog.getLatest().toHTML()
        })
    }

    runPluginVerifier {
        ideVersions.set(properties("pluginVerifierIdeVersions").split(',').map { it.trim() }.filter { it.isNotEmpty() })
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

changelog {
    version.set(properties("pluginVersion"))
    path.set(file("CHANGELOG.md").absolutePath)
    headerParser = org.jetbrains.changelog.Changelog.Header.DUMMY_PARSER
    itemPrefix.set("*")
    keepFutureVersions.set(false)
    unreleasedTerm.set("Unreleased")

    groups.set(
        listOf(
            "Added",
            "Changed",
t            "Deprecated",
            "Removed",
            "Fixed",
            "Security"
        )
    )
}
