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
            var txt = new TextBox();
            var lst = new ListBox { Top = txt.Height + 10 };
            var frm = new Form
            {
                Controls = { txt, lst }
            };

            var input = (from evt in Observable.FromEventPattern(txt, "TextChanged")
                         select ((TextBox)evt.Sender).Text)
                .Throttle(TimeSpan.FromSeconds(1))
                .DistinctUntilChanged()
                .Do(x => Console.WriteLine(x));

            var svc = new DictServiceSoapClient("DictServiceSoap");
            var matchInDict = Observable.FromAsyncPattern<string, string, string, DictionaryWord[]> (svc.BeginMatchInDict, svc.EndMatchInDict);

            Func<string, IObservable<DictionaryWord[]>> matchInWordNetByPrefix = term => matchInDict("wn", term, "prefix");

            Application.Run(frm);

            Console.WriteLine("Press ENTER to quit ...");
            Console.ReadLine();
        }
    }
}
