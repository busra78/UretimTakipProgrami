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
    public partial class Form2 : Form
        
    {
        public double stokMiktari;
        public double toplamStokFiyati;
        public double urunBirimFiyati;
        public int urunKod;
        public int depoKod;
        public int tedarikciKod;
        public string conn;
        public SqlConnection connect;
        public SqlDataAdapter adapter;
        public void db_connection()
        {
            try
            {
                conn = "Data Source=DESKTOP-R4LILUJ\\SQLEXPRESS;Initial Catalog=stockfollowdatabase;Integrated Security=True";
                connect = new SqlConnection(conn);
                connect.Open();
                //MessageBox.Show("Db baglanti ok");
            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public Form2()
        {

            InitializeComponent();
            db_connection();

            //Depo Bind
            string cmdStoreText = "SELECT storeCode,storeName from Store";
            SqlCommand cmdStore = new SqlCommand(cmdStoreText, connect);
            adapter = new SqlDataAdapter(cmdStore);
            DataTable dtStore = new DataTable();
            adapter.Fill(dtStore);

            DepoKoduBindComboBox.DisplayMember = "storeCode";
            DepoKoduBindComboBox.ValueMember = "storeName";
            DepoKoduBindComboBox.DataSource = dtStore;


            //Ürün Bind

            string cmdProductText = "SELECT productCode,productName from Product";
            SqlCommand cmdProduct = new SqlCommand(cmdProductText, connect);
            adapter = new SqlDataAdapter(cmdProduct);
            DataTable dtProduct = new DataTable();
            adapter.Fill(dtProduct);

            UrunKoduBindCombobox.DisplayMember = "productCode";
            UrunKoduBindCombobox.ValueMember = "productName";
            UrunKoduBindCombobox.DataSource = dtProduct;

            //Tedarikçi Bind

            string cmdSupplierText = "SELECT supplierCode,supplierName from Supplier";
            SqlCommand cmdSupplier = new SqlCommand(cmdSupplierText, connect);
            adapter = new SqlDataAdapter(cmdSupplier);
            DataTable dtSupplier = new DataTable();
            adapter.Fill(dtSupplier);

            TedarikciKoduBindComboBox.DisplayMember = "supplierCode";
            TedarikciKoduBindComboBox.ValueMember = "supplierName";
            TedarikciKoduBindComboBox.DataSource = dtSupplier;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string cmdText = "INSERT INTO Product(productCode, productName, productType, productUnitPrice) VALUES (@productCode, @productName, @productType, @productUnitPrice)";
                SqlCommand cmd = new SqlCommand(cmdText, connect);
                cmd.Parameters.AddWithValue("@productCode", UrunKoduTextBox.Text);
                cmd.Parameters.AddWithValue("@productName", UrunAdiTextBox.Text);
                cmd.Parameters.AddWithValue("@productType", UrunTipiTextBox.Text);
                cmd.Parameters.AddWithValue("@productUnitPrice", UrunFiyatiTextBox.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Ürün Tanımlandı");
            }
            catch (SqlException)
            {
                MessageBox.Show("Ürün Tanımlanamadı");

            }


        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                string cmdText = "INSERT INTO Customer(customerCode, customerName,customerAddress, customerPhone,customerEmail) VALUES (@customerCode, @customerName,@customerAddress, @customerPhone,@customerEmail)";
                SqlCommand cmd = new SqlCommand(cmdText, connect);
                cmd.Parameters.AddWithValue("@customerCode", MusteriKoduTextBox.Text);
                cmd.Parameters.AddWithValue("@customerName", MusteriAdiTextBox.Text);
                cmd.Parameters.AddWithValue("@customerAddress", MusteriAdresiTextBox.Text);
                cmd.Parameters.AddWithValue("@customerPhone", MusteriTeliTextBox.Text);
                cmd.Parameters.AddWithValue("@customerEmail", MusteriEmailTextBox.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Müşteri Tanımlandı");
            }
            catch (SqlException)
            {
                MessageBox.Show("Müşteri Tanımlanamadı");

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string cmdText = "INSERT INTO Supplier(supplierCode, supplierName,supplierAddress, supplierPhone,supplierEmail) VALUES (@supplierCode, @supplierName,@supplierAddress, @supplierPhone,@supplierEmail)";
                SqlCommand cmd = new SqlCommand(cmdText, connect);
                cmd.Parameters.AddWithValue("@supplierCode", TedarikciKoduTextBox.Text);
                cmd.Parameters.AddWithValue("@supplierName", TedarikciAdiTextBox.Text);
                cmd.Parameters.AddWithValue("@supplierAddress", TedarikciAdresiTextBox.Text);
                cmd.Parameters.AddWithValue("@supplierPhone", TedarikciTeliTextBox.Text);
                cmd.Parameters.AddWithValue("@supplierEmail", TedarikciEmailTextBox.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Tedarikçi Tanımlandı");
            }
            catch (SqlException)
            {
                MessageBox.Show("Tedarikçi Tanımlanamadı");

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string cmdText = "INSERT INTO Store(storeCode, storeName,storeAddress, storePhone) VALUES (@storeCode, @storeName,@storeAddress, @storePhone)";
                SqlCommand cmd = new SqlCommand(cmdText, connect);
                cmd.Parameters.AddWithValue("@storeCode", DepoKoduTextBox.Text);
                cmd.Parameters.AddWithValue("@storeName", DepoAdiTextBox.Text);
                cmd.Parameters.AddWithValue("@storeAddress", DepoAdresiTextBox.Text);
                cmd.Parameters.AddWithValue("@storePhone", DepoTeliTextBox.Text);


                cmd.ExecuteNonQuery();
                MessageBox.Show("Depo Tanımlandı");
            }
            catch (SqlException)
            {
                MessageBox.Show("Depo Tanımlanamadı");

            }

        }




        private void ProductDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            urunAdıBind.Text = ProductDataGridView.CurrentRow.Cells[1].Value.ToString();
            urunTipiBind.Text = ProductDataGridView.CurrentRow.Cells[2].Value.ToString();
            urunKoduBind.Text = ProductDataGridView.CurrentRow.Cells[0].Value.ToString();
            urunFiyatiBind.Text = ProductDataGridView.CurrentRow.Cells[3].Value.ToString();
        }

      

        private void MusteriDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            musteriAdiBind.Text = MusteriDataGridView.CurrentRow.Cells[1].Value.ToString();
            musteriKoduBind.Text = MusteriDataGridView.CurrentRow.Cells[0].Value.ToString();
            musteriAdresiBind.Text = MusteriDataGridView.CurrentRow.Cells[2].Value.ToString();
            musteriTeliBind.Text = MusteriDataGridView.CurrentRow.Cells[3].Value.ToString();
            musteriEmailBind.Text = MusteriDataGridView.CurrentRow.Cells[4].Value.ToString();

        }

      
        private void TedarikciDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            tedarikciAdiBind.Text = TedarikciDataGridView.CurrentRow.Cells[1].Value.ToString();
            tedarikciKoduBind.Text = TedarikciDataGridView.CurrentRow.Cells[0].Value.ToString();
            tedarikciEmailBind.Text = TedarikciDataGridView.CurrentRow.Cells[3].Value.ToString();
            tedarikciAdresiBind.Text = TedarikciDataGridView.CurrentRow.Cells[2].Value.ToString();
            tedarikciTeliBind.Text = TedarikciDataGridView.CurrentRow.Cells[4].Value.ToString();
        }

        private void DepoDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            depoAdiBind.Text = DepoDataGridView.CurrentRow.Cells[1].Value.ToString();
            depoKoduBind.Text = DepoDataGridView.CurrentRow.Cells[0].Value.ToString();
            depoTeliBind.Text = DepoDataGridView.CurrentRow.Cells[3].Value.ToString();
            depoAdresiBind.Text = DepoDataGridView.CurrentRow.Cells[2].Value.ToString();
        }

       
        private void urunGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                string cmdText = "UPDATE Product SET productName=@productName,productType=@productType,productUnitPrice=@productUnitPrice WHERE productCode=@productCode";
                SqlCommand cmd = new SqlCommand(cmdText, connect);
                cmd.Parameters.AddWithValue("@productCode", urunKoduBind.Text);
                cmd.Parameters.AddWithValue("@productType", urunTipiBind.Text);
                cmd.Parameters.AddWithValue("@productName", urunAdıBind.Text);
                cmd.Parameters.AddWithValue("@productUnitPrice", urunFiyatiBind.Text);


                cmd.ExecuteNonQuery();
                MessageBox.Show("Ürün Güncellendi");
                ProductDataGridView.Refresh();
            }
            catch (SqlException)
            {
                MessageBox.Show("Ürün Güncellenemedi");

            }

        }

        private void urunSil_Click(object sender, EventArgs e)
        {
            try
            {
                string cmdText = "DELETE FROM Product WHERE productCode=@productCode";
                SqlCommand cmd = new SqlCommand(cmdText, connect);
                cmd.Parameters.AddWithValue("@productCode", urunKoduBind.Text);


                cmd.ExecuteNonQuery();
                MessageBox.Show("Ürün Silindi");
                ProductDataGridView.Refresh();
            }
            catch (SqlException)
            {
                MessageBox.Show("Ürün Silinemedi");

            }

        }

        private void TedarikciGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                string cmdText = "UPDATE Supplier SET supplierName=@supplierName,supplierAddress=@supplierAddress,supplierEmail=@supplierEmail,supplierPhone=@supplierPhone WHERE supplierCode=@supplierCode";
                SqlCommand cmd = new SqlCommand(cmdText, connect);
                cmd.Parameters.AddWithValue("@supplierCode", tedarikciKoduBind.Text);
                cmd.Parameters.AddWithValue("@supplierAddress", tedarikciAdresiBind.Text);
                cmd.Parameters.AddWithValue("@supplierName", tedarikciAdiBind.Text);
                cmd.Parameters.AddWithValue("@supplierEmail", tedarikciEmailBind.Text);
                cmd.Parameters.AddWithValue("@supplierPhone", tedarikciTeliBind.Text);


                cmd.ExecuteNonQuery();
                MessageBox.Show("Tedarikçi Güncellendi");
                TedarikciDataGridView.Refresh();
            }
            catch (SqlException)
            {
                MessageBox.Show("Tedarikçi Güncellenemedi");

            }

        }

        private void TedarikciSil_Click(object sender, EventArgs e)
        {
            try
            {
                string cmdText = "DELETE FROM Supplier WHERE supplierCode=@supplierCode";
                SqlCommand cmd = new SqlCommand(cmdText, connect);
                cmd.Parameters.AddWithValue("@supplierCode", tedarikciKoduBind.Text);


                cmd.ExecuteNonQuery();
                MessageBox.Show("Tedarikçi Silindi");
                TedarikciDataGridView.Refresh();
            }
            catch (SqlException)
            {
                MessageBox.Show("Tedarikçi Silinemedi");

            }

        }

        private void MusteriGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                string cmdText = "UPDATE Customer SET customerName=@customerName,customerAddress=@customerAddress,customerEmail=@customerEmail,customerPhone=@customerPhone WHERE customerCode=@customerCode";
                SqlCommand cmd = new SqlCommand(cmdText, connect);
                cmd.Parameters.AddWithValue("@customerCode", musteriKoduBind.Text);
                cmd.Parameters.AddWithValue("@customerAddress", musteriAdresiBind.Text);
                cmd.Parameters.AddWithValue("@customerName", musteriAdiBind.Text);
                cmd.Parameters.AddWithValue("@customerEmail", musteriEmailBind.Text);
                cmd.Parameters.AddWithValue("@customerPhone", musteriTeliBind.Text);


                cmd.ExecuteNonQuery();
                MessageBox.Show("Müşteri Güncellendi");
                TedarikciDataGridView.Refresh();
            }
            catch (SqlException)
            {
                MessageBox.Show("Müşteri Güncellenemedi");

            }

        }

        private void MusteriSil_Click(object sender, EventArgs e)
        {
            try
            {
                string cmdText = "DELETE FROM Customer WHERE customerCode=@customerCode";
                SqlCommand cmd = new SqlCommand(cmdText, connect);
                cmd.Parameters.AddWithValue("@customerCode", musteriKoduBind.Text);


                cmd.ExecuteNonQuery();
                MessageBox.Show("Müşteri Silindi");
                MusteriDataGridView.Refresh();
            }
            catch (SqlException)
            {
                MessageBox.Show("Müşteri Silinemedi");

            }

        }

        private void DepoGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                string cmdText = "UPDATE Store SET storeName=@storeName,storeAddress=@storeAddress,storePhone=@storePhone WHERE storeCode=@storeCode";
                SqlCommand cmd = new SqlCommand(cmdText, connect);
                cmd.Parameters.AddWithValue("@storeCode", depoKoduBind.Text);
                cmd.Parameters.AddWithValue("@storeAddress", depoAdresiBind.Text);
                cmd.Parameters.AddWithValue("@storeName", depoAdiBind.Text);
                cmd.Parameters.AddWithValue("@storePhone", depoTeliBind.Text);


                cmd.ExecuteNonQuery();
                MessageBox.Show("Depo Güncellendi");
                DepoDataGridView.Refresh();
            }
            catch (SqlException)
            {
                MessageBox.Show("Depo Güncellenemedi");

            }
        }

        private void DepoSil_Click(object sender, EventArgs e)
        {
            try
            {
                string cmdText = "DELETE FROM Store WHERE storeCode=@storeCode";
                SqlCommand cmd = new SqlCommand(cmdText, connect);
                cmd.Parameters.AddWithValue("@storeCode", depoKoduBind.Text);


                cmd.ExecuteNonQuery();
                MessageBox.Show("Depo Silindi");
                MusteriDataGridView.Refresh();
            }
            catch (SqlException)
            {
                MessageBox.Show("Depo Silinemedi");

            }

        }

        private void DepoKoduBindComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DepoAdiBindTextBox.Text = DepoKoduBindComboBox.SelectedValue.ToString();
        }

        private void UrunKoduBindCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UrunAdiBindTextBox.Text = UrunKoduBindCombobox.SelectedValue.ToString();


        }

        private void TedarikciKoduBindComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TedarikciAdiBindTextBox.Text = TedarikciKoduBindComboBox.SelectedValue.ToString();
        }
        private void button5_Click(object sender, EventArgs e)
        {
           
            
            string sqlProduct = "SELECT productCode,productUnitPrice FROM Product WHERE productName=@productName";
            SqlCommand cmdProduct = new SqlCommand(sqlProduct, connect);
            cmdProduct.Parameters.AddWithValue("@productName", UrunKoduBindCombobox.SelectedValue.ToString());
            using (SqlDataReader rdrProduct = cmdProduct.ExecuteReader())
            {
                while (rdrProduct.Read())
                {
                    urunBirimFiyati = Convert.ToDouble(rdrProduct["productUnitPrice"]);
                    urunKod = Convert.ToInt32(rdrProduct["productCode"]);
                }
            }
            string sqlStore = "SELECT storeCode FROM Store WHERE storeName=@storeName";
            SqlCommand cmdStore = new SqlCommand(sqlStore, connect);
            cmdStore.Parameters.AddWithValue("@storeName", DepoKoduBindComboBox.SelectedValue.ToString());
            using (SqlDataReader rdrStore = cmdStore.ExecuteReader())
            {
                while (rdrStore.Read())
                {
              
                    depoKod = Convert.ToInt32(rdrStore["storeCode"]);
                }
            }
            string sqlSupplier = "SELECT supplierCode FROM Supplier WHERE supplierName=@supplierName";
            SqlCommand cmdSupplier = new SqlCommand(sqlSupplier, connect);
            cmdSupplier.Parameters.AddWithValue("@supplierName", TedarikciKoduBindComboBox.SelectedValue.ToString());
            using (SqlDataReader rdrSupplier = cmdSupplier.ExecuteReader())
            {
                while (rdrSupplier.Read())
                {

                    depoKod = Convert.ToInt32(rdrSupplier["supplierCode"]);
                }
            }

            stokMiktari = Convert.ToDouble(StokMiktariTextBox.Text.ToString());
            toplamStokFiyati = stokMiktari * urunBirimFiyati;

            StringBuilder MessageText = new StringBuilder();
            MessageText.AppendLine("Stok Giriş Kodu:" + StokKoduTextBox.Text);

            MessageText.AppendLine(" ");

            MessageText.AppendLine("Stok Giriş Tarihi:" + StokTarihiDateTimePicker.Value.ToShortDateString().ToString());
            MessageText.AppendLine(" ");

            MessageText.AppendLine("Depo Adı:" + DepoKoduBindComboBox.SelectedValue.ToString());
            MessageText.AppendLine(" ");

            MessageText.AppendLine("Ürün Adı:" + UrunKoduBindCombobox.SelectedValue.ToString());
            MessageText.AppendLine(" ");

            MessageText.AppendLine("Stok Miktarı:" + StokMiktariTextBox.Text);
            MessageText.AppendLine(" ");

            MessageText.AppendLine("Tedarikçi Adı:" + TedarikciKoduBindComboBox.SelectedValue.ToString());
            MessageText.AppendLine(" ");

            MessageText.AppendLine("Stok Toplam Fiyatı:" + toplamStokFiyati.ToString()+" TL");
            MessageText.AppendLine(" ");
            MessageText.AppendLine("Stok Girişini Onaylıyor Musun?");
            MessageText.AppendLine(" ");

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            var result = MessageBox.Show(MessageText.ToString(), "Stok Giriş Bilgileri",
                                 buttons,
                                 MessageBoxIcon.Warning);


        }

        private void UrunListele_Click(object sender, EventArgs e)
        {
            db_connection();
            string cmdText = "SELECT * from Product";
            SqlCommand cmd = new SqlCommand(cmdText, connect);
            adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.

            ProductDataGridView.DataSource = dt;
            //Formumuzdaki DataGridViewin ver
            ProductDataGridView.Columns["productCode"].HeaderText = "Ürün Kodu";
            ProductDataGridView.Columns["productType"].HeaderText = "Ürün Tipi";
            ProductDataGridView.Columns["productName"].HeaderText = "Ürün Adı";
            ProductDataGridView.Columns["productUnitPrice"].HeaderText = "Ürün Birim Fiyatı";
        }

        private void MusteriListele_Click(object sender, EventArgs e)
        {
            db_connection();
            string cmdText = "SELECT * from Customer";
            SqlCommand cmd = new SqlCommand(cmdText, connect);
            adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.

            MusteriDataGridView.DataSource = dt;
            //Formumuzdaki DataGridViewin ver
            MusteriDataGridView.Columns["customerCode"].HeaderText = "Müşteri Kodu";
            MusteriDataGridView.Columns["customerAddress"].HeaderText = "Adres";
            MusteriDataGridView.Columns["customerName"].HeaderText = "Müşteri Adı";
            MusteriDataGridView.Columns["customerPhone"].HeaderText = "Telefon";
            MusteriDataGridView.Columns["customerEmail"].HeaderText = "Email";
        }

        private void TedarikciListele_Click(object sender, EventArgs e)
        {
            db_connection();
            string cmdText = "SELECT * from Supplier";
            SqlCommand cmd = new SqlCommand(cmdText, connect);
            adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.

            TedarikciDataGridView.DataSource = dt;
            //Formumuzdaki DataGridViewin ver
            TedarikciDataGridView.Columns["supplierCode"].HeaderText = "Tedarikçi Kodu";
            TedarikciDataGridView.Columns["supplierAddress"].HeaderText = "Adres";
            TedarikciDataGridView.Columns["supplierName"].HeaderText = "Tedarikçi Adı";
            TedarikciDataGridView.Columns["supplierPhone"].HeaderText = "Telefon";
            TedarikciDataGridView.Columns["supplierEmail"].HeaderText = "Email";
        }

        private void DepoListele_Click(object sender, EventArgs e)
        {
            db_connection();
            string cmdText = "SELECT * from Store";
            SqlCommand cmd = new SqlCommand(cmdText, connect);
            adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.

            DepoDataGridView.DataSource = dt;
            //Formumuzdaki DataGridViewin ver
            DepoDataGridView.Columns["storeCode"].HeaderText = "Depo Kodu";
            DepoDataGridView.Columns["storeAddress"].HeaderText = "Adres";
            DepoDataGridView.Columns["storeName"].HeaderText = "Depo Adı";
            DepoDataGridView.Columns["storePhone"].HeaderText = "Telefon";

        }
    }
}

