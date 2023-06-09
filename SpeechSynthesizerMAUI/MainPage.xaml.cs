namespace SpeechSynthesizerMAUI;

public partial class MainPage : ContentPage
{
    private Locale[] _voices = Array.Empty<Locale>();
    private int _choosenVoiceIndex = -1;

	public MainPage()
	{
        InitializeComponent();
        DownloadVoices();
	}

    private async void DownloadVoices()
    {
        var voices = await TextToSpeech.GetLocalesAsync();
        _voices = voices.ToArray();
        lvVoices.ItemsSource = _voices.Select(e => e.Name);

        if (_voices.Length <= 0 )
        {
            btnRead.IsEnabled = false;
            return;
        }

        _choosenVoiceIndex = 0;
        lvVoices.SelectedItem = _voices.ElementAt(0).Name;
        btnRead.IsEnabled = true;
    }

    private void LvVoices_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        _choosenVoiceIndex = e.SelectedItemIndex;
    }

    private async void BtnRead_Clicked(object sender, EventArgs e)
    {
        var text = edText.Text;
        var so = new SpeechOptions
        {
            Volume = (float)sVolume.Value,
            Pitch = (float)sPitch.Value,
            Locale = _voices.ElementAt(_choosenVoiceIndex)
        };

        await TextToSpeech.Default.SpeakAsync(text, so);
    }
}

