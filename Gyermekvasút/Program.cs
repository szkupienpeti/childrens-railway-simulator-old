using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Microsoft.Win32;
using Gyermekvasút.DEBUG;

namespace Gyermekvasút
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            string[] argums = new string[Environment.GetCommandLineArgs().Length - 1];

            for (int i = 1; i < Environment.GetCommandLineArgs().Length; i++)
            {
                argums[i - 1] = Environment.GetCommandLineArgs()[i];
            }

            if (argums.Length != 0 && argums[0].Length >= 9 && argums[0].Substring(0,9) == "HOSTCLOSE")
            {
                Application.Run(new HostClose(argums[0]));
                return;
            }

            Application.Run(new StartScreen(argums));
        }

        public static bool Crash { get; set; }

        public static string Menetrend { get; set; }
        public static string[] MenetrendKeszitok { get; set; }
        public static DateTime Ido { get; set; }
        
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            RegistryKey rkey = Registry.CurrentUser.OpenSubKey("Software\\Gyermekvasút", true);
            string executingDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            DirectoryInfo dirinf = new DirectoryInfo(Path.Combine(executingDirectory, "ErrorLog"));
            dirinf.Create();
            using (StreamWriter sw = new StreamWriter(Path.Combine(dirinf.FullName, "ErrorLog_" + DateTime.Now.ToString().Replace(".", "").Replace(":", "").Replace(" ", "_") + ".log")))
            {
                sw.Write(e.Exception.ToString());
                sw.Write("\r\n\r\nGYVSZIM Runtime Log tartalma:\r\n");
                sw.Write(Log.LogText);
                sw.WriteLine("Vége a futásidőben naplózott adatok kivonatának");
                string errorLog = Path.Combine(dirinf.FullName, "ErrorLog_" + DateTime.Now.ToString().Replace(".", "").Replace(":", "").Replace(" ", "_") + ".log");
                rkey.SetValue("LastRunGenErrorLog", errorLog);
            }            
            rkey.SetValue("LastRunCrash", e.Exception.GetType().ToString() + " *** StackTrace: " + e.Exception.StackTrace + " *** TargetSite: " + e.Exception.TargetSite);
            
            #region Screenshot
            //Create a new bitmap.
            var bmpScreenshot = new System.Drawing.Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                           Screen.PrimaryScreen.Bounds.Height,
                                           System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap.
            var gfxScreenshot = System.Drawing.Graphics.FromImage(bmpScreenshot);

            // Take the screenshot from the upper left corner to the right bottom corner.
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                        Screen.PrimaryScreen.Bounds.Y,
                                        0,
                                        0,
                                        Screen.PrimaryScreen.Bounds.Size,
                                        System.Drawing.CopyPixelOperation.SourceCopy);

            // Save the screenshot to the specified path that the user has chosen.
            bmpScreenshot.Save(Path.Combine(dirinf.FullName, "ErrorLog_" + DateTime.Now.ToString().Replace(".", "").Replace(":", "").Replace(" ", "_") + ".png"), System.Drawing.Imaging.ImageFormat.Png);
            #endregion

            
            MessageBox.Show("A program belső hibát (nem kezelt kivételt) észlelt, ezért leáll.\n\nÍrtunk egy ErrorLog-ot, amely tartalmazza a hiba adatait. Ha ez eljut hozánk, az segít, hogy ki tudjuk javítani a hibát, és ezzel egy szebb hellyé tegyük a világot.\nKérünk, értesíts minket, hogy ez történt. Köszi :)\n\nHa még szeretnéd használni a Szimulátort, akkor indítsd el újra. Ha hibaüzenettel kilép, kövesd az utasításokat – ha vannak. Ha következetesen nem sikerül, indítsd újra a gépet!\n\nFIGYELJ! Ha hálózaton futtatod a programot, és a többi gép nem szállt el, akkor miután újra elindítottad a programot ezen a gépen, nyomj CTRL+Q-t!\n\nBéke.", "Összeomlás", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (MessageBox.Show("A belső hibát követően a program működése bizonytalan.\n\nBezárjuk a programot (ajánlott)?", "Mi tévők legyünk?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Crash = true;
            }
            if (Crash)
            {
                if (Index.IndInstance != null)
                {
                    Index.IndInstance.Close();
                }
                else
                {
                    MessageBox.Show("Még bezárni sem tudjuk a programot :(\nLődd ki feladatkezelőből!", "Na, ez a ciki");
                }
            }
        }

        //static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        //{
        //    string executingDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        //    DirectoryInfo dirinf = new DirectoryInfo(Path.Combine(executingDirectory, "ErrorLog"));
        //    dirinf.Create();
        //    using (StreamWriter sw = new StreamWriter(Path.Combine(dirinf.FullName, "ErrorLog_" + DateTime.Now.ToString().Replace('.', '_') + ".log")))
        //    {
        //        sw.Write(e.ExceptionObject.ToString());
        //    }

        //    MessageBox.Show("A program belső hibát (nem kezelt kivételt) észlelt, ezért leáll.\nÉrtesíts valakit a problémáról!", ",Összeomlás", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    Application.Exit();
        //}
    }
}
