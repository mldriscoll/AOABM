using AOABM.ViewModels;
using AOABM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace AOABM
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Chapter), typeof(Chapter));

            foreach(var folder in App.Folders)
            {
                ProcessFolder(folder);
            }
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private void ProcessFolder(Folders folder)
        {
            var i = new FlyoutChapterItem
            {
                Folder = folder,
                Text = getTabs(folder.Layer) + folder.Name,
            };

            if (folder.Images.Any())
            {
                i.Clicked += I_Clicked;
            }
            Items.Add(i);

            foreach (var f in folder.SubFolders)
            {
                ProcessFolder(f);
            }
        }

        private string getTabs(int tabs)
        {
            var ret = string.Empty;
            for(int i = 0; i < tabs; i++)
            {
                ret = ret + "\t";
            }
            return ret;
        }

        private async void I_Clicked(object sender, EventArgs e)
        {
            var s = (FlyoutChapterItem)sender;
            App.CurrentFolder = s.Folder;
            App.CurrentImage = 0;
            await Shell.Current.GoToAsync("Chapter");
        }
    }

    public class FlyoutChapterItem : MenuItem
    {
        public Folders Folder { get; set; }
    }
}