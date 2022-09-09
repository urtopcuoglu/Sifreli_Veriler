using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Sifreli_Veriler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=THINKPAD-E470;Initial Catalog=DBSifreleme;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {

            verigöster();
            
        }

        void verigöster()
        {
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBLVERİLER", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string isim = txtAd.Text;
            byte []isimdizi = ASCIIEncoding.ASCII.GetBytes(isim); //ASCIIEncoding olarak şifrele
            string adsifre=Convert.ToBase64String(isimdizi) ;
            

            string soyad = txtSoyad.Text;
            byte []soyadidizi = ASCIIEncoding.ASCII.GetBytes(soyad); 
            string soyadsifre = Convert.ToBase64String(soyadidizi) ;

            string mail = txtMail.Text;
            byte[] mailidizi = ASCIIEncoding.ASCII.GetBytes(mail);
            string mailsifre = Convert.ToBase64String(mailidizi);

            string sifre = txtSifre.Text;
            byte[] sifredizi = ASCIIEncoding.ASCII.GetBytes(sifre);
            string sifrepass = Convert.ToBase64String(sifredizi);

            string hesapno = txtHesapNo.Text;
            byte[] hesapnodizi = ASCIIEncoding.ASCII.GetBytes(hesapno);
            string hesapnosifre = Convert.ToBase64String(hesapnodizi);

            conn.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO TBLVERİLER(AD,SOYAD,MAIL,SIFRE,HESAPNO) VALUES(@P1,@P2,@P3,@P4,@P5)",conn);
            komut.Parameters.AddWithValue("@P1",adsifre);
            komut.Parameters.AddWithValue("@P2",soyadsifre);
            komut.Parameters.AddWithValue("@P3",mailsifre);
            komut.Parameters.AddWithValue("@P4",sifrepass);
            komut.Parameters.AddWithValue("@P5",hesapnosifre);
            komut.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Şifreleme işlemi başarılı :) . Veriler eklendi");
            verigöster();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtSifre.Text = "";
            txtMail.Text = "";
            txtHesapNo.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string adcozum = txtAd.Text;
            byte[] adcozumdizi = Convert.FromBase64String(adcozum);
            string adveri = ASCIIEncoding.ASCII.GetString(adcozumdizi);
            
        }
    }
}
