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
    public DiscordRpcClient client;
    public double UpdateProgress = 0;


    public void Initialize()
    {
      // Try to create the Discord RPC client and catch it if it fails.
      try
      {
        client = new DiscordRpcClient(ConfigHandler.config.Identifiers.ClientID, autoEvents: false);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error when creating RPC client.");
        Console.WriteLine(ex.Message);
        return;
      }

      client.Logger = new ConsoleLogger(LogLevel.Error, true);
      client.Logger = new FileLogger(Environment.CurrentDirectory + @"\console.log", LogLevel.Error);

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

      System.Timers.Timer UpdateTimer = new System.Timers.Timer(150);
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

    public void Update()
    {
      client.SetPresence(ConstructRichPresence());
      client.Invoke();
    }

    public void Deinitialize()
    {
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

    private RichPresence ConstructRichPresence()
    {
      RichPresence richPresence = new RichPresence();

      richPresence.Assets = new Assets();

      if (ConfigHandler.config.Information.Details != "")
      {
        richPresence.Details = ConfigHandler.config.Information.Details;
      }
      else { richPresence.Details = ""; }

      if (ConfigHandler.config.Information.State != "")
      {
        richPresence.State = ConfigHandler.config.Information.State;
      }
      else { richPresence.State = ""; }

      Timestamps timestamps = new Timestamps();

      if (ConfigHandler.config.Information.StartTimestamp != 0)
      {
        timestamps.StartUnixMilliseconds = ConfigHandler.config.Information.StartTimestamp;

        if (ConfigHandler.config.Information.EndTimestamp != 0 && ConfigHandler.config.Information.EndTimestamp > ConfigHandler.config.Information.StartTimestamp)
        {
          timestamps.EndUnixMilliseconds = ConfigHandler.config.Information.EndTimestamp;
        }

        richPresence.Timestamps = timestamps;
      }

      if (ConfigHandler.config.Images.LargeImage != "")
      {
        richPresence.Assets.LargeImageKey = ConfigHandler.config.Images.LargeImage;
      }
      else { richPresence.Assets.LargeImageKey = ""; }

      if (ConfigHandler.config.Images.LargeImageTooltip != "")
      {
        richPresence.Assets.LargeImageText = ConfigHandler.config.Images.LargeImageTooltip;
      }
      else { richPresence.Assets.LargeImageText = ""; }

      if (ConfigHandler.config.Images.SmallImage != "")
      {
        richPresence.Assets.SmallImageKey = ConfigHandler.config.Images.SmallImage;
      }
      else { richPresence.Assets.SmallImageKey = ""; }

      if (ConfigHandler.config.Images.SmallImageTooltip != "")
      {
        richPresence.Assets.SmallImageText = ConfigHandler.config.Images.SmallImageTooltip;
      }
      else { richPresence.Assets.SmallImageText = ""; }

      return richPresence;
    }
  }
}
