using System;
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

            var moves = from evt in Observable.FromEventPattern<MouseEventArgs>(frm, "MouseMove")
                        select evt.EventArgs.Location;

            var input = from evt in Observable.FromEventPattern(txt, "TextChanged")
                        select ((TextBox)evt.Sender).Text;

            var movesSubscription = moves.Subscribe(pos => Console.WriteLine("Mouse at  : {0}", pos));
            var inputSubscription = input.Subscribe(inp => Console.WriteLine("User wrote: {0}", inp));

            using (new CompositeDisposable(movesSubscription, inputSubscription))
            {
                Application.Run(frm);
            }
        }
    }
}
