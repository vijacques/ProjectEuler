using Akka.Actor;
using Akka.Event;
using System;

namespace AkkaLogger
{
    class LoggingActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();

        public LoggingActor()
        {
            Receive<Greet>(greet =>
            {
                Console.WriteLine("Hello {0}", greet.Who);
                ActorEventSource.Log.InfoEvent(0x700000, "greet from " + greet.Who);
            });

            Receive<Error>(m => ActorEventSource.Log.ActorEvent(0x700000, m.Message.ToString()));
            Receive<Warning>(m => ActorEventSource.Log.WarningEvent(0x700000, m.Message.ToString()));
            Receive<Info>(m => ActorEventSource.Log.ActorEvent(0x700000, m.Message.ToString()));
            Receive<Debug>(m => ActorEventSource.Log.ActorEvent(0x700000, m.Message.ToString()));

            Receive<InitializeLogger>(m =>
            {
                _log.Info("Logger started");
                Sender.Tell(new LoggerInitialized());
            });
        }
    }
}
