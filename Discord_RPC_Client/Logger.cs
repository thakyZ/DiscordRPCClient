using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Discord_RPC_Client
{
  public abstract class LogBase
  {
    public abstract void Log(string message);
  }

  public class Logger : LogBase
  {
    public string filePath = Environment.CurrentDirectory + @"\console.log";

    private StreamWriter streamWriter;

    public override void Log(string message)
    {
      using (streamWriter = new StreamWriter(filePath))
      {
        streamWriter.WriteLine(message);
        Console.WriteLine(message);
        streamWriter.Close();
      }
    }
  }
}
