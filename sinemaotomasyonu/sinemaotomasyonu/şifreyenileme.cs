using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using sinemaotomasyonu.sinemaklsr;

namespace sinemaotomasyonu
{
    public partial class şifreyenileme : Form
    {
        public şifreyenileme()
        {
            InitializeComponent();
        }

        private void şifreyenileme_Load(object sender, EventArgs e)
        {

        }
        public static string MD5Sifrele(string sifrelenecekMetin)
        {
            string hash = " ";

            byte[] data = Encoding.Default.GetBytes(sifrelenecekMetin);

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(Encoding.Default.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider()
                { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })

                {
                    ICryptoTransform transform = tripleDES.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            sinemaclass.kontrol(sinemaclass.baglanti);
            string yenisifre = MD5Sifrele(textBox2.Text);
            if (textBox2.Text == textBox3.Text)
            {
                SqlCommand düzenle = new SqlCommand("update müsteriblg set müsterisifre=@sifre where müsterikullaniciadi=@name ", sinemaclass.baglanti);

                düzenle.Parameters.AddWithValue("@sifre", yenisifre);
                düzenle.Parameters.AddWithValue("@name", textBox1.Text);

                düzenle.ExecuteNonQuery();
            }
            MessageBox.Show("şifre güncellendi");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UserControl1 frm5 = new UserControl1();
            this.Hide();
            frm5.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
