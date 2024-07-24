using System.Windows.Forms;

namespace MyApp
{
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
            textBox1.Text = ini.ReadIniFile("user", "text", "");
            LogSet.LogTrace($"Read  | {textBox1.Text}");
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            ini.WriteIniFile("user", "text", textBox1.Text);
            LogSet.LogTrace($"Write | {textBox1.Text}");
        }
    }
}
