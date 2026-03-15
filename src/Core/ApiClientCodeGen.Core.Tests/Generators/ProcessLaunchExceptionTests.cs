using System;
using FluentAssertions;
using Rapicgen.Core.Generators;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators;

public class ProcessLaunchExceptionTests
{
    [Fact]
    public void Constructor_SetsCommand()
    {
        var sut = new ProcessLaunchException("cmd", "args", "/work", "output", "error");
        sut.Command.Should().Be("cmd");
    }

    [Fact]
    public void Constructor_SetsArguments()
    {
        var sut = new ProcessLaunchException("cmd", "args", "/work", "output", "error");
        sut.Arguments.Should().Be("args");
    }

    [Fact]
    public void Constructor_SetsWorkingDirectory()
    {
        var sut = new ProcessLaunchException("cmd", "args", "/work", "output", "error");
        sut.WorkingDirectory.Should().Be("/work");
    }

    [Fact]
    public void Constructor_SetsOutputData()
    {
        var sut = new ProcessLaunchException("cmd", "args", "/work", "output", "error");
        sut.OutputData.Should().Be("output");
    }

    [Fact]
    public void Constructor_SetsErrorData()
    {
        var sut = new ProcessLaunchException("cmd", "args", "/work", "output", "error");
        sut.ErrorData.Should().Be("error");
    }

    [Fact]
    public void Constructor_MessageContainsCommand()
    {
        var sut = new ProcessLaunchException("mycmd", "args", null, "out", "err");
        sut.Message.Should().Contain("mycmd");
    }

    [Fact]
    public void Constructor_MessageContainsOutput()
    {
        var sut = new ProcessLaunchException("cmd", "args", null, "some output", "err");
        sut.Message.Should().Contain("some output");
    }

    [Fact]
    public void Constructor_MessageContainsError()
    {
        var sut = new ProcessLaunchException("cmd", "args", null, "out", "some error");
        sut.Message.Should().Contain("some error");
    }

    [Fact]
    public void Constructor_NullWorkingDirectory_DoesNotThrow()
    {
        var sut = new ProcessLaunchException("cmd", "args", null, "output", "error");
        sut.WorkingDirectory.Should().BeNull();
    }

    [Fact]
    public void IsException()
    {
        var sut = new ProcessLaunchException("cmd", "args", null, "output", "error");
        sut.Should().BeAssignableTo<Exception>();
    }
}
