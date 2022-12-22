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
using sinemaotomasyonu.sinemaklsr;

namespace sinemaotomasyonu
{
    public partial class vizyondüzenleme : Form
    {
        public vizyondüzenleme()
        {
            InitializeComponent();
        }

        private void vizyondüzenleme_Load(object sender, EventArgs e)
        {
            listeleme();
        }
        void listeleme()
        {
            sinemaclass.kontrol(sinemaclass.baglanti);
            SqlCommand komut = new SqlCommand("select top 1 salonadi,filmadi,tarih,saat,koltukno from satisblg order by satisID desc  ",sinemaclass.baglanti);
            SqlDataReader okuma = komut.ExecuteReader();
            while(okuma.Read())
            {
                label1.Text = okuma["salonadi"].ToString();
                label2.Text = okuma["koltukno"].ToString();
                label3.Text = okuma["tarih"].ToString();
                label4.Text = okuma["saat"].ToString();
                label5.Text = okuma["filmadi"].ToString();
            }
            okuma.Close();
           komut.ExecuteNonQuery();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
