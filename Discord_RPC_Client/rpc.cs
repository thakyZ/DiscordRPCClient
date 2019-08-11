using System;
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

    public RichPresence richPresence;

    void Initialize()
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
        Console.WriteLine("Recieved Update! {0}", e.Presence);
      }

      client.Initialize();

      client.SetPresence(richPresence);
    }

    void constructRichPresence()
    {
      if (ConfigHandler.config.Information.Details != "")
      {
        richPresence.Details = ConfigHandler.config.Information.Details;
      }
      else if (ConfigHandler.config.Information.State != "")
      {
        richPresence.State = ConfigHandler.config.Information.State;
      }
    }
  }
}
