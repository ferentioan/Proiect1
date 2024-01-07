using Proiect1.Models;


namespace Proiect1;

public partial class TransportPage : ContentPage
{
    Produse sl;
    public TransportPage(Produse slist)
    {
        InitializeComponent();
        sl = slist;

    }
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var transport = (Transport)BindingContext;
        await App.Database.SaveTransportAsync(transport);
        listView.ItemsSource = await App.Database.GetTransportsAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var transport = (Transport)BindingContext;
        await App.Database.DeleteTransportAsync(transport);
        listView.ItemsSource = await App.Database.GetTransportsAsync();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetTransportsAsync();
    }
    async void OnAddButtonClicked(object sender, EventArgs e)
    {
        Transport p;
        if (listView.SelectedItem != null)
        {
            p = listView.SelectedItem as Transport;
            var lp = new ListTransport()
            {
                ProduseID = sl.ID,
                TransportID = p.ID
            };
            await App.Database.SaveListTransportAsync(lp);
            p.ListTransports = new List<ListTransport> { lp };
            await Navigation.PopAsync();
        }

    }
}