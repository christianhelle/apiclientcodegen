﻿using ApiClientCodeGen.Tests.Common.Infrastructure;
using FluentAssertions;
using Rapicgen.Core.Generators;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators;

public class GeneratedCodeTests
{
    [Theory, AutoMoqData]
    public void Should_Contain_AutoGeneratedHeader(
        string code,
        string toolName,
        string toolVersion)
    {
        GeneratedCode
            .PrefixAutogeneratedCodeHeader(code, toolName, toolVersion)
            .Should()
            .Contain("<auto-generated>");
    }

    [Theory, AutoMoqData]
    public void Should_Contain_ToolName(
        string code,
        string toolName,
        string toolVersion)
    {
        GeneratedCode
            .PrefixAutogeneratedCodeHeader(code, toolName, toolVersion)
            .Should()
            .Contain(toolName);
    }

    [Theory, AutoMoqData]
    public void Should_Contain_ToolVersion(
        string code,
        string toolName,
        string toolVersion)
    {
        GeneratedCode
            .PrefixAutogeneratedCodeHeader(code, toolName, toolVersion)
            .Should()
            .Contain(toolVersion);
    }
}