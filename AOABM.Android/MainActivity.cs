
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using AOABM.Views;
using Android.Views;

namespace AOABM.Droid
{
    [Activity(Label = "AOABM", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            App.FileSystem = new LocalFileSystem();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            Chapter.GoFullScreen = () => {
                this.Window.AddFlags(WindowManagerFlags.Fullscreen);
                SystemUiFlags uiv = (SystemUiFlags)Window.DecorView.SystemUiVisibility;
                uiv |= SystemUiFlags.LayoutFullscreen;
                uiv |= SystemUiFlags.LayoutStable;
                uiv |= SystemUiFlags.ImmersiveSticky;
                uiv |= SystemUiFlags.LowProfile;
                uiv |= SystemUiFlags.HideNavigation;
                uiv |= SystemUiFlags.LayoutHideNavigation;
                Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiv;
            };

            Chapter.UnFullScreen = () => {
                this.Window.AddFlags(WindowManagerFlags.ForceNotFullscreen);
                SystemUiFlags uiv = (SystemUiFlags)Window.DecorView.SystemUiVisibility;
                uiv |= SystemUiFlags.Visible;
                Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiv;
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}