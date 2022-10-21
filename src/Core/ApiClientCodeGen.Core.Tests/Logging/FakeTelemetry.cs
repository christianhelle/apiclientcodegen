using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace ApiClientCodeGen.Core.Tests.Logging
{
    public class FakeTelemetry : ISupportProperties, ITelemetry
    {
        public IDictionary<string, string> Properties { get; } = new Dictionary<string, string>();

        public ITelemetry DeepClone() { return this; }

        public void Sanitize()
        {
            // Ignore
        }

        public void SerializeData(ISerializationWriter serializationWriter)
        {
            // Ignore
        }

        public DateTimeOffset Timestamp { get; set; }
        public TelemetryContext Context { get; } = new();
        public IExtension Extension { get; set; }
        public string Sequence { get; set; }
    }
}