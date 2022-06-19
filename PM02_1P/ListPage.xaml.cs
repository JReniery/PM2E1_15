using PM02_1P.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM02_1P
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        private int id = 0;
        private Double lat = 0.0;
        private Double lon = 0.0;
        private String descrip = "";
        private Byte[] photo = null;
        public ListPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            lvSitesList.ItemsSource = await App.DB.GetSiteList();
        }      
       

        private async void OnDelete_Clicked(object sender, EventArgs e)
        {
            var site = new Sites
            {
                Id = id,
                Lat = lat,
                Lon = lon,
                Descrip = descrip,
                Photo = photo
            };


            var result = await App.DB.DeleteSites(site);

            if (result > 0)
            {
                await DisplayAlert("Empleado Eliminado", "Aviso", "OK");
            }
            else
            {
                await DisplayAlert("ha ocurrido un error", "Aviso", "OK");
            }
        } 
        private async void OnVerMap_Clicked(object sender, EventArgs e)
        { 
            await Navigation.PushAsync(new MapPage(lat, lon));
        }        

        private void LvSitesList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var site = (Sites)e.Item;
            id = site.Id;
            lat = site.Lat;
            lon = site.Lon;
            descrip = site.Descrip;
            photo = site.Photo;
        }
    }
}