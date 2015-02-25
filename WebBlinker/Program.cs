using System;
using System.Net.Http;
using System.Threading;
using Microsoft.Owin.Hosting;
using WiringPi;

namespace WebBlinker
{
  class Program
  {
    static void Main(string[] args)
    {
      // Start OWIN host and keep it running
      using (WebApp.Start<Startup>(url: Settings.BaseListenUrl))
      {
        while (true)
        {
          Console.WriteLine("Server still alive...");
          Thread.Sleep(60000);
        }
      }
    }
  }
}
