﻿using System.Windows.Forms;

namespace MyApp
{
    [FrmAttribute(typeof(Form1), "One")]
    public partial class Form1 : Form
    {
        private readonly IniManager ini;
        public Form1()
        {
            InitializeComponent();
            ini = new IniManager(@$"{Application.StartupPath}\data.ini");
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            textBox1.Text = ini.ReadIniFile("user1", "text", "");
            LogSet.LogTrace($"User1 Read  | {textBox1.Text}");
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            ini.WriteIniFile("user1", "text", textBox1.Text);
            LogSet.LogTrace($"User1 Write | {textBox1.Text}");
        }
    }
}
