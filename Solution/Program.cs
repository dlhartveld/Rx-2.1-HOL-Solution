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
            var matchInDict = Observable.FromAsyncPattern<string, string, string, DictionaryWord[]>
                (svc.BeginMatchInDict, svc.EndMatchInDict);

            var res = matchInDict("wn", "react", "prefix");
            var subscription = res.Subscribe(words =>
            {
                foreach (var word in words)
                    Console.WriteLine(word.Word);
            });


            Console.WriteLine("Press ENTER to quit ...");
            Console.ReadLine();
        }
    }
}
