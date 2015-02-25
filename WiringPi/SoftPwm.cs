using System.Runtime.InteropServices;

namespace WiringPi
{
  public class SoftPwm
  {
    [DllImport("libwiringPi.so", EntryPoint = "softPwmCreate")]
    public static extern void Create(int pin, int initialValue, int pwmRange);

    [DllImport("libwiringPi.so", EntryPoint = "softPwmWrite")]
    public static extern void Write(int pin, int value);

    [DllImport("libwiringPi.so", EntryPoint = "softPwmStop")]
    public static extern void Stop(int pin);
  }
}
