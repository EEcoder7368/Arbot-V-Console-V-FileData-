using System;
using System.Net.Http;
using System.Text.Json;
using Arbot__V_Console___V_FileData_;

namespace Setup
{
    public class Updater
    {
        /*public static async void update()
        {
            //string url = "https://raw.githubusercontent.com/EEcoder7368/Arbot-V-Console-V-FileData-/refs/heads/no_web_app/Program.cs";
            string url = "https://raw.githubusercontent.com/EEcoder7368/Arbot-V-Console-V-FileData-/refs/heads/no_web_app/releases/latest/app.zip";
            using(HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string fileContent = await response.Content.ReadAsStringAsync();
                    bool is_same = (fileContent == File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Program.cs")));

                    if(!is_same) { File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Program.cs"), fileContent); }
                } catch(HttpRequestException e) {
                    Window.write("Please screenshot this screen and send it to hyper.games.company@gmail.com");
                    Window.write(e.ToString());
                    Window.write("\nPress any key to exit...");
                    Window.read_key();
                    Environment.Exit(1);
                }
            }
        }*/
        public static async void update()
        {
            string apiUrl = "https://api.github.com/repos/EEcoder7368/Arbot-V-Console-V-FileData-/releases/latest";
    
            using(HttpClient client = new HttpClient())
            {
                // GitHub API requires a User-Agent header
                client.DefaultRequestHeaders.Add("User-Agent", "Arbot-Updater");
                
                try
                {
                    // Get release metadata
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string releaseJson = await response.Content.ReadAsStringAsync();
                    
                    // Parse JSON to find the app.zip asset URL
                    // You'll need a JSON library like Newtonsoft.Json (Nuget: Newtonsoft.Json)
                    // Or use System.Text.Json if on .NET 3.1+
                    dynamic release = JsonConvert.DeserializeObject(releaseJson);
                    
                    // Find the app.zip asset
                    string downloadUrl = null;
                    foreach(var asset in release.assets)
                    {
                        if(asset.name == "app.zip")
                        {
                            downloadUrl = asset.browser_download_url;
                            break;
                        }
                    }
                    
                    if(downloadUrl == null)
                    {
                        Window.write("app.zip not found in latest release");
                        throw new IndexOutOfRangeException;
                        return;
                    }
                    
                    // Download the binary file
                    HttpResponseMessage fileResponse = await client.GetAsync(downloadUrl);
                    fileResponse.EnsureSuccessStatusCode();
                    byte[] fileBytes = await fileResponse.Content.ReadAsByteArrayAsync();

                    bool is_same = (fileBytes == File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Arbot (V Console) (V FileData).exe")));
                    if(!is_same) { File.WriteAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Arbot (V Console) (V FileData).exe"), fileBytes); }
                } catch(HttpRequestException e) {
                    Window.write("Please screenshot this screen and send it to hyper.games.company@gmail.com");
                    Window.write(e.ToString());
                    Window.write("\nPress any key to exit...");
                    Window.read_key();
                    Environment.Exit(1);
                }
            }
        }
    }

    public class Uninstaller
    {
        public static void uninstall()
        {
            File.Delete(Info.password_path);
            File.Delete(Info.name_path);
            File.Delete(Info.positives_path);
            File.Delete(Info.negatives_path);
            File.Delete(Info.password_reset_path);

            File.Delete(Timetable.form_path);
            File.Delete(Timetable.monday_path);
            File.Delete(Timetable.tuesday_path);
            File.Delete(Timetable.wednesday_path);
            File.Delete(Timetable.thursday_path);
            File.Delete(Timetable.friday_path);
      
            File.Delete(Settings.Settings_path);

            File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Program.cs"));
            File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo.ico"));
            File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "setup.cs"));
            File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LICENSE"));
            File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Arbot (V Console) (V FileData).csproj"));
            File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Arbot (V Console) (V FileData).sln"));
        }
    }
}
