using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Discord_RPC_Client
{
  public class App
  {
    /// <summary>
    /// The <see cref="thread"/> for the console input.
    /// </summary>
    public static Thread consoleThread;

    /// <summary>
    /// The main <see cref="RPC"/> client.
    /// </summary>
    public static RPC rpc;

    /// <summary>
    /// A <see cref="boolean"/> to check when to stop the client.
    /// </summary>
    static bool toStop = true;

    /// <summary>
    /// A value to check if the <see cref="GUI"/> has been started.
    /// </summary>
    static bool guiStarted = false;

    /// <summary>
    /// The main <see cref="GUI"/> client.
    /// </summary>
    public static GUI gui;

    /// <summary>
    /// The <see cref="thread"/> for the <see cref="GUI"/>.
    /// </summary>
    public static Thread guiThread;

    /// <summary>
    /// The file path for the <see cref="Console"/> to output to.
    /// </summary>
    private static readonly string filePath = Environment.CurrentDirectory + @"\console.log";

    /// <summary>
    /// The file path for the <see cref="RPC"/> to log output to.
    /// </summary>
    private static readonly string rpcLogPath = Environment.CurrentDirectory + @"\rpc-console.log";

    /// <summary>
    /// The <see cref="RPC"/> client's logging system.
    /// </summary>
    public static Logger logger;

    /// <summary>
    /// Start the program.
    /// </summary>
    [STAThread]
    public static void Main()
    {
      // Initializer logger class.
      logger = new Logger(filePath);

      // Read config right off the bat since people like to not have to manually paste their config.
      ConfigHandler.ReadConfig();

      // Creates a RPC client.
      rpc = new RPC();

      // CHeck if the RPC client is.
      if (rpc != null)
      {
        rpc.Initialize(rpcLogPath);
      }

      // Set the inline method to call when the application closes.
      Application.ApplicationExit += (sender, e) =>
      {
        if (rpc != null)
        {
          rpc.Deinitialize();
        }
      };

      new Thread(() => ConsoleThread()).Start();
    }

    /// <summary>
    /// Start the <see cref="GUI"/>
    /// </summary>
    /// <param name="autoHide">A boolean for whether or not to auto hide the <see cref="GUI"/> on startup. (For testing purposes only)</param>
    private static void StartGUI(bool autoHide)
    {
      // Get if the GUI is null or if the GUI has not been started.
      if (gui == null || ! guiStarted)
      {
        // Set the visual styles to be enabled.
        Application.EnableVisualStyles();
        // Start a new thread with the GUI.
        new Thread(() => Application.Run(gui = new GUI())).Start();

        // Check if autoHide has been enabled.
        if (autoHide)
        {
          // Sleep the thread otherwise we'd get an exception.
          Thread.Sleep(150);
          // Now hide that son of a *beep*.
          gui.Invoke((MethodInvoker)delegate { gui.Hide(); });
        }

        // Set the GUI to have been started.
        guiStarted = true;
      }
      else
      {
        // If the GUI has been started then just invoke the GUI to show itself.
        gui.Invoke((MethodInvoker)delegate { gui.Show(); });
      }
    }

    /// <summary>
    /// Start the console Thread.
    /// </summary>
    private static void ConsoleThread()
    {
      // Wait for the value of stopping the thread to actually stop this thread.
      while (toStop)
      {
        // Check for console input.
        // TODO: Move check console input into this function because it's not needed in it's own function
        CheckConsoleInput();
      }
    }

    /// <summary>
    /// The update function for the <see cref="RPC"/> client so it can be called outside the main thread.
    /// </summary>
    public static void UpdateRPC()
    {
      // Try to update the RPC client.
      try
      {
        logger.Log("Updating RPC");
        // Update the RPC using it's update function (yes I know it's a bit redundant).
        rpc.Update();
      }
      catch (Exception ex)
      {
        logger.Log("Error: When updating the RPC client. | " + ex.Message);
        logger.Log(ex.StackTrace);
      }
    }

    /// <summary>
    /// Check console input
    /// TODO: Move this into <see cref="ConsoleThread"/>
    /// </summary>
    private static void CheckConsoleInput()
    {
      // Get input from console output.
      string input = Console.ReadLine();

      // Switch from cases of the input.
      switch (input)
      {
        case "quit":
        case "exit":
        case "stop":
          // Close the application.
          logger.Log("Bye!");
          toStop = false;
          // Try to close the GUI (even if it's closed already).
          try
          {
            gui.Invoke((MethodInvoker)delegate { gui.exit = true; gui.Close(); });
          }
          catch (Exception ex)
          {
            logger.Log("Error: GUI Already Closed. | " + ex.Message);
            logger.Log(ex.StackTrace);
          }
          // Finally close the application.
          Application.Exit();
          break;
        case "update":
          // Try to update the RPC.
          UpdateRPC();
          break;
        case "gui":
          // Try to launch the GUI (without autoHide, because it's not necessary).
          try
          {
            logger.Log("Launching GUI");
            StartGUI(false);
          }
          catch (Exception ex)
          {
            logger.Log("Error: When attempting to launch GUI. | " + ex.Message);
            logger.Log(ex.StackTrace);
          }
          break;
        case var loadFileCommand when new Regex(@"^load\s[a-z0-9]+$").IsMatch(loadFileCommand):
          try
          {
            string arguments = loadFileCommand.Substring(5, loadFileCommand.Length - 5);
            ConfigHandler.ReadConfig(arguments, false);
          }
          catch (Exception ex)
          {
            logger.Log("Failed to parse arguments and read config from file. | " + ex.Message);
            logger.Log(ex.StackTrace);
          }
          break;
        case var loadCommand when new Regex(@"^load$").IsMatch(loadCommand):
          try
          {
            ConfigHandler.ReadConfig();
          }
          catch (Exception ex)
          {
            logger.Log("Failed to parse arguments and read config from file. | " + ex.Message);
            logger.Log(ex.StackTrace);
          }
          break;
        case var saveFileCommand when new Regex(@"^save\s[a-z0-9]+$").IsMatch(saveFileCommand):
          try
          {
            string arguments = saveFileCommand.Substring(5, saveFileCommand.Length - 5);
            ConfigHandler.WriteConfig(arguments, false);
          }
          catch (Exception ex)
          {
            logger.Log("Failed to parse arguments and write config to file. | " + ex.Message);
            logger.Log(ex.StackTrace);
          }
          break;
        case var saveCommand when new Regex(@"^save$").IsMatch(saveCommand):
          try
          {
            ConfigHandler.WriteConfig();
          }
          catch (Exception ex)
          {
            logger.Log("Failed to parse arguments and write config to file. | " + ex.Message);
            logger.Log(ex.StackTrace);
          }
          break;
        default:
          // Output the possible command line arguments if no recognized case is used.
          logger.Log("Commands are as follows: [ quit/exit/stop | update | gui ]");
          break;
      }
    }
  }
}
