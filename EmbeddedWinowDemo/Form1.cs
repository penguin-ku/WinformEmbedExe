using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace EmbeddedWinowDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void embeddedPanel1_StartCompleteEvent(object sender, EmbeddedWinow.ExeStartCompleteEventArgs e)
        {
            MessageBox.Show(e.MainWindowHandle.ToString());
        }
    }
}
