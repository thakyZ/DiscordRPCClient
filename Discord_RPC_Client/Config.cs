using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using System.Security.Permissions;

namespace Discord_RPC_Client
{
  [Serializable]
  public class Config
  {
    [JsonObject("Information")]
    public struct identifiers
    {
      public string ClientID;
    };

    [JsonObject("Identifiers")]
    public struct information
    {
      public string State;
      public string Details;
      public ulong StartTimestamp;
      public ulong EndTimestamp;
    };

    [JsonObject("Images")]
    public struct images
    {
      public string LargeImage;
      public string LargeImageTooltip;
      public string SmallImage;
      public string SmallImageTooltip;
    };

    public identifiers Identifiers;

    public information Information;

    public images Images;
  }

  public static class ConfigHandler
  {

    public static Config config = new Config()
    {
      Identifiers = new Config.identifiers()
      {
        ClientID = ""
      },
      Information = new Config.information()
      {
        State = "",
        Details = "",
        StartTimestamp = 0,
        EndTimestamp = 0
      },
      Images = new Config.images()
      {
        LargeImage = "",
        LargeImageTooltip = "",
        SmallImage = "",
        SmallImageTooltip = ""
      }
    };

    public static string ConfigFolder => Directory.GetCurrentDirectory() + "/config";
    public static string ConfigFile => ConfigFolder + "/config.json";

    public static void WriteConfig()
    {
      string json = JsonConvert.SerializeObject(config, new JsonSerializerSettings() { Formatting = Formatting.Indented });

      CheckConfigFolder();

      try
      {
        File.WriteAllText(ConfigFile, json);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void ReadConfig()
    {
      CheckConfigFile();

      string json = "";

      try
      {
        json = File.ReadAllText(ConfigFile);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }

      try
      {
        config = JsonConvert.DeserializeObject<Config>(json);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void CheckConfigFile()
    {
      CheckConfigFolder();

      if (! File.Exists(ConfigFile))
      {
        WriteConfig();
      }
    }

    public static void CheckConfigFolder()
    {
      if (! Directory.Exists(ConfigFolder))
      {
        try
        {
          Directory.CreateDirectory(ConfigFolder);
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message);
        }
      }
    }
  }

  public class Watcher
  {
    public static void Initializer()
    {
      Run();
    }

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    private static void Run()
    {
      using (FileSystemWatcher watcher = new FileSystemWatcher())
      {
        watcher.Path = ConfigHandler.ConfigFolder;

        // Watch for changes in LastAccess and LastWrite times, and
        // the renaming of files or directories
        watcher.NotifyFilter = NotifyFilters.LastAccess
                             | NotifyFilters.LastWrite
                             | NotifyFilters.FileName
                             | NotifyFilters.DirectoryName;

        // Only watch text files.
        watcher.Filter = "config.json";

        // Add event handlers.
        watcher.Changed += OnChanged;
        watcher.Deleted += OnMissing;
        watcher.Renamed += OnMissing;

        watcher.Error += (sender, e) =>
        {
          Console.WriteLine("FileSystemWatcher ran into an error with {0}", ConfigHandler.ConfigFile);
          Console.WriteLine(e.GetException().Message);
        };
      }
    }

    private static void OnChanged(object source, FileSystemEventArgs e)
    {
      ConfigHandler.ReadConfig();
    }

    private static void OnMissing(object source, EventArgs e)
    {
      ConfigHandler.WriteConfig();
    }
  }
}
