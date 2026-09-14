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
        } catch(HttpRequestExeption e) {
          Window.write("Please screenshot this screen and send it to hyper.games.company@gmail.com");
          Window.write(e.ToString());
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

      File.Delete(Lessons.Form);
      File.Delete(Lessons.Mon_p1);
      File.Delete(Lessons.Mon_p2);
      File.Delete(Lessons.Mon_p3);
      File.Delete(Lessons.Mon_p4);
      File.Delete(Lessons.Mon_lunch);
      File.Delete(Lessons.Mon_p5);
      File.Delete(Lessons.Mon_p6);

      File.Delete(Lessons.Tue_p1);
      File.Delete(Lessons.Tue_p2);
      File.Delete(Lessons.Tue_p3);
      File.Delete(Lessons.Tue_p4);
      File.Delete(Lessons.Tue_lunch);
      File.Delete(Lessons.Tue_p5);
      File.Delete(Lessons.Tue_p6);
      
      File.Delete(Lessons.Wed_p1);
      File.Delete(Lessons.Wed_p2);
      File.Delete(Lessons.Wed_p3);
      File.Delete(Lessons.Wed_p4);
      File.Delete(Lessons.Wed_lunch);
      File.Delete(Lessons.Wed_p5);
      File.Delete(Lessons.Wed_p6);

      File.Delete(Lessons.Thu_p1);
      File.Delete(Lessons.Thu_p2);
      File.Delete(Lessons.Thu_p3);
      File.Delete(Lessons.Thu_p4);
      File.Delete(Lessons.Thu_lunch);
      File.Delete(Lessons.Thu_p5);
      File.Delete(Lessons.Thu_p6);

      File.Delete(Lessons.Fri_p1);
      File.Delete(Lessons.Fri_p2);
      File.Delete(Lessons.Fri_p3);
      File.Delete(Lessons.Fri_p4);
      File.Delete(Lessons.Fri_lunch);
      File.Delete(Lessons.Fri_p5);
      File.Delete(Lessons.Fri_p6);
      
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
