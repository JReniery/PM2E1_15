using Plugin.Media;
using PM02_1P.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PM02_1P
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
         Plugin.Media.Abstractions.MediaFile photoFile = null;        
        public MainPage()
        {
            InitializeComponent();
        }

        private Byte[] ConvertImageToByteArray()
        {
            if (photoFile != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = photoFile.GetStream();
                    stream.CopyTo(memory);
                    return memory.ToArray();
                }
            }
            return null;
        }

        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            if (photoFile == null)
            {
                await DisplayAlert("Error", "No ha tomado una fotografía", "OK");
                return;
            }

            var site = new Sites
            {
                Id = 0,
                Descrip = edtDescrip.Text,
                Lat = Convert.ToDouble(txbLat.Text),
                Lon = Convert.ToDouble(txbLon.Text),                
                Photo = ConvertImageToByteArray()
            };

            var result = await App.DB.SaveSite(site);

            if (result > 0)
            {
                await DisplayAlert("Ubicación Guardada", "Aviso", "OK");
            }
            else
            {
                await DisplayAlert("Ha ocurrido un error", "Aviso", "OK");
            }
        }

        private async void btnList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListPage());
        }

        private void btnExit_Clicked(object sender, EventArgs e)
        {

        }       

        private async void imgPhoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                photoFile = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "MisFotos",
                    Name = "IMG_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"),
                    SaveToAlbum = true
                });

                await DisplayAlert("path directorio", photoFile.Path, "OK");

                if (photoFile != null)
                {
                    imgPhoto.Source = ImageSource.FromStream(() =>
                    {
                        return photoFile.GetStream();
                    });
                }
            }
            catch (Exception)
            {
                //throw ex;
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var location = await Geolocation.GetLocationAsync();

                if (location != null)
                {
                    txbLat.Text = location.Latitude.ToString();
                    txbLon.Text = location.Longitude.ToString();                   
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
    }
}
