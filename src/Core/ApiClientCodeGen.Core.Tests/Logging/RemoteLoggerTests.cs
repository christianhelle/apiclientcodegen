using System;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Logging
{
    public class RemoteLoggerTests
    {
        [Fact]
        public void Constructor_Sets_Up_Default_Loggers()
        {
            new RemoteLogger()
                .Loggers
                .Should()
                .ContainItemsAssignableTo<ExceptionlessRemoteLogger>();
        }

        [Theory, AutoMoqData]
        public void Constructor_Adds_External_Loggers(
            [Frozen] IRemoteLogger logger,
            RemoteLogger sut)
        {
            sut
                .Loggers
                .Should()
                .Contain(logger);
        }

        [Theory, AutoMoqData]
        public void TrackFeatureUsage_Calls_External_Loggers(
            [Frozen] IRemoteLogger logger,
            RemoteLogger sut,
            string featureName,
            string[] tags)
        {
            sut.TrackFeatureUsage(featureName, tags);
            Mock.Get(logger).Verify(c => c.TrackFeatureUsage(featureName, tags));
        }

        [Theory, AutoMoqData]
        public void TrackError_Calls_External_Loggers(
            [Frozen] IRemoteLogger logger,
            RemoteLogger sut,
            Exception exception)
        {
            sut.TrackError(exception);
            Mock.Get(logger).Verify(c => c.TrackError(exception));
        }
    }
}