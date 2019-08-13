using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Discord_RPC_Client
{
  public class App
  {
    public static RPC rpc;

    static bool toStop = true;

    public static GUI gui;

    public static Logger logger;

    [STAThread]
    public static void Main()
    {
      logger = new Logger();

      ConfigHandler.ReadConfig();

      Watcher.Initializer();

      rpc = new RPC();

      if (rpc != null)
      {
        rpc.Initialize();
      }

      Application.ApplicationExit += (sender, e) =>
      {
        if (rpc != null)
        {
          rpc.Deinitialize();
        }
      };

      Application.EnableVisualStyles();
      new Thread(ConsoeleThread).Start();
    }

    private static void StartGUI()
    {
      if (gui == null)
      {
        new Thread(() => Application.Run(gui = new GUI())).Start();
      }
      else
      {
        gui.Invoke(new MethodInvoker(() => { gui.Show(); }));
      }
    }

    private static void ConsoeleThread()
    {
      while (toStop)
      {
        CheckConsoleInput();
      }
    }

    public delegate void CloseDelegate();

    private static void CheckConsoleInput()
    {
      string input = Console.ReadLine();

      switch (input)
      {
        case "quit":
        case "exit":
        case "stop":
          logger.Log("Bye!");
          toStop = false;
          try
          {
            gui.Invoke(new MethodInvoker(() => { gui.exit = true; gui.Close(); }));
          }
          catch (Exception ex)
          {
            logger.Log("Error: GUI Already Closed.");
            logger.Log(ex.Message);
          }
          Application.Exit();
          break;
        case "update":
          try
          {
            logger.Log("Updating RPC");
            rpc.Update();
          }
          catch (Exception ex)
          {
            logger.Log("Error: When updating the RPC client.");
            logger.Log(ex.Message);
          }
          break;
        case "gui":
          try
          {
            logger.Log("Launching GUI");
            StartGUI();
          }
          catch (Exception ex)
          {
            logger.Log("Error: When attempting to launch GUI.");
            logger.Log(ex.Message);
          }
          break;
        default:
          Console.WriteLine("Commands are as follows: [ quit/exit/stop | update | gui ]");
          break;
      }
    }
  }
}
