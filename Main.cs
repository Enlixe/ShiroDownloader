using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace ShiroDownloader
{
    public partial class Main : Form
    {
        static string pathAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        static string pathMinecraft = pathAppData + "\\.minecraft";
        static string pathTlauncher = pathAppData + "\\.tlauncher\\legacy\\Minecraft\\game";
        string pathMod;

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
            }
            else if (Directory.Exists(pathTlauncher)) {
                textStatus.Text = "Cracked Minecraft Installed ( .tlauncher )";
                pathMod = pathTlauncher;
            }
            else {
                textStatus.Text = "No Minecraft Installation Detected";
                btnMcDownload.Enabled = true;
                btnMcDownload.Visible = true;
            }
            pathMod += "\\Shiro";
            if (!Directory.Exists(pathMod)) Directory.CreateDirectory(pathMod);
        }

        private void btnMcDownload_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://tlaun.ch/repo/downloads/TL_Installer_legacy.exe");
        }

        private void mod1Button_Click(object senders, EventArgs ea)
        {
            string name = "RLCraft";
            string url = "https://mediafilez.forgecdn.net/files/4487/650/RLCraft+Server+Pack+1.12.2+-+Release+v2.9.2d.zip";
            string tempPath = @".Shiro_Temp";
            string tempFile = tempPath + $"\\{name}.zip";

            mod1Button.Enabled = false;
            mod1Progress.Visible = true;
            mod1Progress.Text = "Download - Initializing";
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);

            WebClient client = new WebClient();
            client.DownloadProgressChanged += (sender, e) =>
            {
                float downloaded = e.BytesReceived / 1048576f;
                float total = e.TotalBytesToReceive / 1048576f;
                mod1Progress.Invoke((MethodInvoker) delegate {
                    mod1Progress.Text = $"Download - Downloaded {downloaded.ToString("F1")} MB / {total.ToString("F1")} MB";
                });
            };

            Console.WriteLine($"Downloading {url} to {tempPath}...");
            client.DownloadFileAsync(new Uri(url), tempFile);
            client.DownloadFileCompleted += (s, args) =>
            {
                Console.WriteLine("Download complete.");
                mod1Progress.Text = "Download - Completed.";
                Unzip(tempFile, name);
            };
        }

        private void Unzip(string temp_file, string mod_name)
        {
            string modFileZip = pathMod + $"\\{mod_name}.zip";
            string modExtract = pathMod + $"\\{mod_name}";

            mod1Progress.Visible = true;
            mod1Progress.Text = "Unzip - Initializing";
            if (!Directory.Exists(modExtract)) Directory.CreateDirectory(modExtract);
            if (!File.Exists(modFileZip)) if (File.Exists(temp_file)) { File.Move(temp_file, modFileZip); } else;
            else {
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
                            mod1Progress.Text = $"Unzip - Completed, {count} files extracted";
                        });
                    }
                }
            }
        }

    }
}
