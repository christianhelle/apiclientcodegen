# Changelog

## [Unreleased](https://github.com/christianhelle/apiclientcodegen/tree/HEAD)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.39.0...HEAD)

**Implemented enhancements:**

- Refit dependency is added when the dependency is already met in a Transient package reference [\#941](https://github.com/christianhelle/apiclientcodegen/issues/941)
- Switch to System.Text.Json for \(de\)serialization [\#767](https://github.com/christianhelle/apiclientcodegen/issues/767)
- Add setting to generate nullable value types for optional request body properties \(Refitter\) [\#762](https://github.com/christianhelle/apiclientcodegen/issues/762)
- Client code is not generated again when definition file is replaced externally [\#209](https://github.com/christianhelle/apiclientcodegen/issues/209)
- OpenAPI Generator v7.22.0 [\#1565](https://github.com/christianhelle/apiclientcodegen/pull/1565) ([christianhelle](https://github.com/christianhelle))
- NSwag v14.7.1 [\#1557](https://github.com/christianhelle/apiclientcodegen/pull/1557) ([christianhelle](https://github.com/christianhelle))
- Fix full path issues for NSwag, Refitter and Kiota [\#1547](https://github.com/christianhelle/apiclientcodegen/pull/1547) ([christianhelle](https://github.com/christianhelle))
- Microsoft Kiota v1.31.1 [\#1546](https://github.com/christianhelle/apiclientcodegen/pull/1546) ([christianhelle](https://github.com/christianhelle))
- Microsoft Kiota v1.31.0 [\#1530](https://github.com/christianhelle/apiclientcodegen/pull/1530) ([christianhelle](https://github.com/christianhelle))
- OpenAPI Generator v7.21.0 [\#1523](https://github.com/christianhelle/apiclientcodegen/pull/1523) ([christianhelle](https://github.com/christianhelle))
- Fix code issues: AutoRest deprecation messaging, test categorization, and documentation updates [\#1522](https://github.com/christianhelle/apiclientcodegen/pull/1522) ([christianhelle](https://github.com/christianhelle))
- Deprecate AutoRest across CLI, IDE extensions, and docs [\#1521](https://github.com/christianhelle/apiclientcodegen/pull/1521) ([christianhelle](https://github.com/christianhelle))
- Resolve JetBrains plugin invalid archive error [\#1513](https://github.com/christianhelle/apiclientcodegen/pull/1513) ([christianhelle](https://github.com/christianhelle))
- Fix OptionalNullableParameters not applied in VSIX Extensibility Refitter flow [\#1494](https://github.com/christianhelle/apiclientcodegen/pull/1494) ([Copilot](https://github.com/apps/copilot-swe-agent))
- Add nullable value types option for Refitter generator [\#1493](https://github.com/christianhelle/apiclientcodegen/pull/1493) ([christianhelle](https://github.com/christianhelle))
- Add System.Text.Json serialization option for NSwag [\#1492](https://github.com/christianhelle/apiclientcodegen/pull/1492) ([christianhelle](https://github.com/christianhelle))
- Add comprehensive unit tests for core generators and utilities [\#1490](https://github.com/christianhelle/apiclientcodegen/pull/1490) ([christianhelle](https://github.com/christianhelle))
- Honor Refitter generate multiple files option for swagger files [\#1489](https://github.com/christianhelle/apiclientcodegen/pull/1489) ([christianhelle](https://github.com/christianhelle))
- Improve NuGet dependency version checking [\#1488](https://github.com/christianhelle/apiclientcodegen/pull/1488) ([christianhelle](https://github.com/christianhelle))
- Add file type validation to prevent VS crashes on unsupported files [\#1487](https://github.com/christianhelle/apiclientcodegen/pull/1487) ([christianhelle](https://github.com/christianhelle))
- Fix Kiota regeneration from lock file ignoring single-file setting [\#1486](https://github.com/christianhelle/apiclientcodegen/pull/1486) ([christianhelle](https://github.com/christianhelle))
- Update Microsoft Kiota dependencies to version 1.22.0 [\#1485](https://github.com/christianhelle/apiclientcodegen/pull/1485) ([christianhelle](https://github.com/christianhelle))

**Fixed bugs:**

- JetBrains plugin invalid archive - inner JARs must be STORED not compressed [\#1512](https://github.com/christianhelle/apiclientcodegen/issues/1512)
- IntelliJ plugin invalid archive - JARs compressed with DEFLATE instead of STORED [\#1511](https://github.com/christianhelle/apiclientcodegen/issues/1511)
- Cannot regenerate without deleting Custom Tool info [\#1463](https://github.com/christianhelle/apiclientcodegen/issues/1463)
- Generate multiple files option for Refitter is not honored [\#1341](https://github.com/christianhelle/apiclientcodegen/issues/1341)
- Unable to install Microsoft.CSharp version 4.5.0 [\#1006](https://github.com/christianhelle/apiclientcodegen/issues/1006)
- Kiota once more generating multiple files when not told to? [\#978](https://github.com/christianhelle/apiclientcodegen/issues/978)
- Crashes Visual Studio When Opening docker-compose.yml [\#190](https://github.com/christianhelle/apiclientcodegen/issues/190)

**Merged pull requests:**

- Update dependency ruby to v4.0.3 [\#1558](https://github.com/christianhelle/apiclientcodegen/pull/1558) ([renovate[bot]](https://github.com/apps/renovate))
- Update README badges [\#1555](https://github.com/christianhelle/apiclientcodegen/pull/1555) ([christianhelle](https://github.com/christianhelle))
- Update dependency @vscode/vsce to v3.9.1 [\#1554](https://github.com/christianhelle/apiclientcodegen/pull/1554) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency coverlet.collector to v10 [\#1553](https://github.com/christianhelle/apiclientcodegen/pull/1553) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/vscode to v1.116.0 [\#1551](https://github.com/christianhelle/apiclientcodegen/pull/1551) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency webpack to v5.106.2 [\#1550](https://github.com/christianhelle/apiclientcodegen/pull/1550) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @vscode/vsce to v3.9.0 [\#1549](https://github.com/christianhelle/apiclientcodegen/pull/1549) ([renovate[bot]](https://github.com/apps/renovate))
- Update actions/upload-pages-artifact action to v5 [\#1545](https://github.com/christianhelle/apiclientcodegen/pull/1545) ([renovate[bot]](https://github.com/apps/renovate))
- Update microsoft/setup-msbuild action to v3 [\#1544](https://github.com/christianhelle/apiclientcodegen/pull/1544) ([renovate[bot]](https://github.com/apps/renovate))
- Update actions/github-script action to v9 [\#1542](https://github.com/christianhelle/apiclientcodegen/pull/1542) ([renovate[bot]](https://github.com/apps/renovate))
- Update actions/deploy-pages action to v5 [\#1541](https://github.com/christianhelle/apiclientcodegen/pull/1541) ([renovate[bot]](https://github.com/apps/renovate))
- Update actions/configure-pages action to v6 [\#1540](https://github.com/christianhelle/apiclientcodegen/pull/1540) ([renovate[bot]](https://github.com/apps/renovate))
- Update plugin org.jetbrains.intellij.platform to v2.14.0 [\#1539](https://github.com/christianhelle/apiclientcodegen/pull/1539) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency webpack to v5.106.1 [\#1538](https://github.com/christianhelle/apiclientcodegen/pull/1538) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Spectre.Console.Cli to 0.55.0 [\#1537](https://github.com/christianhelle/apiclientcodegen/pull/1537) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Microsoft.NET.Test.Sdk to 18.4.0 [\#1536](https://github.com/christianhelle/apiclientcodegen/pull/1536) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/vscode to v1.115.0 [\#1535](https://github.com/christianhelle/apiclientcodegen/pull/1535) ([renovate[bot]](https://github.com/apps/renovate))
- Fix flaky InstallNSwag\_Does\_NotThrow integration test [\#1534](https://github.com/christianhelle/apiclientcodegen/pull/1534) ([Copilot](https://github.com/apps/copilot-swe-agent))
- Update dependency ts-loader to v9.5.7 [\#1533](https://github.com/christianhelle/apiclientcodegen/pull/1533) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/node to v24.12.2 [\#1532](https://github.com/christianhelle/apiclientcodegen/pull/1532) ([renovate[bot]](https://github.com/apps/renovate))
- Update NSwag to v14.7.0 [\#1531](https://github.com/christianhelle/apiclientcodegen/pull/1531) ([christianhelle](https://github.com/christianhelle))
- Expand docs page meta descriptions [\#1528](https://github.com/christianhelle/apiclientcodegen/pull/1528) ([christianhelle](https://github.com/christianhelle))
- Fix duplicate docs home metadata [\#1527](https://github.com/christianhelle/apiclientcodegen/pull/1527) ([christianhelle](https://github.com/christianhelle))
- Update dependency Azure.Core to 1.53.0 [\#1526](https://github.com/christianhelle/apiclientcodegen/pull/1526) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Refit to 10.1.6 [\#1525](https://github.com/christianhelle/apiclientcodegen/pull/1525) ([renovate[bot]](https://github.com/apps/renovate))
- Update Gradle to v9.4.1 [\#1524](https://github.com/christianhelle/apiclientcodegen/pull/1524) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency coverlet.collector to 8.0.1 [\#1510](https://github.com/christianhelle/apiclientcodegen/pull/1510) ([renovate[bot]](https://github.com/apps/renovate))
- chore\(deps\): update dependency ruby to v4.0.2 [\#1509](https://github.com/christianhelle/apiclientcodegen/pull/1509) ([renovate[bot]](https://github.com/apps/renovate))
- Update GitHub Artifact Actions \(major\) [\#1505](https://github.com/christianhelle/apiclientcodegen/pull/1505) ([renovate[bot]](https://github.com/apps/renovate))
- Update ghcr.io/devcontainers/features/powershell Docker tag to v2 [\#1504](https://github.com/christianhelle/apiclientcodegen/pull/1504) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency webpack-cli to v7 [\#1503](https://github.com/christianhelle/apiclientcodegen/pull/1503) ([renovate[bot]](https://github.com/apps/renovate))
- Update plugin org.jetbrains.intellij.platform to v2.13.1 [\#1500](https://github.com/christianhelle/apiclientcodegen/pull/1500) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Microsoft.NET.Test.Sdk to 18.3.0 [\#1499](https://github.com/christianhelle/apiclientcodegen/pull/1499) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency cake.tool to v6.1.0 [\#1498](https://github.com/christianhelle/apiclientcodegen/pull/1498) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Polly to 8.6.6 [\#1497](https://github.com/christianhelle/apiclientcodegen/pull/1497) ([renovate[bot]](https://github.com/apps/renovate))
- Read OpenAPI spec from disk to detect external changes [\#1491](https://github.com/christianhelle/apiclientcodegen/pull/1491) ([christianhelle](https://github.com/christianhelle))
- Update dependency webpack to v5.105.4 [\#1480](https://github.com/christianhelle/apiclientcodegen/pull/1480) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/node to v24.12.0 [\#1479](https://github.com/christianhelle/apiclientcodegen/pull/1479) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Azure.Core to 1.51.1 [\#1470](https://github.com/christianhelle/apiclientcodegen/pull/1470) ([renovate[bot]](https://github.com/apps/renovate))
- Update Gradle to v9.4.0 [\#1457](https://github.com/christianhelle/apiclientcodegen/pull/1457) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency System.Text.Json to 9.0.14 [\#1456](https://github.com/christianhelle/apiclientcodegen/pull/1456) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency ruby to v4.0.1 [\#1455](https://github.com/christianhelle/apiclientcodegen/pull/1455) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/vscode to v1.110.0 [\#1454](https://github.com/christianhelle/apiclientcodegen/pull/1454) ([renovate[bot]](https://github.com/apps/renovate))

## [1.39.0](https://github.com/christianhelle/apiclientcodegen/tree/1.39.0) (2026-02-18)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.38.0...1.39.0)

**Implemented enhancements:**

- OpenAPI Generator v7.20.0 [\#1481](https://github.com/christianhelle/apiclientcodegen/pull/1481) ([christianhelle](https://github.com/christianhelle))

**Merged pull requests:**

- Update dependency coverlet.collector to v8 [\#1483](https://github.com/christianhelle/apiclientcodegen/pull/1483) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency FluentAssertions to 7.2.1 [\#1478](https://github.com/christianhelle/apiclientcodegen/pull/1478) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency webpack to v5.104.1 \[SECURITY\] [\#1477](https://github.com/christianhelle/apiclientcodegen/pull/1477) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Refit to v10 [\#1476](https://github.com/christianhelle/apiclientcodegen/pull/1476) ([renovate[bot]](https://github.com/apps/renovate))

## [1.38.0](https://github.com/christianhelle/apiclientcodegen/tree/1.38.0) (2026-02-09)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.37.0...1.38.0)

**Implemented enhancements:**

- Update Kiota dependencies to v1.21.3 [\#1474](https://github.com/christianhelle/apiclientcodegen/pull/1474) ([christianhelle](https://github.com/christianhelle))
- Update dependency Microsoft.Kiota.Abstractions to 1.21.3 [\#1473](https://github.com/christianhelle/apiclientcodegen/pull/1473) ([renovate[bot]](https://github.com/apps/renovate))

## [1.37.0](https://github.com/christianhelle/apiclientcodegen/tree/1.37.0) (2026-01-28)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.36.0...1.37.0)

**Implemented enhancements:**

- Microsoft Kiota v1.30.0 [\#1469](https://github.com/christianhelle/apiclientcodegen/pull/1469) ([christianhelle](https://github.com/christianhelle))
- Refitter v1.7.3 [\#1467](https://github.com/christianhelle/apiclientcodegen/pull/1467) ([christianhelle](https://github.com/christianhelle))
- Refitter v1.7.2 [\#1465](https://github.com/christianhelle/apiclientcodegen/pull/1465) ([christianhelle](https://github.com/christianhelle))
- Update Microsoft Kiota package dependencies to v1.21.2 [\#1460](https://github.com/christianhelle/apiclientcodegen/pull/1460) ([christianhelle](https://github.com/christianhelle))
- OpenAPI Generator v7.19.0 [\#1458](https://github.com/christianhelle/apiclientcodegen/pull/1458) ([Copilot](https://github.com/apps/copilot-swe-agent))
- Update command icons to use context-appropriate monikers [\#1453](https://github.com/christianhelle/apiclientcodegen/pull/1453) ([christianhelle](https://github.com/christianhelle))

**Closed issues:**

- VB.NET Support [\#1466](https://github.com/christianhelle/apiclientcodegen/issues/1466)

## [1.36.0](https://github.com/christianhelle/apiclientcodegen/tree/1.36.0) (2026-01-04)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.35.0...1.36.0)

**Implemented enhancements:**

- Improve About dialog in VS out-of-process extension [\#1446](https://github.com/christianhelle/apiclientcodegen/pull/1446) ([christianhelle](https://github.com/christianhelle))
- Update Analytics and About dialog in out-of-process VS extension [\#1445](https://github.com/christianhelle/apiclientcodegen/pull/1445) ([christianhelle](https://github.com/christianhelle))
- Update Visual Studio options category order [\#1444](https://github.com/christianhelle/apiclientcodegen/pull/1444) ([christianhelle](https://github.com/christianhelle))
- Migrate all .sln files to .slnx format [\#1442](https://github.com/christianhelle/apiclientcodegen/pull/1442) ([christianhelle](https://github.com/christianhelle))
- Use built-in Input Prompt when adding new OpenAPI Client [\#1441](https://github.com/christianhelle/apiclientcodegen/pull/1441) ([christianhelle](https://github.com/christianhelle))
- Fix missing icon and license in new VSIX setup [\#1440](https://github.com/christianhelle/apiclientcodegen/pull/1440) ([christianhelle](https://github.com/christianhelle))
- Update NSwag version to v14.6.3 in context menus and docs [\#1439](https://github.com/christianhelle/apiclientcodegen/pull/1439) ([christianhelle](https://github.com/christianhelle))
- Add missing generator versions to context menus [\#1438](https://github.com/christianhelle/apiclientcodegen/pull/1438) ([christianhelle](https://github.com/christianhelle))
- Add Settings pages using the new Visual Studio Extensibility model API's [\#1437](https://github.com/christianhelle/apiclientcodegen/pull/1437) ([christianhelle](https://github.com/christianhelle))
- New Visual Studio extension using the new out-of-process extensibility model [\#1435](https://github.com/christianhelle/apiclientcodegen/pull/1435) ([christianhelle](https://github.com/christianhelle))
- OpenAPI Generator v7.18.0 [\#1430](https://github.com/christianhelle/apiclientcodegen/pull/1430) ([Copilot](https://github.com/apps/copilot-swe-agent))
- Refitter v1.7.1 [\#1423](https://github.com/christianhelle/apiclientcodegen/pull/1423) ([christianhelle](https://github.com/christianhelle))
- NSwag v14.6.3 [\#1408](https://github.com/christianhelle/apiclientcodegen/pull/1408) ([Copilot](https://github.com/apps/copilot-swe-agent))

**Closed issues:**

- Upgrade OpenAPI Generator to v7.18.0 [\#1429](https://github.com/christianhelle/apiclientcodegen/issues/1429)

**Merged pull requests:**

- Update dependency Xunit.SkippableFact to 1.5.61 [\#1434](https://github.com/christianhelle/apiclientcodegen/pull/1434) ([renovate[bot]](https://github.com/apps/renovate))
- Fix Cake Build [\#1433](https://github.com/christianhelle/apiclientcodegen/pull/1433) ([christianhelle](https://github.com/christianhelle))
- Fix .refitter file mess when using New REST API Client [\#1432](https://github.com/christianhelle/apiclientcodegen/pull/1432) ([christianhelle](https://github.com/christianhelle))
- Update dependency ruby to v4 [\#1431](https://github.com/christianhelle/apiclientcodegen/pull/1431) ([renovate[bot]](https://github.com/apps/renovate))
- Update Kiota Dependencies to v1.21.1 [\#1428](https://github.com/christianhelle/apiclientcodegen/pull/1428) ([christianhelle](https://github.com/christianhelle))
- Update actions/cache action to v5 [\#1417](https://github.com/christianhelle/apiclientcodegen/pull/1417) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/vscode to v1.107.0 [\#1416](https://github.com/christianhelle/apiclientcodegen/pull/1416) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/node to v24.10.3 [\#1415](https://github.com/christianhelle/apiclientcodegen/pull/1415) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/node to v24.10.2 [\#1414](https://github.com/christianhelle/apiclientcodegen/pull/1414) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Refit to v9 [\#1412](https://github.com/christianhelle/apiclientcodegen/pull/1412) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Spectre.Console.Cli to 0.53.1 [\#1410](https://github.com/christianhelle/apiclientcodegen/pull/1410) ([renovate[bot]](https://github.com/apps/renovate))
- Update plugin org.jetbrains.intellij.platform to v2.10.5 [\#1409](https://github.com/christianhelle/apiclientcodegen/pull/1409) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @vscode/vsce to v3.7.1 [\#1406](https://github.com/christianhelle/apiclientcodegen/pull/1406) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Polly to 8.6.5 [\#1405](https://github.com/christianhelle/apiclientcodegen/pull/1405) ([renovate[bot]](https://github.com/apps/renovate))
- Update actions/checkout action to v6 [\#1404](https://github.com/christianhelle/apiclientcodegen/pull/1404) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency webpack to v5.103.0 [\#1403](https://github.com/christianhelle/apiclientcodegen/pull/1403) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/vscode to v1.106.1 [\#1402](https://github.com/christianhelle/apiclientcodegen/pull/1402) ([renovate[bot]](https://github.com/apps/renovate))
- Update Gradle to v9.2.1 [\#1400](https://github.com/christianhelle/apiclientcodegen/pull/1400) ([renovate[bot]](https://github.com/apps/renovate))
- Update Spectre.Console.Cli to 0.53.0 and fix breaking Execute signature [\#1398](https://github.com/christianhelle/apiclientcodegen/pull/1398) ([Copilot](https://github.com/apps/copilot-swe-agent))
- Update dependency @types/vscode to v1.106.0 [\#1397](https://github.com/christianhelle/apiclientcodegen/pull/1397) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/node to v24.10.1 [\#1396](https://github.com/christianhelle/apiclientcodegen/pull/1396) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency cake.tool to v6 [\#1395](https://github.com/christianhelle/apiclientcodegen/pull/1395) ([renovate[bot]](https://github.com/apps/renovate))
- Update vstest monorepo to 18.0.1 [\#1393](https://github.com/christianhelle/apiclientcodegen/pull/1393) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @vscode/vsce to v3.7.0 - autoclosed [\#1392](https://github.com/christianhelle/apiclientcodegen/pull/1392) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Azure.Core to 1.50.0 [\#1391](https://github.com/christianhelle/apiclientcodegen/pull/1391) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency mocha to v11.7.5 [\#1389](https://github.com/christianhelle/apiclientcodegen/pull/1389) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/node to v24.10.0 [\#1388](https://github.com/christianhelle/apiclientcodegen/pull/1388) ([renovate[bot]](https://github.com/apps/renovate))
- Update plugin org.jetbrains.intellij.platform to v2.10.4 [\#1387](https://github.com/christianhelle/apiclientcodegen/pull/1387) ([renovate[bot]](https://github.com/apps/renovate))

## [1.35.0](https://github.com/christianhelle/apiclientcodegen/tree/1.35.0) (2025-10-30)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.34.0...1.35.0)

**Implemented enhancements:**

- Support other language output [\#392](https://github.com/christianhelle/apiclientcodegen/issues/392)
- Improve documentation regarding generated code usage [\#180](https://github.com/christianhelle/apiclientcodegen/issues/180)
- Run command from folder path [\#60](https://github.com/christianhelle/apiclientcodegen/issues/60)
- OpenAPI Generator v7.17 [\#1385](https://github.com/christianhelle/apiclientcodegen/pull/1385) ([christianhelle](https://github.com/christianhelle))
- Refitter v1.6.5 [\#1375](https://github.com/christianhelle/apiclientcodegen/pull/1375) ([christianhelle](https://github.com/christianhelle))
- Microsoft Kiota v1.29.0 [\#1372](https://github.com/christianhelle/apiclientcodegen/pull/1372) ([christianhelle](https://github.com/christianhelle))

**Fixed bugs:**

- Could not find a part of the path 'C:\MyRepo\MySolution\MyApiClient\45ec2b05c06b4182a172523b42e8b5db\TempApiClient'. [\#225](https://github.com/christianhelle/apiclientcodegen/issues/225)

**Closed issues:**

- Migrate NuGet publishing in release workflows to Trusted Publishing [\#1357](https://github.com/christianhelle/apiclientcodegen/issues/1357)

**Merged pull requests:**

- Update Kiota dependencies to v1.21.0 [\#1386](https://github.com/christianhelle/apiclientcodegen/pull/1386) ([christianhelle](https://github.com/christianhelle))
- Update Gradle to v9.2.0 [\#1384](https://github.com/christianhelle/apiclientcodegen/pull/1384) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency node to v24 [\#1376](https://github.com/christianhelle/apiclientcodegen/pull/1376) ([renovate[bot]](https://github.com/apps/renovate))
- Update GitHub Artifact Actions \(major\) [\#1373](https://github.com/christianhelle/apiclientcodegen/pull/1373) ([renovate[bot]](https://github.com/apps/renovate))
- Update plugin org.jetbrains.intellij.platform to v2.10.2 [\#1370](https://github.com/christianhelle/apiclientcodegen/pull/1370) ([renovate[bot]](https://github.com/apps/renovate))
- Add README.md to /java folder documenting OpenJDK dependency for code generators [\#1369](https://github.com/christianhelle/apiclientcodegen/pull/1369) ([Copilot](https://github.com/apps/copilot-swe-agent))
- Update plugin org.jetbrains.intellij.platform to v2.10.1 [\#1366](https://github.com/christianhelle/apiclientcodegen/pull/1366) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/node to v22.18.11 [\#1365](https://github.com/christianhelle/apiclientcodegen/pull/1365) ([renovate[bot]](https://github.com/apps/renovate))
- Update actions/setup-node action to v6 [\#1363](https://github.com/christianhelle/apiclientcodegen/pull/1363) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/vscode to v1.105.0 - autoclosed [\#1361](https://github.com/christianhelle/apiclientcodegen/pull/1361) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/node to v22.18.10 [\#1360](https://github.com/christianhelle/apiclientcodegen/pull/1360) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency ruby to v3.4.7 [\#1359](https://github.com/christianhelle/apiclientcodegen/pull/1359) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency webpack to v5.102.1 [\#1355](https://github.com/christianhelle/apiclientcodegen/pull/1355) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency cake.tool to v5.1.0 [\#1353](https://github.com/christianhelle/apiclientcodegen/pull/1353) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Polly to 8.6.4 [\#1339](https://github.com/christianhelle/apiclientcodegen/pull/1339) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Newtonsoft.Json to v13.0.4 [\#1320](https://github.com/christianhelle/apiclientcodegen/pull/1320) ([renovate[bot]](https://github.com/apps/renovate))

## [1.34.0](https://github.com/christianhelle/apiclientcodegen/tree/1.34.0) (2025-10-13)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.32.0...1.34.0)

**Merged pull requests:**

- Update vstest monorepo to v18 \(major\) [\#1356](https://github.com/christianhelle/apiclientcodegen/pull/1356) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency typescript to v5.9.3 [\#1352](https://github.com/christianhelle/apiclientcodegen/pull/1352) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency mocha to v11.7.4 [\#1351](https://github.com/christianhelle/apiclientcodegen/pull/1351) ([renovate[bot]](https://github.com/apps/renovate))
- Update Kiota Dependencies to v1.20.1 [\#1350](https://github.com/christianhelle/apiclientcodegen/pull/1350) ([christianhelle](https://github.com/christianhelle))
- Update dependency @types/node to v22.18.8 [\#1345](https://github.com/christianhelle/apiclientcodegen/pull/1345) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @vscode/vsce to v3.6.2 [\#1338](https://github.com/christianhelle/apiclientcodegen/pull/1338) ([renovate[bot]](https://github.com/apps/renovate))
- Update Microsoft.Kiota package references to v1.20.0 [\#1337](https://github.com/christianhelle/apiclientcodegen/pull/1337) ([christianhelle](https://github.com/christianhelle))
- Update dependency Azure.Core to 1.49.0 [\#1330](https://github.com/christianhelle/apiclientcodegen/pull/1330) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @vscode/vsce to v3.6.1 [\#1329](https://github.com/christianhelle/apiclientcodegen/pull/1329) ([renovate[bot]](https://github.com/apps/renovate))
- Update Gradle to v9.1.0 [\#1327](https://github.com/christianhelle/apiclientcodegen/pull/1327) ([renovate[bot]](https://github.com/apps/renovate))

## [1.32.0](https://github.com/christianhelle/apiclientcodegen/tree/1.32.0) (2025-09-18)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.31.0...1.32.0)

**Implemented enhancements:**

- Switch from Refitter.Core dependency to Refitter CLI tool due to NSwag and Visual Studio SDK compatibility issues [\#1302](https://github.com/christianhelle/apiclientcodegen/issues/1302)
- Fix "Generate Refitter Output" command [\#1323](https://github.com/christianhelle/apiclientcodegen/pull/1323) ([christianhelle](https://github.com/christianhelle))
- Improve IntelliJ Plugin Marketplace experience [\#1322](https://github.com/christianhelle/apiclientcodegen/pull/1322) ([christianhelle](https://github.com/christianhelle))
- Fix command syntax for executing Refitter settings from VS Code [\#1317](https://github.com/christianhelle/apiclientcodegen/pull/1317) ([christianhelle](https://github.com/christianhelle))
- Fix IntelliJ plugin version compatibility [\#1312](https://github.com/christianhelle/apiclientcodegen/pull/1312) ([christianhelle](https://github.com/christianhelle))

**Closed issues:**

- .refitter file fails to generate code [\#1321](https://github.com/christianhelle/apiclientcodegen/issues/1321)

**Merged pull requests:**

- Refitter v1.6.3 [\#1326](https://github.com/christianhelle/apiclientcodegen/pull/1326) ([christianhelle](https://github.com/christianhelle))
- Update dependency @types/node to v22.18.6 [\#1325](https://github.com/christianhelle/apiclientcodegen/pull/1325) ([renovate[bot]](https://github.com/apps/renovate))
- Add version bump scripts to workflows [\#1324](https://github.com/christianhelle/apiclientcodegen/pull/1324) ([christianhelle](https://github.com/christianhelle))
- Update dependency @types/node to v22.18.5 [\#1319](https://github.com/christianhelle/apiclientcodegen/pull/1319) ([renovate[bot]](https://github.com/apps/renovate))
- Update workflow triggers to ignore unnecessary paths in CLI Tool, Smoke Tests, Unit Tests, VS Code, and VSIX workflows [\#1318](https://github.com/christianhelle/apiclientcodegen/pull/1318) ([christianhelle](https://github.com/christianhelle))
- Update dependency @types/node to v22.18.3 [\#1316](https://github.com/christianhelle/apiclientcodegen/pull/1316) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/vscode to v1.104.0 [\#1315](https://github.com/christianhelle/apiclientcodegen/pull/1315) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Azure.Core to 1.48.0 [\#1313](https://github.com/christianhelle/apiclientcodegen/pull/1313) ([renovate[bot]](https://github.com/apps/renovate))

## [1.31.0](https://github.com/christianhelle/apiclientcodegen/tree/1.31.0) (2025-09-07)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.30.0...1.31.0)

**Implemented enhancements:**

- Create IntelliJ plugin matching VSCode extension features [\#1201](https://github.com/christianhelle/apiclientcodegen/issues/1201)
- Refitter v1.6.2 [\#1304](https://github.com/christianhelle/apiclientcodegen/pull/1304) ([christianhelle](https://github.com/christianhelle))
- Revert NSwag version back to v14.4.0 [\#1296](https://github.com/christianhelle/apiclientcodegen/pull/1296) ([christianhelle](https://github.com/christianhelle))
- OpenAPI Generator v7.15.0 [\#1294](https://github.com/christianhelle/apiclientcodegen/pull/1294) ([christianhelle](https://github.com/christianhelle))
- Revert Refitter back to v1.6.1 [\#1291](https://github.com/christianhelle/apiclientcodegen/pull/1291) ([christianhelle](https://github.com/christianhelle))
- Refitter v1.6.2 [\#1288](https://github.com/christianhelle/apiclientcodegen/pull/1288) ([renovate[bot]](https://github.com/apps/renovate))
- Initial IntelliJ plugin implementation with basic features [\#1261](https://github.com/christianhelle/apiclientcodegen/pull/1261) ([christianhelle](https://github.com/christianhelle))
- NSwag v14.5.0 [\#1246](https://github.com/christianhelle/apiclientcodegen/pull/1246) ([renovate[bot]](https://github.com/apps/renovate))

**Fixed bugs:**

- Could not find a part of the path - AKA Path too long [\#1122](https://github.com/christianhelle/apiclientcodegen/issues/1122)

**Closed issues:**

- Setup CoPilot Instructions [\#1249](https://github.com/christianhelle/apiclientcodegen/issues/1249)

**Merged pull requests:**

- Update actions/setup-node action to v5 [\#1311](https://github.com/christianhelle/apiclientcodegen/pull/1311) ([renovate[bot]](https://github.com/apps/renovate))
- Update actions/setup-dotnet action to v5 [\#1310](https://github.com/christianhelle/apiclientcodegen/pull/1310) ([renovate[bot]](https://github.com/apps/renovate))
- Update actions/github-script action to v8 [\#1309](https://github.com/christianhelle/apiclientcodegen/pull/1309) ([renovate[bot]](https://github.com/apps/renovate))
- Update plugin org.jetbrains.intellij.platform to v2.9.0 [\#1308](https://github.com/christianhelle/apiclientcodegen/pull/1308) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Spectre.Console.Cli to 0.51.1 [\#1307](https://github.com/christianhelle/apiclientcodegen/pull/1307) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency mocha to v11.7.2 [\#1306](https://github.com/christianhelle/apiclientcodegen/pull/1306) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/node to v22.18.1 [\#1305](https://github.com/christianhelle/apiclientcodegen/pull/1305) ([renovate[bot]](https://github.com/apps/renovate))
- Switch from Refitter.Core dependency to Refitter CLI [\#1303](https://github.com/christianhelle/apiclientcodegen/pull/1303) ([Copilot](https://github.com/apps/copilot-swe-agent))
- Update dependency @types/node to v22.18.0 [\#1301](https://github.com/christianhelle/apiclientcodegen/pull/1301) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency ts-loader to v9.5.4 [\#1300](https://github.com/christianhelle/apiclientcodegen/pull/1300) ([renovate[bot]](https://github.com/apps/renovate))
- Update actions/upload-pages-artifact action to v4 [\#1295](https://github.com/christianhelle/apiclientcodegen/pull/1295) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Polly to 8.6.3 [\#1292](https://github.com/christianhelle/apiclientcodegen/pull/1292) ([renovate[bot]](https://github.com/apps/renovate))
- chore\(deps\): update actions/setup-java action to v5 [\#1290](https://github.com/christianhelle/apiclientcodegen/pull/1290) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Azure.Core to 1.47.3 [\#1289](https://github.com/christianhelle/apiclientcodegen/pull/1289) ([renovate[bot]](https://github.com/apps/renovate))
- chore\(deps\): update dependency webpack to v5.101.3 [\#1287](https://github.com/christianhelle/apiclientcodegen/pull/1287) ([renovate[bot]](https://github.com/apps/renovate))
- chore\(deps\): update dependency xunit.runner.visualstudio to 3.1.4 [\#1286](https://github.com/christianhelle/apiclientcodegen/pull/1286) ([renovate[bot]](https://github.com/apps/renovate))
- chore\(deps\): update dependency @types/vscode to v1.103.0 [\#1285](https://github.com/christianhelle/apiclientcodegen/pull/1285) ([renovate[bot]](https://github.com/apps/renovate))
- chore\(deps\): update dependency @types/node to v22.17.2 [\#1284](https://github.com/christianhelle/apiclientcodegen/pull/1284) ([renovate[bot]](https://github.com/apps/renovate))
- chore\(deps\): update dependency webpack to v5.101.2 [\#1283](https://github.com/christianhelle/apiclientcodegen/pull/1283) ([renovate[bot]](https://github.com/apps/renovate))
- chore\(deps\): update plugin org.jetbrains.intellij.platform to v2.7.2 [\#1282](https://github.com/christianhelle/apiclientcodegen/pull/1282) ([renovate[bot]](https://github.com/apps/renovate))
- chore\(deps\): update dependency gradle to v9 [\#1274](https://github.com/christianhelle/apiclientcodegen/pull/1274) ([renovate[bot]](https://github.com/apps/renovate))
- chore\(deps\): update actions/checkout action to v5 [\#1269](https://github.com/christianhelle/apiclientcodegen/pull/1269) ([renovate[bot]](https://github.com/apps/renovate))
- chore\(deps\): update dependency azure.core to 1.47.2 - autoclosed [\#1268](https://github.com/christianhelle/apiclientcodegen/pull/1268) ([renovate[bot]](https://github.com/apps/renovate))
- chore\(deps\): update dependency restsharp to 105.2.3 [\#1266](https://github.com/christianhelle/apiclientcodegen/pull/1266) ([renovate[bot]](https://github.com/apps/renovate))
- chore\(deps\): update dependency gradle to v8.14.3 [\#1264](https://github.com/christianhelle/apiclientcodegen/pull/1264) ([renovate[bot]](https://github.com/apps/renovate))
- chore\(deps\): update vstest monorepo to 17.14.1 [\#1263](https://github.com/christianhelle/apiclientcodegen/pull/1263) ([renovate[bot]](https://github.com/apps/renovate))
- Update Kiota dependencies to v1.19.1 [\#1262](https://github.com/christianhelle/apiclientcodegen/pull/1262) ([christianhelle](https://github.com/christianhelle))
- Update dependency webpack to v5.101.1 [\#1260](https://github.com/christianhelle/apiclientcodegen/pull/1260) ([renovate[bot]](https://github.com/apps/renovate))
- Fix long path issue in CSharpFileMerger for Kiota code generation on Windows [\#1252](https://github.com/christianhelle/apiclientcodegen/pull/1252) ([Copilot](https://github.com/apps/copilot-swe-agent))
- Update actions/checkout action to v5 [\#1251](https://github.com/christianhelle/apiclientcodegen/pull/1251) ([renovate[bot]](https://github.com/apps/renovate))
- Add comprehensive GitHub Copilot instructions for repository development workflow [\#1250](https://github.com/christianhelle/apiclientcodegen/pull/1250) ([Copilot](https://github.com/apps/copilot-swe-agent))
- Update dependency node to v22 [\#1248](https://github.com/christianhelle/apiclientcodegen/pull/1248) ([renovate[bot]](https://github.com/apps/renovate))
- Update actions/download-artifact action to v5 [\#1247](https://github.com/christianhelle/apiclientcodegen/pull/1247) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency webpack to v5.101.0 [\#1243](https://github.com/christianhelle/apiclientcodegen/pull/1243) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/node to v22.16.5 [\#1242](https://github.com/christianhelle/apiclientcodegen/pull/1242) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency webpack to v5.100.2 [\#1241](https://github.com/christianhelle/apiclientcodegen/pull/1241) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Azure.Core to 1.47.1 [\#1240](https://github.com/christianhelle/apiclientcodegen/pull/1240) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/node to v22.16.4 - autoclosed [\#1239](https://github.com/christianhelle/apiclientcodegen/pull/1239) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency xunit.runner.visualstudio to 3.1.3 [\#1238](https://github.com/christianhelle/apiclientcodegen/pull/1238) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency System.Text.Json to 8.0.6 [\#1230](https://github.com/christianhelle/apiclientcodegen/pull/1230) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency NuGet.VisualStudio to 17.14.0 [\#1137](https://github.com/christianhelle/apiclientcodegen/pull/1137) ([renovate[bot]](https://github.com/apps/renovate))

## [1.30.0](https://github.com/christianhelle/apiclientcodegen/tree/1.30.0) (2025-07-13)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.29.0...1.30.0)

**Implemented enhancements:**

- Microsoft Kiota v1.28.0 [\#1237](https://github.com/christianhelle/apiclientcodegen/pull/1237) ([christianhelle](https://github.com/christianhelle))

**Closed issues:**

- Generate and Deploy Static Documentation Website from README [\#1226](https://github.com/christianhelle/apiclientcodegen/issues/1226)

**Merged pull requests:**

- Update dependency webpack to v5.100.1 [\#1236](https://github.com/christianhelle/apiclientcodegen/pull/1236) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Polly to 8.6.2 [\#1235](https://github.com/christianhelle/apiclientcodegen/pull/1235) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/vscode to v1.102.0 [\#1234](https://github.com/christianhelle/apiclientcodegen/pull/1234) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/node to v22.16.3 [\#1233](https://github.com/christianhelle/apiclientcodegen/pull/1233) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Azure.Core to 1.47.0 [\#1232](https://github.com/christianhelle/apiclientcodegen/pull/1232) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency webpack to v5.100.0 [\#1231](https://github.com/christianhelle/apiclientcodegen/pull/1231) ([renovate[bot]](https://github.com/apps/renovate))
- Refitter v1.6.1 [\#1229](https://github.com/christianhelle/apiclientcodegen/pull/1229) ([renovate[bot]](https://github.com/apps/renovate))
- Update actions/configure-pages action to v5 [\#1228](https://github.com/christianhelle/apiclientcodegen/pull/1228) ([renovate[bot]](https://github.com/apps/renovate))
- Create Static Documentation Website from README [\#1227](https://github.com/christianhelle/apiclientcodegen/pull/1227) ([Copilot](https://github.com/apps/copilot-swe-agent))
- Update dependency @types/node to v22.16.2 [\#1224](https://github.com/christianhelle/apiclientcodegen/pull/1224) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Microsoft.Rest.ClientRuntime to v3 [\#1219](https://github.com/christianhelle/apiclientcodegen/pull/1219) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Polly to 8.6.1 [\#1216](https://github.com/christianhelle/apiclientcodegen/pull/1216) ([renovate[bot]](https://github.com/apps/renovate))

## [1.29.0](https://github.com/christianhelle/apiclientcodegen/tree/1.29.0) (2025-06-25)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.28.0...1.29.0)

**Implemented enhancements:**

- OpenAPI Generator v7.14.0 [\#1222](https://github.com/christianhelle/apiclientcodegen/pull/1222) ([christianhelle](https://github.com/christianhelle))

**Merged pull requests:**

- Update dependency @vscode/vsce to v3.6.0 [\#1223](https://github.com/christianhelle/apiclientcodegen/pull/1223) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/node to v22.15.33 [\#1221](https://github.com/christianhelle/apiclientcodegen/pull/1221) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency mocha to v11.7.1 [\#1220](https://github.com/christianhelle/apiclientcodegen/pull/1220) ([renovate[bot]](https://github.com/apps/renovate))

## [1.28.0](https://github.com/christianhelle/apiclientcodegen/tree/1.28.0) (2025-06-22)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.27.0...1.28.0)

**Implemented enhancements:**

- Refitter v1.6.0 [\#1217](https://github.com/christianhelle/apiclientcodegen/pull/1217) ([renovate[bot]](https://github.com/apps/renovate))

**Merged pull requests:**

- Update dependency Spectre.Console.Cli to 0.50.0 [\#1218](https://github.com/christianhelle/apiclientcodegen/pull/1218) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency mocha to v11.7.0 [\#1215](https://github.com/christianhelle/apiclientcodegen/pull/1215) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @vscode/vsce to v3.5.0 [\#1214](https://github.com/christianhelle/apiclientcodegen/pull/1214) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/vscode to v1.101.0 [\#1213](https://github.com/christianhelle/apiclientcodegen/pull/1213) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency xunit.runner.visualstudio to 3.1.1 [\#1212](https://github.com/christianhelle/apiclientcodegen/pull/1212) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Azure.Core to 1.46.2 [\#1211](https://github.com/christianhelle/apiclientcodegen/pull/1211) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/node to v22.15.32 [\#1210](https://github.com/christianhelle/apiclientcodegen/pull/1210) ([renovate[bot]](https://github.com/apps/renovate))

## [1.27.0](https://github.com/christianhelle/apiclientcodegen/tree/1.27.0) (2025-06-19)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.26.0...1.27.0)

**Implemented enhancements:**

- Improve the CLI experience [\#1203](https://github.com/christianhelle/apiclientcodegen/issues/1203)
- Microsoft Kiota v1.27.0 [\#1206](https://github.com/christianhelle/apiclientcodegen/pull/1206) ([christianhelle](https://github.com/christianhelle))
- Update CLI Tool to use Spectre.Console [\#1205](https://github.com/christianhelle/apiclientcodegen/pull/1205) ([christianhelle](https://github.com/christianhelle))
- Implement "Generate Refitter Output" in VS Code extension [\#1184](https://github.com/christianhelle/apiclientcodegen/pull/1184) ([christianhelle](https://github.com/christianhelle))

**Fixed bugs:**

- Release workflow fails to upload VSCode asset [\#1182](https://github.com/christianhelle/apiclientcodegen/issues/1182)
- OpenAPI Generator fails in VS Code without error details if the JRE/JDK is not installed [\#1180](https://github.com/christianhelle/apiclientcodegen/issues/1180)
- Argument null exception [\#1157](https://github.com/christianhelle/apiclientcodegen/issues/1157)

**Closed issues:**

- Fix typos and improve grammar in docs [\#1207](https://github.com/christianhelle/apiclientcodegen/issues/1207)
- VSIX workflow stopped working [\#1198](https://github.com/christianhelle/apiclientcodegen/issues/1198)
- Contribution Guidelines [\#1189](https://github.com/christianhelle/apiclientcodegen/issues/1189)
- Reduce Code Duplication in Visual Studio Code Extension [\#1185](https://github.com/christianhelle/apiclientcodegen/issues/1185)

**Merged pull requests:**

- Update Microsoft Kiota dependencies to version 1.19.0 [\#1209](https://github.com/christianhelle/apiclientcodegen/pull/1209) ([christianhelle](https://github.com/christianhelle))
- Fix typos and improve grammar in markdown documentation files [\#1208](https://github.com/christianhelle/apiclientcodegen/pull/1208) ([Copilot](https://github.com/apps/copilot-swe-agent))
- Fix VSIX Builds [\#1200](https://github.com/christianhelle/apiclientcodegen/pull/1200) ([christianhelle](https://github.com/christianhelle))
- Add comprehensive CONTRIBUTING.md with code patterns and PR guidelines [\#1190](https://github.com/christianhelle/apiclientcodegen/pull/1190) ([Copilot](https://github.com/apps/copilot-swe-agent))
- Update dependency @types/node to v22.15.23 [\#1187](https://github.com/christianhelle/apiclientcodegen/pull/1187) ([renovate[bot]](https://github.com/apps/renovate))
- Reduce Code Duplication in VSCode Extension and Fix Linting Issue [\#1186](https://github.com/christianhelle/apiclientcodegen/pull/1186) ([Copilot](https://github.com/apps/copilot-swe-agent))
- Fix VSCode extension asset path in release workflow [\#1183](https://github.com/christianhelle/apiclientcodegen/pull/1183) ([Copilot](https://github.com/apps/copilot-swe-agent))
- Add Java Runtime Check for OpenAPI Generator in VS Code Extension [\#1181](https://github.com/christianhelle/apiclientcodegen/pull/1181) ([Copilot](https://github.com/apps/copilot-swe-agent))
- Update dependency mocha to v11.5.0 [\#1179](https://github.com/christianhelle/apiclientcodegen/pull/1179) ([renovate[bot]](https://github.com/apps/renovate))
- Fix ArgumentException in OpenApiCSharpCodeGenerator.Sanitize method [\#1163](https://github.com/christianhelle/apiclientcodegen/pull/1163) ([Copilot](https://github.com/apps/copilot-swe-agent))

## [1.26.0](https://github.com/christianhelle/apiclientcodegen/tree/1.26.0) (2025-05-23)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.25.0...1.26.0)

**Implemented enhancements:**

- Use .refitter file from CLI Tool [\#1168](https://github.com/christianhelle/apiclientcodegen/issues/1168)
- Allow input file to be empty when using --settings-file argument in CLI Tool [\#1173](https://github.com/christianhelle/apiclientcodegen/pull/1173) ([christianhelle](https://github.com/christianhelle))
- Add support for using `.refitter` settings files from CLI [\#1169](https://github.com/christianhelle/apiclientcodegen/pull/1169) ([Copilot](https://github.com/apps/copilot-swe-agent))
- Add TypeScript support to Visual Studio Code extension [\#1154](https://github.com/christianhelle/apiclientcodegen/pull/1154) ([christianhelle](https://github.com/christianhelle))
- Group C\# Code Generators in Visual Studio Code [\#1153](https://github.com/christianhelle/apiclientcodegen/pull/1153) ([christianhelle](https://github.com/christianhelle))

**Merged pull requests:**

- Update Microsoft Kiota package dependencies to version v1.17.3 [\#1178](https://github.com/christianhelle/apiclientcodegen/pull/1178) ([christianhelle](https://github.com/christianhelle))
- Update dependency @vscode/vsce to v3.4.2 [\#1170](https://github.com/christianhelle/apiclientcodegen/pull/1170) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/node to v22.15.21 [\#1167](https://github.com/christianhelle/apiclientcodegen/pull/1167) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency webpack to v5.99.9 [\#1166](https://github.com/christianhelle/apiclientcodegen/pull/1166) ([renovate[bot]](https://github.com/apps/renovate))
- Update vstest monorepo to 17.14.0 [\#1165](https://github.com/christianhelle/apiclientcodegen/pull/1165) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency mocha to v11.4.0 [\#1162](https://github.com/christianhelle/apiclientcodegen/pull/1162) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Moq to 4.20.72 [\#1161](https://github.com/christianhelle/apiclientcodegen/pull/1161) ([renovate[bot]](https://github.com/apps/renovate))
- Add links to other versions of the extension on the Marketplace [\#1160](https://github.com/christianhelle/apiclientcodegen/pull/1160) ([christianhelle](https://github.com/christianhelle))
- Update dependency @vscode/vsce to v3.4.1 [\#1159](https://github.com/christianhelle/apiclientcodegen/pull/1159) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/node to v22.15.19 [\#1158](https://github.com/christianhelle/apiclientcodegen/pull/1158) ([renovate[bot]](https://github.com/apps/renovate))
- Enhance rapicgen tool management and rename package [\#1156](https://github.com/christianhelle/apiclientcodegen/pull/1156) ([christianhelle](https://github.com/christianhelle))
- Update VS Code docs [\#1155](https://github.com/christianhelle/apiclientcodegen/pull/1155) ([christianhelle](https://github.com/christianhelle))
- Update dependency webpack-cli to v6 [\#1151](https://github.com/christianhelle/apiclientcodegen/pull/1151) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency node to v22 [\#1150](https://github.com/christianhelle/apiclientcodegen/pull/1150) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency mocha to v11 [\#1149](https://github.com/christianhelle/apiclientcodegen/pull/1149) ([renovate[bot]](https://github.com/apps/renovate))

## [1.25.0](https://github.com/christianhelle/apiclientcodegen/tree/1.25.0) (2025-05-16)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.24.0...1.25.0)

**Implemented enhancements:**

- Visual Studio Code Extension [\#1141](https://github.com/christianhelle/apiclientcodegen/pull/1141) ([christianhelle](https://github.com/christianhelle))
- Microsoft Kiota v1.26.1 [\#1140](https://github.com/christianhelle/apiclientcodegen/pull/1140) ([christianhelle](https://github.com/christianhelle))

**Closed issues:**

- System.MissingMethodException: Method not found: 'System.ReadOnlySpan`1\<Char\> System.Text.ValueStringBuilder.AsSpan\(\)'. [\#1132](https://github.com/christianhelle/apiclientcodegen/issues/1132)

**Merged pull requests:**

- Improve VS Code README [\#1146](https://github.com/christianhelle/apiclientcodegen/pull/1146) ([christianhelle](https://github.com/christianhelle))
- Update actions/setup-node action to v4 [\#1145](https://github.com/christianhelle/apiclientcodegen/pull/1145) ([renovate[bot]](https://github.com/apps/renovate))
- Update actions/setup-dotnet action to v4 [\#1144](https://github.com/christianhelle/apiclientcodegen/pull/1144) ([renovate[bot]](https://github.com/apps/renovate))
- Update actions/checkout action to v4 [\#1143](https://github.com/christianhelle/apiclientcodegen/pull/1143) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency @types/node to v20.17.47 [\#1142](https://github.com/christianhelle/apiclientcodegen/pull/1142) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Azure.Core to 1.46.1 [\#1136](https://github.com/christianhelle/apiclientcodegen/pull/1136) ([renovate[bot]](https://github.com/apps/renovate))
- Add support for .NET 8 and 9 to production tests workflow [\#1135](https://github.com/christianhelle/apiclientcodegen/pull/1135) ([christianhelle](https://github.com/christianhelle))
- Smoke tests [\#1133](https://github.com/christianhelle/apiclientcodegen/pull/1133) ([christianhelle](https://github.com/christianhelle))

## [1.24.0](https://github.com/christianhelle/apiclientcodegen/tree/1.24.0) (2025-05-07)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.23.0...1.24.0)

**Implemented enhancements:**

- Microsoft Kiota v1.26.0 [\#1131](https://github.com/christianhelle/apiclientcodegen/pull/1131) ([christianhelle](https://github.com/christianhelle))

**Merged pull requests:**

- Update dependency Azure.Core to 1.46.0 [\#1130](https://github.com/christianhelle/apiclientcodegen/pull/1130) ([renovate[bot]](https://github.com/apps/renovate))

## [1.23.0](https://github.com/christianhelle/apiclientcodegen/tree/1.23.0) (2025-05-05)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.22.0...1.23.0)

**Implemented enhancements:**

- Allow Overriding UserAgent Name [\#1105](https://github.com/christianhelle/apiclientcodegen/issues/1105)
- Refitter v1.5.5 [\#1129](https://github.com/christianhelle/apiclientcodegen/pull/1129) ([renovate[bot]](https://github.com/apps/renovate))
- Add option for setting HTTP User Agent in OpenAPI Generator [\#1127](https://github.com/christianhelle/apiclientcodegen/pull/1127) ([christianhelle](https://github.com/christianhelle))
- Add backward support for OpenAPI Generator v7.10.0 to v7.7.0 [\#1126](https://github.com/christianhelle/apiclientcodegen/pull/1126) ([christianhelle](https://github.com/christianhelle))
- OpenAPI Generator v7.13.0 [\#1124](https://github.com/christianhelle/apiclientcodegen/pull/1124) ([christianhelle](https://github.com/christianhelle))
- Refitter v1.5.4 [\#1123](https://github.com/christianhelle/apiclientcodegen/pull/1123) ([renovate[bot]](https://github.com/apps/renovate))
- Multiple OpenAPI Generator version support [\#1121](https://github.com/christianhelle/apiclientcodegen/pull/1121) ([christianhelle](https://github.com/christianhelle))

**Merged pull requests:**

- Update dependency xunit.runner.visualstudio to 3.1.0 [\#1128](https://github.com/christianhelle/apiclientcodegen/pull/1128) ([renovate[bot]](https://github.com/apps/renovate))
- Update nswag monorepo to 14.4.0 [\#1125](https://github.com/christianhelle/apiclientcodegen/pull/1125) ([renovate[bot]](https://github.com/apps/renovate))
- Update workflows to run on windows-latest [\#1120](https://github.com/christianhelle/apiclientcodegen/pull/1120) ([christianhelle](https://github.com/christianhelle))

## [1.22.0](https://github.com/christianhelle/apiclientcodegen/tree/1.22.0) (2025-04-08)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.21.0...1.22.0)

**Implemented enhancements:**

- Update Kiota dependencies to v1.17.2 [\#1116](https://github.com/christianhelle/apiclientcodegen/pull/1116) ([christianhelle](https://github.com/christianhelle))
- Microsoft Kiota v1.24.3 [\#1109](https://github.com/christianhelle/apiclientcodegen/pull/1109) ([christianhelle](https://github.com/christianhelle))
- Refitter v1.5.3 [\#1108](https://github.com/christianhelle/apiclientcodegen/pull/1108) ([renovate[bot]](https://github.com/apps/renovate))

**Merged pull requests:**

- Update dependency Exceptionless to 6.1.0 [\#1118](https://github.com/christianhelle/apiclientcodegen/pull/1118) ([renovate[bot]](https://github.com/apps/renovate))
- NSwag v14.3.0 [\#1107](https://github.com/christianhelle/apiclientcodegen/pull/1107) ([christianhelle](https://github.com/christianhelle))

## [1.21.0](https://github.com/christianhelle/apiclientcodegen/tree/1.21.0) (2025-03-13)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.20.0...1.21.0)

**Implemented enhancements:**

- Microsoft Kiota v1.24.0 [\#1104](https://github.com/christianhelle/apiclientcodegen/pull/1104) ([christianhelle](https://github.com/christianhelle))
- Fallback to using included OpenJDK if JAVA\_HOME doesn't exist [\#1103](https://github.com/christianhelle/apiclientcodegen/pull/1103) ([christianhelle](https://github.com/christianhelle))

## [1.20.0](https://github.com/christianhelle/apiclientcodegen/tree/1.20.0) (2025-03-03)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.19.0...1.20.0)

**Merged pull requests:**

- Update vstest monorepo to 17.13.0 [\#1100](https://github.com/christianhelle/apiclientcodegen/pull/1100) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Polly to 8.5.2 [\#1086](https://github.com/christianhelle/apiclientcodegen/pull/1086) ([renovate[bot]](https://github.com/apps/renovate))

## [1.19.0](https://github.com/christianhelle/apiclientcodegen/tree/1.19.0) (2025-01-25)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.18.0...1.19.0)

**Implemented enhancements:**

- Refitter v1.5.1 [\#1083](https://github.com/christianhelle/apiclientcodegen/pull/1083) ([christianhelle](https://github.com/christianhelle))

**Merged pull requests:**

- Update Kiota dependencies to v1.16.4 [\#1076](https://github.com/christianhelle/apiclientcodegen/pull/1076) ([christianhelle](https://github.com/christianhelle))

## [1.18.0](https://github.com/christianhelle/apiclientcodegen/tree/1.18.0) (2025-01-10)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.17.0...1.18.0)

**Implemented enhancements:**

- Microsoft Kiota v1.22.0 [\#1073](https://github.com/christianhelle/apiclientcodegen/pull/1073) ([christianhelle](https://github.com/christianhelle))

**Merged pull requests:**

- Update dependency coverlet.collector to 6.0.3 [\#1065](https://github.com/christianhelle/apiclientcodegen/pull/1065) ([renovate[bot]](https://github.com/apps/renovate))

## [1.17.0](https://github.com/christianhelle/apiclientcodegen/tree/1.17.0) (2024-12-06)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.16.0...1.17.0)

**Merged pull requests:**

- Update dependency FluentAssertions to v7 [\#1053](https://github.com/christianhelle/apiclientcodegen/pull/1053) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Xunit.SkippableFact to 1.5.23 [\#1052](https://github.com/christianhelle/apiclientcodegen/pull/1052) ([renovate[bot]](https://github.com/apps/renovate))

## [1.16.0](https://github.com/christianhelle/apiclientcodegen/tree/1.16.0) (2024-11-07)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.15.0...1.16.0)

## [1.15.0](https://github.com/christianhelle/apiclientcodegen/tree/1.15.0) (2024-10-11)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.14.0...1.15.0)

**Closed issues:**

- VS 2022, Unrecognized command or argument '--type-access-modifier'  'public. [\#1016](https://github.com/christianhelle/apiclientcodegen/issues/1016)

## [1.14.0](https://github.com/christianhelle/apiclientcodegen/tree/1.14.0) (2024-10-07)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.11.0...1.14.0)

## [1.11.0](https://github.com/christianhelle/apiclientcodegen/tree/1.11.0) (2024-09-05)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.10.9...1.11.0)

**Merged pull requests:**

- Update dependency Moq to 4.20.71 [\#972](https://github.com/christianhelle/apiclientcodegen/pull/972) ([renovate[bot]](https://github.com/apps/renovate))

## [1.10.9](https://github.com/christianhelle/apiclientcodegen/tree/1.10.9) (2024-08-19)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.10.8...1.10.9)

## [1.10.8](https://github.com/christianhelle/apiclientcodegen/tree/1.10.8) (2024-08-12)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.10.7...1.10.8)

**Fixed bugs:**

- OpenAPI - default Target Framework NET8.0 missing [\#960](https://github.com/christianhelle/apiclientcodegen/issues/960)

## [1.10.7](https://github.com/christianhelle/apiclientcodegen/tree/1.10.7) (2024-07-19)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.10.6...1.10.7)

**Closed issues:**

- Kiota sdk gen output  [\#956](https://github.com/christianhelle/apiclientcodegen/issues/956)

## [1.10.6](https://github.com/christianhelle/apiclientcodegen/tree/1.10.6) (2024-07-07)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.10.5...1.10.6)

## [1.10.5](https://github.com/christianhelle/apiclientcodegen/tree/1.10.5) (2024-07-02)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.10.4...1.10.5)

## [1.10.4](https://github.com/christianhelle/apiclientcodegen/tree/1.10.4) (2024-06-25)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.10.3...1.10.4)

## [1.10.3](https://github.com/christianhelle/apiclientcodegen/tree/1.10.3) (2024-06-07)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.10.2...1.10.3)

## [1.10.2](https://github.com/christianhelle/apiclientcodegen/tree/1.10.2) (2024-05-27)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.10.1...1.10.2)

**Implemented enhancements:**

- Refitter tracks local specification file rather than remote service specification [\#912](https://github.com/christianhelle/apiclientcodegen/issues/912)
- Support class name parameter in KiotaCodeGenerator [\#798](https://github.com/christianhelle/apiclientcodegen/issues/798)
- Add support for Kiota configuration files [\#894](https://github.com/christianhelle/apiclientcodegen/pull/894) ([christianhelle](https://github.com/christianhelle))
- Add support for generating multiples using Microsoft Kiota [\#893](https://github.com/christianhelle/apiclientcodegen/pull/893) ([christianhelle](https://github.com/christianhelle))

**Fixed bugs:**

- Unable to load VSIX package after Microsoft.VSSDK.BuildTools v17.10.2179 update [\#930](https://github.com/christianhelle/apiclientcodegen/issues/930)
- Refitter file gets overwritten [\#907](https://github.com/christianhelle/apiclientcodegen/issues/907)
- Generated .refitter file formatting issue [\#906](https://github.com/christianhelle/apiclientcodegen/issues/906)
- Kiota generates multiple files even configured not to [\#905](https://github.com/christianhelle/apiclientcodegen/issues/905)
- .refitter VS Integration is not appearing [\#889](https://github.com/christianhelle/apiclientcodegen/issues/889)
- Bad kiota installation detection [\#775](https://github.com/christianhelle/apiclientcodegen/issues/775)

**Merged pull requests:**

- Update documentation [\#895](https://github.com/christianhelle/apiclientcodegen/pull/895) ([christianhelle](https://github.com/christianhelle))
- Bump Azure.Identity and Azure.Core [\#892](https://github.com/christianhelle/apiclientcodegen/pull/892) ([dependabot[bot]](https://github.com/apps/dependabot))
- Bump Microsoft.Kiota.Http.HttpClientLibrary, System.Text.Json and System.Text.Encodings.Web [\#891](https://github.com/christianhelle/apiclientcodegen/pull/891) ([dependabot[bot]](https://github.com/apps/dependabot))

## [1.10.1](https://github.com/christianhelle/apiclientcodegen/tree/1.10.1) (2024-05-07)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.10.0...1.10.1)

**Implemented enhancements:**

- Implement a Refitter Settings File as part of generation process [\#845](https://github.com/christianhelle/apiclientcodegen/issues/845)
- Implement option for generating multiple files [\#175](https://github.com/christianhelle/apiclientcodegen/issues/175)

## [1.10.0](https://github.com/christianhelle/apiclientcodegen/tree/1.10.0) (2024-05-04)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.9.8...1.10.0)

**Implemented enhancements:**

- Added kiota version detection and its installation if it's not installed [\#832](https://github.com/christianhelle/apiclientcodegen/pull/832) ([Hiller](https://github.com/Hiller))

**Fixed bugs:**

- Rifitter =\> Refitter typo [\#844](https://github.com/christianhelle/apiclientcodegen/issues/844)
- Half of the provided client generators don't work but show an error [\#836](https://github.com/christianhelle/apiclientcodegen/issues/836)

**Merged pull requests:**

- Bump Azure.Identity and Azure.Core [\#843](https://github.com/christianhelle/apiclientcodegen/pull/843) ([dependabot[bot]](https://github.com/apps/dependabot))
- Bump Microsoft.Kiota.Abstractions from 1.7.12 to 1.8.1 [\#835](https://github.com/christianhelle/apiclientcodegen/pull/835) ([dependabot[bot]](https://github.com/apps/dependabot))
- Update nswag monorepo to v14.0.7 [\#834](https://github.com/christianhelle/apiclientcodegen/pull/834) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Microsoft.VSSDK.BuildTools to v17.9.3174 [\#830](https://github.com/christianhelle/apiclientcodegen/pull/830) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency dotnet-sdk to v6.0.420 [\#829](https://github.com/christianhelle/apiclientcodegen/pull/829) ([renovate[bot]](https://github.com/apps/renovate))
- Bump coverlet.collector from 6.0.1 to 6.0.2 [\#828](https://github.com/christianhelle/apiclientcodegen/pull/828) ([dependabot[bot]](https://github.com/apps/dependabot))
- Bump Microsoft.Kiota.Abstractions from 1.7.11 to 1.7.12 [\#826](https://github.com/christianhelle/apiclientcodegen/pull/826) ([dependabot[bot]](https://github.com/apps/dependabot))

## [1.9.8](https://github.com/christianhelle/apiclientcodegen/tree/1.9.8) (2024-03-11)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.9.7...1.9.8)

**Implemented enhancements:**

- OpenAPI Generator v7.4.0 [\#825](https://github.com/christianhelle/apiclientcodegen/pull/825) ([christianhelle](https://github.com/christianhelle))

## [1.9.7](https://github.com/christianhelle/apiclientcodegen/tree/1.9.7) (2024-03-10)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.9.6...1.9.7)

**Implemented enhancements:**

- Update Microsoft Kiota to v1.12.0 [\#824](https://github.com/christianhelle/apiclientcodegen/pull/824) ([christianhelle](https://github.com/christianhelle))

**Merged pull requests:**

- Update dependency Refitter.Core to v0.9.9 [\#823](https://github.com/christianhelle/apiclientcodegen/pull/823) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency Polly to v8.3.1 [\#822](https://github.com/christianhelle/apiclientcodegen/pull/822) ([renovate[bot]](https://github.com/apps/renovate))
- Bump Microsoft.Kiota.Serialization.Json, System.Text.Json and System.Text.Encodings.Web [\#821](https://github.com/christianhelle/apiclientcodegen/pull/821) ([dependabot[bot]](https://github.com/apps/dependabot))
- Bump Microsoft.Kiota.Serialization.Text from 1.1.3 to 1.1.4 [\#820](https://github.com/christianhelle/apiclientcodegen/pull/820) ([dependabot[bot]](https://github.com/apps/dependabot))
- Bump Microsoft.Kiota.Serialization.Form from 1.1.4 to 1.1.5 [\#819](https://github.com/christianhelle/apiclientcodegen/pull/819) ([dependabot[bot]](https://github.com/apps/dependabot))
- Update dependency Refitter.Core to v0.9.8 [\#818](https://github.com/christianhelle/apiclientcodegen/pull/818) ([renovate[bot]](https://github.com/apps/renovate))
- Bump Microsoft.Kiota.Http.HttpClientLibrary, System.Text.Json and System.Text.Encodings.Web [\#817](https://github.com/christianhelle/apiclientcodegen/pull/817) ([dependabot[bot]](https://github.com/apps/dependabot))
- Bump Microsoft.Kiota.Serialization.Multipart from 1.1.2 to 1.1.3 [\#816](https://github.com/christianhelle/apiclientcodegen/pull/816) ([dependabot[bot]](https://github.com/apps/dependabot))
- Bump Microsoft.Kiota.Abstractions from 1.7.10 to 1.7.11 [\#815](https://github.com/christianhelle/apiclientcodegen/pull/815) ([dependabot[bot]](https://github.com/apps/dependabot))
- Bump Microsoft.Kiota.Serialization.Form from 1.1.3 to 1.1.4 [\#814](https://github.com/christianhelle/apiclientcodegen/pull/814) ([dependabot[bot]](https://github.com/apps/dependabot))
- Bump Microsoft.Kiota.Serialization.Text from 1.1.2 to 1.1.3 [\#813](https://github.com/christianhelle/apiclientcodegen/pull/813) ([dependabot[bot]](https://github.com/apps/dependabot))
- Bump Microsoft.Kiota.Authentication.Azure and Azure.Core [\#812](https://github.com/christianhelle/apiclientcodegen/pull/812) ([dependabot[bot]](https://github.com/apps/dependabot))
- Bump Microsoft.Kiota.Serialization.Json, Microsoft.Kiota.Abstractions, System.Text.Json and System.Text.Encodings.Web [\#808](https://github.com/christianhelle/apiclientcodegen/pull/808) ([dependabot[bot]](https://github.com/apps/dependabot))
- Update dependency coverlet.collector to v6.0.1 [\#806](https://github.com/christianhelle/apiclientcodegen/pull/806) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency McMaster.Extensions.Hosting.CommandLine to v4.1.1 [\#805](https://github.com/christianhelle/apiclientcodegen/pull/805) ([renovate[bot]](https://github.com/apps/renovate))
- Update xunit-dotnet monorepo [\#803](https://github.com/christianhelle/apiclientcodegen/pull/803) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency dotnet-sdk to v6.0.419 [\#802](https://github.com/christianhelle/apiclientcodegen/pull/802) ([renovate[bot]](https://github.com/apps/renovate))

## [1.9.6](https://github.com/christianhelle/apiclientcodegen/tree/1.9.6) (2024-02-08)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.9.5...1.9.6)

**Implemented enhancements:**

- OpenAPI Generator v7.3.0 [\#799](https://github.com/christianhelle/apiclientcodegen/pull/799) ([christianhelle](https://github.com/christianhelle))
- Refitter v0.9.7 [\#796](https://github.com/christianhelle/apiclientcodegen/pull/796) ([christianhelle](https://github.com/christianhelle))
- Microsoft Kiota v1.11.1 [\#793](https://github.com/christianhelle/apiclientcodegen/pull/793) ([christianhelle](https://github.com/christianhelle))
- NSwag v14.0.3 [\#779](https://github.com/christianhelle/apiclientcodegen/pull/779) ([renovate[bot]](https://github.com/apps/renovate))

**Closed issues:**

- Visual Studio freezes when generating client api  [\#776](https://github.com/christianhelle/apiclientcodegen/issues/776)

**Merged pull requests:**

- Update dependency Microsoft.VSSDK.BuildTools to v17.9.3168 [\#797](https://github.com/christianhelle/apiclientcodegen/pull/797) ([renovate[bot]](https://github.com/apps/renovate))
- Bump Microsoft.Kiota.Serialization.Form from 1.1.2 to 1.1.3 [\#794](https://github.com/christianhelle/apiclientcodegen/pull/794) ([dependabot[bot]](https://github.com/apps/dependabot))
- Update dependency dotnet-sdk to v6.0.418 [\#752](https://github.com/christianhelle/apiclientcodegen/pull/752) ([renovate[bot]](https://github.com/apps/renovate))

## [1.9.5](https://github.com/christianhelle/apiclientcodegen/tree/1.9.5) (2024-02-02)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.9.4...1.9.5)

**Implemented enhancements:**

- Microsoft Kiota v1.11.0 [\#778](https://github.com/christianhelle/apiclientcodegen/pull/778) ([christianhelle](https://github.com/christianhelle))

**Merged pull requests:**

- Bump Microsoft.Kiota.Abstractions from 1.7.6 to 1.7.7 [\#777](https://github.com/christianhelle/apiclientcodegen/pull/777) ([dependabot[bot]](https://github.com/apps/dependabot))
- Bump Microsoft.Kiota.Serialization.Form from 1.1.1 to 1.1.2 [\#773](https://github.com/christianhelle/apiclientcodegen/pull/773) ([dependabot[bot]](https://github.com/apps/dependabot))
- Bump Microsoft.Kiota.Serialization.Text from 1.1.1 to 1.1.2 [\#772](https://github.com/christianhelle/apiclientcodegen/pull/772) ([dependabot[bot]](https://github.com/apps/dependabot))
- chore\(deps\): update dependency refitter.core to v0.9.5 [\#764](https://github.com/christianhelle/apiclientcodegen/pull/764) ([renovate[bot]](https://github.com/apps/renovate))
- chore\(deps\): update dependency xunit to v2.6.6 [\#763](https://github.com/christianhelle/apiclientcodegen/pull/763) ([renovate[bot]](https://github.com/apps/renovate))
- Bump Microsoft.Kiota.Http.HttpClientLibrary, System.Text.Json and System.Text.Encodings.Web [\#754](https://github.com/christianhelle/apiclientcodegen/pull/754) ([dependabot[bot]](https://github.com/apps/dependabot))

## [1.9.4](https://github.com/christianhelle/apiclientcodegen/tree/1.9.4) (2024-01-13)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.9.3...1.9.4)

**Merged pull requests:**

- Upgrade Microsoft Kiota to v1.10.1 [\#761](https://github.com/christianhelle/apiclientcodegen/pull/761) ([christianhelle](https://github.com/christianhelle))
- Bump Microsoft.Kiota.Abstractions from 1.7.3 to 1.7.4 [\#755](https://github.com/christianhelle/apiclientcodegen/pull/755) ([dependabot[bot]](https://github.com/apps/dependabot))

## [1.9.3](https://github.com/christianhelle/apiclientcodegen/tree/1.9.3) (2024-01-10)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.9.2...1.9.3)

**Merged pull requests:**

- Bump Polly from 8.2.0 to 8.2.1 [\#743](https://github.com/christianhelle/apiclientcodegen/pull/743) ([dependabot[bot]](https://github.com/apps/dependabot))
- Update nuget monorepo [\#742](https://github.com/christianhelle/apiclientcodegen/pull/742) ([renovate[bot]](https://github.com/apps/renovate))
- Update dependency xunit to v2.6.5 [\#740](https://github.com/christianhelle/apiclientcodegen/pull/740) ([renovate[bot]](https://github.com/apps/renovate))

## [1.9.2](https://github.com/christianhelle/apiclientcodegen/tree/1.9.2) (2023-12-24)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.9.1...1.9.2)

**Implemented enhancements:**

- Upgrade OpenAPI Generator to v7.2.0 [\#738](https://github.com/christianhelle/apiclientcodegen/pull/738) ([christianhelle](https://github.com/christianhelle))

**Closed issues:**

- \[Readme\] Mention refitter and kiota in feature summary [\#735](https://github.com/christianhelle/apiclientcodegen/issues/735)

**Merged pull requests:**

- Update xunit-dotnet monorepo [\#737](https://github.com/christianhelle/apiclientcodegen/pull/737) ([renovate[bot]](https://github.com/apps/renovate))

## [1.9.1](https://github.com/christianhelle/apiclientcodegen/tree/1.9.1) (2023-12-14)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.9.0...1.9.1)

## [1.9.0](https://github.com/christianhelle/apiclientcodegen/tree/1.9.0) (2023-12-11)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.8.10...1.9.0)

**Fixed bugs:**

- Marketplace Review link in the Add New REST API Client dialog blocks status messages [\#696](https://github.com/christianhelle/apiclientcodegen/issues/696)

## [1.8.10](https://github.com/christianhelle/apiclientcodegen/tree/1.8.10) (2023-11-13)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.8.9...1.8.10)

## [1.8.9](https://github.com/christianhelle/apiclientcodegen/tree/1.8.9) (2023-11-10)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.8.8...1.8.9)

## [1.8.8](https://github.com/christianhelle/apiclientcodegen/tree/1.8.8) (2023-11-05)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.8.7...1.8.8)

## [1.8.7](https://github.com/christianhelle/apiclientcodegen/tree/1.8.7) (2023-10-06)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.8.6...1.8.7)

## [1.8.6](https://github.com/christianhelle/apiclientcodegen/tree/1.8.6) (2023-09-22)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.8.5...1.8.6)

## [1.8.5](https://github.com/christianhelle/apiclientcodegen/tree/1.8.5) (2023-09-11)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.8.4...1.8.5)

## [1.8.4](https://github.com/christianhelle/apiclientcodegen/tree/1.8.4) (2023-09-09)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.8.3...1.8.4)

**Implemented enhancements:**

- Automatically add -c command option for OpenApiGenerator [\#610](https://github.com/christianhelle/apiclientcodegen/issues/610)

## [1.8.3](https://github.com/christianhelle/apiclientcodegen/tree/1.8.3) (2023-09-04)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.8.2...1.8.3)

**Fixed bugs:**

- Getting garbled/binary content when attempting to download JSON definition file \(publicly available URL\) [\#620](https://github.com/christianhelle/apiclientcodegen/issues/620)

## [1.8.2](https://github.com/christianhelle/apiclientcodegen/tree/1.8.2) (2023-08-26)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.8.1...1.8.2)

## [1.8.1](https://github.com/christianhelle/apiclientcodegen/tree/1.8.1) (2023-08-09)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.8.0...1.8.1)

## [1.8.0](https://github.com/christianhelle/apiclientcodegen/tree/1.8.0) (2023-08-04)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.7.18...1.8.0)

**Fixed bugs:**

- OpenAPI FormUrlEncodedContent [\#585](https://github.com/christianhelle/apiclientcodegen/issues/585)

## [1.7.18](https://github.com/christianhelle/apiclientcodegen/tree/1.7.18) (2023-07-15)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.7.17...1.7.18)

## [1.7.17](https://github.com/christianhelle/apiclientcodegen/tree/1.7.17) (2023-06-17)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.7.16...1.7.17)

## [1.7.16](https://github.com/christianhelle/apiclientcodegen/tree/1.7.16) (2023-06-11)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.7.15...1.7.16)

## [1.7.15](https://github.com/christianhelle/apiclientcodegen/tree/1.7.15) (2023-05-17)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.7.14...1.7.15)

## [1.7.14](https://github.com/christianhelle/apiclientcodegen/tree/1.7.14) (2023-05-12)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.7.13...1.7.14)

## [1.7.13](https://github.com/christianhelle/apiclientcodegen/tree/1.7.13) (2023-04-30)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.7.12...1.7.13)

## [1.7.12](https://github.com/christianhelle/apiclientcodegen/tree/1.7.12) (2023-04-19)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.7.11...1.7.12)

## [1.7.11](https://github.com/christianhelle/apiclientcodegen/tree/1.7.11) (2023-04-18)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.7.10...1.7.11)

## [1.7.10](https://github.com/christianhelle/apiclientcodegen/tree/1.7.10) (2023-04-18)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.7.9...1.7.10)

**Implemented enhancements:**

- Windows ARM64 not supported [\#498](https://github.com/christianhelle/apiclientcodegen/issues/498)
- Update to Microsoft Kiota to v1.1.2 [\#524](https://github.com/christianhelle/apiclientcodegen/pull/524) ([christianhelle](https://github.com/christianhelle))

**Merged pull requests:**

- Bump Microsoft.Kiota.Http.HttpClientLibrary from 1.0.1 to 1.0.2 [\#526](https://github.com/christianhelle/apiclientcodegen/pull/526) ([dependabot[bot]](https://github.com/apps/dependabot))
- Bump Microsoft.Kiota.Abstractions from 1.1.0 to 1.1.1 [\#525](https://github.com/christianhelle/apiclientcodegen/pull/525) ([dependabot[bot]](https://github.com/apps/dependabot))

## [1.7.9](https://github.com/christianhelle/apiclientcodegen/tree/1.7.9) (2023-04-07)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.7.8...1.7.9)

## [1.7.8](https://github.com/christianhelle/apiclientcodegen/tree/1.7.8) (2023-04-02)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.7.7...1.7.8)

## [1.7.7](https://github.com/christianhelle/apiclientcodegen/tree/1.7.7) (2023-03-19)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.7.3...1.7.7)

## [1.7.3](https://github.com/christianhelle/apiclientcodegen/tree/1.7.3) (2023-03-09)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.7.2...1.7.3)

## [1.7.2](https://github.com/christianhelle/apiclientcodegen/tree/1.7.2) (2023-03-07)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.7.1...1.7.2)

**Implemented enhancements:**

- Support for Refit [\#467](https://github.com/christianhelle/apiclientcodegen/issues/467)

**Fixed bugs:**

- Extensions stopped working in Visual Studio for Mac 2022 [\#490](https://github.com/christianhelle/apiclientcodegen/issues/490)

## [1.7.1](https://github.com/christianhelle/apiclientcodegen/tree/1.7.1) (2023-03-01)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.7.0...1.7.1)

## [1.7.0](https://github.com/christianhelle/apiclientcodegen/tree/1.7.0) (2023-02-27)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.6.98...1.7.0)

## [1.6.98](https://github.com/christianhelle/apiclientcodegen/tree/1.6.98) (2023-02-19)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.6.97...1.6.98)

## [1.6.97](https://github.com/christianhelle/apiclientcodegen/tree/1.6.97) (2023-02-13)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.6.96...1.6.97)

## [1.6.96](https://github.com/christianhelle/apiclientcodegen/tree/1.6.96) (2023-02-08)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.6.95...1.6.96)

**Fixed bugs:**

- MAUI Projects crash when opening solution explorer context menu [\#449](https://github.com/christianhelle/apiclientcodegen/issues/449)

## [1.6.95](https://github.com/christianhelle/apiclientcodegen/tree/1.6.95) (2023-02-07)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.6.94...1.6.95)

## [1.6.94](https://github.com/christianhelle/apiclientcodegen/tree/1.6.94) (2023-02-03)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.6.93...1.6.94)

## [1.6.93](https://github.com/christianhelle/apiclientcodegen/tree/1.6.93) (2023-02-02)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.6.92...1.6.93)

**Fixed bugs:**

- Sometimes a NullReference occurs when loading Visual Studio [\#468](https://github.com/christianhelle/apiclientcodegen/issues/468)

## [1.6.92](https://github.com/christianhelle/apiclientcodegen/tree/1.6.92) (2023-01-28)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.6.91...1.6.92)

## [1.6.91](https://github.com/christianhelle/apiclientcodegen/tree/1.6.91) (2023-01-27)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.6.88...1.6.91)

## [1.6.88](https://github.com/christianhelle/apiclientcodegen/tree/1.6.88) (2023-01-18)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.6.86...1.6.88)

## [1.6.86](https://github.com/christianhelle/apiclientcodegen/tree/1.6.86) (2023-01-05)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.5.84...1.6.86)

**Merged pull requests:**

- Bump Moq from 4.18.3 to 4.18.4 [\#459](https://github.com/christianhelle/apiclientcodegen/pull/459) ([dependabot[bot]](https://github.com/apps/dependabot))

## [1.5.84](https://github.com/christianhelle/apiclientcodegen/tree/1.5.84) (2023-01-05)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.5.0.2352...1.5.84)

## [1.5.0.2352](https://github.com/christianhelle/apiclientcodegen/tree/1.5.0.2352) (2023-01-05)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.6.80...1.5.0.2352)

## [1.6.80](https://github.com/christianhelle/apiclientcodegen/tree/1.6.80) (2022-12-29)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.6.79...1.6.80)

**Fixed bugs:**

- Fix crash on MAUI projects [\#458](https://github.com/christianhelle/apiclientcodegen/pull/458) ([christianhelle](https://github.com/christianhelle))
- Fix Visual Studio 2022 crash when used in MAUI projects [\#457](https://github.com/christianhelle/apiclientcodegen/pull/457) ([christianhelle](https://github.com/christianhelle))

## [1.6.79](https://github.com/christianhelle/apiclientcodegen/tree/1.6.79) (2022-12-17)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.6.78...1.6.79)

## [1.6.78](https://github.com/christianhelle/apiclientcodegen/tree/1.6.78) (2022-11-28)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.5.72...1.6.78)

**Fixed bugs:**

- Recent release causing errors in 'Build' stage [\#437](https://github.com/christianhelle/apiclientcodegen/issues/437)

## [1.5.72](https://github.com/christianhelle/apiclientcodegen/tree/1.5.72) (2022-11-01)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.5.71...1.5.72)

**Implemented enhancements:**

- Missing way to set ParameterDateTimeFormat for NSwag [\#420](https://github.com/christianhelle/apiclientcodegen/issues/420)

## [1.5.71](https://github.com/christianhelle/apiclientcodegen/tree/1.5.71) (2022-10-28)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.5.70...1.5.71)

**Implemented enhancements:**

- Missing way to set skipFormModel for OpenApi Generator [\#421](https://github.com/christianhelle/apiclientcodegen/issues/421)

## [1.5.70](https://github.com/christianhelle/apiclientcodegen/tree/1.5.70) (2022-10-21)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.5.69...1.5.70)

## [1.5.69](https://github.com/christianhelle/apiclientcodegen/tree/1.5.69) (2022-10-09)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.5.67...1.5.69)

**Merged pull requests:**

- Resolve bugs detected by SonarCloud [\#418](https://github.com/christianhelle/apiclientcodegen/pull/418) ([christianhelle](https://github.com/christianhelle))
- Bump Microsoft.NET.Test.Sdk from 17.3.1 to 17.3.2 [\#411](https://github.com/christianhelle/apiclientcodegen/pull/411) ([dependabot[bot]](https://github.com/apps/dependabot))
- Bump NSwag.CodeGeneration.CSharp from 13.16.1 to 13.17.0 [\#409](https://github.com/christianhelle/apiclientcodegen/pull/409) ([dependabot[bot]](https://github.com/apps/dependabot))
- Bump NSwag.Core.Yaml from 13.16.1 to 13.17.0 [\#408](https://github.com/christianhelle/apiclientcodegen/pull/408) ([dependabot[bot]](https://github.com/apps/dependabot))
- Bump Sentry from 3.20.1 to 3.21.0 [\#407](https://github.com/christianhelle/apiclientcodegen/pull/407) ([dependabot[bot]](https://github.com/apps/dependabot))

## [1.5.67](https://github.com/christianhelle/apiclientcodegen/tree/1.5.67) (2022-09-01)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.5.66...1.5.67)

**Merged pull requests:**

- Track dependency failures and execution performance anonymously [\#406](https://github.com/christianhelle/apiclientcodegen/pull/406) ([christianhelle](https://github.com/christianhelle))
- Bump Microsoft.NET.Test.Sdk from 17.3.0 to 17.3.1 [\#405](https://github.com/christianhelle/apiclientcodegen/pull/405) ([dependabot[bot]](https://github.com/apps/dependabot))
- Bump ICSharpCode.CodeConverter from 9.0.3.482 to 9.0.4.493 [\#404](https://github.com/christianhelle/apiclientcodegen/pull/404) ([dependabot[bot]](https://github.com/apps/dependabot))

## [1.5.66](https://github.com/christianhelle/apiclientcodegen/tree/1.5.66) (2022-08-29)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.5.64...1.5.66)

**Merged pull requests:**

- Bump Microsoft.VSSDK.BuildTools from 17.3.2093 to 17.3.2094 [\#403](https://github.com/christianhelle/apiclientcodegen/pull/403) ([dependabot[bot]](https://github.com/apps/dependabot))
- Track Dependency Failures in Application Insights [\#402](https://github.com/christianhelle/apiclientcodegen/pull/402) ([christianhelle](https://github.com/christianhelle))

## [1.5.64](https://github.com/christianhelle/apiclientcodegen/tree/1.5.64) (2022-08-24)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.5.63...1.5.64)

**Merged pull requests:**

- Add CLI Tool support for OpenAPI Generator pass through commands [\#401](https://github.com/christianhelle/apiclientcodegen/pull/401) ([christianhelle](https://github.com/christianhelle))

## [1.5.63](https://github.com/christianhelle/apiclientcodegen/tree/1.5.63) (2022-08-20)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.5.62...1.5.63)

**Implemented enhancements:**

- Restructure CLI Tool commands [\#400](https://github.com/christianhelle/apiclientcodegen/pull/400) ([christianhelle](https://github.com/christianhelle))

## [1.5.62](https://github.com/christianhelle/apiclientcodegen/tree/1.5.62) (2022-08-18)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.5.60...1.5.62)

## [1.5.60](https://github.com/christianhelle/apiclientcodegen/tree/1.5.60) (2022-08-12)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.5.59...1.5.60)

**Merged pull requests:**

- Bump Microsoft.NET.Test.Sdk from 17.2.0 to 17.3.0 [\#399](https://github.com/christianhelle/apiclientcodegen/pull/399) ([dependabot[bot]](https://github.com/apps/dependabot))
- Bump Microsoft.VSSDK.BuildTools from 17.3.2092 to 17.3.2093 [\#398](https://github.com/christianhelle/apiclientcodegen/pull/398) ([dependabot[bot]](https://github.com/apps/dependabot))

## [1.5.59](https://github.com/christianhelle/apiclientcodegen/tree/1.5.59) (2022-08-10)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.5.51...1.5.59)

**Merged pull requests:**

- Bump Microsoft.VSSDK.BuildTools from 17.3.2090 to 17.3.2092 [\#397](https://github.com/christianhelle/apiclientcodegen/pull/397) ([dependabot[bot]](https://github.com/apps/dependabot))
- Add CLI Tool support for generating JMeter test plans [\#396](https://github.com/christianhelle/apiclientcodegen/pull/396) ([christianhelle](https://github.com/christianhelle))

## [1.5.51](https://github.com/christianhelle/apiclientcodegen/tree/1.5.51) (2022-08-05)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.5.50...1.5.51)

**Closed issues:**

- Cannot generate .cs file  [\#388](https://github.com/christianhelle/apiclientcodegen/issues/388)

**Merged pull requests:**

- Bump ICSharpCode.CodeConverter from 9.0.2.451 to 9.0.3.482 [\#395](https://github.com/christianhelle/apiclientcodegen/pull/395) ([dependabot[bot]](https://github.com/apps/dependabot))
- Bump Moq from 4.18.1 to 4.18.2 [\#394](https://github.com/christianhelle/apiclientcodegen/pull/394) ([dependabot[bot]](https://github.com/apps/dependabot))
- Bump xunit from 2.4.1 to 2.4.2 [\#393](https://github.com/christianhelle/apiclientcodegen/pull/393) ([dependabot[bot]](https://github.com/apps/dependabot))
- Bump Sentry from 3.19.0 to 3.20.0 [\#390](https://github.com/christianhelle/apiclientcodegen/pull/390) ([dependabot[bot]](https://github.com/apps/dependabot))

## [1.5.50](https://github.com/christianhelle/apiclientcodegen/tree/1.5.50) (2022-07-04)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.5.49...1.5.50)

**Merged pull requests:**

- Bump Microsoft.VSSDK.BuildTools from 17.3.2083 to 17.3.2088 [\#387](https://github.com/christianhelle/apiclientcodegen/pull/387) ([dependabot[bot]](https://github.com/apps/dependabot))
- Bump ICSharpCode.CodeConverter from 9.0.0.415 to 9.0.2.451 [\#386](https://github.com/christianhelle/apiclientcodegen/pull/386) ([dependabot[bot]](https://github.com/apps/dependabot))

## [1.5.49](https://github.com/christianhelle/apiclientcodegen/tree/1.5.49) (2022-06-18)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.5.48...1.5.49)

## [1.5.48](https://github.com/christianhelle/apiclientcodegen/tree/1.5.48) (2022-06-06)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.5.47...1.5.48)

**Implemented enhancements:**

- Possibility to choose OpenAPI client generator and configure generator specific additional-properties [\#361](https://github.com/christianhelle/apiclientcodegen/issues/361)
- New OpenAPI Generator options [\#378](https://github.com/christianhelle/apiclientcodegen/pull/378) ([christianhelle](https://github.com/christianhelle))

## [1.5.47](https://github.com/christianhelle/apiclientcodegen/tree/1.5.47) (2022-03-04)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.5.46...1.5.47)

## [1.5.46](https://github.com/christianhelle/apiclientcodegen/tree/1.5.46) (2022-02-17)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.5.45...1.5.46)

## [1.5.45](https://github.com/christianhelle/apiclientcodegen/tree/1.5.45) (2022-02-11)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.5.44...1.5.45)

## [1.5.44](https://github.com/christianhelle/apiclientcodegen/tree/1.5.44) (2022-01-23)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.4.42...1.5.44)

## [1.4.42](https://github.com/christianhelle/apiclientcodegen/tree/1.4.42) (2022-01-18)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.4.41...1.4.42)

## [1.4.41](https://github.com/christianhelle/apiclientcodegen/tree/1.4.41) (2021-12-26)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.4.39...1.4.41)

## [1.4.39](https://github.com/christianhelle/apiclientcodegen/tree/1.4.39) (2021-12-18)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.4.38...1.4.39)

## [1.4.38](https://github.com/christianhelle/apiclientcodegen/tree/1.4.38) (2021-12-11)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.4.37...1.4.38)

## [1.4.37](https://github.com/christianhelle/apiclientcodegen/tree/1.4.37) (2021-12-01)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.4.36...1.4.37)

## [1.4.36](https://github.com/christianhelle/apiclientcodegen/tree/1.4.36) (2021-11-29)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.4.35...1.4.36)

## [1.4.35](https://github.com/christianhelle/apiclientcodegen/tree/1.4.35) (2021-11-26)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.4.34...1.4.35)

## [1.4.34](https://github.com/christianhelle/apiclientcodegen/tree/1.4.34) (2021-11-20)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.4.33...1.4.34)

## [1.4.33](https://github.com/christianhelle/apiclientcodegen/tree/1.4.33) (2021-11-16)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.4.32...1.4.33)

## [1.4.32](https://github.com/christianhelle/apiclientcodegen/tree/1.4.32) (2021-11-15)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.4.30...1.4.32)

## [1.4.30](https://github.com/christianhelle/apiclientcodegen/tree/1.4.30) (2021-10-22)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.3.7984...1.4.30)

## [1.3.7984](https://github.com/christianhelle/apiclientcodegen/tree/1.3.7984) (2021-09-27)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.3.8003...1.3.7984)

## [1.3.8003](https://github.com/christianhelle/apiclientcodegen/tree/1.3.8003) (2021-09-27)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.3.7741...1.3.8003)

**Closed issues:**

- Update NuGet packages to support VS2022 [\#229](https://github.com/christianhelle/apiclientcodegen/issues/229)
- Update VSIX manifest to support VS2022 [\#228](https://github.com/christianhelle/apiclientcodegen/issues/228)

## [1.3.7741](https://github.com/christianhelle/apiclientcodegen/tree/1.3.7741) (2021-08-29)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.3.7613...1.3.7741)

**Implemented enhancements:**

- \[OpenAPI Generator\] Add option to generate code with EmitDefaultValue=false [\#246](https://github.com/christianhelle/apiclientcodegen/issues/246)

## [1.3.7613](https://github.com/christianhelle/apiclientcodegen/tree/1.3.7613) (2021-08-15)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.2.7536...1.3.7613)

## [1.2.7536](https://github.com/christianhelle/apiclientcodegen/tree/1.2.7536) (2021-08-07)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.2.7037...1.2.7536)

## [1.2.7037](https://github.com/christianhelle/apiclientcodegen/tree/1.2.7037) (2021-06-08)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.2.6859...1.2.7037)

## [1.2.6859](https://github.com/christianhelle/apiclientcodegen/tree/1.2.6859) (2021-05-20)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.2.6685...1.2.6859)

## [1.2.6685](https://github.com/christianhelle/apiclientcodegen/tree/1.2.6685) (2021-04-30)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.2.6442...1.2.6685)

## [1.2.6442](https://github.com/christianhelle/apiclientcodegen/tree/1.2.6442) (2021-04-13)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.2.6359...1.2.6442)

## [1.2.6359](https://github.com/christianhelle/apiclientcodegen/tree/1.2.6359) (2021-04-05)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.2.6222...1.2.6359)

## [1.2.6222](https://github.com/christianhelle/apiclientcodegen/tree/1.2.6222) (2021-03-27)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.2.6153...1.2.6222)

## [1.2.6153](https://github.com/christianhelle/apiclientcodegen/tree/1.2.6153) (2021-03-20)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.2.5901...1.2.6153)

**Implemented enhancements:**

- Implement support for AutoRest Configuration Files [\#160](https://github.com/christianhelle/apiclientcodegen/issues/160)

**Fixed bugs:**

- Missing AutoRest v3 package references when using "Add New REST API Client" command [\#176](https://github.com/christianhelle/apiclientcodegen/issues/176)

## [1.2.5901](https://github.com/christianhelle/apiclientcodegen/tree/1.2.5901) (2021-02-28)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.4934...1.2.5901)

**Implemented enhancements:**

- Implement support AutoRest v3 [\#164](https://github.com/christianhelle/apiclientcodegen/issues/164)

**Fixed bugs:**

- Visual Studio Extension background loading incorrectly setup [\#173](https://github.com/christianhelle/apiclientcodegen/issues/173)
- AutoRest stopped working [\#156](https://github.com/christianhelle/apiclientcodegen/issues/156)

## [1.1.4934](https://github.com/christianhelle/apiclientcodegen/tree/1.1.4934) (2021-01-09)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.4697...1.1.4934)

## [1.1.4697](https://github.com/christianhelle/apiclientcodegen/tree/1.1.4697) (2020-12-25)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.4559...1.1.4697)

## [1.1.4559](https://github.com/christianhelle/apiclientcodegen/tree/1.1.4559) (2020-12-16)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.4470...1.1.4559)

## [1.1.4470](https://github.com/christianhelle/apiclientcodegen/tree/1.1.4470) (2020-12-11)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.4142...1.1.4470)

**Implemented enhancements:**

- New version of the Restsharp changed the signature of Addfile parameter [\#93](https://github.com/christianhelle/apiclientcodegen/issues/93)

## [1.1.4142](https://github.com/christianhelle/apiclientcodegen/tree/1.1.4142) (2020-12-03)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.3189...1.1.4142)

**Fixed bugs:**

- Extension stopped working on older versions of Visual Studio, but works on the latest 2019 Preview [\#98](https://github.com/christianhelle/apiclientcodegen/issues/98)
- The "OpenApiReference" rule is missing the "Generator" property. [\#63](https://github.com/christianhelle/apiclientcodegen/issues/63)

**Closed issues:**

- BasePath in default constructor will cause exception [\#92](https://github.com/christianhelle/apiclientcodegen/issues/92)

## [1.1.3189](https://github.com/christianhelle/apiclientcodegen/tree/1.1.3189) (2020-11-11)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.3147...1.1.3189)

**Fixed bugs:**

- Plugin is not working in VS 2017 [\#97](https://github.com/christianhelle/apiclientcodegen/issues/97)

## [1.1.3147](https://github.com/christianhelle/apiclientcodegen/tree/1.1.3147) (2020-11-07)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.3012...1.1.3147)

## [1.1.3012](https://github.com/christianhelle/apiclientcodegen/tree/1.1.3012) (2020-10-31)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.2795...1.1.3012)

**Fixed bugs:**

- Visual Studio for Mac build fails on hosted MacOS 10.15 [\#95](https://github.com/christianhelle/apiclientcodegen/issues/95)

## [1.1.2795](https://github.com/christianhelle/apiclientcodegen/tree/1.1.2795) (2020-10-20)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.2631...1.1.2795)

## [1.1.2631](https://github.com/christianhelle/apiclientcodegen/tree/1.1.2631) (2020-10-11)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.2531...1.1.2631)

## [1.1.2531](https://github.com/christianhelle/apiclientcodegen/tree/1.1.2531) (2020-09-23)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.2400...1.1.2531)

## [1.1.2400](https://github.com/christianhelle/apiclientcodegen/tree/1.1.2400) (2020-09-02)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.2394...1.1.2400)

## [1.1.2394](https://github.com/christianhelle/apiclientcodegen/tree/1.1.2394) (2020-09-01)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.2301...1.1.2394)

**Implemented enhancements:**

- Add support for OpenAPI Specification YAML files \(Visual Studio for Mac\) [\#89](https://github.com/christianhelle/apiclientcodegen/issues/89)

## [1.1.2301](https://github.com/christianhelle/apiclientcodegen/tree/1.1.2301) (2020-08-17)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.2039...1.1.2301)

**Implemented enhancements:**

- Add support for OpenAPI Specification YAML files [\#87](https://github.com/christianhelle/apiclientcodegen/issues/87)
- Support Authorization HTTP request header [\#86](https://github.com/christianhelle/apiclientcodegen/issues/86)
- Add support for custom HTTP headers when adding new REST API Client [\#90](https://github.com/christianhelle/apiclientcodegen/pull/90) ([christianhelle](https://github.com/christianhelle))
- Add support for YAML files [\#88](https://github.com/christianhelle/apiclientcodegen/pull/88) ([christianhelle](https://github.com/christianhelle))

## [1.1.2039](https://github.com/christianhelle/apiclientcodegen/tree/1.1.2039) (2020-06-12)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.2007...1.1.2039)

## [1.1.2007](https://github.com/christianhelle/apiclientcodegen/tree/1.1.2007) (2020-06-03)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.1943...1.1.2007)

## [1.1.1943](https://github.com/christianhelle/apiclientcodegen/tree/1.1.1943) (2020-05-21)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.1895...1.1.1943)

## [1.1.1895](https://github.com/christianhelle/apiclientcodegen/tree/1.1.1895) (2020-05-11)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.1852...1.1.1895)

## [1.1.1852](https://github.com/christianhelle/apiclientcodegen/tree/1.1.1852) (2020-05-01)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.1823...1.1.1852)

## [1.1.1823](https://github.com/christianhelle/apiclientcodegen/tree/1.1.1823) (2020-04-26)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.1784...1.1.1823)

## [1.1.1784](https://github.com/christianhelle/apiclientcodegen/tree/1.1.1784) (2020-04-17)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.1715...1.1.1784)

**Implemented enhancements:**

- Add option pages for dependency NuGet package installation [\#50](https://github.com/christianhelle/apiclientcodegen/issues/50)

## [1.1.1715](https://github.com/christianhelle/apiclientcodegen/tree/1.1.1715) (2020-04-04)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.1682...1.1.1715)

## [1.1.1682](https://github.com/christianhelle/apiclientcodegen/tree/1.1.1682) (2020-03-29)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.1586...1.1.1682)

## [1.1.1586](https://github.com/christianhelle/apiclientcodegen/tree/1.1.1586) (2020-03-18)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.1549...1.1.1586)

**Fixed bugs:**

- Missing automatic nuget install on Visual Studio for Mac [\#83](https://github.com/christianhelle/apiclientcodegen/issues/83)

## [1.1.1549](https://github.com/christianhelle/apiclientcodegen/tree/1.1.1549) (2020-03-15)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.1318...1.1.1549)

**Implemented enhancements:**

- support for Visual Studio for Mac ? [\#76](https://github.com/christianhelle/apiclientcodegen/issues/76)
- Swagger 1.2 Spec [\#33](https://github.com/christianhelle/apiclientcodegen/issues/33)
- Add support for Visual Basic [\#23](https://github.com/christianhelle/apiclientcodegen/issues/23)

**Fixed bugs:**

- CLI Tool -v or --verbose mode doesn't work [\#80](https://github.com/christianhelle/apiclientcodegen/issues/80)

## [1.1.1318](https://github.com/christianhelle/apiclientcodegen/tree/1.1.1318) (2020-03-03)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.1152...1.1.1318)

## [1.1.1152](https://github.com/christianhelle/apiclientcodegen/tree/1.1.1152) (2020-02-16)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.1003...1.1.1152)

**Fixed bugs:**

- AutoRest results in error in CLI Tool [\#78](https://github.com/christianhelle/apiclientcodegen/issues/78)

## [1.1.1003](https://github.com/christianhelle/apiclientcodegen/tree/1.1.1003) (2020-02-07)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.788...1.1.1003)

## [1.1.788](https://github.com/christianhelle/apiclientcodegen/tree/1.1.788) (2020-01-17)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.778...1.1.788)

## [1.1.778](https://github.com/christianhelle/apiclientcodegen/tree/1.1.778) (2020-01-04)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.775...1.1.778)

**Implemented enhancements:**

- Download NSwag CLI On-Demand [\#70](https://github.com/christianhelle/apiclientcodegen/issues/70)

## [1.1.775](https://github.com/christianhelle/apiclientcodegen/tree/1.1.775) (2019-12-17)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.753...1.1.775)

## [1.1.753](https://github.com/christianhelle/apiclientcodegen/tree/1.1.753) (2019-12-02)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.717...1.1.753)

**Implemented enhancements:**

- Cross platform CLI tool [\#66](https://github.com/christianhelle/apiclientcodegen/issues/66)

## [1.1.717](https://github.com/christianhelle/apiclientcodegen/tree/1.1.717) (2019-11-09)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.709...1.1.717)

## [1.1.709](https://github.com/christianhelle/apiclientcodegen/tree/1.1.709) (2019-11-01)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.708...1.1.709)

**Implemented enhancements:**

- Create AutoRest Option page [\#61](https://github.com/christianhelle/apiclientcodegen/issues/61)
- Client name setting [\#59](https://github.com/christianhelle/apiclientcodegen/issues/59)

## [1.1.708](https://github.com/christianhelle/apiclientcodegen/tree/1.1.708) (2019-10-28)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.695...1.1.708)

## [1.1.695](https://github.com/christianhelle/apiclientcodegen/tree/1.1.695) (2019-09-20)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.672...1.1.695)

**Implemented enhancements:**

- Add support for OpenAPI Code Generator v4 [\#54](https://github.com/christianhelle/apiclientcodegen/issues/54)
- Add support for Swagger Codegen CLI v3 [\#53](https://github.com/christianhelle/apiclientcodegen/issues/53)

**Fixed bugs:**

- Missing Microsoft.CSharp reference on .NET Standard projects [\#49](https://github.com/christianhelle/apiclientcodegen/issues/49)

## [1.1.672](https://github.com/christianhelle/apiclientcodegen/tree/1.1.672) (2019-08-25)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.658...1.1.672)

## [1.1.658](https://github.com/christianhelle/apiclientcodegen/tree/1.1.658) (2019-07-12)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.650...1.1.658)

## [1.1.650](https://github.com/christianhelle/apiclientcodegen/tree/1.1.650) (2019-07-07)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.642...1.1.650)

## [1.1.642](https://github.com/christianhelle/apiclientcodegen/tree/1.1.642) (2019-06-29)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.637...1.1.642)

**Implemented enhancements:**

- Create Option Pages for specifying path to JAR files [\#35](https://github.com/christianhelle/apiclientcodegen/issues/35)

**Fixed bugs:**

- AutoRest v2.0.4283 generated code fails to build due to missing Newtonsoft.Json reference [\#43](https://github.com/christianhelle/apiclientcodegen/issues/43)

## [1.1.637](https://github.com/christianhelle/apiclientcodegen/tree/1.1.637) (2019-06-20)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.631...1.1.637)

**Implemented enhancements:**

- Add support for portable java [\#28](https://github.com/christianhelle/apiclientcodegen/issues/28)

**Fixed bugs:**

- Missing System.Runtime.Serialization reference on .NET Standard projects [\#41](https://github.com/christianhelle/apiclientcodegen/issues/41)
- Installing NuGet packages fail on Visual Studio 2017 [\#38](https://github.com/christianhelle/apiclientcodegen/issues/38)
- Missing System.ComponentModel.DataAnnotations reference on .NET Standard projects [\#29](https://github.com/christianhelle/apiclientcodegen/issues/29)

## [1.1.631](https://github.com/christianhelle/apiclientcodegen/tree/1.1.631) (2019-06-14)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.1.609...1.1.631)

**Implemented enhancements:**

- Replaces latest Newtonsoft.Json nuget package with older version [\#31](https://github.com/christianhelle/apiclientcodegen/issues/31)
- Add User Settings and Option pages for defining custom paths and download options [\#30](https://github.com/christianhelle/apiclientcodegen/issues/30)

## [1.1.609](https://github.com/christianhelle/apiclientcodegen/tree/1.1.609) (2019-06-05)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.0.20190605.1...1.1.609)

## [1.0.20190605.1](https://github.com/christianhelle/apiclientcodegen/tree/1.0.20190605.1) (2019-06-04)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.0.20190531.2...1.0.20190605.1)

**Fixed bugs:**

- Add New REST API Client fails for .NET Core Web Apps [\#25](https://github.com/christianhelle/apiclientcodegen/issues/25)

## [1.0.20190531.2](https://github.com/christianhelle/apiclientcodegen/tree/1.0.20190531.2) (2019-05-31)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.0.20190603.1...1.0.20190531.2)

## [1.0.20190603.1](https://github.com/christianhelle/apiclientcodegen/tree/1.0.20190603.1) (2019-05-31)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.0.20190531.1...1.0.20190603.1)

## [1.0.20190531.1](https://github.com/christianhelle/apiclientcodegen/tree/1.0.20190531.1) (2019-05-30)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.0.20190530.1...1.0.20190531.1)

## [1.0.20190530.1](https://github.com/christianhelle/apiclientcodegen/tree/1.0.20190530.1) (2019-05-29)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.0.20190528.1...1.0.20190530.1)

## [1.0.20190528.1](https://github.com/christianhelle/apiclientcodegen/tree/1.0.20190528.1) (2019-05-28)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.0.20190526.4...1.0.20190528.1)

## [1.0.20190526.4](https://github.com/christianhelle/apiclientcodegen/tree/1.0.20190526.4) (2019-05-26)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.0.20190526.3...1.0.20190526.4)

## [1.0.20190526.3](https://github.com/christianhelle/apiclientcodegen/tree/1.0.20190526.3) (2019-05-26)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/1.0.20190526.2...1.0.20190526.3)

**Fixed bugs:**

- OpenApiCodeGenerator custom tool generated code doesn't build [\#18](https://github.com/christianhelle/apiclientcodegen/issues/18)
- SwaggerCodeGenerator custom tool generated code fails to build [\#17](https://github.com/christianhelle/apiclientcodegen/issues/17)

## [1.0.20190526.2](https://github.com/christianhelle/apiclientcodegen/tree/1.0.20190526.2) (2019-05-26)

[Full Changelog](https://github.com/christianhelle/apiclientcodegen/compare/45b4261cfcef344d488c82ed8c42d3d75b308d8a...1.0.20190526.2)

**Implemented enhancements:**

- Missing Icon and Visual Studio Marketplace Tags [\#15](https://github.com/christianhelle/apiclientcodegen/issues/15)

**Fixed bugs:**

- Generate NSwag Studio Output context menu is always visible after use [\#14](https://github.com/christianhelle/apiclientcodegen/issues/14)
- Generate NSwag Studio Output uses cached version of the Swagger JSON spec [\#7](https://github.com/christianhelle/apiclientcodegen/issues/7)



\* *This Changelog was automatically generated by [github_changelog_generator](https://github.com/github-changelog-generator/github-changelog-generator)*
