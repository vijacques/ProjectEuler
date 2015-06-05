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
                ActorEventSource.Log.Info("greet from " + greet.Who);
            });

            Receive<Error>(m => ActorEventSource.Log.Error(m.Message.ToString()));
            Receive<Warning>(m => ActorEventSource.Log.Warning(m.Message.ToString()));
            Receive<Info>(m => ActorEventSource.Log.Info(m.Message.ToString()));
            Receive<Debug>(m => ActorEventSource.Log.Debug(m.Message.ToString()));

            Receive<InitializeLogger>(m =>
            {
                ActorEventSource.Log.Info("Logger started");
                Sender.Tell(new LoggerInitialized());
            });
        }
    }
}
