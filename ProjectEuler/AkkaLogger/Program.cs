using Akka.Actor;
using Akka.Event;
using System;
using System.Threading;

namespace AkkaLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggingBus lbus = new LoggingBus();

            ActorEventSource.Log.Load(0x700000, "MyFile");
            // create a new actor system (a container for actors)
            var system = ActorSystem.Create("MySystem");

            // create actor and get a reference to it.
            // this will be an "ActorRef", which is not a 
            // reference to the actual actor instance
            // but rather a client or proxy to it
            var greeter = system.ActorOf<LoggingActor>("greeter");
            greeter.Tell(new InitializeLogger(lbus));

            // send a message to the actor
            greeter.Tell(new Greet("World"));
            greeter.Tell(new Warning(greeter.Path.ToString(), greeter.GetType(), "my first warning!"));

            // prevent the application from exiting before message is handled
            ActorEventSource.Log.ActorEvent(0x700000, "Program.cs");
            ActorEventSource.Log.ActorEvent(0x700000, "Program.cs");
            ActorEventSource.Log.Unload(0x700000);

            //Console.ReadLine();
            Thread.Sleep(2000);

            // akka custom logging setting?
            // logging bus?
        }
    }
}
