using System;
using System.Web.Http;

namespace WebBlinker
{
  public class BlinkController : ApiController
  {
    [HttpPost]
    [Route("api/singleblink")]
    public void PostBlink([FromBody] Pulse pulse)
    {
      Console.WriteLine( "Got update: {0}", pulse.HeartRate);
      Blinker.GetBlinker().HeartRate = pulse.HeartRate;
    }

    [HttpGet]
    [Route("api/helloworld")]
    public string HelloWorld()
    {
      Console.WriteLine("Hello world called");
      return "Hello world!";
    }
  }
}
