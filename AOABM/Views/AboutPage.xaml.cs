using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AOABM.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Username = username.Text;
            App.Password = password.Text;
            Navigation.PushAsync(new DownloadPage());
        }

        Folders currentFolder = null;

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if(App.FileSystem == null) return;


            var baseFolder = await App.FileSystem.GetFolders();

            if (currentFolder == null) currentFolder = baseFolder;

            foreach (var f in baseFolder.SubFolders)
            {
                await processFolder(f);
            }
        }

        private async Task processFolder(Folders folder)
        {
            if (currentFolder.Images == null || !currentFolder.Images.Any())
            {
                currentFolder = folder;
            }

            foreach(var f in folder.SubFolders)
            {
                await processFolder(f);
            }
        }
    }
}