using AOABM.Services;
using SQLite;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AOABM
{
    public partial class App : Application
    {
        public static string Username;
        public static string Password;

        public static HttpClient client = new HttpClient();

        public static IDataStore FileSystem = null;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new BasePage());
        }

        protected override async void OnStart()
        {
            while(FileSystem == null)
            {
                await Task.Delay(100);
            }

            await FileSystem.UpdateDataFolder();
            await FileSystem.LoadFolders();
        }


        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
