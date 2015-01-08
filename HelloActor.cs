using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Akka;
using Akka.Monitoring;

namespace Akka.AOP.Monitoring
{
    public class HelloActor : TypedActor, IHandle<string>
    {
        public void Handle(string message)
        {
            Console.WriteLine("Received: {0}", message);
            if (message == "Goodbye")
            {
                Context.Self.Tell(PoisonPill.Instance);
                Program.ManualResetEvent.Set();
            }
            else
                Sender.Tell("Hello!");
            Context.IncrementActorCreated();

        }
    }
}
