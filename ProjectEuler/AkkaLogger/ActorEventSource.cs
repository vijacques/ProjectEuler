using Akka.Event;
using Microsoft.Diagnostics.Tracing;

namespace AkkaLogger
{
    [EventSource(Name = "AkkaLogger.ActorEventSource")]
    public sealed class ActorEventSource : EventSource
    {
        public static ActorEventSource Log = new ActorEventSource();

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
