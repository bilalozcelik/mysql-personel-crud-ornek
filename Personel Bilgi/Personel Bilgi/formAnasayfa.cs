using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Personel_Bilgi
{
    public partial class formAnasayfa : Form
    {
        public formAnasayfa()
        {
            InitializeComponent();
        }

        veritabaniBaglanti vtislem = new veritabaniBaglanti();
        MySqlConnection baglanti;
        MySqlCommand komut;
        string komutSatiri;
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }
        public void listele()
        {
            try
            {
                baglanti = vtislem.baglan();
                komutSatiri = "select * from bilgiler";
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(komutSatiri, baglanti);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                gridPersonel.DataSource = dt;
                gridPersonel.Columns["perID"].HeaderText = "Personel ID";
                gridPersonel.Columns["adi"].HeaderText = "ADI";
                gridPersonel.Columns["soyadi"].HeaderText = "SOYADI";
                gridPersonel.Columns["birimi"].HeaderText = "BİRİMİ";
                gridPersonel.Columns["tel"].HeaderText = "TELEFON";
                gridPersonel.Columns["adres"].HeaderText = "ADRES";


            }
            catch
            {

                MessageBox.Show("hata oluştu");
            }

        }

        private void gridPersonel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtID.Text = gridPersonel.CurrentRow.Cells["perID"].Value.ToString();
                txtAd.Text = gridPersonel.CurrentRow.Cells["adi"].Value.ToString();
                txtSoyad.Text = gridPersonel.CurrentRow.Cells["soyadi"].Value.ToString();
                txtBirim.Text = gridPersonel.CurrentRow.Cells["birimi"].Value.ToString();
                txtTel.Text = gridPersonel.CurrentRow.Cells["tel"].Value.ToString();
                txtAdres.Text = gridPersonel.CurrentRow.Cells["adres"].Value.ToString();
            }
            catch
            {

                MessageBox.Show("hata oluştu");
            }

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                { baglanti.Open(); }
                komutSatiri = "insert into bilgiler (adi,soyadi,birimi,tel,adres) values(@ad,@soyad,@birim,@tel,@adres)";
                komut = new MySqlCommand(komutSatiri, baglanti);
                komut.Parameters.AddWithValue("@ad", txtAd.Text);
                komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
                komut.Parameters.AddWithValue("@birim", txtBirim.Text);
                komut.Parameters.AddWithValue("@tel", txtTel.Text);
                komut.Parameters.AddWithValue("@adres", txtAdres.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                temizle();
                MessageBox.Show("işlem başarılı");
                listele();
            }
            catch
            {

                MessageBox.Show("hata oluştu");
            }

        }
        public void temizle()
        {
            txtID.Clear();
            txtAd.Clear();
            txtSoyad.Clear();
            txtBirim.Clear();
            txtTel.Clear();
            txtAdres.Clear();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                { baglanti.Open(); }

                komutSatiri = "delete from bilgiler where perID=@pID";
                komut = new MySqlCommand(komutSatiri, baglanti);
                komut.Parameters.AddWithValue("@pID", gridPersonel.CurrentRow.Cells["perID"].Value);
                komut.ExecuteNonQuery();
                baglanti.Close();
                temizle();
                MessageBox.Show("işlem başarılı");
                listele();

            }
            catch 
            {

                MessageBox.Show("hata oluştu");
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                { baglanti.Open(); }
                komutSatiri = "update bilgiler set adi=@ad,soyadi=@soyad,birimi=@birim,tel=@tel,adres=@adres where perID=@pID";
                komut = new MySqlCommand(komutSatiri, baglanti);
                komut.Parameters.AddWithValue("@pID", gridPersonel.CurrentRow.Cells["perID"].Value);
                komut.Parameters.AddWithValue("@ad", txtAd.Text);
                komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
                komut.Parameters.AddWithValue("@birim", txtBirim.Text);
                komut.Parameters.AddWithValue("@tel", txtTel.Text);
                komut.Parameters.AddWithValue("@adres", txtAdres.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                temizle();
                MessageBox.Show("işlem başarılı");
                listele();
            }
            catch
            {

                MessageBox.Show("hata oluştu");
            }
        }

        private void txtArama_TextChanged(object sender, EventArgs e)
        {
            personelArama(txtArama.Text);
        }

        public void personelArama(string aranacakKelime)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                { baglanti.Open(); }
                komut = new MySqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "select * from bilgiler where adi like '" + aranacakKelime + "%' or soyadi like '" + aranacakKelime + "%'";
                MySqlDataAdapter da = new MySqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                baglanti.Close();
                gridPersonel.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("hata oluştu");
            }
        }
    }
}
