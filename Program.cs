using System;
using System.Windows.Forms;

namespace ProgramKontrol
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            
            Loading loadingForm = new Loading();
            loadingForm.Show();
            System.Threading.Thread.Sleep(2000); 
            loadingForm.Close();

            Application.Run(new Form1());
        }
    }
}
