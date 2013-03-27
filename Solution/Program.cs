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

            var moves = Observable.FromEventPattern<MouseEventArgs>(frm, "MouseMove");
            var input = Observable.FromEventPattern(txt, "TextChanged");

            var movesSubscription = moves.Subscribe(evt =>
            {
                Console.WriteLine("Mouse at  : {0}", evt.EventArgs.Location);
            });
            var inputSubscription = input.Subscribe(evt =>
            {
                Console.WriteLine("User wrote: {0}", ((TextBox)evt.Sender).Text);
            });

            using (new CompositeDisposable(movesSubscription, inputSubscription))
            {
                Application.Run(frm);
            }
        }
    }
}
