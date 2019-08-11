using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

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
      public int StartTimestamp;
      public int EndTimestamp;
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
        ClientID = -1
      },
      Information = new Config.information()
      {
        State = "",
        Details = "",
        StartTimestamp = -1,
        EndTimestamp = -1
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
}
