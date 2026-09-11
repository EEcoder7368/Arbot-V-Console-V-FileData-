using System;
using Arbot__V_Console___V_FileData_;

namespace Setup
{
  public class Updater
  {
    public static void update()
    {
      Console.WriteLine("Hi");
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

      File.Delete("Program.cs");
      File.Delete("logo.ico");
      File.Delete("setup.cs");
      File.Delete("LICENSE");
      File.Delete("Arbot (V Console) (V FileData).csproj");
      File.Delete("Arbot (V Console) (V FileData).sln");
    }
  }
}
