using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UretimTakipProgrami
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="deneme" & textBox2.Text == "123")
            {
                this.Hide();
                Anasayfa f = new Anasayfa();
                f.Show();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifresi yanlış..");
            }
        }
    }
}
