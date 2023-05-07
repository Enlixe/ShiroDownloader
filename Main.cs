using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ShiroDownloader
{
    public partial class Main : Form
    {
        static string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        static string pathMc = appData + "\\.minecraft";
        static string pathMod = appData + "\\.minecraft\\Shiro";
        public static string tempPath = @".Shiro_Temp";

        string modRLCName = "RLCraft";
        string modRLCUrl = "https://mediafilez.forgecdn.net/files/4487/650/RLCraft+Server+Pack+1.12.2+-+Release+v2.9.2d.zip";

        string modSF4Name = "Skyfactory 4";
        string modSF4Url = "https://mediafilez.forgecdn.net/files/3565/687/SkyFactory-4_Server_4_2_4.zip";

        public Main()
        {
            InitializeComponent();
            Shown += Main_Shown;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(pathMod)) Directory.CreateDirectory(pathMod);
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            if (!Checking.checkpassed) {
                this.Hide();
                Checking c = new Checking();
                c.Show();
                return;
            }

            if (Directory.Exists(pathMod + "\\" + modRLCName))
            {
                modRLCBtn.Visible = false;
                modRLC.Visible = true;
                modRLC.Text = $"Installed on \"{pathMod + "\\" + modRLCName}\"";
            }
            if (Directory.Exists(pathMod + "\\" + modSF4Name))
            {
                modSF4Btn.Visible = false;
                modSF4.Visible = true;
                modSF4.Text = $"Installed on \"{pathMod + "\\" + modSF4Name}\"";
            }
        }

        private void modRLCBtn_Click(object senders, EventArgs ea)
        {
            modRLCBtn.Visible = false;
            modRLC.Visible = true;
            StartDownload(modRLCName, modRLCUrl, modRLC);
        }

        private void modSF4Btn_Click(object sender, EventArgs e)
        {
            modSF4Btn.Visible = false;
            modSF4.Visible = true;
            StartDownload(modSF4Name, modSF4Url, modSF4);
        }

        private async Task StartDownload(string name, string url, Label progress)
        {
            string tempModZip = tempPath + $"\\{name}.zip";
            string modZip = pathMod + $"\\{name}.zip";
            await Download(name, url, tempModZip, progress);
            await MoveFile(tempModZip, modZip);
            await Unzip(name, modZip, progress);
            await Clearing(modZip, progress);
            await Profile(name, progress);

            MessageBox.Show("Installation Done", "Shiro", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task Download(string name, string url, string tempZip, Label progress)
        {
            if (File.Exists(tempZip)) Console.WriteLine("File existed, continuing");
            else
            {
                Console.WriteLine("Download init.");
                progress.Text = "Download - Initializing";

                Console.WriteLine($"Downloading {url} to {tempPath}...");
                WebClient client = new WebClient();

                client.DownloadProgressChanged += (sender, e) => {
                    float downloaded = e.BytesReceived / 1048576f;
                    float total = e.TotalBytesToReceive / 1048576f;
                    progress.Text = $"Download - Downloaded {downloaded.ToString("F1")} MB / {total.ToString("F1")} MB";
                };

                await client.DownloadFileTaskAsync(new Uri(url), tempZip);

                Console.WriteLine("Download complete.");
                progress.Text = "Download - Completed.";
            }
        }

        private async Task MoveFile(string name, string to)
        {
            name = AppDomain.CurrentDomain.BaseDirectory + name;
            Console.WriteLine($"Moving {name} to {to}");
            File.Move(name, to);
        }

        private async Task Unzip(string name, string modFileZip,Label progress)
        {
            Console.WriteLine("Unzip init.");
            progress.Text = "Unzip - Initializing";

            string unzipDir = pathMod + $"\\{name}\\";
            if (!Directory.Exists(unzipDir)) Directory.CreateDirectory(unzipDir);
            Console.WriteLine($"Unzipping {modFileZip} to {unzipDir}...");
            using (ZipArchive archive = ZipFile.OpenRead(modFileZip)) {
                int count = 0;
                foreach (ZipArchiveEntry entry in archive.Entries) {
                    if (entry.FullName.EndsWith("/")) {
                        Directory.CreateDirectory(Path.Combine(unzipDir, entry.FullName));
                        continue;
                    }
                    string entryPath = Path.Combine(unzipDir, entry.FullName);
                    if (!Directory.Exists(Path.GetDirectoryName(entryPath))) Directory.CreateDirectory(Path.GetDirectoryName(entryPath));
                    entry.ExtractToFile(entryPath, true);
                    count++;

                    progress.Invoke((MethodInvoker)delegate {
                        progress.Text = $"Unzip - {count} files extracted.";
                    });
                }
            }
            Console.WriteLine("Unzip complete.");
            progress.Text = $"Unzip completed.";
        }

        private async Task Clearing(string modZip, Label progress)
        {
            Console.WriteLine("Clearing temp files.");
            progress.Text = $"Clearing temp files.";
            // if (!Directory.EnumerateFileSystemEntries(tempPath).Any()) Directory.Delete(tempPath);
            Directory.Delete(tempPath, true);
            File.Delete(modZip);
        }

        private async Task Profile(string name, Label progress)
        {
            Console.WriteLine("Creating new launcher profiles.");
            progress.Text = $"Creating new launcher profiles.";
            JObject launcherProfilesJson = JObject.Parse(File.ReadAllText(pathMc + "\\launcher_profiles.json"));
            JObject newProfile = new JObject(
                new JProperty("created", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")),
                new JProperty("gameDir", pathMc + $"\\Shiro\\{name}"),
                new JProperty("icon", "Furnace"),
                new JProperty("javaArgs", "-Xmx3G -XX:+UnlockExperimentalVMOptions -XX:+UseG1GC -XX:G1NewSizePercent=20 -XX:G1ReservePercent=20 -XX:MaxGCPauseMillis=50 -XX:G1HeapRegionSize=32M"),
                new JProperty("lastUsed", new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToString("yyyy-MM-ddTHH:mm:ss.fffZ")),
                new JProperty("lastVersionId", "1.12.2-forge-14.23.5.2859"),
                new JProperty("name", name),
                new JProperty("type", "custom")
            );
            string newProfileKey = name;
            launcherProfilesJson["profiles"][newProfileKey] = newProfile;

            File.WriteAllText(pathMc + "\\launcher_profiles.json", launcherProfilesJson.ToString());
            progress.Text = "Installed.";
        }
    }
}
