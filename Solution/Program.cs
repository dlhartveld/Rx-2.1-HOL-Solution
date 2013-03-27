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

            var input = from evt in Observable.FromEventPattern(txt, "TextChanged")
                        select ((TextBox)evt.Sender).Text;

            using (input.Subscribe(inp => Console.WriteLine("User wrote: {0}", inp)))
            {
                Application.Run(frm);
            }
        }
    }
}
