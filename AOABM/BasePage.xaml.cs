using AOABM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AOABM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasePage : ContentPage
    {
        public BasePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            //AfterLoad();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //await AfterLoad();
        }

        private async Task AfterLoad()
        {
            //if (App.FlatFolders.Any())
            //{
            //}
            //else
            //{
            //    await Navigation.PushAsync(new AboutPage());
            //}
        }

        private void ResumeButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new Chapter());
            }
            catch(Exception ex)
            {

            }
        }

        private void SelectButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ChapterSelect(() => Navigation.PushAsync(new Chapter())));
        }

        private void DownloadButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new DownloadPage());
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            await App.FileSystem.Empty();
        }
    }
}