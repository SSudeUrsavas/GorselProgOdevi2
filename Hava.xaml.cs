using System.Collections.ObjectModel;
using System.Text.Json;

namespace GorselProgOdevi2;

public partial class Hava : ContentPage
{
	public Hava()
	{
		InitializeComponent();

        if (File.Exists(fileName))
        {
            string data = File.ReadAllText(fileName);
            Sehirler = JsonSerializer.Deserialize<ObservableCollection<SehirHavaDurumu>>(data);
        }

        listCity.ItemsSource = Sehirler;
	}

    string fileName = Path.Combine(FileSystem.Current.AppDataDirectory, "hdata.json");

    ObservableCollection<SehirHavaDurumu> Sehirler = new ObservableCollection<SehirHavaDurumu>();

    public class SehirHavaDurumu
    {
        public string Name { get; set; }
        public string Source => $"https://www.mgm.gov.tr/sunum/tahmin-klasik-5070.aspx?m={Name}&basla=1&bitir=5&rC=111&rZ=fff";
    }

    private async void Ekle_Clicked(object sender, EventArgs e)
    {
        string sehir = await DisplayPromptAsync("�ehir:", "�ehir ismi", "OK", "Cancel");
        sehir = sehir.ToUpper(System.Globalization.CultureInfo.CurrentCulture);
        sehir = sehir.Replace('�', 'C');
        sehir = sehir.Replace('�', 'G');
        sehir = sehir.Replace('�', 'I');
        sehir = sehir.Replace('�', 'O');
        sehir = sehir.Replace('�', 'U');
        sehir = sehir.Replace('�', 'S');
        Sehirler.Add(new SehirHavaDurumu() { Name = sehir });
    }

    private void Yukle_Clicked(object sender, EventArgs e)
    {
        refreshView.IsRefreshing = false;
    }

    private async void Sil_Clicked(object sender, EventArgs e)
    {
        var d = sender as ImageButton;
        if (d != null)
        {
            var k = Sehirler.First(o => o.Name == d.CommandParameter.ToString());
            var yes = await DisplayAlert("Silinsin mi?", "Silmeyi onaylay�n�z.", "OK", "CANCEL");
            if (yes)
            {
                Sehirler.Remove(k);
                string data = JsonSerializer.Serialize(Sehirler);
                File.WriteAllText(fileName, data);
            }
        }
    }

    private void ContentPage_Unloaded(object sender, EventArgs e)
    {
        string data = JsonSerializer.Serialize(Sehirler);
        File.WriteAllText(fileName, data);
    }
}