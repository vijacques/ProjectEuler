using Akka.Actor;
using Akka.Configuration;
using Akka.Event;
using System;
using System.Threading;

namespace AkkaLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            // create a new actor system (a container for actors)
            var system = ActorSystem.Create("MySystem");

            // create actor and get a reference to it.
            // this will be an "ActorRef", which is not a 
            // reference to the actual actor instance
            // but rather a client or proxy to it
            var greeter = system.ActorOf<LoggingActor>("greeter");
            greeter.Tell(new InitializeLogger(new LoggingBus()));
            greeter.Tell(new Greet("World"));
            greeter.Tell(new Warning(greeter.Path.ToString(), greeter.GetType(), "my first warning!"));

            //Console.ReadLine();
            Thread.Sleep(2000);
        }
    }
}
