using Akka.Actor;
using Akka.Event;

namespace AkkaLogger
{
    class LoggingActor : ReceiveActor
    {
        public LoggingActor()
        {
            Receive<Error>(m => ActorEventSource.Log.Error(m.Message.ToString()));
            Receive<Warning>(m => ActorEventSource.Log.Warning(m.Message.ToString()));
            Receive<Info>(m => ActorEventSource.Log.Info(m.Message.ToString()));
            Receive<Debug>(m => ActorEventSource.Log.Debug(m.Message.ToString()));
            Receive<InitializeLogger>(m => Sender.Tell(new LoggerInitialized()));
        }
    }
}
