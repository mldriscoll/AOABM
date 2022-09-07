using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace AOABM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Chapter : ContentPage
    {
        public Chapter()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            var rightTap = new TapGestureRecognizer();
            Right.GestureRecognizers.Add(rightTap);
            rightTap.Tapped += RightTap_Tapped;

            var leftTap = new TapGestureRecognizer();
            Left.GestureRecognizers.Add(leftTap);
            leftTap.Tapped += LeftTap_Tapped;

            //SizeChanged += Chapter_SizeChanged;
            //LayoutChanged += Chapter_SizeChanged;
            //Shell.Current.SizeChanged += Chapter_SizeChanged;
            //Content.SizeChanged += Chapter_SizeChanged;
            draw();
        }

        public static Action GoFullScreen = null;
        public static Action UnFullScreen = null;

        protected override void OnAppearing()
        {
            GoFullScreen?.Invoke();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            UnFullScreen?.Invoke();
            base.OnDisappearing();
        }

        private async void LeftTap_Tapped(object sender, EventArgs e)
        {
            var finished = await App.FileSystem.NextPicture();
            if (finished)
            {
                await Navigation.PopAsync();
            }
            else
            {
                await draw();
            }
        }

        private async void RightTap_Tapped(object sender, EventArgs e)
        {
            await App.FileSystem.PreviousPicture();
            await draw();
        }

        private void Chapter_SizeChanged(object sender, EventArgs e)
        {
            HeightRequest = Shell.Current.Height;
            WidthRequest = Shell.Current.Width;
            ImageHolder.HeightRequest = Shell.Current.Height;
            ImageHolder.WidthRequest = Shell.Current.Width;
            SizeConstraints();
        }

        double oldwidth = 0;
        double oldheight = 0;
        protected override void OnSizeAllocated(double width, double height)
        {
            if(width == oldwidth && height == oldheight)
            {
                return;
            }

            oldheight = height;
            oldwidth = width;

            ImageHolder.HeightRequest = height;
            ImageHolder.WidthRequest = width;
            SizeConstraints();
            base.OnSizeAllocated(width, height);
        }

        protected override SizeRequest OnSizeRequest(double widthConstraint, double heightConstraint)
        {
            ImageHolder.HeightRequest = heightConstraint;
            ImageHolder.WidthRequest = widthConstraint;
            SizeConstraints();
            return base.OnSizeRequest(widthConstraint, heightConstraint);
        }

        double width = 1;
        double height = 1;

        private async Task draw()
        {
            var image = (await App.FileSystem.CurrentFolder()).Images[await App.FileSystem.GetCurrentPic()];
            var pic = await App.FileSystem.GetImageStream(image);
            //pic.Item1.Seek(0, System.IO.SeekOrigin.Begin);

            width = pic.Item2;
            height = pic.Item3;

            SizeConstraints();

            ImageHolder.Source = ImageSource.FromStream(() => pic.Item1);
        }

        private void SizeConstraints()
        {
            double heightRatio = 1;
            if(height > ImageHolder.HeightRequest)
            {
                heightRatio = ImageHolder.HeightRequest / height;

                ImageHolder.WidthRequest = ImageHolder.WidthRequest * heightRatio;
            }

            if((width * heightRatio) > ImageHolder.WidthRequest)
            {
                var ratio = ImageHolder.WidthRequest / (width * heightRatio);

                ImageHolder.HeightRequest = ImageHolder.HeightRequest * ratio;
            }
        }
    }

    public class PinchToZoomContainer : ContentView
    {
        double currentScale = 1;
        double startScale = 1;
        double xOffset = 0;
        double yOffset = 0;

        public PinchToZoomContainer()
        {
            if (!Device.RuntimePlatform.Equals(Device.Android))
            {
                var pinchGesture = new PinchGestureRecognizer();
                pinchGesture.PinchUpdated += OnPinchUpdated;
                GestureRecognizers.Add(pinchGesture);
                var panGesture = new PanGestureRecognizer();
                panGesture.PanUpdated += OnPanUpdated;
                panGesture.TouchPoints = 1;
                GestureRecognizers.Add(panGesture);
            }
        }

        double x = 0;
        double y = 0;
        public double width = 0;
        public double height = 0;
        public void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    x = e.TotalX;
                    y = e.TotalY;
                    break;
                case GestureStatus.Running:
                    // Translate and ensure we don't pan beyond the wrapped user interface element bounds.  
                    Content.TranslationX += e.TotalX - x;

                    x = e.TotalX;

                    Content.TranslationY += e.TotalY - y;

                    y = e.TotalY;

                    SetBounds();
                    break;
            }
        }

        private void SetBounds()
        {
            if (Content.TranslationX < width - (Content.Width * Content.Scale))
                Content.TranslationX = width - (Content.Width * Content.Scale);

            if (Content.TranslationX > 0)
                Content.TranslationX = 0;

            if (Content.TranslationY < height - (Content.Height * Content.Scale))
                Content.TranslationY = height - (Content.Height * Content.Scale);

            if (Content.TranslationY > 0)
                Content.TranslationY = 0;

        }

        public void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            if (e.Status == GestureStatus.Started)
            {
                startScale = Content.Scale;
                Content.AnchorX = 0;
                Content.AnchorY = 0;
            }
            if (e.Status == GestureStatus.Running)
            {
                currentScale += (e.Scale - 1) * startScale;
                currentScale = Math.Max(1, currentScale);

                double renderedX = Content.X + xOffset;
                double deltaX = renderedX / Width;
                double deltaWidth = Width / (Content.Width * startScale);
                double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                double renderedY = Content.Y + yOffset;
                double deltaY = renderedY / Height;
                double deltaHeight = Height / (Content.Height * startScale);
                double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                double targetX = xOffset - (originX * Content.Width) * (currentScale - startScale);
                double targetY = yOffset - (originY * Content.Height) * (currentScale - startScale);

                Content.TranslationX = targetX.Clamp(-Content.Width * (currentScale - 1), 0);
                Content.TranslationY = targetY.Clamp(-Content.Height * (currentScale - 1), 0);

                SetBounds();

                Content.Scale = currentScale;
            }
            if (e.Status == GestureStatus.Completed)
            {
                xOffset = Content.TranslationX;
                yOffset = Content.TranslationY;
            }
        }
    }
}