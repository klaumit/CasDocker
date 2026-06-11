using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PvMake.Lib;
using System.IO;
using Pva2cpa.Lib;

namespace Pva2cpa
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            dropBox.AllowDrop = true;
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch (Exception)
            {
                Close();
            }
        }

        private void dropBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void dropBox_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string filePath in files)
            {
                using (var err = new StringWriter())
                using (var con = new StringWriter())
                {
                    var dest = Path.GetDirectoryName(filePath);
                    Patching.PostCompilePad(filePath, dest, err, con, "");
                    err.Flush();
                    con.Flush();
                    var errTxt = err.GetStringBuilder().ToString().Trim();
                    var conTxt = con.GetStringBuilder().ToString().Trim();
                    if (!Strings.IsNullOrWhiteSpace(errTxt))
                    {
                        MessageBox.Show(errTxt, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        return;
                    }
                    if (!Strings.IsNullOrWhiteSpace(conTxt))
                    {
                        MessageBox.Show(conTxt, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
        }
    }
}