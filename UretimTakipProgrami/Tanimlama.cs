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
namespace UretimTakipProgrami
{
    public partial class Tanimlama : Form
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
                //MessageBox.Show("Db baglanti ok");
            }
            catch (SqlException )
            {
                throw;
            }
        }
        public Tanimlama()
        {
            InitializeComponent();
            db_connection();
        }

       

      

        private void uruntanimla_Click(object sender, EventArgs e)
        {
            try
            {
                string cmdText = "INSERT INTO Urun(urunKodu,urunAdi,urunMarkasi,urunTipi,urunKategorisi,urunBirimFiyati) VALUES (@urunKodu, @urunAdi, @urunMarkasi, @urunTipi, @urunKategorisi, @urunBirimFiyati)";
                SqlCommand cmd = new SqlCommand(cmdText, connect);
                cmd.Parameters.AddWithValue("@urunKodu", urunKodutextBox.Text);
                cmd.Parameters.AddWithValue("@urunAdi", urunAditextBox.Text);
                cmd.Parameters.AddWithValue("@urunMarkasi", urunMarkasitextBox.Text);
                cmd.Parameters.AddWithValue("@urunTipi", urunTipitextBox.Text);
                cmd.Parameters.AddWithValue("@urunKategorisi", urunKategorisitextBox.Text);
                cmd.Parameters.AddWithValue("@urunBirimFiyati", urunBirimFiyatitextBox.Text);

                cmd.ExecuteNonQuery();
              
                MessageBox.Show("Ürün Başarılı Bir Şekilde Eklendi");
                string kayit = "SELECT * FROM  Urun";

                SqlCommand komut = new SqlCommand(kayit, connect);

                SqlDataAdapter da = new SqlDataAdapter(komut);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["urunKodu"].HeaderText = "Ürün Kodu";
                dataGridView1.Columns["urunKategorisi"].HeaderText = "Kategori";
                dataGridView1.Columns["urunAdi"].HeaderText = "Ürün Adı";
                dataGridView1.Columns["urunTipi"].HeaderText = "Ürün Tipi";
                dataGridView1.Columns["urunMarkasi"].HeaderText = "Markası";


                dataGridView1.Columns["urunBirimFiyati"].HeaderText = "Birim Fiyatı";

            }
            catch
            {
                MessageBox.Show("Ürün Eklenemedi");

            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            urunKodutextBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            urunAditextBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            urunMarkasitextBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            urunTipitextBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            urunKategorisitextBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

            urunbirimtextBox2.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void urunGuncelleTanim_Click(object sender, EventArgs e)
        {
            string cmdText = "UPDATE Urun SET urunKodu=@urunKodu,urunKategorisi=@urunKategorisi,urunBirimFiyati=@urunBirimFiyati WHERE urunAdi=@urunAdi AND urunMarkasi=@urunMarkasi AND urunTipi=@urunTipi";
            SqlCommand cmd = new SqlCommand(cmdText, connect);
            cmd.Parameters.AddWithValue("@urunKodu", urunKodutextBox2.Text);
            cmd.Parameters.AddWithValue("@urunAdi", urunAditextBox2.Text);
            cmd.Parameters.AddWithValue("@urunMarkasi", urunMarkasitextBox2.Text);
            cmd.Parameters.AddWithValue("@urunTipi", urunTipitextBox2.Text);
            cmd.Parameters.AddWithValue("@urunKategorisi", urunKategorisitextBox2.Text);
            cmd.Parameters.AddWithValue("@urunBirimFiyati", Convert.ToDouble(urunbirimtextBox2.Text));

            cmd.ExecuteNonQuery();
            MessageBox.Show("Ürün Başarılı Bir Şekilde Güncellendi");
            string kayit = "SELECT * FROM  Urun";

            SqlCommand komut = new SqlCommand(kayit, connect);

            SqlDataAdapter da = new SqlDataAdapter(komut);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
            dataGridView1.Columns["urunKodu"].HeaderText = "Ürün Kodu";
            dataGridView1.Columns["urunKategorisi"].HeaderText = "Ürün Kategorisi";
            dataGridView1.Columns["urunAdi"].HeaderText = "Ürün Adı";
            dataGridView1.Columns["urunTipi"].HeaderText = "Ürün Tipi";
            dataGridView1.Columns["urunMarkasi"].HeaderText = "Ürün Markası";


            dataGridView1.Columns["urunBirimFiyati"].HeaderText = "Birim Fiyatı";


        }

     
        private void tedarikcitanimla_Click(object sender, EventArgs e)
        {
            try
            {
                string cmdText = "INSERT INTO Tedarikci(tedarikciKodu,tedarikciAdi,tedarikciTelefon,tedarikciEmail,tedarikciAdres) VALUES (@tedarikciKodu, @tedarikciAdi, @tedarikciTelefon, @tedarikciEmail, @tedarikciAdres)";
                SqlCommand cmd = new SqlCommand(cmdText, connect);
                cmd.Parameters.AddWithValue("@tedarikciKodu", tedarikciKodutextBox.Text);
                cmd.Parameters.AddWithValue("@tedarikciAdi", tedarkciAditextBox11.Text);
                cmd.Parameters.AddWithValue("@tedarikciTelefon", tedarikciTelefontextBox12.Text);
                cmd.Parameters.AddWithValue("@tedarikciEmail", tedarikciEmailtextBox7.Text);
                cmd.Parameters.AddWithValue("@tedarikciAdres", tedarikciAdresTextBox.Text);
              

                cmd.ExecuteNonQuery();
                MessageBox.Show("Tedarikçi Başarılı Bir Şekilde Eklendi");
                string kayit2 = "SELECT * FROM  Tedarikci";

                SqlCommand komut2 = new SqlCommand(kayit2, connect);

                SqlDataAdapter da2 = new SqlDataAdapter(komut2);

                DataTable dt2 = new DataTable();
                da2.Fill(dt2);

                dataGridView2.DataSource = dt2;
                dataGridView2.Columns["tedarikciKodu"].HeaderText = "Tedarikçi Kodu";
                dataGridView2.Columns["tedarikciAdi"].HeaderText = "Tedarikçi Adı";
                dataGridView2.Columns["tedarikciTelefon"].HeaderText = "Telefon";
                dataGridView2.Columns["tedarikciEmail"].HeaderText = "Email";
                dataGridView2.Columns["tedarikciAdres"].HeaderText = "Adres";
            }
            catch
            {
                MessageBox.Show("Tedarikçi Eklenemedi");

            }
        }
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {

            tedarikciKodu2.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            tedarikciAdi2.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            tedarikcitel2.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            tedarikciemail2.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            tedarikciAdres2.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();

           
        }


        private void tedarikciGuncelle_Click(object sender, EventArgs e)
        {
            string cmdText = "UPDATE Tedarikci SET tedarikciAdi=@tedarikciAdi,tedarikciAdres=@tedarikciAdres,tedarikciEmail=@tedarikciEmail,tedarikciTelefon=@tedarikciTelefon WHERE  tedarikciKodu=@tedarikciKodu ";
            SqlCommand cmd = new SqlCommand(cmdText, connect);
            cmd.Parameters.AddWithValue("@tedarikciKodu", tedarikciKodu2.Text);
            cmd.Parameters.AddWithValue("@tedarikciAdi", tedarikciAdi2.Text);
            cmd.Parameters.AddWithValue("@tedarikciAdres", tedarikciAdres2.Text);
            cmd.Parameters.AddWithValue("@tedarikciEmail", tedarikciemail2.Text);
            cmd.Parameters.AddWithValue("@tedarikciTelefon", Convert.ToInt32(tedarikcitel2.Text));

            cmd.ExecuteNonQuery();
            MessageBox.Show("Tedarikçi Başarılı Bir Şekilde Güncellendi");
            string kayit2 = "SELECT * FROM  Tedarikci";

            SqlCommand komut2 = new SqlCommand(kayit2, connect);

            SqlDataAdapter da2 = new SqlDataAdapter(komut2);

            DataTable dt2 = new DataTable();
            da2.Fill(dt2);

            dataGridView2.DataSource = dt2;
            dataGridView2.Columns["tedarikciKodu"].HeaderText = "Tedarikçi Kodu";
            dataGridView2.Columns["tedarikciAdi"].HeaderText = "Tedarikçi Adı";
            dataGridView2.Columns["tedarikciTelefon"].HeaderText = "Telefon";
            dataGridView2.Columns["tedarikciEmail"].HeaderText = "Email";
            dataGridView2.Columns["tedarikciAdres"].HeaderText = "Adres";

        }

       
        private void musteritanimla_Click(object sender, EventArgs e)
        {
            try
            {
                string cmdText = "INSERT INTO Musteri(musteriKodu,musteriAdi,musteriTelefon,musteriEmail,musteriAdres) VALUES (@musteriKodu, @musteriAdi, @musteriTelefon, @musteriEmail, @musteriAdres)";
                SqlCommand cmd = new SqlCommand(cmdText, connect);
                cmd.Parameters.AddWithValue("@musteriKodu", musteriKodutextBox10.Text);
                cmd.Parameters.AddWithValue("@musteriAdi", musteriAditextBox13.Text);
                cmd.Parameters.AddWithValue("@musteriTelefon", musteriTelefontextBox14.Text);
                cmd.Parameters.AddWithValue("@musteriEmail", musteriEmailtextBox9.Text);
                cmd.Parameters.AddWithValue("@musteriAdres", musteriAdresTextBox2.Text);


                cmd.ExecuteNonQuery();
                MessageBox.Show("Müşteri Başarılı Bir Şekilde Eklendi");
                string kayit3 = "SELECT * FROM  Musteri";

                SqlCommand komut3 = new SqlCommand(kayit3, connect);

                SqlDataAdapter da3 = new SqlDataAdapter(komut3);

                DataTable dt3 = new DataTable();
                da3.Fill(dt3);

                dataGridView3.DataSource = dt3;
                dataGridView3.Columns["musteriKodu"].HeaderText = "Müşteri Kodu";
                dataGridView3.Columns["musteriAdi"].HeaderText = "Müşteri Adı";
                dataGridView3.Columns["musteriTelefon"].HeaderText = "Telefon";
                dataGridView3.Columns["musteriEmail"].HeaderText = "Email";
                dataGridView3.Columns["musteriAdres"].HeaderText = "Adres";
            }
            catch
            {
                MessageBox.Show("Müşteri Eklenemedi");

            }

        }
        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            musteriKodu2.Text = dataGridView3.CurrentRow.Cells[0].Value.ToString();
            musteriAdi2.Text = dataGridView3.CurrentRow.Cells[1].Value.ToString();
            musteritel2.Text = dataGridView3.CurrentRow.Cells[2].Value.ToString();
            musteriemail2.Text = dataGridView3.CurrentRow.Cells[3].Value.ToString();
            musteriadres2.Text = dataGridView3.CurrentRow.Cells[4].Value.ToString();
        }

        private void musteriGuncelle_Click(object sender, EventArgs e)
        {
            string cmdText = "UPDATE Musteri SET   musteriAdi=@musteriAdi,musteriAdres=@musteriAdres,musteriEmail=@musteriEmail,musteriTelefon=@musteriTelefon WHERE musteriKodu=@musteriKodu ";
            SqlCommand cmd = new SqlCommand(cmdText, connect);
            cmd.Parameters.AddWithValue("@musteriKodu", musteriKodu2.Text);
            cmd.Parameters.AddWithValue("@musteriAdi", musteriAdi2.Text);
            cmd.Parameters.AddWithValue("@musteriAdres", musteriadres2.Text);
            cmd.Parameters.AddWithValue("@musteriEmail", musteriemail2.Text);
            cmd.Parameters.AddWithValue("@musteriTelefon", Convert.ToInt32(musteritel2.Text));

            cmd.ExecuteNonQuery();
            MessageBox.Show("Müşteri Başarılı Bir Şekilde Güncellendi");
            string kayit3 = "SELECT * FROM  Musteri";

            SqlCommand komut3 = new SqlCommand(kayit3, connect);

            SqlDataAdapter da3 = new SqlDataAdapter(komut3);

            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            dataGridView3.DataSource = dt3;
            dataGridView3.Columns["musteriKodu"].HeaderText = "Müşteri Kodu";
            dataGridView3.Columns["musteriAdi"].HeaderText = "Müşteri Adı";
            dataGridView3.Columns["musteriTelefon"].HeaderText = "Telefon";
            dataGridView3.Columns["musteriEmail"].HeaderText = "Email";
            dataGridView3.Columns["musteriAdres"].HeaderText = "Adres";
        }

       

        private void depotanimla_Click(object sender, EventArgs e)
        {
            
            try
            {
                string cmdText = "INSERT INTO Depo(depoKodu,depoAdi,depoTelefon,depoKapasitesi,depoAdres,bosKapasite)" +
                    " VALUES (@depoKodu, @depoAdi, @depoTelefon, @depoKapasitesi, @depoAdres,@bosKapasite)";
                SqlCommand cmd = new SqlCommand(cmdText, connect);
                cmd.Parameters.AddWithValue("@depoKodu", depokodutextBox16.Text);
                cmd.Parameters.AddWithValue("@depoAdi", depoAditextBox17.Text);
                cmd.Parameters.AddWithValue("@depoTelefon", depoTelefontextBox15.Text);
                cmd.Parameters.AddWithValue("@depoKapasitesi",depoKapasitesitextBox18.Text);
                cmd.Parameters.AddWithValue("@depoAdres", depoAdresTextBox3.Text);
                cmd.Parameters.AddWithValue("@bosKapasite", depoKapasitesitextBox18.Text);


                cmd.ExecuteNonQuery();
                MessageBox.Show("Depo Başarılı Bir Şekilde Eklendi");
                string kayit4 = "SELECT * FROM  Depo";

                SqlCommand komut4 = new SqlCommand(kayit4, connect);

                SqlDataAdapter da4 = new SqlDataAdapter(komut4);

                DataTable dt4 = new DataTable();
                da4.Fill(dt4);

                dataGridView4.DataSource = dt4;

                dataGridView4.Columns["depoKodu"].HeaderText = "Depo Kodu";

                dataGridView4.Columns["depoAdi"].HeaderText = "Depo Adı";
                dataGridView4.Columns["depoTelefon"].HeaderText = "Telefon";
                dataGridView4.Columns["depoKapasitesi"].HeaderText = "Kapasite";
                dataGridView4.Columns["depoAdres"].HeaderText = "Adres";
                dataGridView4.Columns["bosKapasite"].HeaderText = "Boş Kapasite";
            }
            catch
            {
                MessageBox.Show("Depo Eklenemedi");

            }
        }
        private void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            depoKodu2.Text = dataGridView4.CurrentRow.Cells[0].Value.ToString();
            depoAdi2.Text = dataGridView4.CurrentRow.Cells[1].Value.ToString();
            depoAdres2.Text = dataGridView4.CurrentRow.Cells[2].Value.ToString();
            depotel2.Text = dataGridView4.CurrentRow.Cells[3].Value.ToString();
            depokapasitesi2.Text = dataGridView4.CurrentRow.Cells[4].Value.ToString();
        }
        private void depoGüncelle_Click(object sender, EventArgs e)
        {
            string cmdText = "UPDATE Depo SET depoAdi=@depoAdi,depoAdres=@depoAdres,depoTelefon=@depoTelefon WHERE depoKodu=@depoKodu ";
            SqlCommand cmd = new SqlCommand(cmdText, connect);
            cmd.Parameters.AddWithValue("@depoKodu", depoKodu2.Text);
            cmd.Parameters.AddWithValue("@depoAdi", depoAdi2.Text);
            cmd.Parameters.AddWithValue("@depoTelefon", Convert.ToInt32(depotel2.Text));
            cmd.Parameters.AddWithValue("@depoAdres", depoAdres2.Text);


            cmd.ExecuteNonQuery();
            MessageBox.Show("Depo Başarılı Bir Şekilde Güncellendi");
            string kayit4 = "SELECT * FROM  Depo";

            SqlCommand komut4 = new SqlCommand(kayit4, connect);

            SqlDataAdapter da4 = new SqlDataAdapter(komut4);

            DataTable dt4 = new DataTable();
            da4.Fill(dt4);

            dataGridView4.DataSource = dt4;

            dataGridView4.Columns["depoKodu"].HeaderText = "Depo Kodu";

            dataGridView4.Columns["depoAdi"].HeaderText = "Depo Adı";
            dataGridView4.Columns["depoTelefon"].HeaderText = "Telefon";
            dataGridView4.Columns["depoKapasitesi"].HeaderText = "Kapasite";
            dataGridView4.Columns["depoAdres"].HeaderText = "Adres";
            dataGridView4.Columns["bosKapasite"].HeaderText = "Boş Kapasite";

        }

      
        private void Tanimlama_Load(object sender, EventArgs e)
        {
            //URUN
            string kayit = "SELECT * FROM  Urun";
            
            SqlCommand komut = new SqlCommand(kayit, connect);
            
            SqlDataAdapter da = new SqlDataAdapter(komut);
    
            DataTable dt = new DataTable();
            da.Fill(dt);
           
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["urunKodu"].HeaderText = "Ürün Kodu";
            dataGridView1.Columns["urunKategorisi"].HeaderText = "Ürün Kategorisi";
            dataGridView1.Columns["urunAdi"].HeaderText = "Ürün Adı";
            dataGridView1.Columns["urunTipi"].HeaderText = "Ürün Tipi";
            dataGridView1.Columns["urunMarkasi"].HeaderText = "Ürün Markası";       
            dataGridView1.Columns["urunBirimFiyati"].HeaderText = "Birim Fiyatı";


            //TEDARİKCİ
            string kayit2 = "SELECT * FROM  Tedarikci";

            SqlCommand komut2 = new SqlCommand(kayit2, connect);

            SqlDataAdapter da2 = new SqlDataAdapter(komut2);

            DataTable dt2 = new DataTable();
            da2.Fill(dt2);

            dataGridView2.DataSource = dt2;
            dataGridView2.Columns["tedarikciKodu"].HeaderText = "Tedarikçi Kodu";
            dataGridView2.Columns["tedarikciAdi"].HeaderText = "Tedarikçi Adı";
            dataGridView2.Columns["tedarikciTelefon"].HeaderText = "Telefon";
            dataGridView2.Columns["tedarikciEmail"].HeaderText = "Email";
            dataGridView2.Columns["tedarikciAdres"].HeaderText = "Adres";

            //MUSTERİ
 
            string kayit3 = "SELECT * FROM  Musteri";

            SqlCommand komut3 = new SqlCommand(kayit3, connect);

            SqlDataAdapter da3 = new SqlDataAdapter(komut3);

            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            dataGridView3.DataSource = dt3;
            dataGridView3.Columns["musteriKodu"].HeaderText = "Müşteri Kodu";
            dataGridView3.Columns["musteriAdi"].HeaderText = "Müşteri Adı";
            dataGridView3.Columns["musteriTelefon"].HeaderText = "Telefon";
            dataGridView3.Columns["musteriEmail"].HeaderText = "Email";
            dataGridView3.Columns["musteriAdres"].HeaderText = "Adres";


            //DEPO
            string kayit4 = "SELECT * FROM  Depo";

            SqlCommand komut4 = new SqlCommand(kayit4, connect);

            SqlDataAdapter da4 = new SqlDataAdapter(komut4);

            DataTable dt4 = new DataTable();
            da4.Fill(dt4);

            dataGridView4.DataSource = dt4;

            dataGridView4.Columns["depoKodu"].HeaderText = "Depo Kodu";

            dataGridView4.Columns["depoAdi"].HeaderText = "Depo Adı";
            dataGridView4.Columns["depoTelefon"].HeaderText = "Telefon";
            dataGridView4.Columns["depoKapasitesi"].HeaderText = "Kapasite";
            dataGridView4.Columns["depoAdres"].HeaderText = "Adres";
            dataGridView4.Columns["bosKapasite"].HeaderText = "Boş Kapasite";



        }

      
    }
}
