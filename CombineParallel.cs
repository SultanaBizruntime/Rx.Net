using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RxNetProject
{
   public class AyncParallel
    {
        public async void ParallelExecutionTest()
        {
            var o = Observable.CombineLatest(
                Observable.Start(() => { Console.WriteLine("Executing 1st on Thread: {0}", Thread.CurrentThread.ManagedThreadId); return "Result A"; }),
                Observable.Start(() => { Console.WriteLine("Executing 2nd on Thread: {0}", Thread.CurrentThread.ManagedThreadId); return "Result B"; }),
                Observable.Start(() => { Console.WriteLine("Executing 3rd on Thread: {0}", Thread.CurrentThread.ManagedThreadId); return "Result C"; })
            ).Finally(() => Console.WriteLine("Done!"));

            foreach (string r in await o.FirstAsync())
                Console.WriteLine(r);
        }
    }
    class CombineParallel
    {
        static void Main()
        {
            var async1 = new AyncParallel();
            async1.ParallelExecutionTest();
            Console.WriteLine("CombineParallel Executed");
            Console.Read();
        }

    }
}
