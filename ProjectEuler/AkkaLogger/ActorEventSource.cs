using Microsoft.Diagnostics.Tracing;

namespace AkkaLogger
{
    [EventSource(Name = "AkkaLogger.MyApp")]
    public sealed class ActorEventSource : EventSource
    {
        public static ActorEventSource Log = new ActorEventSource();

        public void Load(long ImageBase, string Name)
        {
            if (IsEnabled())
                WriteEvent(1, ImageBase, Name);
        }

        public void Unload(long ImageBase)
        {
            if (IsEnabled())
                WriteEvent(2, ImageBase);
        }

        public void ActorEvent(long ImageBase, string Name)
        {
            if (IsEnabled())
                WriteEvent(3, ImageBase, Name);
        }

        public void InfoEvent(long ImageBase, string Name)
        {
            if (IsEnabled())
                WriteEvent(4, ImageBase, Name);
        }

        public void WarningEvent(long ImageBase, string Name)
        {
            if (IsEnabled())
                WriteEvent(5, ImageBase, Name);
        }
    }
}
