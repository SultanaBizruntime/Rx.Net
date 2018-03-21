using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RxNetProject
{
    class SubscribeDemo1
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Current Time: " + DateTime.Now);
            var source = Observable.Interval(TimeSpan.FromSeconds(1));            //creates a sequence

            IConnectableObservable<long> hot = Observable.Publish<long>(source);  // convert the sequence into a hot sequence

            IDisposable subscription1 = hot.Subscribe(                        // no value is pushed to 1st subscription at this point
                                        x => Console.WriteLine("Observer 1: OnNext: {0}", x),
                                        ex => Console.WriteLine("Observer 1: OnError: {0}", ex.Message),
                                        () => Console.WriteLine("Observer 1: OnCompleted"));
            Console.WriteLine("Current Time after 1st subscription: " + DateTime.Now);
            Thread.Sleep(3000);  //idle for 3 seconds
            hot.Connect();       // hot is connected to source and starts pushing value to subscribers 
            Console.WriteLine("Current Time after Connect: " + DateTime.Now);
            Thread.Sleep(3000);  //idle for 3 seconds
            Console.WriteLine("Current Time just before 2nd subscription: " + DateTime.Now);

            IDisposable subscription2 = hot.Subscribe(     // value will immediately be pushed to 2nd subscription
                                        x => Console.WriteLine("Observer 2: OnNext: {0}", x),
                                        ex => Console.WriteLine("Observer 2: OnError: {0}", ex.Message),
                                        () => Console.WriteLine("Observer 2: OnCompleted"));
            Console.ReadKey();
        }
    }
}
