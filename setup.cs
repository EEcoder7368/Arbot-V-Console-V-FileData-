using Arbot__V_Console___V_FileData_;
using System;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Setup
{
    public class Updater
    {
        private class downloading : Exception
        {
            public string ex_message = "";
            public downloading(bool display_line_num = true, [CallerLineNumber] int line_number = -1) 
            { 
                if(display_line_num)
                {
                    ex_message += $"Something went wrong at line {line_number}";
                    return;
                }
                ex_message += "Something went wrong";
            }
            public downloading(string message, bool display_line_num = true, [CallerLineNumber] int line_number = -1) : base(message) 
            {
                if(display_line_num)
                {
                    ex_message += (message + " at line " + line_number.ToString());
                    return;
                }
                ex_message += message;
            }

            public override string ToString() { return ex_message; }
        }
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
        /*public static async void update()
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
                    //response.EnsureSuccessStatusCode();
                    string releaseJson = await response.Content.ReadAsStringAsync();
                    
                    // Parse JSON to find the app.zip asset URL
                    // You'll need a JSON library like Newtonsoft.Json (Nuget: Newtonsoft.Json)
                    // Or use System.Text.Json if on .NET 3.1+
                    //dynamic release = JsonConvert.DeserializeObject(releaseJson);
                    dynamic release = JsonSerializer.Deserialize<object>(releaseJson);
                    
                    // Find the app.zip asset
                    string downloadUrl = null;
                    string app_name = "";
                    #if __IOS__
                        app_name = "Arbot (V Console) (V FileData)_ios";
                    #endif
                    #if __DESKTOP__
                        app_name = "Arbot (V Console) (V FileData)";
                    #endif
                    foreach(var asset in release.ArrayEnumerator )
                    {
                        if(asset.name == app_name)
                        {
                            downloadUrl = asset.browser_download_url;
                            break;
                        }
                    }
                    
                    if(downloadUrl == null)
                    {
                        throw new IndexOutOfRangeException("Lastest github executible not found.");
                        return;
                    }
                    
                    // Download the binary file
                    HttpResponseMessage fileResponse = await client.GetAsync(downloadUrl);
                    fileResponse.EnsureSuccessStatusCode();
                    byte[] fileBytes = await fileResponse.Content.ReadAsByteArrayAsync();

                    #if __DESKTOP__
                        bool is_same = (fileBytes == File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Arbot (V Console) (V FileData).exe")));
                        if(!is_same) { File.WriteAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Arbot (V Console) (V FileData).exe"), fileBytes); }
                    #endif
                    #if __IOS__
                        bool is_same = (fileBytes == File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Arbot (V Console) (V FileData)_ios.exe")));
                        if(!is_same) { File.WriteAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Arbot (V Console) (V FileData)_ios.exe"), fileBytes); }
                    #endif
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
            #if IOS
                string file_to_download = "Arbot (V Console) (V FileData)_ios.exe";
            #else
                string file_to_download = "Arbot (V Console) (V FileData).exe";
            #endif

            try
            {
                string latest_release_url = "https://api.github.com/repos/EEcoder7368/Arbot-V-Console-V-FileData-/releases/latest";
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");

                HttpResponseMessage response = await client.GetAsync(latest_release_url);
                if(response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new downloading("Error 404"); 
                } else {
                    response.EnsureSuccessStatusCode();
                }
                string response_body = await response.Content.ReadAsStringAsync();

                using JsonDocument doc = JsonDocument.Parse(response_body);
                JsonElement root = doc.RootElement;
                string download_url = "";

                if (root.TryGetProperty("assets", out JsonElement assets))
                {
                    foreach (JsonElement asset in assets.EnumerateArray())
                    {
                        if (asset.TryGetProperty("name", out JsonElement name) && name.GetString().EndsWith(file_to_download, StringComparison.OrdinalIgnoreCase))
                        {
                            download_url = asset.GetProperty("browser_download_url").GetString();
                            break;
                        }
                    }
                }

                if (string.IsNullOrEmpty(download_url))
                {
                    throw new downloading("Lastest github executible not found.", false);
                }

                HttpResponseMessage downloadResponse = await client.GetAsync(download_url);
                try
                {
                    downloadResponse.EnsureSuccessStatusCode();
                } catch(Exception e) {
                    throw new downloading();
                }
                
                byte[] file_bytes = await downloadResponse.Content.ReadAsByteArrayAsync();

                #if IOS
                    bool is_same = (file_bytes == File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Arbot (V Console) (V FileData)_ios.exe")));
                    if(!is_same) { File.WriteAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Arbot (V Console) (V FileData)_ios.exe"), file_bytes); }
                #else
                    bool is_same = (file_bytes == File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Arbot (V Console) (V FileData).exe")));
                    if(!is_same) { File.WriteAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Arbot (V Console) (V FileData).exe"), file_bytes); }
                #endif
            }  catch(Exception e) {
                Window.write("\nPlease screenshot this screen and send it to \nhyper.games.company@gmail.com");
                Window.write(e.ToString());
                Window.write("\nPress any key to exit...\n");
                Window.read_key();
                Environment.Exit(1);
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
