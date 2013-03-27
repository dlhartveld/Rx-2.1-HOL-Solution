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

            // Turn the user input into a tamed sequence of strings.
            //var textChanged = from evt in Observable.FromEventPattern(txt, "TextChanged")
            //                  select ((TextBox)evt.Sender).Text;

            //var textChanged = new[] { "reac", "reactive", "bing" }.ToObservable();

            // rea, reac, react, reacti, reactiv, reactive
            var textChanged = (from len in Enumerable.Range(3, 6)
                               select "reactive".Substring(0, len))
                              .ToObservable();

            var input = textChanged
                            .Where(term => term.Length >= 3)
                            .Throttle(TimeSpan.FromSeconds(1))
                            .DistinctUntilChanged()
                            .Do(x => Console.WriteLine("Text changed: {0}", x));

            // Bridge with the web service's MatchInDict method.
            var svc = new DictServiceSoapClient("DictServiceSoap");
            var matchInDict = Observable.FromAsyncPattern<string, string, string, DictionaryWord[]> (svc.BeginMatchInDict, svc.EndMatchInDict);

            Func<string, IObservable<DictionaryWord[]>> matchInWordNetByPrefix = term => matchInDict("wn", term, "prefix");

            var res = (from term in input
                       select matchInWordNetByPrefix(term))
                      .Switch();

            // Synchronize with the UI thread and populate the ListBox or signal an error.
            using (res.ObserveOn(lst).Subscribe(
                words => {
                    lst.Items.Clear();
                    lst.Items.AddRange((from word in words select word.Word).ToArray());
                },
                ex => MessageBox.Show("An error occurred: " + ex.Message, frm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ))
            {
                Application.Run(frm);
            } // Proper disposal happens upon exiting the application.

            Console.WriteLine("Press ENTER to quit ...");
            Console.ReadLine();
        }
    }
}
