﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Solution
{
    class Program
    {
        static void Main(string[] args)
        {
            var txt = new TextBox();

            var frm = new Form
            {
                Controls = { txt }
            };

            var input = (from evt in Observable.FromEventPattern(txt, "TextChanged")
                         select ((TextBox)evt.Sender).Text)

                        .Timestamp()
                        .Do(inp => Console.WriteLine("I: " + inp.Timestamp.Millisecond + " - " + inp.Value))
                        .Select(x => x.Value)

                        .Throttle(TimeSpan.FromSeconds(1))

                        .Timestamp()
                        .Do(inp => Console.WriteLine("T: " + inp.Timestamp.Millisecond + " - " + inp.Value))
                        .Select(x => x.Value)

                        .DistinctUntilChanged();

            using (input.Subscribe(inp => Console.WriteLine("User wrote: " + inp)))
            {
                Application.Run(frm);
            }
        }
    }
}
