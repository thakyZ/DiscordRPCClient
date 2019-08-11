using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Discord_RPC_Client
{
  public class App
  {
    public static void Main()
    {
      Logger logger = new Logger();

      ConfigHandler.ReadConfig();

      RPC rpc = new RPC();
      rpc.Initialize();

      Console.ReadLine();

      Application.ApplicationExit += (sender, e) =>
      {
        rpc.Deinitialize();
        logger.CloseLogger();
      };
    }
  }
}
