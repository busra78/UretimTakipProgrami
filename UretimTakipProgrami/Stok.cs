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
    public partial class Stok : Form
    {
        //STOK GIRISI ICIN
     
      
        float stokGirisUrunBirimFiyati=0;
        int eskiGirisdepoBosKapasite=0;
        int yeniGirisdepoBosKapasite=0;
        int urunMiktari=0;
        int eskiUrunMiktari = 0;
        int yeniUrunMiktari = 0;

        //STOK CIKISI ICIN
  
     
        float stokCikisUrunBirimFiyati = 0;
        int eskiCikisdepoBosKapasite = 0;
        int yeniCikisdepoBosKapasite = 0;
        int urunMiktariCikis = 0;
        bool reader = false;
        int eskiUrunMiktari1 = 0;
        int yeniUrunMiktari1 = 0;


        //STOK TRANSFER
        bool readerTransfer = false;
        bool fazlaUrunMiktari = false;
        bool azUrunMiktari = false;
        bool esitUrunMiktari = false;
        int AnlikUrunMiktari = 0;
        int urunMiktariTransfer = 0;
        int AnlikUrunMiktariTransfer = 0;

        int bulunanEskiBosKapasite = 0;
        int bulunanYeniBosKapasite = 0;
        int transferEskiBosKapasite = 0;
        int transferYeniBosKapasite = 0;
        int eskiUrunMiktari2 = 0;
        int yeniUrunMiktari2 = 0;



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

        public Stok()
        {
            InitializeComponent();
            db_connection();
        }


        private void Stok_Load(object sender, EventArgs e)
        {
            
            this.musteriTableAdapter.Fill(this.veritabani.Musteri);
            this.depoTableAdapter.Fill(this.veritabani.Depo);
            this.tedarikciTableAdapter.Fill(this.veritabani.Tedarikci);


            //STOK GIRISI ÜRÜN KATEGORİSİNİ YÜKLE
            string cmdTexturunKategorisi = "SELECT DISTINCT urunKategorisi FROM Urun";
            SqlCommand cmd1 = new SqlCommand(cmdTexturunKategorisi, connect);
            SqlDataReader readerurunkategorisi;
            readerurunkategorisi = cmd1.ExecuteReader();
            DataTable dtKategori = new DataTable();
            dtKategori.Load(readerurunkategorisi);
            urunKategorisicomboBox5.ValueMember = "urunKategorisi";
            urunKategorisicomboBox5.DisplayMember = "urunKategorisi";
            urunKategorisicomboBox5.DataSource = dtKategori;

            //STOK CIKISI DEPO KODUNU YÜKLE
            string cmdTextDepo = "SELECT  depoKodu FROM Depo";
            SqlCommand cmd2 = new SqlCommand(cmdTextDepo, connect);
            SqlDataReader readerDepo;
            readerDepo = cmd2.ExecuteReader();
            DataTable dtDepo = new DataTable();
            dtDepo.Load(readerDepo);
            depoKodu.ValueMember = "depoKodu";
            depoKodu.DisplayMember = "depoKodu";
            depoKodu.DataSource = dtDepo;


            //STOK TRANSFER BULUNDUGU DEPO KODUNU YÜKLE
            string cmdTextDepoTransfer1 = "SELECT  depoKodu FROM Depo";
            SqlCommand cmd3 = new SqlCommand(cmdTextDepoTransfer1, connect);
            SqlDataReader readerDepoTransfer1;
            readerDepoTransfer1 = cmd3.ExecuteReader();
            DataTable dtDepoTransfer1 = new DataTable();
            dtDepoTransfer1.Load(readerDepoTransfer1);
            bulunduguDepocomboBox13.ValueMember = "depoKodu";
            bulunduguDepocomboBox13.DisplayMember = "depoKodu";
            bulunduguDepocomboBox13.DataSource = dtDepoTransfer1;
            cmd3.ExecuteNonQuery();



            //STOK TRANSFER TRANSFER DEPO KODUNU YÜKLE
            string cmdTextDepoTransfer2 = "SELECT  depoKodu FROM Depo WHERE NOT depoKodu=@depoKodu";
            SqlCommand cmd4 = new SqlCommand(cmdTextDepoTransfer2, connect);
            cmd4.Parameters.AddWithValue("@depoKodu", bulunduguDepocomboBox13.SelectedValue);
           
            SqlDataReader readerDepoTransfer2;
            readerDepoTransfer2 = cmd4.ExecuteReader();
            DataTable dtDepoTransfer2 = new DataTable();
            dtDepoTransfer2.Load(readerDepoTransfer2);
            transferdepocomboBox14.ValueMember = "depoKodu";
            transferdepocomboBox14.DisplayMember = "depoKodu";
            transferdepocomboBox14.DataSource = dtDepoTransfer2;
            cmd4.ExecuteNonQuery();


            //STOK GIRIS GRIDVIEW
            string kayit1 = "SELECT * FROM  StokGiris ";

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

            //STOK CIKIS GRIDVIEW
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


            //STOK TRANSFER GRIDVIEW

       
            string kayit3 = "SELECT * FROM  StokTransfer";

            SqlCommand komut3 = new SqlCommand(kayit3, connect);

            SqlDataAdapter da3 = new SqlDataAdapter(komut3);

            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            dataGridView6.DataSource = dt3;

            dataGridView6.Columns["stokTransferKodu"].HeaderText = "Stok Transfer Kodu";

            dataGridView6.Columns["stokTransferTarihi"].HeaderText = "Stok Transfer Tarihi";
            dataGridView6.Columns["bulunduguDepoKodu"].HeaderText = "Bulunduğu Depo Kodu";
            dataGridView6.Columns["urunMarkasi"].HeaderText = "Ürün Markası";
            dataGridView6.Columns["urunAdi"].HeaderText = "Ürün Adı";
            dataGridView6.Columns["urunTipi"].HeaderText = "Ürün Tipi";
            dataGridView6.Columns["transferDepoKodu"].HeaderText = "Transfer Depo Kodu";
            dataGridView6.Columns["transferMiktari"].HeaderText = "Transfer Miktarı";
           






        }


        //STOK GİRİSİ-1
        private void StokGirisiYap_Click(object sender, EventArgs e)
        {
           
            try
            {

                string isAvailableDepo = "SELECT bosKapasite FROM Depo WHERE depoKodu=@depoKodu";
                SqlCommand cmd1 = new SqlCommand(isAvailableDepo, connect);
                cmd1.Parameters.AddWithValue("@depoKodu", depoAdicomboBox4.SelectedValue.ToString());

                using (SqlDataReader reader = cmd1.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        eskiGirisdepoBosKapasite = Convert.ToInt32(reader["bosKapasite"]);
                    }

                }

                cmd1.ExecuteNonQuery();
                yeniGirisdepoBosKapasite = eskiGirisdepoBosKapasite - Convert.ToInt32(stokGirisMiktaritextBox1.Text);

                if (yeniGirisdepoBosKapasite < 0)
                {
                    MessageBox.Show("Girilen Stok Giriş Miktarı Deponun Boş Kapasitesini Aşıyor..");
               
                }
                else
                {
                    //Stok Girisi
                   string stokGiris = "INSERT INTO StokGiris(stokGirisKodu,stokGirisTarihi,tedarikciKodu,urun_Markasi,urun_Adi,urun_Tipi,stokGirisMiktari,stokGirisAciklama,stokGirisToplamMaliyet,depoKodu) " +
                   "VALUES (@stokGirisKodu, @stokGirisTarihi, @tedarikciKodu,@urun_Markasi, @urun_Adi,@urun_Tipi,@stokGirisMiktari,@stokGirisAciklama,@stokGirisToplamMaliyet,@depoKodu)";

                    SqlCommand cmd2 = new SqlCommand(stokGiris, connect);
                    cmd2.Parameters.AddWithValue("@stokGirisKodu", stokGirisKodutextBox.Text);
                    cmd2.Parameters.AddWithValue("@stokGirisTarihi", stokGirisTarihidateTimePicker1.Value);
                    cmd2.Parameters.AddWithValue("@tedarikciKodu", tedarikcicomboBox1.SelectedValue.ToString());
                    cmd2.Parameters.AddWithValue("@urun_Markasi", urunMarkasicomboBox6.SelectedValue.ToString());
                    cmd2.Parameters.AddWithValue("@urun_Adi", urunadicomboBox3.SelectedValue.ToString());
                    cmd2.Parameters.AddWithValue("@urun_Tipi", urunTipicomboBox2.SelectedValue.ToString());
                    cmd2.Parameters.AddWithValue("@stokGirisMiktari", Convert.ToDouble(stokGirisMiktaritextBox1.Text));
                    cmd2.Parameters.AddWithValue("@stokGirisAciklama", stokGirisAciklamasirichTextBox1.Text);
                    cmd2.Parameters.AddWithValue("@stokGirisToplamMaliyet", Convert.ToDouble(toplamMaliyettextBox5.Text));
                    cmd2.Parameters.AddWithValue("@depoKodu", depoAdicomboBox4.SelectedValue.ToString());


                    cmd2.ExecuteNonQuery();




                    string depoguncelle = "UPDATE Depo SET bosKapasite=@bosKapasite WHERE depoKodu=@depoKodu";
                    SqlCommand cmd3 = new SqlCommand(depoguncelle, connect);
                    cmd3.Parameters.AddWithValue("@depoKodu", depoAdicomboBox4.SelectedValue.ToString());
                    cmd3.Parameters.AddWithValue("@bosKapasite", yeniGirisdepoBosKapasite);

                    cmd3.ExecuteNonQuery();


                    string depoStokUrun = "SELECT * FROM Depo_Stok_Urun WHERE depoKodu = @depoKodu AND urunAdi = @urunAdi AND urunMarkasi = @urunMarkasi AND urunTipi = @urunTipi";
                    SqlCommand cmd4 = new SqlCommand(depoStokUrun, connect);

                    cmd4.Parameters.AddWithValue("@depoKodu", depoAdicomboBox4.SelectedValue.ToString());
                    cmd4.Parameters.AddWithValue("@urunAdi", urunadicomboBox3.SelectedValue.ToString());
                    cmd4.Parameters.AddWithValue("@urunMarkasi", urunMarkasicomboBox6.SelectedValue.ToString());
                    cmd4.Parameters.AddWithValue("@urunTipi", urunTipicomboBox2.SelectedValue.ToString());
                    using (SqlDataReader reader = cmd4.ExecuteReader())
                    {
                        //Eger Depo_Stok_Urunde urun varsa urun miktarini arttır.
                        if (reader.Read())
                        {
                            urunMiktari = Convert.ToInt32(reader["urunMiktari"]) + Convert.ToInt32(stokGirisMiktaritextBox1.Text);

                        }
                        //Eger Depo_Stok_Urunde urun yoksa urun miktarini gir.
                        else
                        {
                            urunMiktari = Convert.ToInt32(stokGirisMiktaritextBox1.Text);
                        }
                    }
                    cmd4.ExecuteNonQuery();

                    string updateOrInsert = @"IF NOT EXISTS(SELECT * FROM Depo_Stok_Urun WHERE depoKodu = @depoKodu AND urunAdi = @urunAdi AND urunMarkasi = @urunMarkasi AND urunTipi = @urunTipi)
                    INSERT INTO Depo_Stok_Urun(depoKodu,urunAdi,urunMarkasi,urunTipi,urunMiktari) VALUES(@depoKodu,@urunAdi,@urunMarkasi,@urunTipi,@urunMiktari)
                    ELSE UPDATE Depo_Stok_Urun SET urunMiktari=@urunMiktari WHERE  depoKodu = @depoKodu AND urunAdi = @urunAdi AND urunMarkasi = @urunMarkasi AND urunTipi = @urunTipi ";
                    SqlCommand cmd5 = new SqlCommand(updateOrInsert, connect);

                    cmd5.Parameters.AddWithValue("@depoKodu", depoAdicomboBox4.SelectedValue.ToString());
                    cmd5.Parameters.AddWithValue("@urunAdi", urunadicomboBox3.SelectedValue.ToString());
                    cmd5.Parameters.AddWithValue("@urunMarkasi", urunMarkasicomboBox6.SelectedValue.ToString());
                    cmd5.Parameters.AddWithValue("@urunTipi", urunTipicomboBox2.SelectedValue.ToString());
                    cmd5.Parameters.AddWithValue("@urunMiktari", urunMiktari);


                    cmd5.ExecuteNonQuery();

                    MessageBox.Show("Stok Girişi Başarılı Bir Şekilde Eklendi");






                    //STOK GIRIS GRIDVIEW
                    string kayit1 = "SELECT * FROM  StokGiris ";

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
                    stokGirisUrunBirimFiyati = 0;
                    eskiGirisdepoBosKapasite = 0;
                    yeniGirisdepoBosKapasite = 0;
                    urunMiktari = 0;

                }



                

           


            }
                catch
            {
               
               MessageBox.Show("Stok Girişi Geçersiz Veri Girişi Nedeniyle Eklenemedi");

            }
         
        }     
        //STOK GİRİSİ-2 O ÜRÜN KATEGORİSİNE UYGUN ÜRÜN ADI MARKASI TİPİ GÖSTERİR.
        private void urunKategorisicomboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

            string cmdText = "SELECT DISTINCT urunAdi,urunMarkasi,urunTipi FROM Urun WHERE urunKategorisi=@urunKategorisi";
            SqlCommand cmd = new SqlCommand(cmdText, connect);
            cmd.Parameters.AddWithValue("@urunKategorisi", urunKategorisicomboBox5.SelectedValue.ToString());
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            DataTable dtUrunAdi = new DataTable();
            dtUrunAdi.Load(reader);

            urunadicomboBox3.ValueMember = "urunAdi";
            urunadicomboBox3.DisplayMember = "urunAdi";
            urunadicomboBox3.DataSource = dtUrunAdi;

            reader = cmd.ExecuteReader();
            DataTable dtUrunMarkasi = new DataTable();
            dtUrunMarkasi.Load(reader);
            urunMarkasicomboBox6.ValueMember = "urunMarkasi";
            urunMarkasicomboBox6.DisplayMember = "urunMarkasi";
            urunMarkasicomboBox6.DataSource = dtUrunMarkasi;

            reader = cmd.ExecuteReader();
            DataTable dtUrunTipi = new DataTable();
            dtUrunTipi.Load(reader);
            urunTipicomboBox2.ValueMember = "urunTipi";
            urunTipicomboBox2.DisplayMember = "urunTipi";
            urunTipicomboBox2.DataSource = dtUrunTipi;
        }
        // STOK GİRİSİ-3 GİRİLEN ÜRÜN BİRİM FİYATINA GÖRE TOPLAM MALİYETİ HESAPLAR.
        private void stokGirisMiktaritextBox1_TextChanged(object sender, EventArgs e)
        {

            string cmdText = "SELECT urunBirimFiyati FROM Urun WHERE urunAdi=@urunAdi AND urunTipi=@urunTipi AND urunMarkasi=@urunMarkasi";
            SqlCommand cmd = new SqlCommand(cmdText, connect);
            cmd.Parameters.AddWithValue("@urunAdi", urunadicomboBox3.SelectedValue);
            cmd.Parameters.AddWithValue("@urunMarkasi", urunMarkasicomboBox6.SelectedValue);
            cmd.Parameters.AddWithValue("@urunTipi", urunTipicomboBox2.SelectedValue);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    stokGirisUrunBirimFiyati = Convert.ToSingle(reader["urunBirimFiyati"]);
                }
              
            }
            string miktar = stokGirisMiktaritextBox1.Text;
            if (stokGirisMiktaritextBox1.Text=="")
            {
                toplamMaliyettextBox5.Text = "";
            }
            else
            {
                toplamMaliyettextBox5.Text = (Convert.ToSingle(miktar) * stokGirisUrunBirimFiyati).ToString();


            }






        }
        //STOK GİRİSİ-4 DISPLAY GRIDVIEW
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            stgKodu2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            stgTarih2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            stgtedarikci2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            stgurunMarkasi2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            stgurunAdi2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            stgurunTipi2.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            stggirisMiktar2.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            stgAciklama2.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            stgtoplamMaliyet2.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            stgdepo2.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();

        }
        //STOK GİRİSİ-5 STOK GIRISI GUNCELLE
        private void stgGuncelle_Click(object sender, EventArgs e)
        {
            string cmdText = "UPDATE StokGiris SET stokGirisTarihi=@stokGirisTarihi,stokGirisAciklama=@stokGirisAciklama WHERE stokGirisKodu=@stokGirisKodu ";
            SqlCommand cmd = new SqlCommand(cmdText, connect);
            cmd.Parameters.AddWithValue("@stokGirisKodu", stgKodu2.Text);
            cmd.Parameters.AddWithValue("@stokGirisAciklama", stgAciklama2.Text);
            cmd.Parameters.AddWithValue("@stokGirisTarihi", stgTarih2.Text);



            cmd.ExecuteNonQuery();
            MessageBox.Show("Stok Girişi Başarılı Bir Şekilde Güncellendi");
            //STOK GIRIS GRIDVIEW
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


        }
        //STOK GİRİSİ-6 STOK GIRISI IPTAL
        private void stgIptal_Click(object sender, EventArgs e)
        {
            Int32 Index = dataGridView1.Rows.Count - 1;
            if (dataGridView1.CurrentRow.Index + 1 == Index)
            {
                DateTime now = DateTime.Now.Date;

                string stokGirisIptalInsert = "INSERT INTO StokGirisIptal(stokGirisKodu,stokGirisIptalTarihi) VALUES(@stokGirisKodu,@stokGirisIptalTarihi)";
                SqlCommand cmdStokGirisIptalInsert = new SqlCommand(stokGirisIptalInsert, connect);
                cmdStokGirisIptalInsert.Parameters.AddWithValue("@stokGirisKodu", stgKodu2.Text);
                cmdStokGirisIptalInsert.Parameters.AddWithValue("@stokGirisIptalTarihi", now);



                cmdStokGirisIptalInsert.ExecuteNonQuery();

                string depoGuncelleOku = "SELECT bosKapasite FROM Depo WHERE depoKodu=@depoKodu";
                SqlCommand cmdDepoGuncelleOku = new SqlCommand(depoGuncelleOku, connect);
                cmdDepoGuncelleOku.Parameters.AddWithValue("@depoKodu", stgdepo2.Text);

                using (SqlDataReader readerDepoGuncelleOku = cmdDepoGuncelleOku.ExecuteReader())
                {
                    while (readerDepoGuncelleOku.Read())
                    {
                        eskiGirisdepoBosKapasite = Convert.ToInt32(readerDepoGuncelleOku["bosKapasite"]);
                    }
                }
                cmdDepoGuncelleOku.ExecuteNonQuery();

                yeniGirisdepoBosKapasite = eskiGirisdepoBosKapasite + Convert.ToInt32(stggirisMiktar2.Text);




                string depoGuncelle = "UPDATE Depo SET  bosKapasite=@bosKapasite  WHERE depoKodu=@depoKodu";
                SqlCommand cmdDepoGuncelle = new SqlCommand(depoGuncelle, connect);
                cmdDepoGuncelle.Parameters.AddWithValue("@depoKodu", stgdepo2.Text);
                cmdDepoGuncelle.Parameters.AddWithValue("@bosKapasite", yeniGirisdepoBosKapasite);


                cmdDepoGuncelle.ExecuteNonQuery();






                string depoStokGuncelleOku = "SELECT urunMiktari FROM Depo_Stok_Urun WHERE depoKodu=@depoKodu AND urunAdi=@urunAdi AND urunMarkasi=@urunMarkasi AND urunTipi=@urunTipi";
                SqlCommand cmdDepoStokGuncelleOku = new SqlCommand(depoStokGuncelleOku, connect);
                cmdDepoStokGuncelleOku.Parameters.AddWithValue("@depoKodu", stgdepo2.Text);
                cmdDepoStokGuncelleOku.Parameters.AddWithValue("@urunAdi", stgurunAdi2.Text);
                cmdDepoStokGuncelleOku.Parameters.AddWithValue("@urunMarkasi", stgurunMarkasi2.Text);
                cmdDepoStokGuncelleOku.Parameters.AddWithValue("@urunTipi", stgurunTipi2.Text);

                using (SqlDataReader readerDepoStokGuncelleOku = cmdDepoStokGuncelleOku.ExecuteReader())
                {
                    while (readerDepoStokGuncelleOku.Read())
                    {
                        eskiUrunMiktari = Convert.ToInt32(readerDepoStokGuncelleOku["urunMiktari"]);
                    }
                }
                cmdDepoStokGuncelleOku.ExecuteNonQuery();

                yeniUrunMiktari = eskiUrunMiktari - Convert.ToInt32(stggirisMiktar2.Text);


                if (yeniUrunMiktari == 0)
                {
                    string depoStokDelete = "DELETE FROM Depo_Stok_Urun    WHERE depoKodu=@depoKodu AND urunAdi=@urunAdi AND urunMarkasi=@urunMarkasi AND urunTipi=@urunTipi";
                    SqlCommand cmdDepoStokDelete = new SqlCommand(depoStokDelete, connect);
                    cmdDepoStokDelete.Parameters.AddWithValue("@depoKodu", stgdepo2.Text);
                    cmdDepoStokDelete.Parameters.AddWithValue("@urunAdi", stgurunAdi2.Text);
                    cmdDepoStokDelete.Parameters.AddWithValue("@urunMarkasi", stgurunMarkasi2.Text);
                    cmdDepoStokDelete.Parameters.AddWithValue("@urunTipi", stgurunTipi2.Text);


                    cmdDepoStokDelete.ExecuteNonQuery();
                }


                string depoStokGuncelle = "UPDATE Depo_Stok_Urun SET urunMiktari=@urunMiktari   WHERE depoKodu=@depoKodu AND urunAdi=@urunAdi AND urunMarkasi=@urunMarkasi AND urunTipi=@urunTipi";
                SqlCommand cmdDepoStokGuncelle = new SqlCommand(depoStokGuncelle, connect);
                cmdDepoStokGuncelle.Parameters.AddWithValue("@depoKodu", stgdepo2.Text);
                cmdDepoStokGuncelle.Parameters.AddWithValue("@urunAdi", stgurunAdi2.Text);
                cmdDepoStokGuncelle.Parameters.AddWithValue("@urunMarkasi", stgurunMarkasi2.Text);
                cmdDepoStokGuncelle.Parameters.AddWithValue("@urunTipi", stgurunTipi2.Text);

                cmdDepoStokGuncelle.Parameters.AddWithValue("@urunMiktari", yeniUrunMiktari);

                cmdDepoStokGuncelle.ExecuteNonQuery();
















                string cmdText = "DELETE FROM StokGiris  WHERE stokGirisKodu=@stokGirisKodu ";
                SqlCommand cmd = new SqlCommand(cmdText, connect);
                cmd.Parameters.AddWithValue("@stokGirisKodu", stgKodu2.Text);




                cmd.ExecuteNonQuery();

            




                MessageBox.Show("Stok Girişi Başarılı Bir Şekilde İptal Edildi");
                //STOK GIRIS GRIDVIEW
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

            }
            else
            {
                MessageBox.Show("Sadece Son Yapılan Stok Girişi İptal Edilebilir.");
            }

        }


        //STOK CIKISI-1
        private void StokCikisiYap_Click(object sender, EventArgs e)
        {
            try
            {
                string isAvaliableUrun = "SELECT * FROM Depo_Stok_Urun WHERE depoKodu = @depoKodu AND urunAdi = @urunAdi AND urunMarkasi = @urunMarkasi AND urunTipi = @urunTipi";
                SqlCommand cmd1 = new SqlCommand(isAvaliableUrun, connect);

                cmd1.Parameters.AddWithValue("@depoKodu", depoKodu.SelectedValue.ToString());
                cmd1.Parameters.AddWithValue("@urunAdi", urunAdicomboBox10.SelectedValue.ToString());
                cmd1.Parameters.AddWithValue("@urunMarkasi", urunMarkasicomboBox9.SelectedValue.ToString());
                cmd1.Parameters.AddWithValue("@urunTipi", urunTipicomboBox11.SelectedValue.ToString());


                using (SqlDataReader reader1 = cmd1.ExecuteReader())
                {
                    if (reader1.Read())
                    {

                        urunMiktariCikis = Convert.ToInt32(reader1["urunMiktari"]) - Convert.ToInt32(stokCikisMiktaritextBox4.Text);

                        reader = true;




                    
                    }
                    else
                    {
                        reader = false;
                       
                    }
                   
                }
               cmd1.ExecuteNonQuery();
                if (reader == true)
                {
                    if (urunMiktariCikis < 0)
                    {

                        MessageBox.Show("Girilen Stok Çıkış Miktarı için Depo Yetersiz...");


                    }
                    else if (urunMiktariCikis == 0)
                    {

                        string stokCikis1 = "INSERT INTO StokCikis(stokCikisKodu,stokCikisTarihi,musteriKodu,urunMarkasi,urunAdi,urunTipi,stokCikisMiktari,stokCikisAciklama,stokCikisToplamMaliyet,depoKodu) " +
                        "VALUES (@stokCikisKodu, @stokCikisTarihi, @musteriKodu, @urunMarkasi, @urunAdi,@urunTipi,@stokCikisMiktari,@stokCikisAciklama,@stokCikisToplamMaliyet,@depoKodu)";
                        SqlCommand cmd2 = new SqlCommand(stokCikis1, connect);
                        cmd2.Parameters.AddWithValue("@stokCikisKodu", stokCikistextBox3.Text);
                        cmd2.Parameters.AddWithValue("@stokCikisTarihi", stokcikisdateTimePicker2.Value);
                        cmd2.Parameters.AddWithValue("@musteriKodu", musteriAdicomboBox12.SelectedValue.ToString());
                        cmd2.Parameters.AddWithValue("@urunMarkasi", urunMarkasicomboBox9.SelectedValue.ToString());
                        cmd2.Parameters.AddWithValue("@urunAdi", urunAdicomboBox10.SelectedValue.ToString());
                        cmd2.Parameters.AddWithValue("@urunTipi", urunTipicomboBox11.SelectedValue.ToString());
                        cmd2.Parameters.AddWithValue("@stokCikisMiktari", Convert.ToDouble(stokCikisMiktaritextBox4.Text));
                        cmd2.Parameters.AddWithValue("@stokCikisAciklama", stokCikisiAciklamasirichTextBox2.Text);
                        cmd2.Parameters.AddWithValue("@stokCikisToplamMaliyet", Convert.ToDouble(toplamMaliyettextBox6.Text));
                        cmd2.Parameters.AddWithValue("@depoKodu", depoKodu.SelectedValue.ToString());

                        cmd2.ExecuteNonQuery();



                        string depotext1 = "SELECT bosKapasite FROM Depo WHERE depoKodu=@depoKodu";
                        SqlCommand cmd3 = new SqlCommand(depotext1, connect);
                        cmd3.Parameters.AddWithValue("@depoKodu", depoKodu.SelectedValue.ToString());

                        using (SqlDataReader reader2 = cmd3.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                eskiCikisdepoBosKapasite = Convert.ToInt32(reader2["bosKapasite"]);
                            }

                        }

                        cmd3.ExecuteNonQuery();

                        yeniCikisdepoBosKapasite = eskiCikisdepoBosKapasite + Convert.ToInt32(stokCikisMiktaritextBox4.Text);

                        string depoguncelle1 = "UPDATE Depo SET bosKapasite=@bosKapasite WHERE depoKodu=@depoKodu";
                        SqlCommand cmd4 = new SqlCommand(depoguncelle1, connect);
                        cmd4.Parameters.AddWithValue("@depoKodu", depoKodu.SelectedValue.ToString());
                        cmd4.Parameters.AddWithValue("@bosKapasite", yeniCikisdepoBosKapasite);

                        cmd4.ExecuteNonQuery();



                        string querydelete = "DELETE  FROM Depo_Stok_Urun  WHERE depoKodu = @depoKodu AND urunAdi = @urunAdi AND urunMarkasi = @urunMarkasi AND urunTipi = @urunTipi ";
                        SqlCommand cmd5 = new SqlCommand(querydelete, connect);

                        cmd5.Parameters.AddWithValue("@depoKodu", depoKodu.SelectedValue.ToString());
                        cmd5.Parameters.AddWithValue("@urunAdi", urunAdicomboBox10.SelectedValue.ToString());
                        cmd5.Parameters.AddWithValue("@urunMarkasi", urunMarkasicomboBox9.SelectedValue.ToString());
                        cmd5.Parameters.AddWithValue("@urunTipi", urunTipicomboBox11.SelectedValue.ToString());
                        cmd5.Parameters.AddWithValue("@urunMiktari", urunMiktariCikis);


                        cmd5.ExecuteNonQuery();


                        MessageBox.Show("Depodan Ürün Silindi ve Stok Çıkışı Başarılı Bir Şekilde Eklendi");
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

                        stokCikisUrunBirimFiyati = 0;
                        eskiCikisdepoBosKapasite = 0;
                        yeniCikisdepoBosKapasite = 0;
                        urunMiktariCikis = 0;
                    }
                    else
                    {


                        string stokCikis2 = "INSERT INTO StokCikis(stokCikisKodu,stokCikisTarihi,musteriKodu,urunMarkasi,urunAdi,urunTipi,stokCikisMiktari,stokCikisAciklama,stokCikisToplamMaliyet,depoKodu) " +
                       "VALUES (@stokCikisKodu, @stokCikisTarihi, @musteriKodu, @urunMarkasi, @urunAdi,@urunTipi,@stokCikisMiktari,@stokCikisAciklama,@stokCikisToplamMaliyet,@depoKodu)";
                        SqlCommand cmd6 = new SqlCommand(stokCikis2, connect);
                        cmd6.Parameters.AddWithValue("@stokCikisKodu", stokCikistextBox3.Text);
                        cmd6.Parameters.AddWithValue("@stokCikisTarihi", stokcikisdateTimePicker2.Value);
                        cmd6.Parameters.AddWithValue("@musteriKodu", musteriAdicomboBox12.SelectedValue.ToString());
                        cmd6.Parameters.AddWithValue("@urunMarkasi", urunMarkasicomboBox9.SelectedValue.ToString());
                        cmd6.Parameters.AddWithValue("@urunAdi", urunAdicomboBox10.SelectedValue.ToString());
                        cmd6.Parameters.AddWithValue("@urunTipi", urunTipicomboBox11.SelectedValue.ToString());
                        cmd6.Parameters.AddWithValue("@stokCikisMiktari", Convert.ToDouble(stokCikisMiktaritextBox4.Text));
                        cmd6.Parameters.AddWithValue("@stokCikisAciklama", stokCikisiAciklamasirichTextBox2.Text);
                        cmd6.Parameters.AddWithValue("@stokCikisToplamMaliyet", Convert.ToDouble(toplamMaliyettextBox6.Text));
                        cmd6.Parameters.AddWithValue("@depoKodu", depoKodu.SelectedValue.ToString());

                        cmd6.ExecuteNonQuery();

                        string depotext2 = "SELECT bosKapasite FROM Depo WHERE depoKodu=@depoKodu";
                        SqlCommand cmd7 = new SqlCommand(depotext2, connect);
                        cmd7.Parameters.AddWithValue("@depoKodu", depoKodu.SelectedValue.ToString());

                        using (SqlDataReader reader3 = cmd7.ExecuteReader())
                        {
                            while (reader3.Read())
                            {
                                eskiCikisdepoBosKapasite = Convert.ToInt32(reader3["bosKapasite"]);
                            }

                        }

                        cmd7.ExecuteNonQuery();
                        yeniCikisdepoBosKapasite = eskiCikisdepoBosKapasite + Convert.ToInt32(stokCikisMiktaritextBox4.Text);

                        string depoguncelle2 = "UPDATE Depo SET bosKapasite=@bosKapasite WHERE depoKodu=@depoKodu";
                        SqlCommand cmd8 = new SqlCommand(depoguncelle2, connect);
                        cmd8.Parameters.AddWithValue("@depoKodu", depoKodu.SelectedValue.ToString());
                        cmd8.Parameters.AddWithValue("@bosKapasite", yeniCikisdepoBosKapasite);

                        cmd8.ExecuteNonQuery();


                        string query = "UPDATE Depo_Stok_Urun SET urunMiktari=@urunMiktari WHERE  depoKodu = @depoKodu AND urunAdi = @urunAdi AND urunMarkasi = @urunMarkasi AND urunTipi = @urunTipi ";
                        SqlCommand cmd9 = new SqlCommand(query, connect);

                        cmd9.Parameters.AddWithValue("@depoKodu", depoKodu.SelectedValue.ToString());
                        cmd9.Parameters.AddWithValue("@urunAdi", urunAdicomboBox10.SelectedValue.ToString());
                        cmd9.Parameters.AddWithValue("@urunMarkasi", urunMarkasicomboBox9.SelectedValue.ToString());
                        cmd9.Parameters.AddWithValue("@urunTipi", urunTipicomboBox11.SelectedValue.ToString());
                        cmd9.Parameters.AddWithValue("@urunMiktari", urunMiktariCikis);


                        cmd9.ExecuteNonQuery();
                        MessageBox.Show("Depodaki Ürün Güncellendi ve Stok Çıkışı Başarılı Bir Şekilde Eklendi");
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

                        stokCikisUrunBirimFiyati = 0;
                        eskiCikisdepoBosKapasite = 0;
                        yeniCikisdepoBosKapasite = 0;
                        urunMiktariCikis = 0;


                    }


                }
                else
                {
                    MessageBox.Show("Depoda Böyle Bir Ürün Yok...");
                }

            }
            catch
            {
         
                MessageBox.Show("Stok Çıkışı Yanlış Giriş Nedeniyle Eklenemedi");

            }

        }
        //STOK ÇIKISI-2 O DEPODAKİ UYGUN ÜRÜN ADI MARKASI TİPİ GÖSTERİR.
        private void depoKodu_SelectedIndexChanged(object sender, EventArgs e)
        {


            string cmdText = "SELECT urunAdi,urunMarkasi,urunTipi FROM Depo_Stok_Urun WHERE depoKodu=@depoKodu";
            SqlCommand cmd = new SqlCommand(cmdText, connect);
            cmd.Parameters.AddWithValue("@depoKodu", depoKodu.SelectedValue);

            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            DataTable dtUrunAdi = new DataTable();
            dtUrunAdi.Load(reader);
            urunAdicomboBox10.ValueMember = "urunAdi";
            urunAdicomboBox10.DisplayMember = "urunAdi";
            urunAdicomboBox10.DataSource = dtUrunAdi;

            reader = cmd.ExecuteReader();
            DataTable dtUrunMarkasi = new DataTable();
            dtUrunMarkasi.Load(reader);
            urunMarkasicomboBox9.ValueMember = "urunMarkasi";
            urunMarkasicomboBox9.DisplayMember = "urunMarkasi";
            urunMarkasicomboBox9.DataSource = dtUrunMarkasi;

            reader = cmd.ExecuteReader();
            DataTable dtUrunTipi = new DataTable();
            dtUrunTipi.Load(reader);
            urunTipicomboBox11.ValueMember = "urunTipi";
            urunTipicomboBox11.DisplayMember = "urunTipi";
            urunTipicomboBox11.DataSource = dtUrunTipi;
        }
        //STOK CIKISI-3 GİRİLEN ÜRÜN BİRİM FİYATINA GÖRE TOPLAM MALİYETİ HESAPLAR. 
        private void stokCikisMiktaritextBox4_TextChanged(object sender, EventArgs e)
        {
           

            string cmdText = "SELECT urunBirimFiyati FROM Urun WHERE urunAdi=@urunAdi AND urunTipi=@urunTipi AND urunMarkasi=@urunMarkasi";
            SqlCommand cmd = new SqlCommand(cmdText, connect);
            cmd.Parameters.AddWithValue("@urunAdi", urunAdicomboBox10.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@urunMarkasi", urunMarkasicomboBox9.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@urunTipi", urunTipicomboBox11.SelectedValue.ToString());
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    stokCikisUrunBirimFiyati = Convert.ToSingle(reader["urunBirimFiyati"]);
                }

            }
            string miktar = stokCikisMiktaritextBox4.Text;
            if (miktar == "")
            {
                toplamMaliyettextBox6.Text = "";

            }
            else { toplamMaliyettextBox6.Text = (Convert.ToSingle(miktar) * stokCikisUrunBirimFiyati).ToString(); }

        }
        //STOK CIKISI-4 DISPLAY DATAGRIDVIEW
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            stcKodu2.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            stcTarih2.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            stcmusteri2.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            stcurunMarkasi2.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            stcurunTipi2.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
            stcurunAdi2.Text = dataGridView2.CurrentRow.Cells[5].Value.ToString();
            stccikisMiktari2.Text = dataGridView2.CurrentRow.Cells[6].Value.ToString();
            stcaciklama2.Text = dataGridView2.CurrentRow.Cells[7].Value.ToString();
            stctoplamMaliyet2.Text = dataGridView2.CurrentRow.Cells[8].Value.ToString();
            stcdepo2.Text = dataGridView2.CurrentRow.Cells[9].Value.ToString();
        }
        //STOK CIKISI-5 STOK CIKISI GUNCELLE
        private void stcGuncelle_Click(object sender, EventArgs e)
        {
            string cmdText = "UPDATE StokCikis SET stokCikisTarihi=@stokCikisTarihi,stokCikisAciklama=@stokCikisAciklama WHERE stokCikisKodu=@stokCikisKodu ";
            SqlCommand cmd = new SqlCommand(cmdText, connect);
            cmd.Parameters.AddWithValue("@stokCikisKodu", stcKodu2.Text);
            cmd.Parameters.AddWithValue("@stokCikisAciklama", stcaciklama2.Text);
            cmd.Parameters.AddWithValue("@stokCikisTarihi", stcTarih2.Text);



            cmd.ExecuteNonQuery();
            MessageBox.Show("Stok Çıkışı Başarılı Bir Şekilde Güncellendi");
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


        }
        //STOK CIKISI-6 STOK CIKISI IPTAL
        private void stcIptal_Click(object sender, EventArgs e)
        {
            Int32 Index = dataGridView2.Rows.Count - 1;
            if (dataGridView2.CurrentRow.Index + 1 == Index)
            {

                DateTime now = DateTime.Now.Date;

                string stokCikisIptalInsert = "INSERT INTO StokCikisIptal(stokCikisKodu,stokCikisIptalTarihi) VALUES(@stokCikisKodu,@stokCikisIptalTarihi)";
                SqlCommand cmdStokCikisIptalInsert = new SqlCommand(stokCikisIptalInsert, connect);
                cmdStokCikisIptalInsert.Parameters.AddWithValue("@stokCikisKodu", stcKodu2.Text);
                cmdStokCikisIptalInsert.Parameters.AddWithValue("@stokCikisIptalTarihi", now);



                cmdStokCikisIptalInsert.ExecuteNonQuery();

                string depoGuncelleOku = "SELECT bosKapasite FROM Depo WHERE depoKodu=@depoKodu";
                SqlCommand cmdDepoGuncelleOku = new SqlCommand(depoGuncelleOku, connect);
                cmdDepoGuncelleOku.Parameters.AddWithValue("@depoKodu", stcdepo2.Text);

                using (SqlDataReader readerDepoGuncelleOku = cmdDepoGuncelleOku.ExecuteReader())
                {
                    while (readerDepoGuncelleOku.Read())
                    {
                        eskiCikisdepoBosKapasite = Convert.ToInt32(readerDepoGuncelleOku["bosKapasite"]);
                    }
                }
                cmdDepoGuncelleOku.ExecuteNonQuery();

                yeniCikisdepoBosKapasite = eskiCikisdepoBosKapasite - Convert.ToInt32(stccikisMiktari2.Text);




                string depoGuncelle = "UPDATE Depo SET  bosKapasite=@bosKapasite  WHERE depoKodu=@depoKodu";
                SqlCommand cmdDepoGuncelle = new SqlCommand(depoGuncelle, connect);
                cmdDepoGuncelle.Parameters.AddWithValue("@depoKodu", stcdepo2.Text);
                cmdDepoGuncelle.Parameters.AddWithValue("@bosKapasite", yeniCikisdepoBosKapasite);


                cmdDepoGuncelle.ExecuteNonQuery();






                string depoStokGuncelleOku = "SELECT urunMiktari FROM Depo_Stok_Urun WHERE depoKodu=@depoKodu AND urunAdi=@urunAdi AND urunMarkasi=@urunMarkasi AND urunTipi=@urunTipi";
                SqlCommand cmdDepoStokGuncelleOku = new SqlCommand(depoStokGuncelleOku, connect);
                cmdDepoStokGuncelleOku.Parameters.AddWithValue("@depoKodu", stcdepo2.Text);
                cmdDepoStokGuncelleOku.Parameters.AddWithValue("@urunAdi", stcurunAdi2.Text);
                cmdDepoStokGuncelleOku.Parameters.AddWithValue("@urunMarkasi", stcurunMarkasi2.Text);
                cmdDepoStokGuncelleOku.Parameters.AddWithValue("@urunTipi", stcurunTipi2.Text);

                using (SqlDataReader readerDepoStokGuncelleOku = cmdDepoStokGuncelleOku.ExecuteReader())
                {
                    while (readerDepoStokGuncelleOku.Read())
                    {
                        eskiUrunMiktari1 = Convert.ToInt32(readerDepoStokGuncelleOku["urunMiktari"]);
                    }
                }
                cmdDepoStokGuncelleOku.ExecuteNonQuery();

                yeniUrunMiktari1 = eskiUrunMiktari1 + Convert.ToInt32(stccikisMiktari2.Text);


               


                string depoStokGuncelle = "UPDATE Depo_Stok_Urun SET urunMiktari=@urunMiktari   WHERE depoKodu=@depoKodu AND urunAdi=@urunAdi AND urunMarkasi=@urunMarkasi AND urunTipi=@urunTipi";
                SqlCommand cmdDepoStokGuncelle = new SqlCommand(depoStokGuncelle, connect);
                cmdDepoStokGuncelle.Parameters.AddWithValue("@depoKodu", stcdepo2.Text);
                cmdDepoStokGuncelle.Parameters.AddWithValue("@urunAdi", stcurunAdi2.Text);
                cmdDepoStokGuncelle.Parameters.AddWithValue("@urunMarkasi", stcurunMarkasi2.Text);
                cmdDepoStokGuncelle.Parameters.AddWithValue("@urunTipi", stcurunTipi2.Text);

                cmdDepoStokGuncelle.Parameters.AddWithValue("@urunMiktari", yeniUrunMiktari1);

                cmdDepoStokGuncelle.ExecuteNonQuery();























                string cmdText = "DELETE FROM StokCikis  WHERE stokCikisKodu=@stokCikisKodu ";
                SqlCommand cmd = new SqlCommand(cmdText, connect);
                cmd.Parameters.AddWithValue("@stokCikisKodu", stcKodu2.Text);




                cmd.ExecuteNonQuery();
                MessageBox.Show("Stok Çıkışı Başarılı Bir Şekilde İptal Edildi");
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



            }
            else
            {
                MessageBox.Show("Sadece Son Yapılan Stok Girişi İptal Edilebilir.");
            }

        }

        //STOK TRANSFER-1
        private void TransferEt_Click(object sender, EventArgs e)
        {
            try
            {
                string bulunanDepoKapasitesi = "SELECT bosKapasite FROM Depo WHERE depoKodu=@depoKodu";
                SqlCommand cmdbulunanDepo = new SqlCommand(bulunanDepoKapasitesi, connect);
                cmdbulunanDepo.Parameters.AddWithValue("@depoKodu", bulunduguDepocomboBox13.SelectedValue.ToString());

                using(SqlDataReader readerBulunduguDepo = cmdbulunanDepo.ExecuteReader())
                {
                    while (readerBulunduguDepo.Read())
                    {
                        bulunanEskiBosKapasite = Convert.ToInt32(readerBulunduguDepo["bosKapasite"]);
                    }
                }
                cmdbulunanDepo.ExecuteNonQuery();

                string transferDepoKapasitesi = "SELECT bosKapasite FROM Depo WHERE depoKodu=@depoKodu";
                SqlCommand cmdtransferDepo = new SqlCommand(transferDepoKapasitesi, connect);
                cmdtransferDepo.Parameters.AddWithValue("@depoKodu", transferdepocomboBox14.SelectedValue.ToString());

                using(SqlDataReader readerTransferDepo = cmdtransferDepo.ExecuteReader())
                {
                    while (readerTransferDepo.Read())
                    {
                        transferEskiBosKapasite = Convert.ToInt32(readerTransferDepo["bosKapasite"]);
                    }
                }
                cmdtransferDepo.ExecuteNonQuery();

                bulunanYeniBosKapasite = bulunanEskiBosKapasite + Convert.ToInt32(stMiktartextBox1.Text);
                transferYeniBosKapasite=transferEskiBosKapasite- Convert.ToInt32(stMiktartextBox1.Text);

                if (transferYeniBosKapasite >= 0)
                {

                    string urunMiktariTransfera = "SELECT urunMiktari FROM Depo_Stok_Urun WHERE depoKodu=@depoKodu AND urunAdi=@urunAdi AND urunMarkasi=@urunMarkasi AND urunTipi=@urunTipi ";
                    SqlCommand cmdTransferUrun = new SqlCommand(urunMiktariTransfera, connect);

                    cmdTransferUrun.Parameters.AddWithValue("@depoKodu", transferdepocomboBox14.SelectedValue.ToString());
                    cmdTransferUrun.Parameters.AddWithValue("@urunAdi", sturunAdicomboBox1.SelectedValue.ToString());
                    cmdTransferUrun.Parameters.AddWithValue("@urunMarkasi", stUrunMarkasicomboBox3.SelectedValue.ToString());
                    cmdTransferUrun.Parameters.AddWithValue("@urunTipi", sturunTipicomboBox2.SelectedValue.ToString());

                    using (SqlDataReader readerTransferurunMiktari = cmdTransferUrun.ExecuteReader())
                    {
                        while (readerTransferurunMiktari.Read())
                        {
                            AnlikUrunMiktariTransfer = Convert.ToInt32(readerTransferurunMiktari["urunMiktari"]);

                        }
                    }


                    string isAvaliableUrunMiktari = "SELECT urunMiktari FROM Depo_Stok_Urun WHERE depoKodu = @depoKodu AND urunAdi = @urunAdi AND urunMarkasi = @urunMarkasi AND urunTipi = @urunTipi";
                    SqlCommand cmd1 = new SqlCommand(isAvaliableUrunMiktari, connect);

                    cmd1.Parameters.AddWithValue("@depoKodu", bulunduguDepocomboBox13.SelectedValue.ToString());
                    cmd1.Parameters.AddWithValue("@urunAdi", sturunAdicomboBox1.SelectedValue.ToString());
                    cmd1.Parameters.AddWithValue("@urunMarkasi", stUrunMarkasicomboBox3.SelectedValue.ToString());
                    cmd1.Parameters.AddWithValue("@urunTipi", sturunTipicomboBox2.SelectedValue.ToString());


                    using (SqlDataReader reader1 = cmd1.ExecuteReader())
                    {
                        if (reader1.Read())
                        {

                            AnlikUrunMiktari = Convert.ToInt32(reader1["urunMiktari"]);
                            readerTransfer = true;
                            if (Convert.ToInt32(reader1["urunMiktari"]) < Convert.ToInt32(stMiktartextBox1.Text))
                            {
                                fazlaUrunMiktari = true;
                            }
                            else if (Convert.ToInt32(reader1["urunMiktari"]) > Convert.ToInt32(stMiktartextBox1.Text))
                            {
                                azUrunMiktari = true;
                            }
                            else
                            {
                                esitUrunMiktari = true;
                            }







                        }
                        else
                        {
                            readerTransfer = false;

                        }

                    }
                    cmd1.ExecuteNonQuery();

                    if (readerTransfer == true)
                    {
                        if (fazlaUrunMiktari == true)
                        {
                            MessageBox.Show("Girilen Ürün Transfer Miktarı Depodaki Ürün Miktarından Fazla..");
                        }
                        else if (azUrunMiktari == true)
                        {

                            urunMiktariTransfer = AnlikUrunMiktari - Convert.ToInt32(stMiktartextBox1.Text);

                            string transferUpdate = "UPDATE Depo_Stok_Urun SET urunMiktari=@urunMiktari WHERE  depoKodu = @depoKodu AND urunAdi = @urunAdi AND urunMarkasi = @urunMarkasi AND urunTipi = @urunTipi ";
                            SqlCommand cmd2 = new SqlCommand(transferUpdate, connect);

                            cmd2.Parameters.AddWithValue("@depoKodu", bulunduguDepocomboBox13.SelectedValue.ToString());
                            cmd2.Parameters.AddWithValue("@urunAdi", sturunAdicomboBox1.SelectedValue.ToString());
                            cmd2.Parameters.AddWithValue("@urunMarkasi", stUrunMarkasicomboBox3.SelectedValue.ToString());
                            cmd2.Parameters.AddWithValue("@urunTipi", sturunTipicomboBox2.SelectedValue.ToString());
                            cmd2.Parameters.AddWithValue("@urunMiktari", urunMiktariTransfer);


                            cmd2.ExecuteNonQuery();

                            urunMiktariTransfer = 0;
                            urunMiktariTransfer = AnlikUrunMiktariTransfer + Convert.ToInt32(stMiktartextBox1.Text);
                            string updateOrInsert = @"IF NOT EXISTS(SELECT * FROM Depo_Stok_Urun WHERE depoKodu = @depoKodu AND urunAdi = @urunAdi AND urunMarkasi = @urunMarkasi AND urunTipi = @urunTipi)
                    INSERT INTO Depo_Stok_Urun(depoKodu,urunAdi,urunMarkasi,urunTipi,urunMiktari) VALUES(@depoKodu,@urunAdi,@urunMarkasi,@urunTipi,@urunMiktari)
                    ELSE UPDATE Depo_Stok_Urun SET urunMiktari=@urunMiktari WHERE  depoKodu = @depoKodu AND urunAdi = @urunAdi AND urunMarkasi = @urunMarkasi AND urunTipi = @urunTipi ";
                            SqlCommand cmd3 = new SqlCommand(updateOrInsert, connect);

                            cmd3.Parameters.AddWithValue("@depoKodu", transferdepocomboBox14.SelectedValue.ToString());
                            cmd3.Parameters.AddWithValue("@urunAdi", sturunAdicomboBox1.SelectedValue.ToString());
                            cmd3.Parameters.AddWithValue("@urunMarkasi", stUrunMarkasicomboBox3.SelectedValue.ToString());
                            cmd3.Parameters.AddWithValue("@urunTipi", sturunTipicomboBox2.SelectedValue.ToString());
                            cmd3.Parameters.AddWithValue("@urunMiktari", urunMiktariTransfer);


                            cmd3.ExecuteNonQuery();

                            string stokTransferInsert = "INSERT INTO StokTransfer(stokTransferKodu,bulunduguDepoKodu,stokTransferTarihi,transferDepoKodu,urunAdi,urunMarkasi,urunTipi,transferMiktari) VALUES(@stokTransferKodu,@bulunduguDepoKodu,@stokTransferTarihi,@transferDepoKodu,@urunAdi,@urunMarkasi,@urunTipi,@transferMiktari)";
                            SqlCommand cmdstInsert = new SqlCommand(stokTransferInsert, connect);
                            cmdstInsert.Parameters.AddWithValue("@stokTransferKodu",stokTransferKod.Text);
                            cmdstInsert.Parameters.AddWithValue("@bulunduguDepoKodu",bulunduguDepocomboBox13.SelectedValue.ToString());
                            cmdstInsert.Parameters.AddWithValue("@stokTransferTarihi",transferdateTimePicker3.Value);
                            cmdstInsert.Parameters.AddWithValue("@transferDepoKodu",transferdepocomboBox14.SelectedValue.ToString());
                            cmdstInsert.Parameters.AddWithValue("@urunAdi",sturunAdicomboBox1.SelectedValue.ToString());
                            cmdstInsert.Parameters.AddWithValue("@urunTipi",sturunTipicomboBox2.SelectedValue.ToString());
                            cmdstInsert.Parameters.AddWithValue("@urunMarkasi",stUrunMarkasicomboBox3.SelectedValue.ToString());
                            cmdstInsert.Parameters.AddWithValue("@transferMiktari",Convert.ToInt32(stMiktartextBox1.Text));
                            cmdstInsert.ExecuteNonQuery();



                            string bulunduguDepoGuncelle = "UPDATE  Depo SET bosKapasite=@bosKapasite WHERE depoKodu=@depoKodu";
                            SqlCommand cmdstbulunduguDepo = new SqlCommand(bulunduguDepoGuncelle, connect);
                            cmdstbulunduguDepo.Parameters.AddWithValue("@depoKodu", bulunduguDepocomboBox13.SelectedValue.ToString());
                            cmdstbulunduguDepo.Parameters.AddWithValue("@bosKapasite",bulunanYeniBosKapasite);
                           
                            cmdstbulunduguDepo.ExecuteNonQuery();

                            string transferDepoGuncelle = "UPDATE  Depo SET bosKapasite=@bosKapasite WHERE depoKodu=@depoKodu";
                            SqlCommand cmdstTransferDepo = new SqlCommand(transferDepoGuncelle, connect);
                            cmdstTransferDepo.Parameters.AddWithValue("@depoKodu", transferdepocomboBox14.SelectedValue.ToString());
                            cmdstTransferDepo.Parameters.AddWithValue("@bosKapasite", transferYeniBosKapasite);

                            cmdstTransferDepo.ExecuteNonQuery();






                        }
                        else
                        {
                            urunMiktariTransfer = AnlikUrunMiktari - Convert.ToInt32(stMiktartextBox1.Text);

                            string transferDelete = "DELETE FROM Depo_Stok_Urun WHERE  depoKodu = @depoKodu AND urunAdi = @urunAdi AND urunMarkasi = @urunMarkasi AND urunTipi = @urunTipi ";
                            SqlCommand cmd4 = new SqlCommand(transferDelete, connect);

                            cmd4.Parameters.AddWithValue("@depoKodu", bulunduguDepocomboBox13.SelectedValue.ToString());
                            cmd4.Parameters.AddWithValue("@urunAdi", sturunAdicomboBox1.SelectedValue.ToString());
                            cmd4.Parameters.AddWithValue("@urunMarkasi", stUrunMarkasicomboBox3.SelectedValue.ToString());
                            cmd4.Parameters.AddWithValue("@urunTipi", sturunTipicomboBox2.SelectedValue.ToString());



                            cmd4.ExecuteNonQuery();

                            string updateOrInsert1 = @"IF NOT EXISTS(SELECT * FROM Depo_Stok_Urun WHERE depoKodu = @depoKodu AND urunAdi = @urunAdi AND urunMarkasi = @urunMarkasi AND urunTipi = @urunTipi)
                    INSERT INTO Depo_Stok_Urun(depoKodu,urunAdi,urunMarkasi,urunTipi,urunMiktari) VALUES(@depoKodu,@urunAdi,@urunMarkasi,@urunTipi,@urunMiktari)
                    ELSE UPDATE Depo_Stok_Urun SET urunMiktari=@urunMiktari WHERE  depoKodu = @depoKodu AND urunAdi = @urunAdi AND urunMarkasi = @urunMarkasi AND urunTipi = @urunTipi ";
                            SqlCommand cmd5 = new SqlCommand(updateOrInsert1, connect);

                            cmd5.Parameters.AddWithValue("@depoKodu", transferdepocomboBox14.SelectedValue.ToString());
                            cmd5.Parameters.AddWithValue("@urunAdi", sturunAdicomboBox1.SelectedValue.ToString());
                            cmd5.Parameters.AddWithValue("@urunMarkasi", stUrunMarkasicomboBox3.SelectedValue.ToString());
                            cmd5.Parameters.AddWithValue("@urunTipi", sturunTipicomboBox2.SelectedValue.ToString());
                            cmd5.Parameters.AddWithValue("@urunMiktari", Convert.ToInt32(stMiktartextBox1.Text));


                            cmd5.ExecuteNonQuery();

                            string stokTransferInsert1 = "INSERT INTO StokTransfer(stokTransferKodu,bulunduguDepoKodu,stokTransferTarihi,transferDepoKodu,urunAdi,urunMarkasi,urunTipi,transferMiktari) VALUES(@stokTransferKodu,@bulunduguDepoKodu,@stokTransferTarihi,@transferDepoKodu,@urunAdi,@urunMarkasi,@urunTipi,@transferMiktari)";
                            SqlCommand cmdstInsert1 = new SqlCommand(stokTransferInsert1, connect);
                            cmdstInsert1.Parameters.AddWithValue("@stokTransferKodu", stokTransferKod.Text);
                            cmdstInsert1.Parameters.AddWithValue("@bulunduguDepoKodu", bulunduguDepocomboBox13.SelectedValue.ToString());
                            cmdstInsert1.Parameters.AddWithValue("@stokTransferTarihi", transferdateTimePicker3.Value);
                            cmdstInsert1.Parameters.AddWithValue("@transferDepoKodu", transferdepocomboBox14.SelectedValue.ToString());
                            cmdstInsert1.Parameters.AddWithValue("@urunAdi", sturunAdicomboBox1.SelectedValue.ToString());
                            cmdstInsert1.Parameters.AddWithValue("@urunTipi", sturunTipicomboBox2.SelectedValue.ToString());
                            cmdstInsert1.Parameters.AddWithValue("@urunMarkasi", stUrunMarkasicomboBox3.SelectedValue.ToString());
                            cmdstInsert1.Parameters.AddWithValue("@transferMiktari", Convert.ToInt32(stMiktartextBox1.Text));

                            cmdstInsert1.ExecuteNonQuery();




                            string bulunduguDepoGuncelle1 = "UPDATE  Depo SET bosKapasite=@bosKapasite WHERE depoKodu=@depoKodu";
                            SqlCommand cmdstbulunduguDepo1 = new SqlCommand(bulunduguDepoGuncelle1, connect);
                            cmdstbulunduguDepo1.Parameters.AddWithValue("@depoKodu", bulunduguDepocomboBox13.SelectedValue.ToString());
                            cmdstbulunduguDepo1.Parameters.AddWithValue("@bosKapasite", bulunanYeniBosKapasite);

                            cmdstbulunduguDepo1.ExecuteNonQuery();

                            string transferDepoGuncelle1 = "UPDATE  Depo SET bosKapasite=@bosKapasite WHERE depoKodu=@depoKodu";
                            SqlCommand cmdstTransferDepo1 = new SqlCommand(transferDepoGuncelle1, connect);
                            cmdstTransferDepo1.Parameters.AddWithValue("@depoKodu", transferdepocomboBox14.SelectedValue.ToString());
                            cmdstTransferDepo1.Parameters.AddWithValue("@bosKapasite", transferYeniBosKapasite);

                            cmdstTransferDepo1.ExecuteNonQuery();






                        }
                    }
                    else
                    {
                        MessageBox.Show("Depoda Böyle Bir Ürün Yok..");
                    }







                }

                else
                {
                    MessageBox.Show("Transfer Edilecek Depoda Bu Kadar Yer Yok...");
                }

                 bulunanEskiBosKapasite = 0;
                 bulunanYeniBosKapasite = 0;
                 transferEskiBosKapasite = 0;
                 transferYeniBosKapasite = 0;
                //STOK TRANSFER GRIDVIEW


                string kayit3 = "SELECT * FROM  StokTransfer";

                SqlCommand komut3 = new SqlCommand(kayit3, connect);

                SqlDataAdapter da3 = new SqlDataAdapter(komut3);

                DataTable dt3 = new DataTable();
                da3.Fill(dt3);

                dataGridView6.DataSource = dt3;

                dataGridView6.Columns["stokTransferKodu"].HeaderText = "Stok Transfer Kodu";

                dataGridView6.Columns["stokTransferTarihi"].HeaderText = "Stok Transfer Tarihi";
                dataGridView6.Columns["bulunduguDepoKodu"].HeaderText = "Bulunduğu Depo Kodu";
                dataGridView6.Columns["urunMarkasi"].HeaderText = "Ürün Markası";
                dataGridView6.Columns["urunAdi"].HeaderText = "Ürün Adı";
                dataGridView6.Columns["urunTipi"].HeaderText = "Ürün Tipi";
                dataGridView6.Columns["transferDepoKodu"].HeaderText = "Transfer Depo Kodu";
                dataGridView6.Columns["transferMiktari"].HeaderText = "Transfer Miktarı";
            }
            catch
            {
                MessageBox.Show("Yanlış Giriş Nedeniyle Stok Transferi Eklenemedi.");
            }
        }
        //STOK TRANSFER-2 BULUNDUGU  DEPODAKİ UYGUN ÜRÜN ADI MARKASI TİPİ GÖSTERİR.
        private void bulunduguDepocomboBox13_SelectedIndexChanged(object sender, EventArgs e)
        {

            string cmdText = "SELECT urunAdi,urunMarkasi,urunTipi FROM Depo_Stok_Urun WHERE  depoKodu=@depoKodu";
            SqlCommand cmd = new SqlCommand(cmdText, connect);
            cmd.Parameters.AddWithValue("@depoKodu", bulunduguDepocomboBox13.SelectedValue);

            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            DataTable dtUrunAdi = new DataTable();
            dtUrunAdi.Load(reader);
            sturunAdicomboBox1.ValueMember = "urunAdi";
            sturunAdicomboBox1.DisplayMember = "urunAdi";
            sturunAdicomboBox1.DataSource = dtUrunAdi;

            reader = cmd.ExecuteReader();
            DataTable dtUrunMarkasi = new DataTable();
            dtUrunMarkasi.Load(reader);
            stUrunMarkasicomboBox3.ValueMember = "urunMarkasi";
            stUrunMarkasicomboBox3.DisplayMember = "urunMarkasi";
            stUrunMarkasicomboBox3.DataSource = dtUrunMarkasi;

            reader = cmd.ExecuteReader();
            DataTable dtUrunTipi = new DataTable();
            dtUrunTipi.Load(reader);
            sturunTipicomboBox2.ValueMember = "urunTipi";
            sturunTipicomboBox2.DisplayMember = "urunTipi";
            sturunTipicomboBox2.DataSource = dtUrunTipi;


            //STOK TRANSFER TRANSFER DEPO KODUNU YÜKLE
            string cmdTextDepoTransfer2 = "SELECT  depoKodu FROM Depo WHERE NOT depoKodu=@depoKodu";
            SqlCommand cmd4 = new SqlCommand(cmdTextDepoTransfer2, connect);
            cmd4.Parameters.AddWithValue("@depoKodu", bulunduguDepocomboBox13.SelectedValue);

            SqlDataReader readerDepoTransfer2;
            readerDepoTransfer2 = cmd4.ExecuteReader();
            DataTable dtDepoTransfer2 = new DataTable();
            dtDepoTransfer2.Load(readerDepoTransfer2);
            transferdepocomboBox14.ValueMember = "depoKodu";
            transferdepocomboBox14.DisplayMember = "depoKodu";
            transferdepocomboBox14.DataSource = dtDepoTransfer2;
            cmd4.ExecuteNonQuery();


        }

        private void dataGridView6_SelectionChanged(object sender, EventArgs e)
        {
            stKod2.Text = dataGridView6.CurrentRow.Cells[0].Value.ToString();
            sttbdepo2.Text = dataGridView6.CurrentRow.Cells[1].Value.ToString();
            sttTarih2.Text = dataGridView6.CurrentRow.Cells[2].Value.ToString();
            stttDepo2.Text = dataGridView6.CurrentRow.Cells[3].Value.ToString();
            stturunAdi2.Text = dataGridView6.CurrentRow.Cells[4].Value.ToString();
            sttUrunMarkasi2.Text = dataGridView6.CurrentRow.Cells[5].Value.ToString();
            sttUrunTipi2.Text = dataGridView6.CurrentRow.Cells[6].Value.ToString();
            sttMiktar2.Text = dataGridView6.CurrentRow.Cells[7].Value.ToString();
           
        }

        private void sttIptal_Click(object sender, EventArgs e)
        {
            Int32 Index = dataGridView6.Rows.Count - 1;
            if (dataGridView6.CurrentRow.Index+1 == Index)
            {


                string bulunduguDepoGuncelleOku = "SELECT bosKapasite FROM Depo WHERE depoKodu=@depoKodu";
                SqlCommand cmdbulunduguDepoGuncelleOku = new SqlCommand(bulunduguDepoGuncelleOku, connect);
                cmdbulunduguDepoGuncelleOku.Parameters.AddWithValue("@depoKodu", sttbdepo2.Text);

                using(SqlDataReader readerbulunduguDepoGuncelleOku = cmdbulunduguDepoGuncelleOku.ExecuteReader())
                {
                    while (readerbulunduguDepoGuncelleOku.Read())
                    {
                        bulunanEskiBosKapasite = Convert.ToInt32(readerbulunduguDepoGuncelleOku["bosKapasite"]);
                    }
                }
                cmdbulunduguDepoGuncelleOku.ExecuteNonQuery();
                bulunanYeniBosKapasite = bulunanEskiBosKapasite + Convert.ToInt32(sttMiktar2.Text);


                string bulunduguDepoGuncelle = "UPDATE Depo SET bosKapasite=@bosKapasite  WHERE depoKodu=@depoKodu";
                SqlCommand cmdbulunduguDepoGuncelle = new SqlCommand(bulunduguDepoGuncelle, connect);
                cmdbulunduguDepoGuncelle.Parameters.AddWithValue("@depoKodu", sttbdepo2.Text);

                cmdbulunduguDepoGuncelle.Parameters.AddWithValue("@bosKapasite", bulunanYeniBosKapasite);
                cmdbulunduguDepoGuncelle.ExecuteNonQuery();

                string transferDepoGuncelleOku = "SELECT bosKapasite FROM Depo WHERE depoKodu=@depoKodu";
                SqlCommand cmdtransferDepoGuncelleOku = new SqlCommand(transferDepoGuncelleOku, connect);
                cmdtransferDepoGuncelleOku.Parameters.AddWithValue("@depoKodu", stttDepo2.Text);

                using (SqlDataReader readertransferDepoGuncelleOku = cmdtransferDepoGuncelleOku.ExecuteReader())
                {
                    while (readertransferDepoGuncelleOku.Read())
                    {
                        transferEskiBosKapasite = Convert.ToInt32(readertransferDepoGuncelleOku["bosKapasite"]);
                    }
                }
                cmdtransferDepoGuncelleOku.ExecuteNonQuery();
                transferYeniBosKapasite = transferEskiBosKapasite - Convert.ToInt32(sttMiktar2.Text);


                string transferDepoGuncelle = "UPDATE Depo SET bosKapasite=@bosKapasite  WHERE depoKodu=@depoKodu";
                SqlCommand cmdtransferDepoGuncelle = new SqlCommand(transferDepoGuncelle, connect);
                cmdtransferDepoGuncelle.Parameters.AddWithValue("@depoKodu", stttDepo2.Text);

                cmdtransferDepoGuncelle.Parameters.AddWithValue("@bosKapasite", transferYeniBosKapasite);
                cmdtransferDepoGuncelle.ExecuteNonQuery();
                DateTime now = DateTime.Now.Date;

                string stokTransferIptalInsert = "INSERT INTO StokTransferIptal(stokTransferKodu,stokTransferIptalTarihi) VALUES(@stokTransferKodu,@stokTransferIptalTarihi)";
                SqlCommand cmdStokTransferIptalInsert = new SqlCommand(stokTransferIptalInsert, connect);
                cmdStokTransferIptalInsert.Parameters.AddWithValue("@stokTransferKodu",stKod2.Text);
                cmdStokTransferIptalInsert.Parameters.AddWithValue("@stokTransferIptalTarihi",now);

                cmdStokTransferIptalInsert.ExecuteNonQuery();

                string cmdText = "DELETE FROM StokTransfer  WHERE stokTransferKodu=@stokTransferKodu ";
                SqlCommand cmd = new SqlCommand(cmdText, connect);
                cmd.Parameters.AddWithValue("@stokTransferKodu", stKod2.Text);




                cmd.ExecuteNonQuery();

                string bulunduguDepoStokGuncelleOku = "SELECT urunMiktari FROM Depo_Stok_Urun WHERE depoKodu=@depoKodu";
                SqlCommand cmdbulunduguDepoStokGuncelleOku = new SqlCommand(bulunduguDepoStokGuncelleOku, connect);
                cmdbulunduguDepoStokGuncelleOku.Parameters.AddWithValue("@depoKodu", sttbdepo2.Text);

                using (SqlDataReader readerbulunduguDepoStokGuncelleOku = cmdbulunduguDepoStokGuncelleOku.ExecuteReader())
                {
                    while (readerbulunduguDepoStokGuncelleOku.Read())
                    {
                        eskiUrunMiktari2 = Convert.ToInt32(readerbulunduguDepoStokGuncelleOku["urunMiktari"]);
                    }
                }
                cmdbulunduguDepoStokGuncelleOku.ExecuteNonQuery();
                yeniUrunMiktari2 = eskiUrunMiktari2 + Convert.ToInt32(sttMiktar2.Text);


                string bulunduguDepoStokGuncelleorInsert = @"IF NOT EXISTS(SELECT * FROM Depo_Stok_Urun WHERE depoKodu = @depoKodu AND urunAdi = @urunAdi AND urunMarkasi = @urunMarkasi AND urunTipi = @urunTipi)
                    INSERT INTO Depo_Stok_Urun(depoKodu,urunAdi,urunMarkasi,urunTipi,urunMiktari) VALUES(@depoKodu,@urunAdi,@urunMarkasi,@urunTipi,@urunMiktari)
                    ELSE UPDATE Depo_Stok_Urun SET urunMiktari=@urunMiktari WHERE  depoKodu = @depoKodu AND urunAdi = @urunAdi AND urunMarkasi = @urunMarkasi AND urunTipi = @urunTipi ";
                SqlCommand cmdbulunduguDepoStokGuncelleorInsert = new SqlCommand(bulunduguDepoStokGuncelleorInsert, connect);
                cmdbulunduguDepoStokGuncelleorInsert.Parameters.AddWithValue("@depoKodu", sttbdepo2.Text);
                cmdbulunduguDepoStokGuncelleorInsert.Parameters.AddWithValue("@urunAdi", stturunAdi2.Text);
                cmdbulunduguDepoStokGuncelleorInsert.Parameters.AddWithValue("@urunTipi", sttUrunTipi2.Text);
                cmdbulunduguDepoStokGuncelleorInsert.Parameters.AddWithValue("@urunMarkasi", sttUrunMarkasi2.Text);
                cmdbulunduguDepoStokGuncelleorInsert.Parameters.AddWithValue("@urunMiktari", yeniUrunMiktari2);
                cmdbulunduguDepoStokGuncelleorInsert.ExecuteNonQuery();

                eskiUrunMiktari2 = 0;
                yeniUrunMiktari2 = 0;
                string transferDepoStokGuncelleOku = "SELECT urunMiktari FROM Depo_Stok_Urun WHERE depoKodu=@depoKodu";
                SqlCommand cmdtransferDepoStokGuncelleOku = new SqlCommand(transferDepoStokGuncelleOku, connect);
                cmdtransferDepoStokGuncelleOku.Parameters.AddWithValue("@depoKodu", stttDepo2.Text);

                using (SqlDataReader readertransferDepoStokGuncelleOku = cmdtransferDepoStokGuncelleOku.ExecuteReader())
                {
                    while (readertransferDepoStokGuncelleOku.Read())
                    {
                        eskiUrunMiktari2 = Convert.ToInt32(readertransferDepoStokGuncelleOku["urunMiktari"]);
                    }
                }
                cmdtransferDepoStokGuncelleOku.ExecuteNonQuery();
                yeniUrunMiktari2 = eskiUrunMiktari2 - Convert.ToInt32(sttMiktar2.Text);


                if (yeniUrunMiktari2 == 0)
                {
                    string transferDepoStokDelete = "DELETE FROM Depo_Stok_Urun  WHERE depoKodu = @depoKodu AND urunAdi = @urunAdi AND urunMarkasi = @urunMarkasi AND urunTipi = @urunTipi ";
                    SqlCommand cmdtransferDepoStokDelete = new SqlCommand(transferDepoStokDelete, connect);
                    cmdtransferDepoStokDelete.Parameters.AddWithValue("@depoKodu", stttDepo2.Text);
                    cmdtransferDepoStokDelete.Parameters.AddWithValue("@urunAdi", stturunAdi2.Text);
                    cmdtransferDepoStokDelete.Parameters.AddWithValue("@urunTipi", sttUrunTipi2.Text);
                    cmdtransferDepoStokDelete.Parameters.AddWithValue("@urunMarkasi", sttUrunMarkasi2.Text);
         
                    cmdtransferDepoStokDelete.ExecuteNonQuery();
                }
                else if (yeniUrunMiktari2 > 0) {
                    string transferDepoStokGuncelle = "UPDATE Depo_Stok_Urun SET urunMiktari = @urunMiktari WHERE depoKodu = @depoKodu AND urunAdi = @urunAdi AND urunMarkasi = @urunMarkasi AND urunTipi = @urunTipi ";
                    SqlCommand cmdtransferDepoStokGuncelle = new SqlCommand(transferDepoStokGuncelle, connect);
                    cmdtransferDepoStokGuncelle.Parameters.AddWithValue("@depoKodu", stttDepo2.Text);
                    cmdtransferDepoStokGuncelle.Parameters.AddWithValue("@urunAdi", stturunAdi2.Text);
                    cmdtransferDepoStokGuncelle.Parameters.AddWithValue("@urunTipi", sttUrunTipi2.Text);
                    cmdtransferDepoStokGuncelle.Parameters.AddWithValue("@urunMarkasi", sttUrunMarkasi2.Text);
                    cmdtransferDepoStokGuncelle.Parameters.AddWithValue("@urunMiktari", yeniUrunMiktari2);
                    cmdtransferDepoStokGuncelle.ExecuteNonQuery();
                }

              
                MessageBox.Show("Stok Transferi Başarılı Bir Şekilde İptal Edildi");


                //STOK TRANSFER GRIDVIEW


                string kayit3 = "SELECT * FROM  StokTransfer";

                SqlCommand komut3 = new SqlCommand(kayit3, connect);

                SqlDataAdapter da3 = new SqlDataAdapter(komut3);

                DataTable dt3 = new DataTable();
                da3.Fill(dt3);

                dataGridView6.DataSource = dt3;

                dataGridView6.Columns["stokTransferKodu"].HeaderText = "Stok Transfer Kodu";

                dataGridView6.Columns["stokTransferTarihi"].HeaderText = "Stok Transfer Tarihi";
                dataGridView6.Columns["bulunduguDepoKodu"].HeaderText = "Bulunduğu Depo Kodu";
                dataGridView6.Columns["urunMarkasi"].HeaderText = "Ürün Markası";
                dataGridView6.Columns["urunAdi"].HeaderText = "Ürün Adı";
                dataGridView6.Columns["urunTipi"].HeaderText = "Ürün Tipi";
                dataGridView6.Columns["transferDepoKodu"].HeaderText = "Transfer Depo Kodu";
                dataGridView6.Columns["transferMiktari"].HeaderText = "Transfer Miktarı";


            }
            else
            {
                MessageBox.Show("Sadece Son Yapılan Stok Transferi İptal Edilebilir.");
            }
        }
    }
}


       
    

