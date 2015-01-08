using Akka.Actor;
using Akka.DI.CastleWindsor;
using Akka.Monitoring;
using Akka.Monitoring.Impl;
using Castle.Core;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Akka.AOP.Monitoring
{
    class Program
    {
        public static AutoResetEvent ManualResetEvent = new AutoResetEvent(false);

        private static ActorSystem system;
        
        static void Main(string[] args)
        {
         
           using (system = ActorSystem.Create("akka-performance-demo"))
            {
                var registeredMonitor = ActorMonitoringExtension.RegisterMonitor(system, new Monitor());

                IWindsorContainer container = new WindsorContainer();
                container.Register( Component.For<IInterceptor>().
                                    ImplementedBy<MonitorInterceptor>().
                                    Named("monitorInterceptor"),
                                    Component.For<HelloActor>().
                                    LifestyleTransient().
                                    Interceptors(InterceptorReference.ForKey("monitorInterceptor")).
                                    Anywhere);

                WindsorDependencyResolver propsResolver =
                        new WindsorDependencyResolver(container, system);

                var hello = system.ActorOf(propsResolver.Create<HelloActor>(), "Worker1");

                hello.Tell("What's Up");
                hello.Tell("Goodbye");

                var count = 20;
                while (count >= 0)
                {
                    ActorMonitoringExtension.Monitors(system).IncrementDebugsLogged();
                    Console.WriteLine("Logging debug...");
                    Thread.Sleep(100);
                    count--;
                }

                while (ManualResetEvent.WaitOne())
                {
                    Console.WriteLine("Shutting down...");
                    system.Shutdown();
                    Console.WriteLine("Shutdown complete");
                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();
                    return;
                }

            }
            
        }
    }
}
