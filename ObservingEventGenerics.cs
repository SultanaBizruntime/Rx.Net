using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RxNetProject
{
    class ObservingEventGenerics
    {
        public class SomeEventArgs : EventArgs { }
        public static event EventHandler<SomeEventArgs> GenericEvent;

        static void Main()
        {
            // To consume GenericEvent as an IObservable:
            IObservable<EventPattern<SomeEventArgs>> eventAsObservable = Observable.FromEventPattern<SomeEventArgs>(
                ev => GenericEvent += ev,
                ev => GenericEvent -= ev);

            Console.WriteLine(GenericEvent == null ? "GenericEvent == null" : "GenericEvent != null");
            Thread.Sleep(1000);

            Console.WriteLine("Subscribe");
            Thread.Sleep(1000);
            //Create two event subscribers
            var s = eventAsObservable.Subscribe(args => Console.WriteLine("Received event for s subscriber"));
            var t = eventAsObservable.Subscribe(args => Console.WriteLine("Received event for t subscriber"));

            // After subscribing the event handler has been added
            Console.WriteLine(GenericEvent == null ? "GenericEvent == null" : "GenericEvent != null");
            Thread.Sleep(1000);

            Console.WriteLine("Raise event");
            if (null != GenericEvent)
            {
               // GenericEvent(null, EventArgs.Empty);
            }

            // Allow some time before unsubscribing or event may not happen
            Thread.Sleep(100);

            Console.WriteLine("Unsubscribe");
            s.Dispose();
            t.Dispose();
            Thread.Sleep(1000);

            // After unsubscribing the event handler has been removed
            Console.WriteLine(GenericEvent == null ? "GenericEvent == null" : "GenericEvent != null");

            Console.ReadKey();
        }
    }
}
