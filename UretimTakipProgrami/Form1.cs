﻿using System;
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
    public partial class Form1 : Form
    {
        public string kullaniciAdi = "yetkili";
        public string sifre = "yetkili123";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          //  if(kullaniciAdiTextBox.Text==kullaniciAdi && sifre == passwordTextBox.Text)
          //  {

                this.Hide();
                Form2 f2 = new Form2();
                f2.Show();

           // }
        }
    }
}
