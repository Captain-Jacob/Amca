using System;
using System.Windows.Forms;

namespace ProgramKontrol
{
    public class Loading : Form
    {
        public Loading()
        {
            this.Text = "Bekle yeğenim.";
            this.Size = new System.Drawing.Size(300, 100);
            Label lbl = new Label();
            lbl.Text = "Uygulamayı başlatıyoz yeğen . . . az bekle da .";
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Controls.Add(lbl);
        }
    }
}
