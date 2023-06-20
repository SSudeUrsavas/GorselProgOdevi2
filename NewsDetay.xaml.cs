using Microsoft.Maui.ApplicationModel.DataTransfer;

namespace GorselProgOdevi2;

public partial class NewsDetay : ContentPage
{
	Item haber;
	public NewsDetay()
	{
		InitializeComponent(); //Item item
        //haber = item;                         //Bu kýsým çalýþmadýðý için haber kýsmý çalýþmýyor
		//webView.Source = item.link;
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		await ShareUri(haber.link, Share.Default);
    }

	public async Task ShareUri(string uri, IShare share)
	{
		await share.RequestAsync(new ShareTextRequest
		{
			Uri = uri,
			Title = haber.title
		});
	}
}