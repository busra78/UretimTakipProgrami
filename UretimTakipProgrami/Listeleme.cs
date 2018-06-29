using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UretimTakipProgrami
{
    public partial class Listeleme : Form
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
            catch (SqlException)
            {
                throw;
            }
        }
        public Listeleme()
        {
            InitializeComponent();
            db_connection();
        }

      
      

        private void Listeleme_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'veritabani.Depo' table. You can move, or remove it, as needed.
            this.depoTableAdapter.Fill(this.veritabani.Depo);
            // TODO: This line of code loads data into the 'veritabani.Musteri' table. You can move, or remove it, as needed.
            this.musteriTableAdapter.Fill(this.veritabani.Musteri);
            // TODO: This line of code loads data into the 'veritabani.Tedarikci' table. You can move, or remove it, as needed.
            this.tedarikciTableAdapter.Fill(this.veritabani.Tedarikci);
            // TODO: This line of code loads data into the 'veritabani.Urun' table. You can move, or remove it, as needed.
            this.urunTableAdapter.Fill(this.veritabani.Urun);

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            urunKodutextBox.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            urunKategorisitextBox.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            urunAditextBox.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            urunTipitextBox.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            urunMarkasitextBox.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            urunBirimFiyatitextBox.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void urunguncelle_Click(object sender, EventArgs e)
        {
               string cmdText = "UPDATE Urun SET urunKodu=@urunKodu,urunKategorisi=@urunKategorisi,urunBirimFiyati=@urunBirimFiyati WHERE urunAdi=@urunAdi AND urunMarkasi=@urunMarkasi AND urunTipi=@urunTipi";
                SqlCommand cmd = new SqlCommand(cmdText, connect);
                cmd.Parameters.AddWithValue("@urunKodu", urunKodutextBox.Text);
                cmd.Parameters.AddWithValue("@urunAdi", urunAditextBox.Text);
                cmd.Parameters.AddWithValue("@urunMarkasi", urunMarkasitextBox.Text);
                cmd.Parameters.AddWithValue("@urunTipi", urunTipitextBox.Text);
                cmd.Parameters.AddWithValue("@urunKategorisi", urunKategorisitextBox.Text);
                cmd.Parameters.AddWithValue("@urunBirimFiyati",Convert.ToDouble(urunBirimFiyatitextBox.Text));
         
                cmd.ExecuteNonQuery();
                MessageBox.Show("Ürün Başarılı Bir Şekilde Güncellendi");
            
            
        }

       

        private void urunyenile_Click(object sender, EventArgs e)
        {
           
        }
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            tedarikciKodutextBox.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            tedarkciAditextBox11.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            tedarikciAdresTextBox.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
            tedarikciEmailtextBox7.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            tedarikciTelefontextBox12.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            
        }
        private void tedarikciguncelle_Click(object sender, EventArgs e)
        {
            string cmdText = "UPDATE Tedarikci SET  tedarikciAdi=@tedarikciAdi,  tedarikciAdres=@tedarikciAdres,tedarikciEmail=@tedarikciEmail,tedarikciTelefon=@tedarikciTelefon WHERE tedarikciKodu=@tedarikciKodu ";
            SqlCommand cmd = new SqlCommand(cmdText, connect);
            cmd.Parameters.AddWithValue("@tedarikciKodu", tedarikciKodutextBox.Text);
            cmd.Parameters.AddWithValue("@tedarikciAdi", tedarkciAditextBox11.Text);
            cmd.Parameters.AddWithValue("@tedarikciAdres", tedarikciAdresTextBox.Text);
            cmd.Parameters.AddWithValue("@tedarikciEmail", tedarikciEmailtextBox7.Text);
            cmd.Parameters.AddWithValue("@tedarikciTelefon", Convert.ToInt32(tedarikciTelefontextBox12.Text));

            cmd.ExecuteNonQuery();
            MessageBox.Show("Tedarikçi Başarılı Bir Şekilde Güncellendi");

        }

       
        private void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            depokodutextBox16.Text = dataGridView4.CurrentRow.Cells[0].Value.ToString();
            depoAditextBox17.Text = dataGridView4.CurrentRow.Cells[1].Value.ToString();
            depoTelefontextBox15.Text = dataGridView4.CurrentRow.Cells[3].Value.ToString();
            depoAdresTextBox3.Text = dataGridView4.CurrentRow.Cells[2].Value.ToString();
            depoKapasitesitextBox18.Text = dataGridView4.CurrentRow.Cells[4].Value.ToString();
          

        }
        private void depoguncelle_Click(object sender, EventArgs e)
        {
            string cmdText = "UPDATE Depo SET   depoAdi=@depoAdi,depoAdres=@depoAdres,depoTelefon=@depoTelefon WHERE depoKodu=@depoKodu  ";
            SqlCommand cmd = new SqlCommand(cmdText, connect);
            cmd.Parameters.AddWithValue("@depoKodu", depokodutextBox16.Text);
            cmd.Parameters.AddWithValue("@depoAdi", depoAditextBox17.Text);
            cmd.Parameters.AddWithValue("@depoTelefon", Convert.ToInt16(depoTelefontextBox15.Text));
            cmd.Parameters.AddWithValue("@depoAdres", depoAdresTextBox3.Text);
            

       cmd.ExecuteNonQuery();
            MessageBox.Show("Depo Başarılı Bir Şekilde Güncellendi");
        }

       
        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            musteriKodutextBox10.Text = dataGridView3.CurrentRow.Cells[0].Value.ToString();
            musteriAditextBox13.Text = dataGridView3.CurrentRow.Cells[1].Value.ToString();
            musteriAdresTextBox2.Text = dataGridView3.CurrentRow.Cells[4].Value.ToString();
            musteriEmailtextBox9.Text = dataGridView3.CurrentRow.Cells[3].Value.ToString();
            musteriTelefontextBox14.Text = dataGridView3.CurrentRow.Cells[2].Value.ToString();
        }
        private void musteriguncelle_Click(object sender, EventArgs e)
        {
            string cmdText = "UPDATE Musteri SET  musteriAdi=@musteriAdi, musteriAdres=@musteriAdres,musteriEmail=@musteriEmail,musteriTelefon=@musteriTelefon WHERE musteriKodu=@musteriKodu ";
            SqlCommand cmd = new SqlCommand(cmdText, connect);
            cmd.Parameters.AddWithValue("@musteriKodu", musteriKodutextBox10.Text);
            cmd.Parameters.AddWithValue("@musteriAdi", musteriAditextBox13.Text);
            cmd.Parameters.AddWithValue("@musteriAdres", musteriAdresTextBox2.Text);
            cmd.Parameters.AddWithValue("@musteriEmail", musteriEmailtextBox9.Text);
            cmd.Parameters.AddWithValue("@musteriTelefon", Convert.ToInt32(musteriTelefontextBox14.Text));

            cmd.ExecuteNonQuery();
            MessageBox.Show("Müşteri Başarılı Bir Şekilde Güncellendi");
        }

       
       
    }
}
