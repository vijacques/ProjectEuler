using Akka.Actor;
using Akka.Event;

namespace AkkaLogger
{
    class LoggingActor : ReceiveActor
    {
        public LoggingActor()
        {
            Receive<Error>(m => AkkaEventSource.Log.Error(m.Message.ToString()));
            Receive<Warning>(m => AkkaEventSource.Log.Warning(m.Message.ToString()));
            Receive<Info>(m => AkkaEventSource.Log.Info(m.Message.ToString()));
            Receive<Debug>(m => AkkaEventSource.Log.Debug(m.Message.ToString()));
            Receive<InitializeLogger>(m => Sender.Tell(new LoggerInitialized()));
        }
    }
}
