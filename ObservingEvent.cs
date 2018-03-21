using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RxNetProject
{
    class ObservingEvent
    {
        public static event EventHandler SimpleEvent;

        static void Main()
        {
            Console.WriteLine("Setup observable");
            // To consume SimpleEvent as an IObservable:
            var eventAsObservable = Observable.FromEventPattern(
                    ev => SimpleEvent += ev,
                    ev => SimpleEvent -= ev);

            // SimpleEvent is null until we subscribe
            Console.WriteLine(SimpleEvent == null ? "SimpleEvent == null" : "SimpleEvent != null");
            Thread.Sleep(1000);

            Console.WriteLine("Subscribe");
            Thread.Sleep(1000);
            //Create two event subscribers
            var s = eventAsObservable.Subscribe(args => Console.WriteLine("Received event for s subscriber"));
            var t = eventAsObservable.Subscribe(args => Console.WriteLine("Received event for t subscriber"));

            // After subscribing the event handler has been added
            Console.WriteLine(SimpleEvent == null ? "SimpleEvent == null" : "SimpleEvent != null");
            Thread.Sleep(1000);

            Console.WriteLine("Raise event");
            if (null != SimpleEvent)
            {
                SimpleEvent(null, EventArgs.Empty);
            }

            // Allow some time before unsubscribing or event may not happen
            Thread.Sleep(100);

            Console.WriteLine("Unsubscribe");
            s.Dispose();
            t.Dispose();
            Thread.Sleep(1000);



            // After unsubscribing the event handler has been removed
            Console.WriteLine(SimpleEvent == null ? "SimpleEvent == null" : "SimpleEvent != null");

            Console.ReadKey();
        }

    }
}
