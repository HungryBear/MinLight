using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MinLight.View
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var form = new ViewForm(args[0]))
            {
                Application.Idle += form.OnIndle;
                Application.Run(form);
            }
        }
    }
}
