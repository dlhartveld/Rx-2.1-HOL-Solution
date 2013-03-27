﻿using System;
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
        public static IObservable<T> LogTimestampedValues<T>(this IObservable<T> source, Action<Timestamped<T>> onNext)
        {
            return source.Timestamp().Do(onNext).Select(x => x.Value);
        }

        static void Main(string[] args)
        {
            var txt = new TextBox();
            var lbl = new Label { Left = txt.Width + 20 };
            var frm = new Form
            {
                Controls = { txt, lbl }
            };

            var input = (from evt in Observable.FromEventPattern(txt, "TextChanged")
                         select ((TextBox)evt.Sender).Text)
                        .LogTimestampedValues(x => Console.WriteLine("I: " + x.Timestamp.Millisecond + " - " + x.Value))
                        .Throttle(TimeSpan.FromSeconds(1))
                        .LogTimestampedValues(x => Console.WriteLine("T: " + x.Timestamp.Millisecond + " - " + x.Value))
                        .DistinctUntilChanged();

            using (input.ObserveOn(lbl).Subscribe(inp => lbl.Text = inp))
            {
                Application.Run(frm);
            }
        }
        
    }
}
