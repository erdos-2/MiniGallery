using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MiniGallery
{
    public partial class MainForm : Form
    {
        UploadImageForm uif = new UploadImageForm();
        ViewImageForm vif = new ViewImageForm();
        public MainForm()
        {
            InitializeComponent();
            uif.MdiParent = this;
            vif.MdiParent = this;
            uif.Show();
        }
        
        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != uif)
            {
                ActiveMdiChild.Hide();
                uif.Show();
            }
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != vif)
            {
                ActiveMdiChild.Hide();
                vif.Show();
            }
        }
    }
}
