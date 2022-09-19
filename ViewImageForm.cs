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
    public partial class ViewImageForm : Form
    {
        public ViewImageForm()
        {
            InitializeComponent();
        }

        private Image ConvertByteArrToImage(byte[] byteArr)
        {
            using (MemoryStream ms = new MemoryStream(byteArr))
            {
                return Image.FromStream(ms);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
                using (SqlConnection conn = new(connString))
                {
                    conn.Open();
                    string cmdText = "SELECT * FROM Pictures";
                    using (SqlCommand cmd = new(cmdText, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            flpImages.Controls.Clear();
                            while (reader.Read())
                            {
                                PictureBox pictureBox1 = new();
                                pictureBox1.Height = 320;
                                pictureBox1.Width = 320;
                                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                                pictureBox1.Image = ConvertByteArrToImage((byte[])reader[2]);

                                flpImages.Controls.Add(pictureBox1);
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
