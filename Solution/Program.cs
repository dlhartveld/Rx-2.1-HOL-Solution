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
            IObservable<int> source;

            IDisposable subscription = source.Select(i -> i + 1).Subscribe(
                el => Console.WriteLine("Received {0} from source.", el),
                ex => Console.WriteLine("Source signaled an error: {0}.", ex.Message),
                () => Console.WriteLine("Source said there are no messages to follow anymore.")
            );

            Console.WriteLine("Press ENTER to unsubscribe...");
            Console.ReadLine();

            subscription.Dispose();
        }
    }
}
