using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Sales
{
    public partial class Form_Photo : Form
    {
        public static string PHOTOPATH = string.Empty;
        public Form_Photo()
        {
            InitializeComponent();
        }

        private void Form_Photo_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = GetImage(PHOTOPATH);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        public Image GetImage(string path)
        {
            Image result = null;
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                result = Image.FromStream(fs);
                fs.Close();
            }
            catch
            {

            }
            return result;
        }
    }
}
