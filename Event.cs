using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RxNetProject
{
    class Event
    {
        public static event EventHandler SimpleEvent;

        static void Main()
        {
            // To consume SimpleEvent as an IObservable:
            var eventAsObservable = Observable.FromEventPattern(
                ev => SimpleEvent += ev,
                ev => SimpleEvent -= ev);
            var s = eventAsObservable.Subscribe(args => Console.WriteLine("Received event for s subscriber"));

            // After subscribing the event handler has been added
            Console.WriteLine(SimpleEvent == null ? "SimpleEvent == null" : "SimpleEvent != null");

            Console.WriteLine("Raise event");
            if (null != SimpleEvent)
            {
                SimpleEvent(null, EventArgs.Empty);
            }
            // Allow some time before unsubscribing or event may not happen
            Thread.Sleep(1000);
            Console.WriteLine("done");
            Console.Read();
        }
    }
}
