using System;
using System.Collections.Generic;
using System.Diagnostics;              //Selam dost bu kod 12/08/2025 tarafında benim Yakup Nail Ceylan
using System.IO;                         // tarafından yapıldı ve çalışıyordu , daha sonra çalışmazsa
using System.Threading.Tasks;          //allah kurtarsın sorumluluk kabul etmiyom
using System.Windows.Forms;

namespace ProgramKontrol
{
    public partial class Form1 : Form
    {
        private ProgramKontrol kontrol;
        private List<(string Ad, string Yol, string Parametre)> TumProgramlar;

        public Form1()
        {
            InitializeComponent();

            kontrol = new ProgramKontrol(LogEkle, Ilerle, SetDurum);

            // Dosyalar buradan tanımlı
            TumProgramlar = new List<(string, string, string)>
            {
                // ("Chrome", @"\\bgosrvdata\source$\Yeni Bilgisayar Kurulumu\Varsayılan Kurulumlar\Chorome\ChromeSetup.exe", "/silent /nolaunch /norestart /suppressmsgboxes"),
                    //above one is a example
                
            };

            foreach (var (ad, _, _) in TumProgramlar)
            {
                checkedListBox1.Items.Add(ad);
            }
        }

        private void btnBaslat_Click(object sender, EventArgs e)
        {
            btnBaslat.Enabled = false;

            var secilenler = new List<(string, string)>();

            foreach (int index in checkedListBox1.CheckedIndices)
            {
                var (_, yol, param) = TumProgramlar[index];
                secilenler.Add((yol, param));
            }

            if (secilenler.Count == 0)
            {
                MessageBox.Show("Yeğenim bir şey seç de amcan ne kuracağını bilsin", "Sinirli Dayı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnBaslat.Enabled = true;
                return;
            }

            kontrol.Baslat(secilenler);
        }

        // Loglar
        private void LogEkle(string mesaj)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(LogEkle), mesaj);
                return;
            }
            txtLog.AppendText($"{DateTime.Now:HH:mm:ss} | {mesaj}\r\n");
        }

        // Progress barı
        private void Ilerle(int deger)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(Ilerle), deger);
                return;
            }
            progressBar1.Value = Math.Min(progressBar1.Maximum, deger);
        }

        private void SetDurum(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(SetDurum), text);
                return;
            }
            lblDurum.Text = text;
        }
    }

    public class ProgramKontrol
    {
        private readonly Action<string> LogYaz;
        private readonly Action<int> Ilerle;
        private readonly Action<string> DurumYaz;

        public ProgramKontrol(Action<string> log, Action<int> ilerle, Action<string> durum)
        {
            LogYaz = log;
            Ilerle = ilerle;
            DurumYaz = durum;
        }

        public async void Baslat(List<(string Yol, string Parametre)> secilenler)
        {
            int toplam = secilenler.Count;
            int index = 0;

            foreach (var (tamYol, parametre) in secilenler)
            {
                index++;
                var dosyaAdi = Path.GetFileName(tamYol);
                DurumYaz($"Kurulum başlıyor: {dosyaAdi}");

                if (!File.Exists(tamYol))
                {
                    LogYaz($"Dosya bulunamadı: {dosyaAdi}");
                    Ilerle(index * 100 / toplam);
                    continue;
                }

                try
                {
                    var process = new Process();
                    string ext = Path.GetExtension(tamYol).ToLower();

               if (ext == ".msi")
                {
                    process.StartInfo.FileName = "msiexec";
                    string sessiz = string.IsNullOrWhiteSpace(parametre) ? "/quiet /norestart" : parametre;
                    process.StartInfo.Arguments = $"/i \"{tamYol}\" {sessiz}";
                }
                else if (ext == ".bat")
                {
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = $"/c \"{tamYol}\" {parametre}";
                }
                else if (ext == ".exe")
                {
                    process.StartInfo.FileName = tamYol;
                    string sessiz = string.IsNullOrWhiteSpace(parametre) ? "/S /norestart /suppressmsgboxes" : parametre; 
                    process.StartInfo.Arguments = sessiz;
                }

                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;


                    process.Start();

                    await Task.Run(() => process.WaitForExit());

                    if (process.ExitCode == 0)
                        LogYaz($"{dosyaAdi} İkindi kılında program hazır.");
                    else
                        LogYaz($"{dosyaAdi} EEE ben ikindiyi nerede kılacam , olmadı yeğen :{process.ExitCode}");
                }
                catch (Exception ex)
                {
                    LogYaz($"{dosyaAdi} EEE ben ikindiyi nerede kılacam , olmadı yeğen: {ex.Message}");
                }

                Ilerle(index * 100 / toplam);
            }

            DurumYaz("Tüm ikindiler kılındı.");
        }
    }
}
 // dayı nazaran amca daha basit , ikisini birleştirmek isterseneniz kolay gelsin