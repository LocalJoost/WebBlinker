using System.Web.Http;
using Owin;

namespace WebBlinker
{
  public class Startup
  {
    // This code configures Web API contained in the class Startup, which is additionally specified as the type parameter in WebApplication.Start
    public void Configuration(IAppBuilder appBuilder)
    {
      // Configure Web API for Self-Host
      var config = new HttpConfiguration();

      //Use attribute routes!
      config.MapHttpAttributeRoutes();
      appBuilder.UseWebApi(config);
      Blinker.GetBlinker().Start();
    }
  }
}
