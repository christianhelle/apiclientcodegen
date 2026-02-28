use zed_extension_api::{
    self as zed, SlashCommand, SlashCommandArgumentCompletion, SlashCommandOutput,
    SlashCommandOutputSection, Worktree,
};
use zed_extension_api::process::Command;

struct RestApiClientGenerator;

/// C# generators: (command, display_name, requires_java)
const CSHARP_GENERATORS: &[(&str, &str, bool)] = &[
    ("nswag", "NSwag (v14.6.3)", false),
    ("refitter", "Refitter (v1.7.3)", false),
    ("openapi", "OpenAPI Generator (v7.20.0)", true),
    ("kiota", "Microsoft Kiota (v1.30.0)", false),
    ("swagger", "Swagger Codegen (v3.0.34)", true),
    ("autorest", "AutoRest (v3.0.0-beta)", false),
];

/// TypeScript generators: (command, display_name)
const TYPESCRIPT_GENERATORS: &[(&str, &str)] = &[
    ("angular", "Angular"),
    ("aurelia", "Aurelia"),
    ("axios", "Axios"),
    ("fetch", "Fetch"),
    ("inversify", "Inversify"),
    ("jquery", "JQuery"),
    ("nestjs", "NestJS"),
    ("node", "Node"),
    ("reduxquery", "Redux Query"),
    ("rxjs", "RxJS"),
];

const DEFAULT_NAMESPACE: &str = "GeneratedCode";

fn stdout_str(bytes: &[u8]) -> String {
    String::from_utf8_lossy(bytes).to_string()
}

/// Check if a command is available on the system
fn is_command_available(cmd: &str, args: &[&str]) -> bool {
    Command::new(cmd)
        .args(args.iter().map(|s| s.to_string()))
        .output()
        .is_ok()
}

fn is_dotnet_installed() -> bool {
    is_command_available("dotnet", &["--version"])
}

fn is_java_installed() -> bool {
    is_command_available("java", &["-version"])
}

fn is_rapicgen_installed() -> bool {
    if let Ok(output) = Command::new("dotnet")
        .args(["tool", "list", "--global"])
        .output()
    {
        stdout_str(&output.stdout).to_lowercase().contains("rapicgen")
    } else {
        false
    }
}

fn install_rapicgen() -> Result<(), String> {
    let result = Command::new("dotnet")
        .args(["tool", "install", "--global", "rapicgen"])
        .output()
        .map_err(|e| format!("Failed to install rapicgen: {e}"))?;

    if result.status == Some(0) {
        return Ok(());
    }

    // Try update if install failed (already installed but outdated)
    let result = Command::new("dotnet")
        .args(["tool", "update", "--global", "rapicgen"])
        .output()
        .map_err(|e| format!("Failed to update rapicgen: {e}"))?;

    if result.status == Some(0) {
        Ok(())
    } else {
        Err(format!(
            "Failed to install/update rapicgen: {}",
            stdout_str(&result.stderr)
        ))
    }
}

fn ensure_rapicgen() -> Result<(), String> {
    if !is_dotnet_installed() {
        return Err(
            ".NET SDK not found. Please install .NET SDK 8.0 or higher from \
             https://dotnet.microsoft.com/download/dotnet"
                .to_string(),
        );
    }
    if !is_rapicgen_installed() {
        install_rapicgen()?;
    }
    Ok(())
}

/// Derive the output file path for a C# generator
fn get_output_path(spec_file: &str, generator: &str) -> String {
    let filename = spec_file
        .rsplit('/')
        .next()
        .unwrap_or(spec_file)
        .rsplit('\\')
        .next()
        .unwrap_or(spec_file);
    let stem = filename
        .rsplit_once('.')
        .map(|(name, _)| name)
        .unwrap_or(filename);
    let dir = spec_file
        .rfind('/')
        .or_else(|| spec_file.rfind('\\'))
        .map(|pos| &spec_file[..pos])
        .unwrap_or(".");
    format!("{dir}/{stem}.{generator}.cs")
}

/// Derive the output directory for a TypeScript generator
fn get_typescript_output_dir(spec_file: &str, generator: &str) -> String {
    let filename = spec_file
        .rsplit('/')
        .next()
        .unwrap_or(spec_file)
        .rsplit('\\')
        .next()
        .unwrap_or(spec_file);
    let stem = filename
        .rsplit_once('.')
        .map(|(name, _)| name)
        .unwrap_or(filename);
    let dir = spec_file
        .rfind('/')
        .or_else(|| spec_file.rfind('\\'))
        .map(|pos| &spec_file[..pos])
        .unwrap_or(".");
    format!("{dir}/{stem}-{generator}-typescript")
}

fn run_csharp_generator(generator: &str, spec_file: &str, namespace: &str) -> Result<String, String> {
    let requires_java = CSHARP_GENERATORS
        .iter()
        .find(|(cmd, _, _)| *cmd == generator)
        .map(|(_, _, java)| *java)
        .unwrap_or(false);

    if requires_java && !is_java_installed() {
        return Err(format!(
            "Java Runtime Environment not found. The {generator} generator requires Java. \
             Please install Java from https://adoptium.net/"
        ));
    }

    ensure_rapicgen()?;

    let output_file = get_output_path(spec_file, generator);

    let result = Command::new("rapicgen")
        .args([
            "csharp".to_string(),
            generator.to_string(),
            spec_file.to_string(),
            namespace.to_string(),
            output_file.clone(),
        ])
        .output()
        .map_err(|e| format!("Failed to execute rapicgen: {e}"))?;

    if result.status == Some(0) {
        let display_name = CSHARP_GENERATORS
            .iter()
            .find(|(cmd, _, _)| *cmd == generator)
            .map(|(_, name, _)| *name)
            .unwrap_or(generator);
        Ok(format!(
            "✅ Successfully generated C# client code with {display_name}\n\n\
             Output: {output_file}\n\
             Namespace: {namespace}"
        ))
    } else {
        Err(format!(
            "Code generation failed:\n{}\n{}",
            stdout_str(&result.stdout),
            stdout_str(&result.stderr)
        ))
    }
}

fn run_typescript_generator(generator: &str, spec_file: &str) -> Result<String, String> {
    if !is_java_installed() {
        return Err(
            "Java Runtime Environment not found. TypeScript generators require Java. \
             Please install Java from https://adoptium.net/"
                .to_string(),
        );
    }

    ensure_rapicgen()?;

    let ts_output_dir = get_typescript_output_dir(spec_file, generator);

    let result = Command::new("rapicgen")
        .args([
            "typescript".to_string(),
            generator.to_string(),
            spec_file.to_string(),
            ts_output_dir.clone(),
        ])
        .output()
        .map_err(|e| format!("Failed to execute rapicgen: {e}"))?;

    if result.status == Some(0) {
        let display_name = TYPESCRIPT_GENERATORS
            .iter()
            .find(|(cmd, _)| *cmd == generator)
            .map(|(_, name)| *name)
            .unwrap_or(generator);
        Ok(format!(
            "✅ Successfully generated TypeScript client code for {display_name}\n\n\
             Output directory: {ts_output_dir}"
        ))
    } else {
        Err(format!(
            "Code generation failed:\n{}\n{}",
            stdout_str(&result.stdout),
            stdout_str(&result.stderr)
        ))
    }
}

fn run_refitter_settings(settings_file: &str) -> Result<String, String> {
    ensure_rapicgen()?;

    let result = Command::new("rapicgen")
        .args([
            "csharp".to_string(),
            "refitter".to_string(),
            ".".to_string(),
            "--settings-file".to_string(),
            settings_file.to_string(),
        ])
        .output()
        .map_err(|e| format!("Failed to execute rapicgen: {e}"))?;

    if result.status == Some(0) {
        Ok(format!(
            "✅ Successfully generated Refitter output from settings file\n\n\
             Settings file: {settings_file}"
        ))
    } else {
        Err(format!(
            "Refitter generation failed:\n{}\n{}",
            stdout_str(&result.stdout),
            stdout_str(&result.stderr)
        ))
    }
}

/// Parse slash command arguments
/// Expected formats:
///   /generate-csharp <generator> <spec-file> [namespace]
///   /generate-typescript <generator> <spec-file>
///   /generate-refitter <settings-file>
fn parse_args(args: &[String]) -> (Option<&str>, Option<&str>, Option<&str>) {
    let first = args.first().map(|s| s.as_str());
    let second = args.get(1).map(|s| s.as_str());
    let third = args.get(2).map(|s| s.as_str());
    (first, second, third)
}

impl zed::Extension for RestApiClientGenerator {
    fn new() -> Self {
        RestApiClientGenerator
    }

    fn complete_slash_command_argument(
        &self,
        command: SlashCommand,
        args: Vec<String>,
    ) -> Result<Vec<SlashCommandArgumentCompletion>, String> {
        match command.name.as_str() {
            "generate-csharp" => {
                if args.is_empty() || (args.len() == 1 && !args[0].contains(' ')) {
                    Ok(CSHARP_GENERATORS
                        .iter()
                        .map(|(cmd, display_name, _)| SlashCommandArgumentCompletion {
                            label: display_name.to_string(),
                            new_text: cmd.to_string(),
                            run_command: false,
                        })
                        .collect())
                } else {
                    Ok(vec![])
                }
            }
            "generate-typescript" => {
                if args.is_empty() || (args.len() == 1 && !args[0].contains(' ')) {
                    Ok(TYPESCRIPT_GENERATORS
                        .iter()
                        .map(|(cmd, display_name)| SlashCommandArgumentCompletion {
                            label: display_name.to_string(),
                            new_text: cmd.to_string(),
                            run_command: false,
                        })
                        .collect())
                } else {
                    Ok(vec![])
                }
            }
            "generate-refitter" => Ok(vec![]),
            _ => Err(format!("Unknown command: {}", command.name)),
        }
    }

    fn run_slash_command(
        &self,
        command: SlashCommand,
        args: Vec<String>,
        _worktree: Option<&Worktree>,
    ) -> Result<SlashCommandOutput, String> {
        match command.name.as_str() {
            "generate-csharp" => {
                let (generator, file, namespace) = parse_args(&args);

                let generator = generator.ok_or(
                    "Usage: /generate-csharp <generator> <spec-file> [namespace]\n\n\
                     Available generators: nswag, refitter, openapi, kiota, swagger, autorest\n\n\
                     Example: /generate-csharp nswag ./swagger.json MyApp.Client",
                )?;

                if !CSHARP_GENERATORS.iter().any(|(cmd, _, _)| *cmd == generator) {
                    return Err(format!(
                        "Unknown generator: {generator}\n\n\
                         Available generators: nswag, refitter, openapi, kiota, swagger, autorest"
                    ));
                }

                let spec_file = file.ok_or(
                    "Please provide the path to the OpenAPI/Swagger specification file.\n\n\
                     Example: /generate-csharp nswag ./swagger.json",
                )?;

                let ns = namespace.unwrap_or(DEFAULT_NAMESPACE);
                let text = run_csharp_generator(generator, spec_file, ns)?;

                Ok(SlashCommandOutput {
                    sections: vec![SlashCommandOutputSection {
                        range: (0..text.len()).into(),
                        label: format!("Generate C# Client ({generator})"),
                    }],
                    text,
                })
            }

            "generate-typescript" => {
                let (generator, file, _) = parse_args(&args);

                let generator = generator.ok_or(
                    "Usage: /generate-typescript <generator> <spec-file>\n\n\
                     Available generators: angular, aurelia, axios, fetch, inversify, jquery, nestjs, node, reduxquery, rxjs\n\n\
                     Example: /generate-typescript axios ./swagger.json",
                )?;

                if !TYPESCRIPT_GENERATORS.iter().any(|(cmd, _)| *cmd == generator) {
                    return Err(format!(
                        "Unknown generator: {generator}\n\n\
                         Available generators: angular, aurelia, axios, fetch, inversify, jquery, nestjs, node, reduxquery, rxjs"
                    ));
                }

                let spec_file = file.ok_or(
                    "Please provide the path to the OpenAPI/Swagger specification file.\n\n\
                     Example: /generate-typescript axios ./swagger.json",
                )?;

                let text = run_typescript_generator(generator, spec_file)?;

                Ok(SlashCommandOutput {
                    sections: vec![SlashCommandOutputSection {
                        range: (0..text.len()).into(),
                        label: format!("Generate TypeScript Client ({generator})"),
                    }],
                    text,
                })
            }

            "generate-refitter" => {
                let settings_file = args.first().ok_or(
                    "Usage: /generate-refitter <settings-file>\n\n\
                     Provide the path to a .refitter settings file.\n\n\
                     Example: /generate-refitter ./api.refitter",
                )?;

                let text = run_refitter_settings(settings_file)?;

                Ok(SlashCommandOutput {
                    sections: vec![SlashCommandOutputSection {
                        range: (0..text.len()).into(),
                        label: "Generate Refitter Output".to_string(),
                    }],
                    text,
                })
            }

            cmd => Err(format!("Unknown slash command: {cmd}")),
        }
    }
}

zed::register_extension!(RestApiClientGenerator);
