using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxNetProject
{
    class TimeSelector1
    {
         static void Main(string[] args)
        {
            IObservable<string> obj = Observable.Generate(
                0, //Sets the initial value like for loop    
                _ => true, //Don't stop till i say so, infinite loop    
                i => i + 1, //Increment the counter by 1 everytime    
                i => new string('#', i), //Append #    
                i => TimeSelector(i)); //delegated this to private method which just calculates time    

            //Subscribe here    
            using (obj.Subscribe(Console.WriteLine))
            {
                Console.WriteLine("Press any key to exit!!!");
                Console.ReadLine();
            }

            Console.WriteLine("TimeSelector Done..!!");
            Console.ReadKey();
        }

        //Returns TimeSelector    
        private static TimeSpan TimeSelector(int i)
        {
            return TimeSpan.FromSeconds(i);
        }

        
    }
}
