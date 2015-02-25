using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BlinkerPhone
{
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class MainPage : Page
  {
    private BlinkerModel model;
    public MainPage()
    {
      this.InitializeComponent();
      this.NavigationCacheMode = NavigationCacheMode.Required;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      model = new BlinkerModel();
    }

    private void ButtonClick(object sender, RoutedEventArgs e)
    {
      int pulse;
      if (int.TryParse(PulseBox.Text, out pulse))
      {
        model.SendSimulatedPulse(pulse);
      }
    }

    private void ButtonClick1(object sender, RoutedEventArgs e)
    {
      model.StartListening();

    }

    private void ButtonClick2(object sender, RoutedEventArgs e)
    {
      model.StopListening();
    }
  }
}
