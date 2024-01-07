namespace Proiect1;
using Proiect1.Models;



public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
	}
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (Produse)BindingContext;
        slist.Date = DateTime.UtcNow;
        Warehouse selectedShop = (WarehousePicker.SelectedItem as Warehouse);
        slist.WarehouseID = selectedShop.ID;
        await App.Database.SaveProduseAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (Produse)BindingContext;
        await App.Database.DeleteProduseAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TransportPage((Produse)
       this.BindingContext)
        {
            BindingContext = new Transport()
        });

    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shopl = (Produse)BindingContext;
        var items = await App.Database.GetWarehousesAsync();
        WarehousePicker.ItemsSource = (System.Collections.IList)items;
        WarehousePicker.ItemDisplayBinding = new Binding("WarehouseDetails");

        listView.ItemsSource = await App.Database.GetListTransportsAsync(shopl.ID);
    }
    async void OnDeleteItemClicked(object sender, EventArgs e)
    {
        var selectedTransport = listView.SelectedItem as Transport;

        if (selectedTransport != null)
        {
            var produse = (Produse)BindingContext;

            await App.Database.DeleteListTransportAsync(produse.ID, selectedTransport.ID);

            listView.ItemsSource = await App.Database.GetListTransportsAsync(produse.ID);
        }
    }

}