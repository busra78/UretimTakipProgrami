using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UretimTakipProgrami
{
    public partial class Anasayfa : Form
    {
        public string conn;
        public SqlConnection connect;
        public SqlDataAdapter mySqlDataAdapter;
        public void db_connection()
        {
            try
            {
                conn = "Data Source=DESKTOP-R4LILUJ\\SQLEXPRESS;Initial Catalog=UretimTakipVeritabani;Integrated Security=True";
                connect = new SqlConnection(conn);
                connect.Open();

            }
            catch (SqlException)
            {
                throw;
            }
        }
        public Anasayfa()
        {
            InitializeComponent();
            db_connection();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Tanimlama f = new Tanimlama();
            f.Show();

        }

        private void button6_Click(object sender, EventArgs e)
        {

            Listeleme f = new Listeleme();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
      
            Stok f = new Stok();
            f.Show();
        }

    

        private void button3_Click(object sender, EventArgs e)
        {
     
            Rapor f = new Rapor();
            f.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Anasayfa t = new Anasayfa();
            t.Show();
        }

       

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Giris t = new Giris();
            t.Show();
        }

        private void Anasayfa_Load(object sender, EventArgs e)
        {
            string kayit1 = "SELECT * FROM  StokGiris";

            SqlCommand komut1 = new SqlCommand(kayit1, connect);

            SqlDataAdapter da1 = new SqlDataAdapter(komut1);

            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            dataGridView1.DataSource = dt1;

            dataGridView1.Columns["stokGirisKodu"].HeaderText = "Stok Giriş Kodu";

            dataGridView1.Columns["stokGirisTarihi"].HeaderText = "Stok Giriş Tarihi";
            dataGridView1.Columns["tedarikciKodu"].HeaderText = "Tedarikçi Kodu";
            dataGridView1.Columns["urun_Markasi"].HeaderText = "Ürün Markası";
            dataGridView1.Columns["urun_Adi"].HeaderText = "Ürün Adı";
            dataGridView1.Columns["urun_Tipi"].HeaderText = "Ürün Tipi";
            dataGridView1.Columns["stokGirisMiktari"].HeaderText = "Stok Giriş Miktarı";
            dataGridView1.Columns["stokGirisAciklama"].HeaderText = "Açıklama";
            dataGridView1.Columns["stokGirisToplamMaliyet"].HeaderText = "Toplam Maliyet";
            dataGridView1.Columns["depoKodu"].HeaderText = "Depo Kodu";


            string kayit2 = "SELECT * FROM  StokCikis";

            SqlCommand komut2 = new SqlCommand(kayit2, connect);

            SqlDataAdapter da2 = new SqlDataAdapter(komut2);

            DataTable dt2 = new DataTable();
            da2.Fill(dt2);

            dataGridView2.DataSource = dt2;

            dataGridView2.Columns["stokCikisKodu"].HeaderText = "Stok Çıkış Kodu";

            dataGridView2.Columns["stokCikisTarihi"].HeaderText = "Stok Çıkış Tarihi";
            dataGridView2.Columns["musteriKodu"].HeaderText = "Müşteri Kodu";
            dataGridView2.Columns["urunMarkasi"].HeaderText = "Ürün Markası";
            dataGridView2.Columns["urunAdi"].HeaderText = "Ürün Adı";
            dataGridView2.Columns["urunTipi"].HeaderText = "Ürün Tipi";
            dataGridView2.Columns["stokCikisMiktari"].HeaderText = "Stok Çıkış Miktarı";
            dataGridView2.Columns["stokCikisAciklama"].HeaderText = "Açıklama";
            dataGridView2.Columns["stokCikisToplamMaliyet"].HeaderText = "Toplam Maliyet";
            dataGridView2.Columns["depoKodu"].HeaderText = "Depo Kodu";


            string kayit3 = "SELECT * FROM  StokTransfer";

            SqlCommand komut3 = new SqlCommand(kayit3, connect);

            SqlDataAdapter da3 = new SqlDataAdapter(komut3);

            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            dataGridView3.DataSource = dt3;

            dataGridView3.Columns["stokTransferKodu"].HeaderText = "Stok Transfer Kodu";

            dataGridView3.Columns["stokTransferTarihi"].HeaderText = "Stok Transfer Tarihi";
            dataGridView3.Columns["bulunduguDepoKodu"].HeaderText = "Bulunduğu Depo Kodu";
            dataGridView3.Columns["urunMarkasi"].HeaderText = "Ürün Markası";
            dataGridView3.Columns["urunAdi"].HeaderText = "Ürün Adı";
            dataGridView3.Columns["urunTipi"].HeaderText = "Ürün Tipi";
            dataGridView3.Columns["transferDepoKodu"].HeaderText = "Transfer Depo Kodu";
            dataGridView3.Columns["transferMiktari"].HeaderText = "Transfer Miktarı";

            string kayit4 = "SELECT * FROM  StokTransferIptal";

            SqlCommand komut4 = new SqlCommand(kayit4, connect);

            SqlDataAdapter da4 = new SqlDataAdapter(komut4);

            DataTable dt4 = new DataTable();
            da4.Fill(dt4);

            dataGridView4.DataSource = dt4;

            dataGridView4.Columns["stokTransferKodu"].HeaderText = "Stok Transfer Iptal Kodu";

            dataGridView4.Columns["stokTransferIptalTarihi"].HeaderText = "Stok Transfer Iptal Tarihi";



            string kayit5 = "SELECT * FROM  StokCikisIptal";

            SqlCommand komut5 = new SqlCommand(kayit5, connect);

            SqlDataAdapter da5 = new SqlDataAdapter(komut5);

            DataTable dt5 = new DataTable();
            da5.Fill(dt5);

            dataGridView5.DataSource = dt5;

            dataGridView5.Columns["stokCikisKodu"].HeaderText = "Stok Çıkış Iptal Kodu";

            dataGridView5.Columns["stokCikisIptalTarihi"].HeaderText = "Stok Çıkış Iptal Tarihi";



            string kayit6 = "SELECT * FROM  StokGirisIptal";

            SqlCommand komut6 = new SqlCommand(kayit6, connect);

            SqlDataAdapter da6 = new SqlDataAdapter(komut6);

            DataTable dt6 = new DataTable();
            da6.Fill(dt6);

            dataGridView6.DataSource = dt6;

            dataGridView6.Columns["stokGirisKodu"].HeaderText = "Stok Giriş Iptal Kodu";

            dataGridView6.Columns["stokGirisIptalTarihi"].HeaderText = "Stok Giriş Iptal Tarihi";


        }

        private void button7_Click(object sender, EventArgs e)
        {
            string kayit1 = "SELECT * FROM  StokGiris";

            SqlCommand komut1 = new SqlCommand(kayit1, connect);

            SqlDataAdapter da1 = new SqlDataAdapter(komut1);

            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            dataGridView1.DataSource = dt1;

            dataGridView1.Columns["stokGirisKodu"].HeaderText = "Stok Giriş Kodu";

            dataGridView1.Columns["stokGirisTarihi"].HeaderText = "Stok Giriş Tarihi";
            dataGridView1.Columns["tedarikciKodu"].HeaderText = "Tedarikçi Kodu";
            dataGridView1.Columns["urun_Markasi"].HeaderText = "Ürün Markası";
            dataGridView1.Columns["urun_Adi"].HeaderText = "Ürün Adı";
            dataGridView1.Columns["urun_Tipi"].HeaderText = "Ürün Tipi";
            dataGridView1.Columns["stokGirisMiktari"].HeaderText = "Stok Giriş Miktarı";
            dataGridView1.Columns["stokGirisAciklama"].HeaderText = "Açıklama";
            dataGridView1.Columns["stokGirisToplamMaliyet"].HeaderText = "Toplam Maliyet";
            dataGridView1.Columns["depoKodu"].HeaderText = "Depo Kodu";


            string kayit2 = "SELECT * FROM  StokCikis";

            SqlCommand komut2 = new SqlCommand(kayit2, connect);

            SqlDataAdapter da2 = new SqlDataAdapter(komut2);

            DataTable dt2 = new DataTable();
            da2.Fill(dt2);

            dataGridView2.DataSource = dt2;

            dataGridView2.Columns["stokCikisKodu"].HeaderText = "Stok Çıkış Kodu";

            dataGridView2.Columns["stokCikisTarihi"].HeaderText = "Stok Çıkış Tarihi";
            dataGridView2.Columns["musteriKodu"].HeaderText = "Müşteri Kodu";
            dataGridView2.Columns["urunMarkasi"].HeaderText = "Ürün Markası";
            dataGridView2.Columns["urunAdi"].HeaderText = "Ürün Adı";
            dataGridView2.Columns["urunTipi"].HeaderText = "Ürün Tipi";
            dataGridView2.Columns["stokCikisMiktari"].HeaderText = "Stok Çıkış Miktarı";
            dataGridView2.Columns["stokCikisAciklama"].HeaderText = "Açıklama";
            dataGridView2.Columns["stokCikisToplamMaliyet"].HeaderText = "Toplam Maliyet";
            dataGridView2.Columns["depoKodu"].HeaderText = "Depo Kodu";


            string kayit3 = "SELECT * FROM  StokTransfer";

            SqlCommand komut3 = new SqlCommand(kayit3, connect);

            SqlDataAdapter da3 = new SqlDataAdapter(komut3);

            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            dataGridView3.DataSource = dt3;

            dataGridView3.Columns["stokTransferKodu"].HeaderText = "Stok Transfer Kodu";

            dataGridView3.Columns["stokTransferTarihi"].HeaderText = "Stok Transfer Tarihi";
            dataGridView3.Columns["bulunduguDepoKodu"].HeaderText = "Bulunduğu Depo Kodu";
            dataGridView3.Columns["urunMarkasi"].HeaderText = "Ürün Markası";
            dataGridView3.Columns["urunAdi"].HeaderText = "Ürün Adı";
            dataGridView3.Columns["urunTipi"].HeaderText = "Ürün Tipi";
            dataGridView3.Columns["transferDepoKodu"].HeaderText = "Transfer Depo Kodu";
            dataGridView3.Columns["transferMiktari"].HeaderText = "Transfer Miktarı";

            string kayit4 = "SELECT * FROM  StokTransferIptal";

            SqlCommand komut4 = new SqlCommand(kayit4, connect);

            SqlDataAdapter da4 = new SqlDataAdapter(komut4);

            DataTable dt4 = new DataTable();
            da4.Fill(dt4);

            dataGridView4.DataSource = dt4;

            dataGridView4.Columns["stokTransferKodu"].HeaderText = "Stok Transfer Iptal Kodu";

            dataGridView4.Columns["stokTransferIptalTarihi"].HeaderText = "Stok Transfer Iptal Tarihi";



            string kayit5 = "SELECT * FROM  StokCikisIptal";

            SqlCommand komut5 = new SqlCommand(kayit5, connect);

            SqlDataAdapter da5 = new SqlDataAdapter(komut5);

            DataTable dt5 = new DataTable();
            da5.Fill(dt5);

            dataGridView5.DataSource = dt5;

            dataGridView5.Columns["stokCikisKodu"].HeaderText = "Stok Çıkış Iptal Kodu";

            dataGridView5.Columns["stokCikisIptalTarihi"].HeaderText = "Stok Çıkış Iptal Tarihi";



            string kayit6 = "SELECT * FROM  StokGirisIptal";

            SqlCommand komut6 = new SqlCommand(kayit6, connect);

            SqlDataAdapter da6 = new SqlDataAdapter(komut6);

            DataTable dt6 = new DataTable();
            da6.Fill(dt6);

            dataGridView6.DataSource = dt6;

            dataGridView6.Columns["stokGirisKodu"].HeaderText = "Stok Giriş Iptal Kodu";

            dataGridView6.Columns["stokGirisIptalTarihi"].HeaderText = "Stok Giriş Iptal Tarihi";

        }
    }
}
