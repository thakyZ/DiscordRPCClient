using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiscordRPC;
using DiscordRPC.Logging;
using DiscordRPC.Message;
using System.Windows.Forms;

namespace Discord_RPC_Client
{
  public class RPC
  {
    /// <summary>
    /// The <see cref="Timer"/> to set toward the <see cref="GUI.update_ProgressBar"/> on the <see cref="GUI"/>.
    /// </summary>
    System.Timers.Timer UpdateTimer;

    /// <summary>
    /// The <see cref="DiscordRpcClient"/> variable.
    /// </summary>
    public DiscordRpcClient client;

    /// <summary>
    /// The progress value of the <see cref="UpdateTimer"/>.
    /// </summary>
    public double UpdateProgress = 0;

    /// <summary>
    /// Creates the <see cref="DiscordRpcClient"/> if it hasn't been created already.
    /// </summary>
    public void CreateRPCClient()
    {
      try
      {
        client = new DiscordRpcClient(ConfigHandler.config.GetIdentifiers().ClientID, autoEvents: false);
        Initialize();
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error when creating RPC client.");
        Console.WriteLine(ex.Message);
        return;
      }
    }

    /// <summary>
    /// Initializes the <see cref="DiscordRpcClient"/>. Must be called separately.
    /// </summary>
    /// <param name="logFilePath">The path to the log file.</param>
    public void Initialize(string logFilePath)
    {
      // Try to create the Discord RPC client and catch it if it fails.
      try
      {
        client = new DiscordRpcClient(ConfigHandler.config.GetIdentifiers().ClientID, autoEvents: false);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error when creating RPC client.");
        Console.WriteLine(ex.Message);
        return;
      }

      // Clear file if it doesn't exist already.
      System.IO.File.WriteAllBytes(logFilePath, new byte[0]);
      // Set the RPC client to log to RPC log file.
      client.Logger = new FileLogger(logFilePath, LogLevel.Error);

      SetupClient();
    }

    /// <summary>
    /// Initializes the <see cref="DiscordRpcClient"/> class. Must be called separately.
    /// </summary>
    public void Initialize()
    {
      // Try to create the Discord RPC client and catch it if it fails.
      try
      {
        client = new DiscordRpcClient(ConfigHandler.config.GetIdentifiers().ClientID, autoEvents: false);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error when creating RPC client.");
        Console.WriteLine(ex.Message);
        return;
      }

      // Log only to console.
      client.Logger = new ConsoleLogger(LogLevel.Error, true);

      SetupClient();
    }

    /// <summary>
    /// Setup the <see cref="DiscordRpcClient"/> (used to shorten the length of Initialize and keep everything constant).
    /// </summary>
    private void SetupClient()
    {

      client.OnReady += (sender, e) =>
      {
        Console.WriteLine("Received Ready from user {0}", e.User.Username);
      };

      client.OnPresenceUpdate += (sender, e) =>
      {
        Console.WriteLine("Received Update! {0}", e.Presence);
      };

      client.OnError += (sender, e) =>
      {
        Console.WriteLine("Error: {0}", e.Message);
      };

      client.OnConnectionEstablished += (sender, e) =>
      {
        Console.WriteLine("Connection Established, {0}", e.TimeCreated);
      };

      client.OnConnectionFailed += (sender, e) =>
      {
        Console.WriteLine("Connection Filed: {0} | {1}", e.TimeCreated, e.FailedPipe);
      };

      UpdateTimer = new System.Timers.Timer(150);
      UpdateTimer.Elapsed += (sender, e) =>
      {
        if (UpdateProgress == 0)
        {
          Update();
        }

        UpdateProgress++;

        if (UpdateProgress >= 150)
        {
          UpdateProgress = 0;
        }

        try
        {
          App.gui.Invoke(new MethodInvoker(() => { App.gui.UpdateProgressBar((int)UpdateProgress); }));
        }
        catch
        {
          return;
        }
      };
      UpdateTimer.Start();

      client.Initialize();

      client.SetPresence(ConstructRichPresence());
    }

    /// <summary>
    /// Invokes the <see cref="DiscordRpcClient"/>, and updates the data.
    /// </summary>
    public void Update()
    {
      client.SetPresence(ConstructRichPresence());
      client.Invoke();
    }

    /// <summary>
    /// De-initializes the <see cref="DiscordRpcClient"/>.
    /// </summary>
    public void Deinitialize()
    {
      // Try to dispose the RPC client.
      try
      {
        client.Dispose();
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error: Error when disposing of RPC client.");
        Console.WriteLine(ex.Message);
      }
    }

    /// <summary>
    /// Constructs a <see cref="RichPresence"/> from <see cref="Config"/> and <see cref="ConfigHandler"/> data.
    /// </summary>
    /// <returns>The constructed <see cref="RichPresence"/> class.</returns>
    private RichPresence ConstructRichPresence()
    {
      // Construct the temporary RichPresence 
      RichPresence richPresence = new RichPresence
      {
        // Set Details
        Details = ConfigHandler.config.GetInformation().Details,
        // Set State
        State = ConfigHandler.config.GetInformation().State,
        // Initialize the Timestamps class
        Timestamps = ConfigHandler.config.GetInformation().StartTimestamp > 0 ? new Timestamps()
        {
          StartUnixMilliseconds = ConfigHandler.config.GetInformation().StartTimestamp,
          EndUnixMilliseconds = ConfigHandler.config.GetInformation().StartTimestamp > 0 ? ConfigHandler.config.GetInformation().EndTimestamp : ulong.MinValue
        } : null,
        // Initialize the Assets class
        Assets = new Assets()
        {
          LargeImageKey = ConfigHandler.config.GetImages().LargeImage,
          LargeImageText = ConfigHandler.config.GetImages().LargeImageTooltip,
          SmallImageKey = ConfigHandler.config.GetImages().SmallImage,
          SmallImageText = ConfigHandler.config.GetImages().SmallImageTooltip
        }
      };

      return richPresence;
    }
  }
}
