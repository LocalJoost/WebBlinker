using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using Microsoft.Band;
using Microsoft.Band.Sensors;
using WebBlinker;

namespace BlinkerPhone
{
  public class BlinkerModel
  {
    public async void SendSimulatedPulse(int value)
    {
      var pulse = new Pulse { HeartRate = value };
      var client = new HttpClient();
      await client.PostAsJsonAsync(new Uri(Settings.ServiceUrl), pulse);
    }

    private IBandClient bandClient;

    public async void StartListening()
    {
      var pairedBands = await BandClientManager.Instance.GetBandsAsync();
      if (pairedBands.Any())
      {
        var band = pairedBands.First();
        bandClient = await BandClientManager.Instance.ConnectAsync(band);
        var sensor = bandClient.SensorManager.HeartRate;
        sensor.ReadingChanged += SensorReadingChanged;
        await sensor.StartReadingsAsync();
      }
    }

    public async void StopListening()
    {
      var sensor = bandClient.SensorManager.HeartRate;
      sensor.ReadingChanged -= SensorReadingChanged;
      await sensor.StopReadingsAsync();
      bandClient.Dispose();
    }

    async void SensorReadingChanged(object sender, BandSensorReadingEventArgs<IBandHeartRateReading> e)
    {
      try
      {
        if (e.SensorReading != null)
        {
          var pulse = new Pulse { HeartRate = e.SensorReading.HeartRate };
          using (var client = new HttpClient())
          {
            await client.PostAsJsonAsync(new Uri(Settings.ServiceUrl), pulse);
            Debug.WriteLine("Sending update, pulserate = {0}", pulse.HeartRate);
          }
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine("Error reading/sending data: {0}", ex);
      }
    }
  }
}
