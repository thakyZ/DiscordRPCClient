using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Discord_RPC_Client
{
  /// <summary>
  /// <see cref="Abstract"/> constructor for the <see cref="Logger"/>.
  /// </summary>
  public abstract class LogBase
  {
    /// <summary>
    /// <see cref="Abstract"/> method for writing to log file.
    /// </summary>
    /// <param name="message"></param>
    public abstract void Log(string message);
  }

  /// <summary>
  /// Class for logging to file.
  /// </summary>
  public class Logger : LogBase
  {
    /// <summary>
    /// The file path for the console to output to.
    /// </summary>
    public string filePath = Environment.CurrentDirectory + @"\console.log";

    public Logger(string filePath) => this.filePath = filePath;

    /// <summary>
    /// The <see cref="StreamWriter"/> to write to log file.
    /// </summary>
    private StreamWriter streamWriter;

    /// <summary>
    /// Log to file.
    /// </summary>
    /// <param name="message">The message in a <see cref="string"/> format to log to console and file.</param>
    public override void Log(string message)
    {
      // Create the StreamWriter while using it
      using (streamWriter = new StreamWriter(filePath))
      {
        // Write to file.
        streamWriter.WriteLine(message);
        // Write to console.
        Console.WriteLine(message);
        // Close the file.
        streamWriter.Close();
      }
    }
  }
}
