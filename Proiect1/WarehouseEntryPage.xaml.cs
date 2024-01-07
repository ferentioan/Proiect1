namespace Proiect1;
using Proiect1.Models;

public partial class WarehouseEntryPage : ContentPage
{
	public WarehouseEntryPage()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetWarehousesAsync();
    }
    async void OnWarehouseAddedClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WarehousePage
        {
            BindingContext = new Warehouse()
        });
    }
    async void OnListViewItemSelected(object sender,
   SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            await Navigation.PushAsync(new WarehousePage
            {
                BindingContext = e.SelectedItem as Warehouse
            });
        }
    }
}