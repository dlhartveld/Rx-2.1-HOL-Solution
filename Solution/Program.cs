using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution
{
    class Program
    {
        static void Main(string[] args)
        {
            // Uncomment one of these to run the exercise code:

            //Exercises();
            //Exercise9();
            Exercise10();
        }

        private static void Exercises()
        {
            // Comment out all but one of the following lines ...

            //IObservable<int> source = Observable.Empty<int>();
            //IObservable<int> source = Observable.Throw<int>(new Exception("Oops"));
            //IObservable<int> source = Observable.Return(42);
            //IObservable<int> source = Observable.Range(5, 3);
            //IObservable<int> source = Observable.Generate(0, i => i < 5, i => i + 1, i => i * i);
            IObservable<int> source = Observable.Never<int>();

            // ... to see the observable in action after subscription:

            IDisposable subscription = source.Subscribe(
                x => Console.WriteLine("OnNext:  {0}", x),
                ex => Console.WriteLine("OnError: {0}", ex.Message),
                () => Console.WriteLine("OnCompleted")
            );

            Console.WriteLine("Press ENTER to unsubscribe...");
            Console.ReadLine();

            subscription.Dispose();
        }

        static void Exercise9()
        {
            IObservable<int> source = Observable.Range(0, 10);
            source.ForEachAsync(
                    x => Console.WriteLine("OnNext: {0}", x)
                )
                .Wait();
        }

        static void Exercise10()
        {
            IObservable<int> source = Observable.Generate(
                0, i => i < 5,
                i => i + 1,
                i => i * i,
                i => TimeSpan.FromSeconds(i)
            );

            using (source.Subscribe(
                       x => Console.WriteLine("OnNext:  {0}", x),
                       ex => Console.WriteLine("OnError: {0}", ex.Message),
                       () => Console.WriteLine("OnCompleted")
                  ))
            {
                Console.WriteLine("Press ENTER to unsubscribe...");
                Console.ReadLine();
            }

        }
    }
}
