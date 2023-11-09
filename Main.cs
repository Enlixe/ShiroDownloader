using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json.Linq;

namespace ShiroDownloader
{
    public partial class Main : Form
    {
        // ============================================================
        //* REQUIRED
        private static readonly string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string pathMc = appData + "\\.minecraft";
        private static readonly string pathMod = pathMc + "\\Shiro";
        public static readonly string tempPath = @".Shiro_Temp";

        // ============================================================
        //? ADD NEW MOD HERE
        private static readonly string modRLCName = "RLCraft";
        private static readonly string modRLCUrl = "https://mediafilez.forgecdn.net/files/4487/650/RLCraft+Server+Pack+1.12.2+-+Release+v2.9.2d.zip";
        private static readonly string modSF4Name = "Skyfactory 4";
        private static readonly string modSF4Url = "https://mediafilez.forgecdn.net/files/3565/687/SkyFactory-4_Server_4_2_4.zip";
        private static readonly string modCCName = "Crazy Craft";
        private static readonly string modCCUrl = "https://mediafilez.forgecdn.net/files/4773/313/CCU+Server+Pack+Powershell+-+0.9.9.zip";

        // ============================================================
        //* INIT
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
            if (!Checking.checkpassed)
            {
                Hide();
                Checking c = new Checking();
                c.Closed += (s, args) => Close();
                c.Show();
                return;
            }

            // ========================================================
            //? ADD NEW MOD INSTALLED CHECK HERE
            InstalledMod(modRLCName, modRLC, modRLCBtn);
            InstalledMod(modSF4Name, modSF4, modSF4Btn);
            InstalledMod(modCCName, modCC, modCCBtn);
        }

        // ============================================================
        //? ADD NEW MOD INSTALL BUTTON ON CLICK HERE
        private async void modRLCBtn_Click(object senders, EventArgs ea) { await StartDownload(modRLCName, modRLCUrl, modRLC, modRLCBtn); }
        private async void modSF4Btn_Click(object sender, EventArgs e) { await StartDownload(modSF4Name, modSF4Url, modSF4, modSF4Btn); }
        private async void modCCBtn_Click(object sender, EventArgs e) { await StartDownload(modCCName, modCCUrl, modCC, modCCBtn); }

        // ============================================================
        //* MOD INSTALLATION
        private async Task StartDownload(string name, string url, Label progress, Button modBtn)
        {
            Logger("Starting Mod Download Task", progress);
            progress.Visible = true;
            modBtn.Visible = false;
            string tempModZip = tempPath + $"\\{name}.zip";
            string modZip = pathMod + $"\\{name}.zip";

            await Download(name, url, tempModZip, progress);
            await MoveFile(tempModZip, modZip);
            await Unzip(name, modZip, progress);

            if (name == "Crazy Craft")
            {
                Logger("Addons - Downloading required files for running this modpacks.", progress);
                string _tempModZip = tempPath + $"\\CTM-MC1.16.1-1.1.2.6.jar";
                string _modZip = pathMod + $"\\{name}\\mods\\CTM-MC1.16.1-1.1.2.6.jar";
                await Download("CTM", "https://mediafilez.forgecdn.net/files/3137/659/CTM-MC1.16.1-1.1.2.6.jar", _tempModZip, progress);
                await MoveFile(_tempModZip, _modZip);
            }

            await Clearing(modZip, progress);
            await Profile(name, progress);

            Logger("Completed Mod Download", progress);
            MessageBox.Show("Installation Done", "Shiro", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task Download(string name, string url, string tempZip, Label progress)
        {
            Logger("Download - Initializing.", progress);

            if (File.Exists(tempZip)) Logger("Download - Temp file existed, continuing.", progress);
            else
            {
                Logger($"Download - Downloading {url} to {tempPath}...", progress);

                WebClient client = new WebClient();
                client.DownloadProgressChanged += (sender, e) =>
                {
                    float downloaded = e.BytesReceived / 1048576f;
                    float total = e.TotalBytesToReceive / 1048576f;
                    Logger($"Download - Downloaded {downloaded:F1} MB / {total:F1} MB", progress);
                };
                await client.DownloadFileTaskAsync(new Uri(url), tempZip);

                Logger("Download - Completed.", progress);
            }
            await Task.Delay(2000);
        }
        private async Task MoveFile(string name, string to)
        {
            name = AppDomain.CurrentDomain.BaseDirectory + name;
            Console.WriteLine($"Move - Moving {name} to {to}");
            if (!File.Exists(to)) File.Move(name, to);
            else { File.Delete(to); File.Move(name, to); }
            await Task.Delay(2000);
        }
        private async Task Unzip(string name, string modFileZip,Label progress)
        {
            string unzipDir = pathMod + $"\\{name}\\";
            Logger("Unzip - Initializing.", progress);

            Console.WriteLine($"Unzipping {modFileZip} to {unzipDir}...");
            if (!Directory.Exists(unzipDir)) Directory.CreateDirectory(unzipDir);
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

            Logger("Unzip - Completed.", progress);
            await Task.Delay(2000);
        }
        private async Task Clearing(string modZip, Label progress)
        {
            Logger("Cleaner - Clearing temp files.", progress);

            if (Directory.Exists(tempPath)) Directory.Delete(tempPath, true);
            if (File.Exists(modZip)) File.Delete(modZip);

            Logger("Cleaner - Completed.", progress);
            await Task.Delay(2000);
        }
        private async Task Profile(string name, Label progress)
        {
            Logger("Profile - Creating new launcher profiles.", progress);

            await Task.Delay(1000);
            JObject launcherProfilesJson = JObject.Parse(File.ReadAllText(pathMc + "\\launcher_profiles.json"));
            JObject newProfile = new JObject(
                new JProperty("created", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")),
                new JProperty("gameDir", pathMc + $"\\Shiro\\{name}"),
                new JProperty("icon", "data:image/png;base64,UklGRnQRAABXRUJQVlA4IGgRAACQPACdASqAAIAAPpE6lUgloyIhM3o9wLASCWQAwYgTldSi+XfGn0d2+/96Uv7du0+eF9M/+P34bopPWI/xNqftXtTP3vmW4x7TfsHnv/ue+v9x8QJ2vaBX7f0Hmh9j/YA/WP1B/2Hho/h/+d7Af9J/w3rF/4nke+qvYQ/YL01/Yx+7PsuftI6btgS9qa7r+GloPKf47SjXT1DfI4rjf+aoAFFWAkT165xE+ea6k29dmLtE0k9Epp4zMCA4eVweDBRZ8YJ7gAJgTxDef7TupfT9g0gUou7zlIcqiOYBVhPfuTDvZIZQmRIV5XKz7qaNTXiVEyWRDnXuzVVolgBII5TN047janeUIuZ6GwmSLBHjuGxiiJcbkeM9SAu9Nxe3/fsxI5cCBE+K7qbN6xXXkolQcBAEh+2Pn5zafG0LPQhOBRnFD669CZLFJBNlqT+pb2MCM9GfFTyS2nG2/a2WHj3NlBRcoV+GenpVhwnVCM6PLXCuKQbqIWflk5lnpWm4juD1YhEdQKYqPV3B7HZJf+eNOHrLluS1JZzfwGG/6gR+xRj8iJ0Jae2JPg3LTULEYv2wgLhQBsnvIbrTmM0cCxceqAeVNG56/j4adX9wT9o+K+jR7nKC69zFf2dpYjY5pEGdMbheyqMdoRwt0gYQAP7/NImkryehUmx5IMS9jqrqp7TM6luLyO1IBHu+RoMJvEPMHiUDkloOt+ROtYeAPetiWtwtgjuftqnW8K6JvssPjY1jHIFX/eo1LdlWzKf26MBQZnO6pj4YDs1AKXi7QReuuDTv6G/vkMLP7+a0/iri/jnnSUwGfAfdM+hlJ2rELs9zvLX2fkoiL2I/LVKtHh2J51KALPWfHO8Zp0lw7C38ubJ5YJoGQuz7RgpNeiQg52HxOalRHHCwxNSmSUzejwXPDwybf5/cXKsP1W5p4Ai4OsYEVwWWvtdTeHhqajY/fo7YAhS8HyGQZMeYkyE7YaD8otUevygaVuq//Jv9TNj93rNOuIWxm9rvmzncfy0xpbie6NCPAYtjru07tDw5AcOo8Ka90nycXKx4AmMiQOsMOEIazFF5vqDPy264j24t4NL2rCmrTAZfTKp2pgaHQPl37PNZ+mf8D0yBu27h4ZqHSmsj1z3pxwP8I1qDxIpWN3pBjsKbt9UW3pXmUVj7f7LFxsqpWRNyGcIOhUDpqmf7PK6rY+2zZbTU37YSU+p9Rs0MGVjxA/CYBbDRCfMbXC6bvYKIlw1ScgQx6i1v+2YK/abvth3tcOnpkAjd9Rc4ed0KJjn2r+7gCwB0tL8D6TjZtA0RKTJvDqV7ML1C3QAFDBvQFQc1Nu4snGN4pD6Ya64jBzv2Bh2QKlF7stCLJRPlmeiB69lov/+/ADgv/doIH7Z7IT+rSwm2zm9Up15x1zZT3ebfTAS40n8qmgCNsWq1vpbwHbCASMA1wVch/LdzQ1uMIu8OrTf1G+Spt0BZEwgFEafhIgbEifV2aGWH6mXc2dK+i3sMfxjZdS511+sJAdm2CjNej+iSkM4UTJzl1wAuaFyLT1dER9NC14MKA/h9zST53hdATqDamOyvJW0lYVTwhq6XOBD24C8YMWKwSReK57u5N1EK+WKqE9fqA6p+d00c9wNlpW1f3DOCDq9Sb9lUWksHUzmSEf7s49NgP4VNIUtfuW8TnclC5K+mary7vXYheMAmu6M+Wj2nA9z41otlmKShuCnquapDr/1ffkj1Ix6i6fmkotBCYQQM4+whu8uTGLw1fZitW13TH9A4HEfmclBoGEWqmzyugPdzhrFbjbcn+xpvqJVt6ZnG8VmWG30FpeGYwHL42qes5nr7Mo8qf5bWxdWQ2NuGdk/Aq3NMTEGKcJ32+hwX0QzGUjAxu3ky5Ml+/1JGI8p/gDGHv8yJiXsyWQ64vjPb8toA7n0kOwemxbQlq4NifsjwFZQIN9kfhjy2PQtLw/8orOo7UBsO4i0bp4Y3XfSN/KGZNBnowbQxpJlIeYKW7DnRjIlRNjUqMoGcYquj8G7RhlFsrE5yFWdqlrr3uP3PEsGGYQLsWmJk9zLD2orFzFD//+ERz/Omub5TeekvzGFQ+nR1QwZk4/D6hqh/BcqdoWFXUREzwf3T0yVLiA0npJTRsELNdvDcBPnxAGHmzvL5N+3V3cmQtzzSpS26owFjy7mnj8FycJX5SgRMLOnmtNVj1LJedFIt4ODsO31kcvB8jzIThjO1sSFI2eEPeS0/lVA1FIajqeE3SESvBPYxcQfk3s/Wg7axWFAdyyGk4S7sBMt3aLk/ltgW7Bbxz4kZ8gMmZkLBmofCyY8sfvg/oVtzk3v6/wTEKM/UGYILtI0eX5P400beXtArSDOjbcc0V/X4HR8irvwLCFiGaeQYUyCDgySb6Kl4i1rZBuGrQOYRDDzxt9ZETBz+dmOMPWr15wChkXbWIk1dk1mNP6VOWogxFESbxnMXLEEMoU/ZZ3chPGGWgHDzWzxGrHQmnjzq7B6/y1eaaVntdjNExjxrRNr3wAHUoLWT+07xcHJtVJyheY+6gv/piW+9s8u1pxvIHDfZmRCEiZLNeemsvfeB5WK2cnkP0vj4Ljg1+uD+XBQ1i81/whhA8vnizkvPMPqWeFgh6XYz2wumy7rJjtL8PUzukhh4GKfQSPNLcn5CKgfpRqVjhVi1R6eulePucuIh2ilAr0Pq0kN07xR3Uz7Jvjsm37u5NwVEASnVE9TcpHS2qDM3kpV82we655fzsJN5FqPizWRzFz0F1pi+4IMvIZ8xYaFsS8+VZ6CLks/BzqezPGJylyz1oN/IJRhAo5bQk2vxgmzoBVv/C3On3OVaPLu5diQqhHXuT3l0rzGj/V/25ajx587qziYSmaY+pDco6xqY5iSB3YUTD6867FAVmf+Tr6/rVLAfdL0aOfY5V0cyOyIFU8xJMuZ9GrefpI4qC4KTr+qi7AMNamwTIS7g7C9nmaipEJUcMOyrl0ztJJem9FfNxOxK/qypb0Jm6M7a90agHCNzyFzC1ROTIoc0KSWPp9TkDnrwcWnJgHD2eOKomojRf5lYbYTe1U6Fp7v/Hl/XcA2gljbdW5/z3cZX7uqvx7vjzwGKo7EXa3GHjk4cmby9amCWS1lk/6E3CVja9zSbmRtf8vF5i1NbWOcEFVgtBBKwNRjh5zgTCgsQLuRn58Qee/JzE3LbwyLK9vNwwhkppLwCwhqOYppHHt+C/q3f7f6oPZZd7KuA0QRlQ8cM08w8dsgv5qEqK7bpre0mxKtWunLtv+A/frML8zeb1tUwlI3NN9jsZ1LdXWHzYVncej6nKDWxsOt4TW9+wbwc8t1Db688ZuuktRL1TmR4/G0UDkfmt75/L/mlXVB0yk64HFkTxT6vRRtZhIEeunt+LHv8GmMDuGDTGjyXTrDqamucdLGNkqwheZDovz9OoFnl9S72lYAkVw26bFn569Kwz4WBE6xRUZB8ymfnaMN1zA5HnzRH7zKAZz7ge3OcavJ+IZkoG4mVdg0dPFrXIgHwUIbckRVkTRC+XHc/vkrqaBsWG6V4ore/iffaaaf6wnVt3HUDZYGzqB9QhyX16JRuU85+5N23KDP4ZreO25wGGnfek0yZkLy/i3xIuPVFtDTaOmWEZ5OrXgAajob1H8pXASCa+7eFFp1zx4xiaounqUDSjxXasa7Un5/S63UZGIdmjAqo+iWSjwUeDkrjb/ZUgPfHOyjEEbFX2IeDiTn04cRrIOA/IWuDEGRc1eQZXzYfPZuDykLwxZtL8d9ZFb8j9Igoi25EvhOZcDmlDktLoNMAkzKcal9+Hv0KXWe53FRcKIn7jNF2iaLvQEUpw2D2YMpIQ06ZinuPmLX3FSu6XtthCzP49CUVfnVS68ROUUENkNzUeFCrR+khpSVAgL4M2pgjQNGDUeHHv4erzJHEUhoTAXTa8jBwFXRTq5EcqtzNe35JDycDgPoa72+lQzRPImioVjnW+ZUFgasyelYNEYA1CUhtPAGN9ZwCf3jYaVwkH6/Z1GvhOx+P2nyJe8Rg5iGpwFYYAsoPnmDnVzprbStZW/1QZeNIdwOP+v7ODVBiaQlKKOA7DzQ2f92YnsL69qFJr718OWqkgczwkwyx7G7X+O9kK473VYrzruELbOCBg576rZ4h7RHmTZpg2B59vQSEZXdp+i0f0Aigf0bOyWTYhYDJOGVMolyu5T6Llmu64bb+kt4V4W19mVg1IyxDKpkufEzJ7JjZ0SuM59qSudvuiPHzewdL/fLSp2Gtk+4iPFPjNHMXV5SoTEQ6BTcQCmm5qrGWEh61CGg6GjxocTAq6sVrku4G3WxfWAy0yAQi0qvHMqeWzj9G0Rx4Gl/CFrkihO2LZK6RQ5KgHXnhkqWZuFh8+8lxchzxyOcdNnPbFPZTwhUzZ2vw/d1237hPV9OQfOJ0T4XvVUrZuLTce4N8y1JZuLZdS3o4fgk8gm2zpNHH1azs8YCXz6/+fzATS86T0wyRwTQ2xiT4gD1OL4Ow7qDnkcXWc37A7tj2xAN+CpmXHqrone2ukSfcaFDZx3tq2XY+sxjofkI8slBL255fe56E9sjEbx5xDWE9LuUc7PGrOtWJm0r1LtVNQwmssvx5OnZNZo2xD76+wtaedTIz+uNzD7/UfU3SwqzwXCVfm302WeOMkF0BP8tFFRh+djsGyPzxDLovBBh2FCNHqW92OGXLayBoq6lgi6PdXbP3MD/L5Z5FFH4wpVCM5TPApjLhqw2p6MMP29PL3QAwOP3iaSAsndeWF5G2udTgDfEE96zgSQbQ3YuAo7ld3pAwpU4UogXcZr7xw0uKeYYuo69Nnlfv95wN4aW8zTeUU+/0w/5SyQ7hXCtqZvaAfYs2pRRg0fxw0UZ+z+UpcfQy32s5fXi3YFckXa2IDfdqcgVxayTFf2HLi0sRoYFzc3hUmYtZmzXgLVC/nj81z3q/Fy9spFKC+zW4QegZQavTjgGcSIuOjSCTmgOOqYw2uyvT8avlbdkNHyFnl3YGb9F03NmKeMdZNoDH3tVGzXHxqZEz85+NASjgw1H6br2644w8dX+H8Ks+T1M25sa22OYdr7IceruSW0Y+oAK3tJ9otpZsGiBr5SKvMezh+FkKvG6qo3ZJprOPDvL6+BolO5zAaAmO2Dk6eYdH/RN/kTIvRyZoPSkwHtt1E+vHnFROgW+v0iqBM5o/ts6oN0ELp6oLYrOVAIdyzRH/Zx0TBQFKO8JR6wujy2ej91eatexaXPP3WgHEN+9SG5pK3tzEo9beVEjFiMx8qx6OUcLw9WOo92uzcDAADlQna464fSVo2Y0l0knpBk9cycJqRzetVopWo8ceheqgt4gvtQ6CDdeq8MfgBlCWTXi+6BeHVcvf05nB1AxS5wJnYOg3et1QGq6Hqw2CmsG4wlfi8/lhV4YMjYBrnAtIOBf+IQGNkpIxaWM4C9JxOso5dMWjQk94W5/QhnOjWj8tm1o42BudpqUduY1EwuDHEb140pij5iQOv2pQ63deVE3j61eOMDN+TIiFsd2lXe0hDYUJ/1hF+V1yH0FjWVz3icQEs5g71r4C2p9VryqWy1gbD3+bYUO7hMgOxp7zP24lKUhLYpc1Ryd0N36ABHpjdJ0z6d0rqVeTQ4nNtXCe6KH1rVHoMxHEcSSIH4MhHAFzGzyoVROE/sBOra2cuizIzncCRq5F3dTYaqRdaskyckzcn7iwCkMnH2FpyvLQJR3MwYKE7SBRjBvSrxM/AR/5Mkh342o0fdumr8LQZ1sHJ0h9MbFmKFUVb2OYhf/F1q/GAiCjJimfuZsrpwyrEeD+eGyHKtPwY6BULLTnre+Pat66iBdVtTjqeqId+Ej3Jt4sP3KNqrw8YDyK2WOq1CI4ckAl4lLyTUbwNnW3ddncjSNNQM0VoNMWLCF4wQkN62QeED6ZxUmQoe0tJ9PPicTFVs+w50kZ14AA"),
                new JProperty("javaArgs", "-Xmx3G -XX:+UnlockExperimentalVMOptions -XX:+UseG1GC -XX:G1NewSizePercent=20 -XX:G1ReservePercent=20 -XX:MaxGCPauseMillis=50 -XX:G1HeapRegionSize=32M"),
                new JProperty("lastUsed", new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToString("yyyy-MM-ddTHH:mm:ss.fffZ")),
                new JProperty("lastVersionId", name == "Crazy Craft" ? "1.16.5-forge-36.2.39" : "1.12.2-forge-14.23.5.2859"),
                new JProperty("name", name),
                new JProperty("type", "custom")
            );
            string newProfileKey = name;
            launcherProfilesJson["profiles"][newProfileKey] = newProfile;
            File.WriteAllText(pathMc + "\\launcher_profiles.json", launcherProfilesJson.ToString());

            Logger("Profile - Completed.", progress);
        }

        // ============================================================
        //* HELPER FUNCTION
        private void InstalledMod(string modName, Label modLabel, Button modBtn)
        {
            if (Directory.Exists(pathMod + "\\" + modName))
            {
                modBtn.Visible = false;
                modLabel.Visible = true;
                modLabel.Text = $"Installed on \"{pathMod + "\\" + modRLCName}\"";
            }
        }
        private void Logger(string message, Label progress)
        {
            Console.WriteLine(message);
            progress.Text = message;
        }
    }
}
