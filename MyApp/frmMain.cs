using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace MyApp
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            foreach (var type in FrmAttributeAttribute.GetAllWithAttribute(Assembly.GetExecutingAssembly()))
            {
                if (Global.IniManager.ReadIniFile(type.Name, "Include", "false") != "false")
                {
                    var item = type.GetTypeInfo().GetCustomAttribute<FrmAttributeAttribute>().CreateItem(MenuStrip);
                    item.Click += ToolStripMenuItem_Click;
                    item.Tag = type;
                }
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem obj = (ToolStripMenuItem)sender;

            Form frm = new Form();
            if (FindMdiWindows(obj.Name, ref frm))
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
            }
            else
            {
                var frm2 = CreateForm<Form>(obj.Tag);
                frm2.MdiParent = this;
                frm2.WindowState = FormWindowState.Maximized;
                frm2.Show();
            }
        }

        private bool FindMdiWindows(string fromName, ref Form getform)
        {
            foreach (Form frm in MdiChildren)
            {
                if (frm.Name == fromName)
                {
                    getform = frm;
                    return true;
                }
            }
            return false;
        }

        private T CreateForm<T>(object tag) where T : Form, new()
            => (T)Activator.CreateInstance((Type)tag);
    }

}
