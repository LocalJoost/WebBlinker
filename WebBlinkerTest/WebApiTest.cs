using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebBlinker;

namespace WebBlinkerTest
{
  [TestClass]
  public class WebApiTest
  {
    [TestMethod]
    public async Task TestPost()
    {
      var pulse = new Pulse { HeartRate = 75 };
      const string baseUrl = Settings.ServiceUrl;
      var client = new HttpClient();
      await client.PostAsJsonAsync(new Uri(baseUrl), pulse);
    }
  }
}
