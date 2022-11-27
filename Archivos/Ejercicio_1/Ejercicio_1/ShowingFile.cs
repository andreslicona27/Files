using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejercicio_1
{
    public partial class ShowingFile : Form
    {
        public string content;
        public ShowingFile(string path)
        {
            InitializeComponent();
            txtFileShow.Text = File.ReadAllText(path);
            txtFileShow.Modified = false;
        }

        private void ShowingFile_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txtFileShow.Modified)
            {
                content = txtFileShow.Text;
                this.DialogResult = DialogResult.Yes;
            }
        }
    }
}
