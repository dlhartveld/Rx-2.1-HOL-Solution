using System;
using System.Collections.Generic;
using System.Linq;
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
            var lbl = new Label();
            var frm = new Form {
                Controls = { lbl }
            };

            var moves = Observable.FromEventPattern<MouseEventArgs>(frm, "MouseMove");
            using (moves.Subscribe(evt => lbl.Text = evt.EventArgs.Location.ToString()))
            {
                Application.Run(frm);
            }

            // Proper clean-up just got a lot easier...
        }
    }
}
