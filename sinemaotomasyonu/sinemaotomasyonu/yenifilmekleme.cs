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
    public partial class yenifilmekleme : Form
    {
        public yenifilmekleme()
        {
            InitializeComponent();
        }
        void comboboxveriaktar()
        {
            sinemaclass.kontrol(sinemaclass.baglanti);

            SqlCommand aktar = new SqlCommand("select turadi  from turblg", sinemaclass.baglanti);

            SqlDataReader oku = aktar.ExecuteReader();

            while (oku.Read())
            {
                comboBox1.Items.Add(oku["turadi"]);
            }
            oku.Close();
        }

        void listeleme()
        {
            sinemaclass.kontrol(sinemaclass.baglanti);
            SqlCommand komut = new SqlCommand("select * from filmblg", sinemaclass.baglanti);
            SqlDataAdapter adr = new SqlDataAdapter(komut);
            DataTable tablo = new DataTable();
            adr.Fill(tablo);
            dataGridView1.DataSource = tablo;
        }
        private void yenifilmekleme_Load(object sender, EventArgs e)
        {
            // filmadi,filmsüresi,turadi,filmfiyat
            comboboxveriaktar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            SqlCommand ekle = new SqlCommand($"insert into filmblg(filmadi,filmsüresi,turadi,filmfiyat) values(@filmname,@filmclock,@turname,@filmticket)", sinemaclass.baglanti);
            ekle.Parameters.AddWithValue("@filmname", textBox1.Text);
            ekle.Parameters.AddWithValue("@filmclock", textBox2.Text);
            ekle.Parameters.AddWithValue("@turname", comboBox1.Text);
            ekle.Parameters.AddWithValue("@filmticket", textBox4.Text);

            ekle.ExecuteNonQuery();
            sinemaclass.kontrol(sinemaclass.baglanti);
            listeleme();
        }

        string silinecekID;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            textBox1.Text = (dataGridView1.CurrentRow.Cells["filmadi"].Value).ToString();
            textBox2.Text = (dataGridView1.CurrentRow.Cells["filmsüresi"].Value).ToString();
            comboBox1.Text = (dataGridView1.CurrentRow.Cells["turadi"].Value).ToString();
            textBox4.Text = (dataGridView1.CurrentRow.Cells["filmfiyat"].Value).ToString();

            silinecekID = (dataGridView1.CurrentRow.Cells["filmID"].Value).ToString();
            textBox3.Text = silinecekID.ToString();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            sinemaclass.kontrol(sinemaclass.baglanti);

            SqlCommand sil = new SqlCommand("delete from filmblg where filmID=@filmıd", sinemaclass.baglanti);

            sil.Parameters.AddWithValue("@filmıd", Convert.ToInt32(silinecekID));
            sil.ExecuteNonQuery();
            listeleme();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sinemaclass.kontrol(sinemaclass.baglanti);

            SqlCommand düzenle = new SqlCommand("update filmblg set filmadi=@filmname,filmsüresi=@filmclock,turadi=@turname,filmfiyat=@filmticket where filmID=@filmıd", sinemaclass.baglanti);

            düzenle.Parameters.AddWithValue("@filmname", textBox1.Text);
            düzenle.Parameters.AddWithValue("@filmclock", textBox2.Text);
            düzenle.Parameters.AddWithValue("@turname", comboBox1.Text);
            düzenle.Parameters.AddWithValue("@filmticket", textBox4.Text);
            düzenle.Parameters.AddWithValue("@filmıd", textBox3.Text);


            düzenle.ExecuteNonQuery();
            listeleme();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            admindüzenleme frm5 = new admindüzenleme();
            this.Hide();
            frm5.Show();
        }
    }
}
