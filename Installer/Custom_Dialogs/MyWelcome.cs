using System;

namespace WixSharp.Custom_Dialogs
{
    public partial class MyWelcome
    {

        //
        // Summary:
        //     Initializes a new instance of the WixSharp.UI.Forms.WelcomeDialog class.
        public MyWelcome()
        {
            InitializeComponent();
        }

        private void WelcomeDialog_Load(object sender, EventArgs e)
        {
            image.Image = WixSharp.Properties.Resources.MavenLogo;
            ResetLayout();
        }

        private void ResetLayout()
        {
            int num = (int)((double)next.Height * 2.3);
            int num2 = num - bottomPanel.Height;
            bottomPanel.Top -= num2;
            bottomPanel.Height = num;
            imgPanel.Height = base.ClientRectangle.Height - bottomPanel.Height;
            if (image.Image != null)
            {
                float num3 = (float)image.Image.Width / (float)image.Image.Height;
                image.Width = (int)((float)image.Height * num3);
            }

            textPanel.Left = image.Right + 5;
            textPanel.Width = bottomPanel.Width - image.Width - 10;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            base.Shell.Cancel();
        }

        private void next_Click(object sender, EventArgs e)
        {
            base.Shell.GoNext();
        }

        private void back_Click(object sender, EventArgs e)
        {
            base.Shell.GoPrev();
        }
    }
}
