using System.Configuration;
using System.Data.SqlClient;

namespace MiniGallery
{
    public partial class UploadImageForm : Form
    {
        public UploadImageForm()
        {
            InitializeComponent();
        }

        public void Insert(string filePath, byte[] byteArr)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
                using (SqlConnection conn = new(connString))
                {
                    conn.Open();
                    string cmdText = "INSERT INTO Pictures(FilePath, Image) VALUES(@FilePath, @Image);";
                    using (SqlCommand cmd = new(cmdText, conn))
                    {
                        cmd.Parameters.AddWithValue("@FilePath", filePath);
                        cmd.Parameters.AddWithValue("@Image", byteArr);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Image Uploaded Successfully!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private byte[] ConvertImageToBinaryArr(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new() { Filter = "Joseph (*.jpg *.jpeg)|*.jpg;*.jpeg;", Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pbUploadImage.Image = Image.FromFile(ofd.FileName);
                    txtFilePath.Text = ofd.FileName;
                    Insert(txtFilePath.Text, ConvertImageToBinaryArr(pbUploadImage.Image));
                }
            }
        }
    }
}