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
            var frm = new Form()
            {
                Controls = { lbl }
            };

            frm.MouseMove += (sender, evArgs) =>
            {
                lbl.Text = evArgs.Location.ToString();  // This has become a position-tracking label.
            };

            Application.Run(frm);
        }
    }
}
