using Plugin.LocalNotification;
using Proiect1.Models;

namespace Proiect1;

public partial class WarehousePage : ContentPage
{
	public WarehousePage()
	{
		InitializeComponent();
	}
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var warehouse = (Warehouse)BindingContext;
        await App.Database.SaveWarehouseAsync(warehouse);
        await Navigation.PopAsync();
    }
    async void OnShowMapButtonClicked(object sender, EventArgs e)
    {
        var shop = (Warehouse)BindingContext;
        var address = shop.Adress;
        var locations = await Geocoding.GetLocationsAsync(address);

        var options = new MapLaunchOptions { Name = "Depozitul Meu" };
        var location = locations?.FirstOrDefault();

        // Setează notificarea cu o zi înainte
        var notificationTime = DateTime.Now.AddDays(1);

        var request = new NotificationRequest
        {
            Title = "Maine va ajunge comanda ta!",
            Description = address,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = notificationTime
            }
        };

        LocalNotificationCenter.Current.Show(request);

        await Map.OpenAsync(location, options);
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var shop = (Warehouse)BindingContext;

        if (await DisplayAlert("Confirm Delete", $"Do you want to delete {shop.WarehouseName}?", "Yes", "No"))
        {
            await App.Database.DeleteWarehouseAsync(shop);
            await Navigation.PopAsync();
        }
    }
}