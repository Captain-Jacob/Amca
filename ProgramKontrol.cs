using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ProgramKontrol
{
    public class ProgramKontrol2
    {
        private readonly Action<string> LogYaz;
        private readonly Action<int> Ilerle;
        private readonly Action<string> DurumYaz;

        public ProgramKontrol2(Action<string> log, Action<int> ilerle, Action<string> durum)
        {
            LogYaz = log;
            Ilerle = ilerle;
            DurumYaz = durum;
        }

        public async void Baslat(List<(string TamYol, string Parametre)> kurulumlar)
        {
            int toplam = kurulumlar.Count;
            int index = 0;

            foreach (var (tamYol, parametre) in kurulumlar)
            {
                var dosyaAdi = Path.GetFileName(tamYol);
                index++;
                DurumYaz($"Yeğenim ha şunu kuruyom: {dosyaAdi}");

                if (!File.Exists(tamYol))
                {
                    LogYaz($"Yeğenim şu : {dosyaAdi} bulamadım bilgine.");
                    continue;
                }

                try
                {
                    var process = new Process();
                    process.StartInfo.FileName = tamYol;
                    process.StartInfo.Arguments = parametre;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();
                    await Task.Run(() => process.WaitForExit());

                    if (process.ExitCode == 0)
                        LogYaz($"{dosyaAdi}'yi kurduk yeğen ");
                    else
                        LogYaz($"La olmadı yeğen ({process.ExitCode}): {dosyaAdi}");
                }
                catch (Exception ex)
                {
                    LogYaz($"Başaramadım affet beni yeğenim: {ex.Message}");
                }

                Ilerle(index * 100 / toplam);
            }

            DurumYaz("Alayını kurdum yeğen dayıya sor inanmıyorsan.");
        }
    }
}
