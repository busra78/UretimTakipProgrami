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
    public partial class Rapor : Form
    {
        int depoKapasitesi = 0;
        int kullanilanAlanVariable = 0;
        int toplamUrunMiktari = 0;
        
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
        public Rapor()
        {
            InitializeComponent();
            db_connection();
        }

    

       
       

        private void Rapor_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'veritabani.Depo_Stok_Urun' table. You can move, or remove it, as needed.
            this.depo_Stok_UrunTableAdapter.Fill(this.veritabani.Depo_Stok_Urun);
            // TODO: This line of code loads data into the 'veritabani.Depo' table. You can move, or remove it, as needed.
            this.depoTableAdapter.Fill(this.veritabani.Depo);
            // TODO: This line of code loads data into the 'veritabani.StokCikis' table. You can move, or remove it, as needed.
            this.stokCikisTableAdapter.Fill(this.veritabani.StokCikis);
            // TODO: This line of code loads data into the 'veritabani.StokGiris' table. You can move, or remove it, as needed.
            this.stokGirisTableAdapter.Fill(this.veritabani.StokGiris);

            string kayit1 = "SELECT * FROM  Depo";

            SqlCommand komut1 = new SqlCommand(kayit1, connect);

            SqlDataAdapter da1 = new SqlDataAdapter(komut1);

            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            dataGridView1.DataSource = dt1;

            dataGridView1.Columns["depoKodu"].HeaderText = "Depo Kodu";

            dataGridView1.Columns["depoAdi"].HeaderText = "Depo Adı";
            dataGridView1.Columns["depoAdres"].HeaderText = "Adres";
            dataGridView1.Columns["depoTelefon"].HeaderText = "Telefon";
            dataGridView1.Columns["depoKapasitesi"].HeaderText = "Depo Kapasitesi";
            dataGridView1.Columns["bosKapasite"].HeaderText = "Boş Kapasite";


            string kayit2 = "SELECT * FROM  Depo_Stok_Urun ";

            SqlCommand komut2 = new SqlCommand(kayit2, connect);
            
            SqlDataAdapter da2 = new SqlDataAdapter(komut2);

            DataTable dt2 = new DataTable();
            da2.Fill(dt2);

            dataGridView2.DataSource = dt2;

            dataGridView2.Columns["depoKodu"].HeaderText = "Depo Kodu";

            dataGridView2.Columns["urunAdi"].HeaderText = "Ürün Adı";
            dataGridView2.Columns["urunMarkasi"].HeaderText = "Ürün Markası";
            dataGridView2.Columns["urunTipi"].HeaderText = "Ürün Tipi";
            dataGridView2.Columns["urunMiktari"].HeaderText = "Ürün Miktarı";

            listBox3.Items.Add("Ödeme Yapılacak Tedarikçi     Ödeme Yapılacak Miktar");
            listBox3.Items.Add("");


            string kayit3 = "SELECT tedarikciAdi,stokGirisToplamMaliyet FROM StokGiris JOIN Tedarikci ON Tedarikci.tedarikciKodu=StokGiris.tedarikciKodu";

            SqlCommand komut3 = new SqlCommand(kayit3, connect);

            using(SqlDataReader reader = komut3.ExecuteReader())
            {
                while (reader.Read())
                {
                    listBox3.Items.Add(Convert.ToString(reader["tedarikciAdi"]) + "                                " + Convert.ToInt32(reader["stokGirisToplamMaliyet"])+ "₺");

                }
            }
            listBox3.Items.Add("");

            string kayit4 = "SELECT SUM(stokGirisToplamMaliyet) AS odenecekToplamMaliyet FROM StokGiris";
            SqlCommand komut4 = new SqlCommand(kayit4, connect);
            using(SqlDataReader reader2 = komut4.ExecuteReader())
            {
                while (reader2.Read())
                {
                    listBox3.Items.Add("Ödenecek Toplam Maliyet:           " + Convert.ToString(reader2["odenecekToplamMaliyet"])+ "₺");
                }
            }


            listBox4.Items.Add("Ödeme Alınacak Müşteri     Ödeme Alınacak Miktar");
            listBox4.Items.Add("");
            string kayit5 = "SELECT musteriAdi,stokCikisToplamMaliyet FROM stokCikis JOIN Musteri ON Musteri.musteriKodu=StokCikis.musteriKodu";
            SqlCommand komut5 = new SqlCommand(kayit5, connect);
            using(SqlDataReader reader3 = komut5.ExecuteReader())
            {
                if (reader3.Read())
                {
                    listBox4.Items.Add(Convert.ToString(reader3["musteriAdi"]) + "                                " + Convert.ToInt32(reader3["stokCikisToplamMaliyet"])+ "₺");

                }
            }
            listBox4.Items.Add("");

            string kayit6 = "SELECT SUM(stokCikisToplamMaliyet) AS alınacakToplamMaliyet FROM StokCikis";
            SqlCommand komut6 = new SqlCommand(kayit6, connect);
            using (SqlDataReader reader4 = komut6.ExecuteReader())
            {
                if (reader4.Read())
                {
                    listBox4.Items.Add("Alınacak Toplam Maliyet:           " + Convert.ToString(reader4["alınacakToplamMaliyet"])+ "₺");
                }
            }
            

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (depoKoducomboBox1.SelectedItem != null)
            {
                string depoDurumu = "SELECT * FROM Depo WHERE depoKodu=@depoKodu";
                SqlCommand cmd1 = new SqlCommand(depoDurumu, connect);
                cmd1.Parameters.AddWithValue("@depoKodu", depoKoducomboBox1.SelectedValue.ToString());

                using (SqlDataReader reader = cmd1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        depoKodu.Text = Convert.ToString(reader["depoKodu"]);
                        depoKapasitesi = Convert.ToInt32(reader["depoKapasitesi"]);
                        kullanilanAlanVariable = Convert.ToInt32(reader["depoKapasitesi"]) - Convert.ToInt32(reader["bosKapasite"]);
                        kullanılanAlan.Text = kullanilanAlanVariable.ToString();
                    }


                }
                if (depoKapasitesi - kullanilanAlanVariable == 10)
                {
                    kritikDepoDurumu.Text = "Depo Durumu Kritik";
                    kritikDepoDurumu.ForeColor = Color.Red;
                }
                else
                {
                    kritikDepoDurumu.Text = "Depo Durumu Müsait";
                    kritikDepoDurumu.ForeColor = Color.Green;
                }
            }
           
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            uruneGore.Visible = false;
            depoyaGoreGroupBox.Visible = true;
        }

        private void depoyaGorecomboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string depoyaGoreDepoStok = "SELECT SUM(urunMiktari) as toplamUrunMiktari FROM Depo_Stok_Urun WHERE depoKodu=@depoKodu";
            SqlCommand cmd1 = new SqlCommand(depoyaGoreDepoStok, connect);
            if (depoyaGorecomboBox1.SelectedItem != null)
            {
                cmd1.Parameters.AddWithValue("@depoKodu", depoyaGorecomboBox1.SelectedValue);

                using (SqlDataReader reader = cmd1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        depoyaGoreDepoKodutextBox1.Text = depoyaGorecomboBox1.SelectedValue.ToString();
                        toplamUrunMiktaritextBox1.Text = Convert.ToString(reader["toplamUrunMiktari"]);
                        
                    }


                }
                cmd1.ExecuteNonQuery();
            }
            listBox1.Items.Add(String.Format("{0}      {1}","Ürün Adı","Ürün Miktarı"));
            listBox1.Items.Add("");
            string depoyaGoreDepoStok1 = "SELECT UrunAdi,UrunMarkasi,urunTipi,urunMiktari FROM Depo_Stok_Urun WHERE depoKodu=@depoKodu";
            SqlCommand cmd2 = new SqlCommand(depoyaGoreDepoStok1, connect);
            if (depoyaGorecomboBox1.SelectedItem != null)
            {
                cmd2.Parameters.AddWithValue("@depoKodu", depoyaGorecomboBox1.SelectedValue.ToString());

                using (SqlDataReader reader2 = cmd2.ExecuteReader())
                {
                    while (reader2.Read())
                    {
                        listBox1.Items.Add(String.Format("{0}        {1}",Convert.ToString(reader2["urunAdi"]),Convert.ToString(reader2["urunMiktari"])));
                       
                    }


                }
                cmd2.ExecuteNonQuery();
            }
           



        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            depoyaGoreGroupBox.Visible = false;
            uruneGore.Visible = true;
            
         

            //STOK GIRISI ÜRÜN KATEGORİSİNİ YÜKLE
            string cmdTexturun = "SELECT DISTINCT urunAdi FROM Depo_Stok_Urun";
            SqlCommand cmd1 = new SqlCommand(cmdTexturun, connect);
            SqlDataReader readerurun;
            readerurun = cmd1.ExecuteReader();
            DataTable dtUrun = new DataTable();
            dtUrun.Load(readerurun);
            urunAdicomboBox3.ValueMember = "urunAdi";
            urunAdicomboBox3.DisplayMember = "urunAdi";
            urunAdicomboBox3.DataSource = dtUrun;





        }

        

        private void urunTipicomboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            string cmdTexturun = "SELECT DISTINCT urunMarkasi FROM Depo_Stok_Urun WHERE urunAdi=@urunAdi AND urunTipi=@urunTipi";
            SqlCommand cmd1 = new SqlCommand(cmdTexturun, connect);
            cmd1.Parameters.AddWithValue("@urunAdi", urunAdicomboBox3.SelectedValue.ToString());
            cmd1.Parameters.AddWithValue("@urunTipi", urunTipicomboBox2.SelectedValue.ToString());
            SqlDataReader readerurun;
            readerurun = cmd1.ExecuteReader();
            DataTable dttip = new DataTable();
            dttip.Load(readerurun);
            urunMarkasicomboBox1.ValueMember = "urunMarkasi";
            urunMarkasicomboBox1.DisplayMember = "urunMarkasi";
            urunMarkasicomboBox1.DataSource = dttip;
        }

        private void urunAdicomboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cmdTexturun = "SELECT DISTINCT urunTipi FROM Depo_Stok_Urun WHERE urunAdi=@urunAdi ";
            SqlCommand cmd1 = new SqlCommand(cmdTexturun, connect);
            cmd1.Parameters.AddWithValue("@urunAdi", urunAdicomboBox3.SelectedValue.ToString());

            SqlDataReader readerurun;
            readerurun = cmd1.ExecuteReader();
            DataTable dttip = new DataTable();
            dttip.Load(readerurun);
            urunTipicomboBox2.ValueMember = "urunTipi";
            urunTipicomboBox2.DisplayMember = "urunTipi";
            urunTipicomboBox2.DataSource = dttip;
        }

        private void urunMarkasicomboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            if(urunAdicomboBox3.SelectedItem!=null && urunTipicomboBox2.SelectedItem!=null && urunMarkasicomboBox1.SelectedItem != null)
            {
                string depoyaGoreDepoStok1 = "SELECT depoKodu,urunMiktari FROM Depo_Stok_Urun WHERE urunAdi=@urunAdi AND urunMarkasi=@urunMarkasi AND urunTipi=@urunTipi ";
                SqlCommand cmd2 = new SqlCommand(depoyaGoreDepoStok1, connect);
               
                    cmd2.Parameters.AddWithValue("@urunAdi", urunAdicomboBox3.SelectedValue.ToString());
                cmd2.Parameters.AddWithValue("@urunTipi", urunTipicomboBox2.SelectedValue.ToString());
                cmd2.Parameters.AddWithValue("@urunMarkasi", urunMarkasicomboBox1.SelectedValue.ToString());

                using (SqlDataReader reader2 = cmd2.ExecuteReader())
                    {
                    string baslik = "Depo Kodları  Miktarları ";
                    listBox2.Items.Add(baslik);
                    while (reader2.Read())
                        {
                        string depoUrunMiktari = "       " + Convert.ToString(reader2["depoKodu"]) + "     " + Convert.ToInt32(reader2["urunMiktari"]);

                        listBox2.Items.Add(depoUrunMiktari);

                        }
                    listBox2.Items.Add("");

                }
                    cmd2.ExecuteNonQuery();
                string depoyaGoreDepoStok2 = "SELECT SUM(urunMiktari) AS toplamUrunMiktari FROM Depo_Stok_Urun WHERE urunAdi=@urunAdi AND urunMarkasi=@urunMarkasi AND urunTipi=@urunTipi ";
                SqlCommand cmd3 = new SqlCommand(depoyaGoreDepoStok2, connect);

                cmd3.Parameters.AddWithValue("@urunAdi", urunAdicomboBox3.SelectedValue.ToString());
                cmd3.Parameters.AddWithValue("@urunTipi", urunTipicomboBox2.SelectedValue.ToString());
                cmd3.Parameters.AddWithValue("@urunMarkasi", urunMarkasicomboBox1.SelectedValue.ToString());

                using (SqlDataReader reader3 = cmd3.ExecuteReader())
                {
                    string baslik = "";


                    while (reader3.Read())
                    {
                        baslik = "Toplam Ürun Miktari:" + Convert.ToInt32(reader3["toplamUrunMiktari"]);

                    }
                    listBox2.Items.Add(baslik);

                }
                cmd3.ExecuteNonQuery();


            }
        }

       
    }
}
