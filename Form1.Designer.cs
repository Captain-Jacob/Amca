using System;
using System.Windows.Forms;

namespace ProgramKontrol
{
    partial class Form1
    {
        private Button btnBaslat;
        private TextBox txtLog;
        private ProgressBar progressBar1;
        private Label lblDurum;
        private CheckedListBox checkedListBox1;

        private void InitializeComponent()
        {
            this.btnBaslat = new Button();
            this.txtLog = new TextBox();
            this.progressBar1 = new ProgressBar();
            this.lblDurum = new Label();
            this.checkedListBox1 = new CheckedListBox();

            // btnBaslat
            this.btnBaslat.Text = "Yetkiyi amcaya ver yeğenim";
            this.btnBaslat.Location = new System.Drawing.Point(20, 20);
            this.btnBaslat.Size = new System.Drawing.Size(200, 40);
            this.btnBaslat.Click += new EventHandler(this.btnBaslat_Click);

            // txtLog
            this.txtLog.Multiline = true;
            this.txtLog.ScrollBars = ScrollBars.Vertical;
            this.txtLog.Location = new System.Drawing.Point(20, 280);
            this.txtLog.Size = new System.Drawing.Size(400, 150);
            this.txtLog.ReadOnly = true;

            // progressBar1
            this.progressBar1.Location = new System.Drawing.Point(20, 440);
            this.progressBar1.Size = new System.Drawing.Size(400, 20);

            // lblDurum
            this.lblDurum.Text = "Hazır";
            this.lblDurum.Location = new System.Drawing.Point(20, 470);
            this.lblDurum.Size = new System.Drawing.Size(400, 20);

            // checkedListBox1
            this.checkedListBox1.Location = new System.Drawing.Point(20, 80);
            this.checkedListBox1.Size = new System.Drawing.Size(400, 180);
            this.checkedListBox1.CheckOnClick = true;

            // Form1
            this.ClientSize = new System.Drawing.Size(450, 510);
            this.Controls.Add(this.btnBaslat);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblDurum);
            this.Controls.Add(this.checkedListBox1);
            this.Text = "Amcanın Tatil Evi";   
            
        }
    }
}
