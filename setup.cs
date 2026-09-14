using System;
using System.Net.Http;
using Arbot__V_Console___V_FileData_;

namespace Setup
{
  public class Updater
  {
    public static void update()
    {
      string url = "https://raw.githubusercontent.com/EEcoder7368/Arbot-V-Console-V-FileData-/refs/heads/no_web_app/Program.cs?scrlybrkr=34243470";
      using(HttpClient client = new HttpClient())
      {
        try
        {
          HttpResponseMessage response = client.Get(url);
          response.EnsureSuccessStatusCode();
          string fileContent = response.Content.ReadAsString();
          bool is_same = (fileContent == File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Program.cs"));

          if(!is_same) { File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Program.cs", fileContent)); }
        } catch(HttpRequestExeption e) {
          Window.write("Please screenshot this screen and send it to hyper.games.company@gmail.com");
          Window.write(e.ToString());
          Window.write("\nPress any key to exit...");
          Window.read_key();
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

      File.Delete(Lessons.form_path);
      File.Delete(Lessons.monday_path);
      File.Delete(Lessons.tuesday_path);
      File.Delete(Lessons.wednesday_path);
      File.Delete(Lessons.thursday_path);
      File.Delete(Lessons.friday_path);
      
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
