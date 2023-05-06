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
using System.Windows.Forms;
using System.Xml.Linq;

namespace ShiroDownloader
{
    public partial class Main : Form
    {
        static string pathAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        static string pathMinecraft = pathAppData + "\\.minecraft";
        static string pathTlauncher = pathAppData + "\\.tlauncher\\legacy\\Minecraft\\game";
        string pathMod;
        static string tempPath = @".Shiro_Temp";

        bool premium;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            textStatus.Text = "Checking...";
            if (Directory.Exists(pathMinecraft)) {
                textStatus.Text = "Official Minecraft Installed ( .minecraft )";
                pathMod = pathMinecraft;
                pathMod += "\\Shiro";
                premium = true;
            }
            else if (Directory.Exists(pathTlauncher)) {
                textStatus.Text = "Cracked Minecraft Installed ( .tlauncher )";
                pathMod = pathTlauncher;
                pathMod += "\\mods\\1.12.2";
                premium = false;
            }
            else {
                textStatus.Text = "No Minecraft Installation Detected";
                btnMcDownload.Enabled = true;
                btnMcDownload.Visible = true;

                mod1Button.Enabled = false;
            }
            if (!Directory.Exists(pathMod)) Directory.CreateDirectory(pathMod);
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);

            if (premium)
                if (!Directory.Exists(pathMinecraft + "\\versions\\1.12.2-forge-14.23.5.2859"))
                {
                    var result = MessageBox.Show("Looks like you doesnt have forge 1.12.2 installed,\nwant to install it now ?", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        WebClient client = new WebClient();
                        client.DownloadFileAsync(
                            new Uri("https://maven.minecraftforge.net/net/minecraftforge/forge/1.12.2-14.23.5.2859/forge-1.12.2-14.23.5.2859-installer.jar"),
                            tempPath + "\\Forge.jar"
                        );
                        client.DownloadFileCompleted += (s, args) =>
                        {
                            ProcessStartInfo startInfo = new ProcessStartInfo();
                            startInfo.FileName = tempPath + "\\Forge.jar";
                            Process.Start(startInfo);
                        };
                    }
                }
        }

        private void btnMcDownload_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://tlaun.ch/repo/downloads/TL_Installer_legacy.exe");
        }

        private void mod1Button_Click(object senders, EventArgs ea)
        {
            string name = "RLCraft";
            string url = "https://mediafilez.forgecdn.net/files/4487/650/RLCraft+Server+Pack+1.12.2+-+Release+v2.9.2d.zip";

            DownloadModpack(name, url);
        }

        private void DownloadModpack(string name, string url)
        {
            string tempFile = tempPath + $"\\{name}.zip";

            Console.WriteLine("Download init.");
            mod1Progress.Visible = true;
            mod1Progress.Text = "Download - Initializing";
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);

            WebClient client = new WebClient();
            client.DownloadProgressChanged += (sender, e) =>
            {
                float downloaded = e.BytesReceived / 1048576f;
                float total = e.TotalBytesToReceive / 1048576f;
                mod1Progress.Invoke((MethodInvoker)delegate {
                    mod1Progress.Text = $"Download - Downloaded {downloaded.ToString("F1")} MB / {total.ToString("F1")} MB";
                });
            };

            Console.WriteLine($"Downloading {url} to {tempPath}...");
            client.DownloadFileAsync(new Uri(url), tempFile);
            client.DownloadFileCompleted += (s, args) =>
            {
                Console.WriteLine("Download complete.");
                mod1Progress.Text = "Download - Completed.";
                Thread.Sleep(1000);
                Unzip(tempFile, name);
            };
        }

        private void Unzip(string temp_file, string mod_name)
        {
            string modFileZip = pathMod + $"\\{mod_name}.zip";
            string modExtract = pathMod;
            if (premium) modExtract += $"\\{mod_name}";

            Console.WriteLine("Unzip init.");
            mod1Progress.Visible = true;
            mod1Progress.Text = "Unzip - Initializing";
            Thread.Sleep(1000);
            if (!Directory.Exists(modExtract)) Directory.CreateDirectory(modExtract);
            if (!File.Exists(modFileZip)) if (File.Exists(temp_file)) File.Move(temp_file, modFileZip);
            if (File.Exists(modFileZip)) {
                using (ZipArchive archive = ZipFile.OpenRead(modFileZip))
                {
                    int count = 0;
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.FullName.EndsWith("/")) {
                            Directory.CreateDirectory(Path.Combine(modExtract, entry.FullName));
                            continue;
                        }
                        string entryPath = Path.Combine(modExtract, entry.FullName);
                        if (!Directory.Exists(Path.GetDirectoryName(entryPath))) Directory.CreateDirectory(Path.GetDirectoryName(entryPath));
                        entry.ExtractToFile(entryPath, true);

                        count++;
                        mod1Progress.Invoke((MethodInvoker) delegate {
                            Console.WriteLine("Download complete.");
                            mod1Progress.Text = $"Unzip - Completed, {count} files extracted";
                            Clearing(modFileZip);
                        });
                    }
                }
            }
        }

        private void Clearing(string zipFile)
        {
            Console.WriteLine("Clearing temp files.");
            if (!Directory.EnumerateFileSystemEntries(tempPath).Any()) Directory.Delete(tempPath);
            if (File.Exists(zipFile)) File.Delete(zipFile);

            if (premium) Profile();
            else MessageBox.Show("Installation Done", "Shiro", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Profile()
        {
            JObject launcherProfilesJson = JObject.Parse(File.ReadAllText(pathMinecraft + "\\launcher_profiles.json"));
            JObject newProfile = new JObject(
                new JProperty("created", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")),
                new JProperty("gameDir", pathMinecraft + "\\Shiro\\RLCraft"),
                new JProperty("icon", "Furnace"),
                new JProperty("javaArgs", "-Xmx3G -XX:+UnlockExperimentalVMOptions -XX:+UseG1GC -XX:G1NewSizePercent=20 -XX:G1ReservePercent=20 -XX:MaxGCPauseMillis=50 -XX:G1HeapRegionSize=32M"),
                new JProperty("lastUsed", new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToString("yyyy-MM-ddTHH:mm:ss.fffZ")),
                new JProperty("lastVersionId", "1.12.2-forge-14.23.5.2859"),
                new JProperty("name", "RL Craft"),
                new JProperty("type", "custom")
            );
            string newProfileKey = "RLCraft";
            launcherProfilesJson["profiles"][newProfileKey] = newProfile;

            File.WriteAllText(pathMinecraft + "\\launcher_profiles.json", launcherProfilesJson.ToString());
        }
    }
}
