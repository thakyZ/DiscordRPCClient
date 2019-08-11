using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Discord_RPC_Client
{
  public class Logger
  {
    FileStream ostrm;
    StreamWriter writer;
    TextWriter oldOut = Console.Out;

    public Logger()
    {
      try
      {
        ostrm = new FileStream(Environment.CurrentDirectory + "/console.log", FileMode.OpenOrCreate, FileAccess.Write);
        writer = new StreamWriter(ostrm);
      }
      catch (Exception e)
      {
        Console.WriteLine("Cannot open console.log for writing");
        Console.WriteLine(e.Message);
        return;
      }

      Console.SetOut(writer);
    }

    public void CloseLogger()
    {
      writer.Close();
      ostrm.Close();
      Console.WriteLine("Done");
    }
  }
}
