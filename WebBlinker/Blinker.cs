using System;
using System.Threading;
using System.Threading.Tasks;
using WiringPi;

namespace WebBlinker
{
  public class Blinker
  {
    private DateTime lastReceivedUpdate = DateTime.MinValue;
    private int heartRate;
    private Task task;

    private Blinker()
    {
      if (Init.WiringPiSetup() != -1)
      {
        GPIO.pinMode(BluePin, (int)GPIO.GPIOpinmode.Output);
        GPIO.pinMode(GreenPin, (int)GPIO.GPIOpinmode.Output);
        GPIO.pinMode(RedPin, (int)GPIO.GPIOpinmode.Output);
        GPIO.pinMode(SoundPin, (int)GPIO.GPIOpinmode.Output);
      }
    }

    public void Start()
    {
      if (task == null)
      {
        cancellationTokenSource = new CancellationTokenSource();
        task = new Task(() => ShowHeartRateBlinking(cancellationTokenSource.Token), 
           cancellationTokenSource.Token);
        task.Start();
      }
    }

    public void Stop()
    {
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
        task = null;
      }
    }

    private CancellationTokenSource cancellationTokenSource = null;

    private async Task ShowHeartRateBlinking(CancellationToken cancellationToken)
    {
      while (!cancellationToken.IsCancellationRequested)
      {
        if (DateTime.Now - lastReceivedUpdate < TimeSpan.FromSeconds(5))
        {
          DoBlink();
          await Task.Delay(60000/HeartRate, cancellationToken);
        }
        else
        {
          await Task.Delay(10000, cancellationToken);
        }
      }
    }

    private async Task DoBlink()
    {
      var pin = GetPin();
      GPIO.digitalWrite(pin, 1);
      GPIO.digitalWrite(SoundPin, 1);
      await Task.Delay(50);
      GPIO.digitalWrite(pin, 0);
      GPIO.digitalWrite(SoundPin, 0);
    }

    private int GetPin()
    {
      if( HeartRate < 80) return BluePin;
      return HeartRate < 130 ? GreenPin : RedPin;
    }

    public int HeartRate
    {
      get { return heartRate; }
      set
      {
        if (value >= 0 && value <= 200)
        {
          lastReceivedUpdate = DateTime.Now;
          Console.WriteLine("Got updated: {0}", value);
          heartRate = value;
        }
      }
    }

    private static Blinker blinker;
    public static Blinker GetBlinker()
    {
      return blinker ?? (blinker = new Blinker());
    }

    public const int RedPin = 27;
    public const int GreenPin = 28;
    public const int BluePin = 29;
    public const int SoundPin = 0;
  }
}
