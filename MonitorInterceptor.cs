using Akka.Actor;
using Akka.Monitoring.Impl;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Monitoring;

namespace Akka.AOP.Monitoring
{
    public class MonitorInterceptor : IInterceptor
    {
        
        public void Intercept(IInvocation invocation)
        {
            try
            {
                Actor.ActorBase actorProxy = (Actor.ActorBase)invocation.Proxy;
                IInternalActor internalActor = (IInternalActor)actorProxy;
                IActorContext oontext = internalActor.ActorContext;
                

                if (invocation.Method.Name.Equals("PreStart", StringComparison.InvariantCultureIgnoreCase))
                {
                    oontext.IncrementActorCreated();
                    
                }
                if (invocation.Method.Name.Equals("AroundReceive", StringComparison.InvariantCultureIgnoreCase))
                {
                    oontext.IncrementMessagesReceived();

                }

                Console.WriteLine("{0} - {1}",invocation.TargetType.Name, invocation.Method.Name);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            invocation.Proceed();
        }
    }
}
