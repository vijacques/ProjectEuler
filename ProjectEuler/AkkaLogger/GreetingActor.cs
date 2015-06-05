﻿using Akka.Actor;
using System;

namespace AkkaLogger
{
    class GreetingActor : ReceiveActor
    {
        public GreetingActor()
        {
            // Tell the actor to respond
            // to the Greet message
            Receive<Greet>(greet =>
            {
                Console.WriteLine("Hello {0}", greet.Who);
            });
        }
    }
}
