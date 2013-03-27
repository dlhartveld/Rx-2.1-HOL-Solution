using Solution.DictionarySuggestService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Solution
{
    static class Program
    {
        static void Main(string[] args)
        {
            var svc = new DictServiceSoapClient("DictServiceSoap");
            svc.BeginMatchInDict("wn", "react", "prefix",
                iar =>
                {
                    var words = svc.EndMatchInDict(iar);
                    foreach (var word in words)
                        Console.WriteLine(word.Word);
                },
                null
            );

            Console.WriteLine("Press ENTER to quit ...");
            Console.ReadLine();
        }
    }
}
