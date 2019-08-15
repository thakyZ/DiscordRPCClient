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
  /// <summary>
  /// The <see cref="Serializable"/> config class.
  /// </summary>
  [Serializable]
  public class Config
  {
    #region Identifiers
    #region Content
    /// <summary>
    /// The class for the <see cref="RPC"/> client identifiers group.
    /// </summary>
    public class Identifiers
    {
      /// <summary>
      /// The <see cref="DiscordRPC.DiscordRpcClient.ApplicationID"/> value.
      /// </summary>
      [JsonProperty(Order = 1, PropertyName = "Client_ID")]
      public string ClientID = "";
    };
    #endregion
    #region Variables
    /// <summary>
    /// The variable for the <see cref="RPC"/> client identifiers group.
    /// </summary>
    [JsonProperty(Order = 1, PropertyName = "Identifiers")]
    private Identifiers identifiers = new Identifiers();

    /// <summary>
    /// Gets the <see cref="Information"/> group.
    /// </summary>
    /// <returns>The <see cref="Information"/> group.</returns>
    public Identifiers GetIdentifiers() => identifiers;
    /// <summary>
    /// Sets the <see cref="Information"/> group.
    /// </summary>
    /// <param name="value">The <see cref="Information"/> group.</param>
    public void SetIdentifiers(Identifiers value) => identifiers = value;
    #endregion
    #endregion

    #region Information
    #region Content
    /// <summary>
    /// The class for the <see cref="RPC"/> client information group.
    /// </summary>
    public class Information
    {
      /// <summary>
      /// The <see cref="DiscordRPC.RichPresence.Details"/> value.
      /// </summary>
      [JsonProperty(Order = 1, PropertyName = "Details")]
      public string Details = "";
      /// <summary>
      /// The <see cref="DiscordRPC.RichPresence.State"/> value.
      /// </summary>
      [JsonProperty(Order = 2, PropertyName = "State")]
      public string State = "";
      /// <summary>
      /// The <see cref="DiscordRPC.Timestamps.StartUnixMilliseconds"/> value.
      /// </summary>
      [JsonProperty(Order = 3, PropertyName = "Start_Timestamp")]
      public ulong StartTimestamp = 0;
      /// <summary>
      /// The <see cref="DiscordRPC.Timestamps.EndUnixMilliseconds"/> value.
      /// </summary>
      [JsonProperty(Order = 4, PropertyName = "End_Timestamp")]
      public ulong EndTimestamp = 0;
    };
    #endregion
    #region Variables
    /// <summary>
    /// The variable for the <see cref="RPC"/> client information group.
    /// </summary>
    [JsonProperty(Order = 2, PropertyName = "Information")]
    private Information information = new Information();

    /// <summary>
    /// Gets the <see cref="Information"/> group.
    /// </summary>
    /// <returns>The <see cref="Information"/> group.</returns>
    public Information GetInformation() => information;
    /// <summary>
    /// Sets the <see cref="Information"/> group.
    /// </summary>
    /// <param name="value">The <see cref="Information"/> group.</param>
    public void SetInformation(Information value) => information = value;
    #endregion
    #endregion

    #region Images
    #region Content
    /// </summary>
    /// The class for the <see cref="RPC"/> client images group.
    /// </summary>
    public class Images
    {
      /// <summary>
      /// The <see cref="DiscordRPC.Assets.LargeImageKey"/> value.
      /// </summary>
      [JsonProperty(Order = 1, PropertyName = "Large_Image")]
      public string LargeImage = "";
      /// <summary>
      /// The <see cref="DiscordRPC.Assets.LargeImageText"/> value.
      /// </summary>
      [JsonProperty(Order = 2, PropertyName = "Large_Image_Tooltip")]
      public string LargeImageTooltip = "";
      /// <summary>
      /// The <see cref="DiscordRPC.Assets.SmallImageKey"/> value.
      /// </summary>
      [JsonProperty(Order = 3, PropertyName = "Small_Image")]
      public string SmallImage = "";
      /// <summary>
      /// The <see cref="DiscordRPC.Assets.smallImageText"/> value.
      /// </summary>
      [JsonProperty(Order = 4, PropertyName = "Small_Image_Tooltip")]
      public string SmallImageTooltip = "";
    };
    #endregion
    #region Variables
    /// <summary>
    /// The variable for the <see cref="RPC"/> client images group.
    /// </summary>
    [JsonProperty(Order = 3, PropertyName = "Images")]
    private Images images = new Images();

    /// <summary>
    /// Gets the <see cref="Information"/> group.
    /// </summary>
    /// <returns>The <see cref="Information"/> group.</returns>
    public Images GetImages() => images;
    /// <summary>
    /// Sets the <see cref="Information"/> group.
    /// </summary>
    /// <param name="value">The <see cref="Information"/> group.</param>
    public void SetImages(Images value) => images = value;
    #endregion
    #endregion
  }

  /// <summary>
  /// The class to handle the <see cref="Config"/> statically.
  /// </summary>
  public static class ConfigHandler
  {
    /// <summary>
    /// The public config value.
    /// </summary>
    public static Config config = new Config();
    /// <summary>
    /// The folder to load configs from
    /// TODO: Make the <see cref="ConfigHandler"/> import other configs from the folder.
    /// </summary>
    public static string ConfigFolder => Directory.GetCurrentDirectory() + "/config";
    /// <summary>
    /// Set the config file from the config folder.
    /// </summary>
    public static string ConfigFile => ConfigFolder + "/config.json";

    /// <summary>
    /// Write the config to file.
    /// TODO: Maybe export the <see cref="Config"/> to a named file.
    /// </summary>
    public static void WriteConfig()
    {
      // So we want to serialize the config data into a string.
      string json = JsonConvert.SerializeObject(config, new JsonSerializerSettings() { Formatting = Formatting.Indented });

      // Then we want to check to see if the config folder exists, so we don't have a boo-boo.
      CheckConfigFolder();

      // Try to write to the config file. It may crash so we will just try.
      try
      {
        File.WriteAllText(ConfigFile, json);
      }
      catch (Exception ex)
      {
        App.logger.Log("Error: When writing config to file. | " + ex.Message);
          App.logger.Log(ex.StackTrace);
      }
    }

    /// <summary>
    /// The class to handle the <see cref="Config"/> statically with file path.
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="isExternal"></param>
    public static void WriteConfig(string filePath, bool isExternal = true)
    {
      if (!isExternal)
      {
        // So we want to serialize the config data into a string.
        string json = JsonConvert.SerializeObject(config, new JsonSerializerSettings() { Formatting = Formatting.Indented });

        CheckConfigFolder();

        try
        {
          File.WriteAllText(ConfigFolder + @"\" + filePath + ".json", json);
        }
        catch (Exception ex)
        {
          App.logger.Log("Error: When writing config to file. | " + ex.Message);
          App.logger.Log(ex.StackTrace);
        }
      }
    }

    /// <summary>
    /// Read the <see cref="Config"/> from file.
    /// </summary>
    public static void ReadConfig()
    {
      // So we are going to see if the config file exists yet.
      // And if it doesn't we'll write it.
      CheckConfigFile();

      // Set the JSON to blank, because otherwise we'll just be doomed.
      string json = "";

      // Try to read the file. Maybe we don't have the right permissions, so we have to try.
      try
      {
        json = File.ReadAllText(ConfigFile);
      }
      catch (Exception ex)
      {
        App.logger.Log("Error: When trying to read config file.");
        App.logger.Log(ex.Message);
      }

      // Now we are going to try to parse the config file into JSON data.
      // But if that fails, maybe it's because the JSON data is still "" or just not validated/
      try
      {
        config = JsonConvert.DeserializeObject<Config>(json);
      }
      catch (Exception ex)
      {
        App.logger.Log("Error: When trying to deserialize the config file. | " + ex.Message);
        App.logger.Log(ex.StackTrace);
      }

      try
      {
        App.gui.Invoke((MethodInvoker)delegate
        {
          App.gui.UpdateTextBoxes();
        });
      }
      catch (Exception ex)
      {
        App.logger.Log("Error when trying to update the GUI's TextBoxes | " + ex.Message);
        App.logger.Log(ex.StackTrace);
      }
    }

    /// <summary>
    /// Read the <see cref="Config"/> from file with file path.
    /// </summary>
    /// <param name="filePath">The path to the file. If the <see cref="isExternal"/> value is <see langword="true"/> only include the name without extension.</param>
    /// <param name="isExternal">A <see cref="bool"/> to tell if the file is external from the config folder.</param>
    public static void ReadConfig(string filePath, bool isExternal = true)
    {
      if (!isExternal)
      {
        CheckConfigFolder();

        string json = "";

        try
        {
          json = File.ReadAllText(ConfigFolder + @"\" + filePath + ".json");
        }
        catch (Exception ex)
        {
          App.logger.Log("Error: When trying to read config file. | " + ex.Message);
          App.logger.Log(ex.StackTrace);
        }

        try
        {
          config = JsonConvert.DeserializeObject<Config>(json);
        }
        catch (Exception ex)
        {
          App.logger.Log("Error: When trying to deserialize the config file. | " + ex.Message);
          App.logger.Log(ex.StackTrace);
        }
      }
    }

    /// <summary>
    /// Check the config file by:
    /// 1. Checking if the config folder exists.
    /// 2. If the file doesn't exist then create it.
    /// </summary>
    public static void CheckConfigFile()
    {
      CheckConfigFolder();

      if (! File.Exists(ConfigFile))
      {
        WriteConfig();
      }
    }

    /// <summary>
    /// Check the config folder by:
    /// 1. Checking the folder's existence.
    /// 2. Then trying to create the folder.
    /// </summary>
    public static void CheckConfigFolder()
    {
      // Ono the folder doesn't exit.
      if (! Directory.Exists(ConfigFolder))
      {
        // Whelp we'll have to create it.
        try
        {
          Directory.CreateDirectory(ConfigFolder);
        }
        catch (Exception ex)
        {
          App.logger.Log("Error: When trying to create the Config Folder. | " + ex.Message);
          App.logger.Log(ex.StackTrace);
        }
      }
    }

    /// <summary>
    /// We are going to set the value of a <see cref="Config"/> variable using <see cref="string"/>s.
    /// </summary>
    /// <param name="configVariable"></param>
    /// <param name="value"></param>
    public static void SetValue(ref string configVariable, string value) => configVariable = value;

    /// <summary>
    /// We are going to set the value of a <see cref="Config"/> variable using <see cref="ulong"/>s.
    /// </summary>
    /// <param name="configVariable">The variable that requires a <see cref="ulong"/>.</param>
    /// <param name="value">The <see cref="ulong"/> variable.</param>
    public static void SetValue(ref ulong configVariable, ulong value) => configVariable = value;
  }
}
