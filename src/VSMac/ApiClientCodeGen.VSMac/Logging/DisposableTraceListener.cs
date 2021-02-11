using System;
using System.Diagnostics;

namespace ApiClientCodeGen.VSMac.Logging
{
    public class DisposableTraceListener : IDisposable
    {
        private readonly TraceListener innerListener;

        public DisposableTraceListener(TraceListener innerListener)
        {
            this.innerListener = innerListener ?? throw new ArgumentNullException(nameof(innerListener));
            Trace.Listeners.Add(innerListener);
        }

        public void Dispose()
        {
            if (Trace.Listeners.Contains(innerListener))
                Trace.Listeners.Remove(innerListener);
            
            innerListener.Dispose();
        }
    }
}