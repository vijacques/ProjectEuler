using Akka.Actor;
using Akka.Event;
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
            var logger = system.ActorOf<LoggingActor>("logger");
            var greeter = system.ActorOf<GreetingActor>("greeter");
            
            logger.Tell(new InitializeLogger(new LoggingBus()));
            greeter.Tell(new Greet("world"));

            AkkaEventSource.Log.Warning("my first warning!");
            system.Log.Warning("my second warning!");
            
            //Console.ReadLine();
            Thread.Sleep(2000);
        }
    }
}
