using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevForge.Lib.Common;
using DevForge.Resources;

// ReSharper disable ArrangeObjectCreationWhenTypeEvident
// ReSharper disable LocalizableElement

namespace DevForge
{
    public partial class DeviceForm : Form
    {
        public DeviceForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Icon = ResExt.GetStream("app.ico").ToIcon();
        }
    }
}