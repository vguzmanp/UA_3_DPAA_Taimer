﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace rubenDesign
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        private void buttonExitClient_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.loginWindow.Show();
        }
    }
}
