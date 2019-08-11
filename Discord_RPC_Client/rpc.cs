using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiscordRPC;
using DiscordRPC.Logging;

namespace Discord_RPC_Client
{
  public class RPC
  {
    public DiscordRpcClient client;

    public void Initialize()
    {
      // Create a discord client
      client = new DiscordRpcClient(ConfigHandler.config.Identifiers.ClientID);

      client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

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

      client.Initialize();

      client.SetPresence(ConstructRichPresence());
    }

    public void Deinitialize()
    {
      client.Dispose();
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
