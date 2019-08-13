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
    public class identifiers
    {
      [JsonProperty(Order = 1, PropertyName = "Client_ID")]
      public string ClientID = "";
    };

    public class information
    {
      [JsonProperty(Order = 1, PropertyName = "Details")]
      public string Details = "";
      [JsonProperty(Order = 2, PropertyName = "State")]
      public string State = "";
      [JsonProperty(Order = 3, PropertyName = "Start_Timestamp")]
      public ulong StartTimestamp = 0;
      [JsonProperty(Order = 4, PropertyName = "End_Timestamp")]
      public ulong EndTimestamp = 0;
    };

    public class images
    {
      [JsonProperty(Order = 1, PropertyName = "Large_Image")]
      public string LargeImage = "";
      [JsonProperty(Order = 2, PropertyName = "Large_Image_Tooltip")]
      public string LargeImageTooltip = "";
      [JsonProperty(Order = 3, PropertyName = "Small_Image")]
      public string SmallImage = "";
      [JsonProperty(Order = 4, PropertyName = "Small_Image_Tooltip")]
      public string SmallImageTooltip = "";
    };

    [JsonProperty(Order = 1, PropertyName = "Identifiers")]
    public identifiers Identifiers = new identifiers();

    [JsonProperty(Order = 2, PropertyName = "Information")]
    public information Information = new information();

    [JsonProperty(Order = 3, PropertyName = "Images")]
    public images Images = new images();
  }

  public static class ConfigHandler
  {
    public static Config config = new Config();
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

    public static void SetValue(ref string configVariable, string value)
    {
      configVariable = value;
    }

    public static void SetValue(ref ulong configVariable, ulong value)
    {
      configVariable = value;
    }
  }

  public class Watcher
  {
    public static void Initializer() => Run();

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
