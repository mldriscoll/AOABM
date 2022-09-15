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
    public partial class ChapterSelect : ContentPage
    {
        private readonly Action onChapterSelected;

        public ChapterSelect(Action onChapterSelected)
        {
            InitializeComponent();
            PostLoad();
            this.onChapterSelected = onChapterSelected;
        }

        async void PostLoad()
        {
            var folders = await App.FileSystem.GetFolders();

            foreach(var f in folders.SubFolders)
            {
                ProcessFolder(f, 0);
            }
        }

        private void ProcessFolder(Folders folder, int indent)
        {
            if (folder.Images.Count == 0)
            {
                SL.Children.Add(new Label { Text = folder.DisplayName });
                foreach(var f in folder.SubFolders)
                {
                    ProcessFolder(f, indent + 1);
                }
            }
            else
            {
                var b = new FoldersButton { Text = folder.DisplayName, Folder = folder, BorderWidth = 0, Margin = new Thickness(2,2,2,2), HeightRequest = 30, Padding = 0, VerticalOptions = LayoutOptions.Start, TextTransform = TextTransform.None };
                b.Clicked += B_Clicked;
                SL.Children.Add(b); 
            }

        }

        private async void B_Clicked(object sender, EventArgs e)
        {
            var button = (FoldersButton)sender;
            await App.FileSystem.SetCurrentChapter(button.Folder.Name);
            await App.FileSystem.SetCurrentPicture(0);
            await Navigation.PopModalAsync();
            onChapterSelected();
        }
    }

    public class FoldersButton : Button
    {
        public Folders Folder { get; set; }
    }
}