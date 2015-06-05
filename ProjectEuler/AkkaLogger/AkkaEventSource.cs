using Akka.Event;
using Microsoft.Diagnostics.Tracing;

namespace AkkaLogger
{
    [EventSource(Name = "AkkaLogger.AkkaEventSource")]
    public sealed class AkkaEventSource : EventSource
    {
        public static AkkaEventSource Log = new AkkaEventSource();

        public void Debug(string message)
        {
            if (IsEnabled())
                WriteEvent(1, message);
        }

        public void Error(string message)
        {
            if (IsEnabled())
                WriteEvent(2, message);
        }

        public void Info(string message)
        {
            if (IsEnabled())
                WriteEvent(3, message);
        }

        public void Warning(string message)
        {
            if (IsEnabled())
                WriteEvent(4, message);
        }
    }
}
