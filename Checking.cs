using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace ShiroDownloader
{
    public partial class Checking : Form
    {
        static string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        static string tempPath = Main.tempPath;

        private int checkPass;
        public static bool checkpassed = false;

        public Checking()
        {
            InitializeComponent();
            Shown += Checking_Shown;
        }

        private void Checking_Load(object sender, EventArgs e)
        {
            
        }

        private void Checking_Shown(object sender, EventArgs e)
        {
            RefreshCheck();
            Continue();
        }

        private void RefreshCheck()
        {
            checkPass = 0;
            // Check Java
            Console.WriteLine("Checking Java Home...");
            CheckJavaBtn.Visible = false;
            string java = CheckJava();
            Console.WriteLine("Java Home directory: " + java != null ? java : "Not Installed");
            if (java != null)
            {
                checkJava.Text = $"Installed \"{java}\"";
                checkPass++;
            }
            else
            {
                checkJava.Text = "Not installed.";
                CheckJavaBtn.Visible = true;
            }

            Thread.Sleep(1000);

            // Check Mc
            Console.WriteLine("Checking Minecraft...");
            CheckMcBtn.Visible = false;
            string mc = CheckMinecraft();
            Console.WriteLine("Minecraft directory: " + mc != null ? mc : "Not Installed");
            if (mc != null)
            {
                checkMc.Text = $"Installed \"{mc}\"";
                checkPass++;
            }
            else
            {
                checkMc.Text = "Not installed.";
                CheckMcBtn.Visible = true;
            }

            Thread.Sleep(1000);

            // Check Forge
            Console.WriteLine("Checking Forge...");
            CheckForgeBtn.Visible = false;
            CheckForgeBtn2.Visible = false;
            string forge = CheckForge();
            string forge2 = CheckForge2();
            Console.WriteLine("Forge directory: " + forge != null ? forge : "Not Installed");
            Console.WriteLine("Forge directory: " + forge2 != null ? forge2 : "Not Installed");
            if (forge != null)
            {
                checkForge.Text = $"Installed \"" + new DirectoryInfo(forge).Name + "\"";
                checkPass++;
                checkForge2.Text = "Not installed.";
                CheckForgeBtn2.Visible = true;
            }
            else if (forge2 != null)
            {
                checkForge2.Text = $"Installed \"" + new DirectoryInfo(forge2).Name + "\"";
                checkPass++;
                checkForge.Text = "Not installed.";
                CheckForgeBtn.Visible = true;
            }   
        }
        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            RefreshBtn.Visible = false;
            RefreshCheck();
            Thread.Sleep(3000);
            Continue();
            RefreshBtn.Enabled = true;
            RefreshBtn.Visible = true;
        }

        private void Continue()
        {
            Console.WriteLine("Checking passed : " + checkPass);
            if (checkPass == 3) {
                ContinueBtn.Enabled = true;
                ContinueBtn.Visible = true;
                checkpassed = true;
                Console.WriteLine("Checking passed, can continue");
            }
            else
            {
                ContinueBtn.Visible = false;
                checkpassed = false;
                Console.WriteLine("Checking isnt passed");
            }
        }
        private void ContinueBtn_Click(object sender, EventArgs e)
        {
            Hide();
            Main m = new Main();
            m.Closed += (s, args) => Close();
            m.Show();
        }

        private static string CheckJava()
        {
            string javaHome = Environment.GetEnvironmentVariable("JAVA_HOME");
            string jrePath = @"C:\Program Files\Java";
            string jrePath86 = @"C:\Program Files (x86)\Java";
            string jdkPath = @"C:\Program Files\Eclipse Adoptium";
            if (javaHome != null) return javaHome;
            else if (Directory.Exists(jdkPath))
            { string[] dir = Directory.GetDirectories(jdkPath); return dir[0]; }
            else if (Directory.Exists(jrePath))
            { string[] dir = Directory.GetDirectories(jrePath); return dir[0]; }
            else if (Directory.Exists(jrePath86))
            { string[] dir = Directory.GetDirectories(jrePath86); return dir[0]; }
            else return null;
        }
        private void CheckJavaBtn_Click(object sender, EventArgs e)
        {
            Process.Start("https://adoptium.net/");
        }

        private static string CheckMinecraft()
        {
            string mcPath = appData + @"\.minecraft";
            if (Directory.Exists(mcPath)) return mcPath;
            else return null;
        }
        private void CheckMcBtn_Click(object sender, EventArgs e)
        {
            Process.Start("https://skmedix.pl/downloads");
        }

        private static string CheckForge()
        {
            string forgePath = appData + @"\.minecraft\versions\1.12.2-forge-14.23.5.2859";
            if (Directory.Exists(forgePath)) return forgePath;
            else return null;
        }
        private static string CheckForge2()
        {
            string forgePath2 = appData + @"\.minecraft\versions\1.16.5-forge-36.2.39";
            if (Directory.Exists(forgePath2)) return forgePath2;
            else return null;
        }
        private void CheckForgeBtn_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Looks like you doesnt have forge 1.12 installed,\nwant to install it now ?", "Shiro", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                WebClient client = new WebClient();
                client.DownloadFileAsync(
                    new Uri("https://maven.minecraftforge.net/net/minecraftforge/forge/1.12.2-14.23.5.2859/forge-1.12.2-14.23.5.2859-installer.jar"),
                    tempPath + "\\Forge.jar"
                );
                client.DownloadFileCompleted += (s, args) =>
                {
                    Thread.Sleep(3000);
                    var resultOp = MessageBox.Show("Forge 1.12.2 Installation, is installed\nWant to open it ?", "Shiro", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (resultOp == DialogResult.OK)
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = tempPath + "\\Forge.jar";
                        Process.Start(startInfo);
                    }
                };
            }
        }

        private void CheckForgeBtn2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Looks like you doesnt have forge 1.16 installed,\nwant to install it now ?", "Shiro", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                WebClient client = new WebClient();
                client.DownloadFileAsync(
                    new Uri("https://maven.minecraftforge.net/net/minecraftforge/forge/1.16.5-36.2.39/forge-1.16.5-36.2.39-installer.jar"),
                    tempPath + "\\Forge.jar"
                );
                client.DownloadFileCompleted += (s, args) =>
                {
                    Thread.Sleep(3000);
                    var resultOp = MessageBox.Show("Forge 1.16.5 Installation, is installed\nWant to open it ?", "Shiro", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (resultOp == DialogResult.OK)
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = tempPath + "\\Forge.jar";
                        Process.Start(startInfo);
                    }
                };
            }
        }
    }
}
