using System;
using AOABM.Views;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;

namespace AOABM.Droid
{
    public class PinchToZoomContainerRenderer : ViewRenderer<PinchToZoomContainer, Android.Views.View>
    {
        Android.Views.View vw;
        private bool _panStarted = false;
        private bool _pinchStarted = false;
        private double _originDistance;

        private static float mDownX;
        private static float mDownY;
        private static float SCROLL_THRESHOLD = 10;
        private static bool isTap;

        public PinchToZoomContainerRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<PinchToZoomContainer> e)
        {
            base.OnElementChanged(e);

            _panStarted = false;
            _pinchStarted = false;

            if (Control == null)
            {
                vw = new Android.Views.View(Context);

                SetNativeControl(vw);
            }

            if (e.OldElement != null)
            {

            }
            if (e.NewElement != null)
            {

            }
        }

        private double GetDistance(float x1, float y1, float x2, float y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        private void EndPan(PinchToZoomContainer element, float x, float y)
        {
            element.OnPanUpdated(this, new PanUpdatedEventArgs(GestureStatus.Completed, 0, x, y));
            _panStarted = false;
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            var totalX = e.GetX();
            var totalY = e.GetY();
            var element = Element as PinchToZoomContainer;

            switch (e.Action)
            {

                case MotionEventActions.Down:

                    _pinchStarted = false;
                    _panStarted = true;

                    element.OnPanUpdated(this, new PanUpdatedEventArgs(GestureStatus.Started, e.ActionIndex, totalX, totalY));

                    break;

                case MotionEventActions.Move:
                    if (e.PointerCount > 1)
                    {
                        var x1 = e.GetX(1);
                        var y1 = e.GetY(1);

                        if (_panStarted)
                        {
                            EndPan(element, totalX, totalY);
                        }

                        if (!_pinchStarted)
                        {
                            _pinchStarted = true;

                            element.OnPinchUpdated(this, new PinchGestureUpdatedEventArgs(GestureStatus.Started));
                            _originDistance = GetDistance(totalX, totalY, x1, y1);
                        }
                        else
                        {
                            var distance = GetDistance(totalX, totalY, x1, y1);
                            var scale = distance / _originDistance;
                            _originDistance = distance;

                            var centre = new Xamarin.Forms.Point(Math.Min(totalX, x1) + Math.Abs(x1 - totalX) / 2, Math.Min(totalY, y1) + Math.Abs(y1 - totalY) / 2);
                            var origin = new Xamarin.Forms.Point(centre.X / this.Width, centre.Y / this.Height);

                            element.OnPinchUpdated(this, new PinchGestureUpdatedEventArgs(GestureStatus.Running, scale, origin));
                        }
                    }
                    else if (_panStarted)
                    {
                        element.OnPanUpdated(this, new PanUpdatedEventArgs(GestureStatus.Running, e.ActionIndex, totalX, totalY));
                    }

                    break;

                case MotionEventActions.Up:
                    if (_panStarted)
                    {
                        EndPan(element, totalX, totalY);
                    }
                    else if (_pinchStarted)
                    {
                        element.OnPinchUpdated(this, new PinchGestureUpdatedEventArgs(GestureStatus.Completed));
                        _pinchStarted = false;
                    }
                    break;

                default:

                    return base.OnTouchEvent(e);
            }

            return true;
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            switch (ev.ActionMasked)
            {
                case MotionEventActions.Down:
                    mDownX = ev.GetX();
                    mDownY = ev.GetY();
                    isTap = true;
                    break;
                case MotionEventActions.Cancel:
                case MotionEventActions.Move:
                    var x = ev.GetX();
                    var y = ev.GetY();
                    if (isTap && (Math.Abs(mDownX - x) > SCROLL_THRESHOLD || Math.Abs(mDownY - y) > SCROLL_THRESHOLD))
                    {

                        isTap = false;
                        return true;
                    }
                    break;
                default:
                    break;
            }
            OnTouchEvent(ev);
            return false;

        }
    }
}