using AOABM.Services;
using AOABM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AOABM
{
    public partial class App : Application
    {
        public static string Username;
        public static string Password;

        public static HttpClient client = new HttpClient();

        public static IFileSystem FileSystem;
        public static List<Folders> Folders;
        public static List<Folders> FlatFolders;
        public static Folders CurrentFolder;
        public static int CurrentImage = 0;

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();

            //MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            await LoadFolders();

            MainPage = new AppShell();
        }

        public static async Task LoadFolders()
        {
            Folders = (await FileSystem.GetFolders())?.SubFolders ?? new List<Folders>();

            FlatFolders = new List<Folders>();
            foreach(var folder in Folders)
            {
                ProcessFolder(folder);
            }
        }

        private static void ProcessFolder(Folders folder)
        {
            if (folder.Images.Any())
            {
                FlatFolders.Add(folder);
            }

            foreach(var f in folder.SubFolders)
            {
                ProcessFolder(f);
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
