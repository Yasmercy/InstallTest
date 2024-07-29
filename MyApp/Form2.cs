using System.Windows.Forms;

namespace MyApp
{
    [FrmAttribute(typeof(Form2), "Two")]
    public partial class Form2 : Form
    {
        private readonly IniManager ini;
        public Form2()
        {
            InitializeComponent();
            ini = new IniManager(@$"{Application.StartupPath}\data.ini");
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            textBox1.Text = ini.ReadIniFile("user2", "text", "");
            LogSet.LogTrace($"User2 Read  | {textBox1.Text}");
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            ini.WriteIniFile("user2", "text", textBox1.Text);
            LogSet.LogTrace($"User2 Write | {textBox1.Text}");
        }
    }
}
