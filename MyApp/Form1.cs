﻿using System.Windows.Forms;

namespace MyApp
{
    public partial class Form1 : Form
    {
        IniManager ini;
        public Form1()
        {
            InitializeComponent();
            ini = new IniManager("data/data.ini");
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            textBox1.Text = ini.ReadIniFile("user", "text", "");
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            ini.WriteIniFile("user", "text", textBox1.Text);
        }
    }
}
