namespace GorselProgOdevi2;

public partial class News : ContentPage
{
	public News()
	{
		InitializeComponent();

		lstMenu.ItemsSource = new List<Kategori>()
		{
			new Kategori() {Baslik = "Manþet", Link = "https://www.trthaber.com/manset_articles.rss"},
			new Kategori() {Baslik = "Son Dakika", Link = "https://www.trthaber.com/sondakika_articles.rss"},
			new Kategori() {Baslik = "Ekonomi", Link = "https://www.trthaber.com/ekonomi_articles.rss"},
			new Kategori() {Baslik = "Bilim Teknoloji", Link = "https://www.trthaber.com/bilim_teknoloji_articles.rss"},
			new Kategori() {Baslik = "Eðitim", Link = "https://www.trthaber.com/egitim_articles.rss"},
		};
	}

    private void lstMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

	public class Kategori
	{
		public string Baslik { get; set; }
		public string Link { get; set; }
	}

	public static async Task<string> HaberleriGetir(Kategori ctg)
	{
		try
		{
			HttpClient client = new HttpClient();
			string url = $"https://api.rss2json.com/v1/api.json?rss_url={ctg.Link}";

			using HttpResponseMessage response = await client.GetAsync(url);
			response.EnsureSuccessStatusCode();
			string jsondata = await response.Content.ReadAsStringAsync();
			return jsondata;
		}
		catch
		{
			return null;
		}
	}
}

public class Root
{
    public string status { get; set; }
    public Feed feed { get; set; }
    public List<Item> items { get; set; }
}

public class Enclosure
{
    public string link { get; set; }
    public string type { get; set; }
}

public class Feed
{
    public string url { get; set; }
    public string title { get; set; }
    public string link { get; set; }
    public string author { get; set; }
    public string description { get; set; }
    public string image { get; set; }
}

public class Item
{
    public string title { get; set; }
    public string pubDate { get; set; }
    public string link { get; set; }
    public string guid { get; set; }
    public string author { get; set; }
    public string thumbnail { get; set; }
    public string description { get; set; }
    public string content { get; set; }
    public Enclosure enclosure { get; set; }
    public List<object> categories { get; set; }
}
