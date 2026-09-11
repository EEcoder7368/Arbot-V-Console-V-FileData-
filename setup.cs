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
    }
  }
}
