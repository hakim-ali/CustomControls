using System;
using Xamarin.Forms.Platform.Android;
using Android.Views;
using Android.Hardware;
using System.Collections.Generic;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Labs.Droid;
using Xamarin.Forms.CustomControls.Controls;
using Xamarin.Forms.CustomControls.Droid;

[assembly: ExportRenderer(typeof(CameraView), typeof(CameraViewRenderer))]

namespace Xamarin.Forms.CustomControls.Droid
{
    public class CameraViewRenderer : ViewRenderer<CameraView, CameraPreview>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CameraView> e)
        {
            base.OnElementChanged(e);

            if (this.Control == null)
            {
                this.SetNativeControl(new CameraPreview(this.Context));
            }

            this.Control.PreviewCamera = this.Control.PreviewCamera ?? Camera.Open((int)e.NewElement.Camera);
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case "Camera":
                    this.Control.SwitchCamera(Camera.Open((int)this.Element.Camera));
                    break;
                default:
                    break;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Control.PreviewCamera.Release();
            }

            base.Dispose(disposing);
        }
    }

    public class CameraPreview : ViewGroup, ISurfaceHolderCallback
    {
        const string TAG = "Preview";

        SurfaceView mSurfaceView;
        ISurfaceHolder mHolder;
        Camera.Size mPreviewSize;
        IList<Camera.Size> mSupportedPreviewSizes;
        Camera _camera;

        public Camera PreviewCamera
        {
            get { return _camera; }
            set
            {
                _camera = value;
                if (_camera != null)
                {
                    mSupportedPreviewSizes = PreviewCamera.GetParameters().SupportedPreviewSizes;
                    RequestLayout();
                }
            }
        }

        public CameraPreview(Context context)
            : base(context)
        {
            mSurfaceView = new SurfaceView(context);
            AddView(mSurfaceView);

            // Install a SurfaceHolder.Callback so we get notified when the
            // underlying surface is created and destroyed.
            mHolder = mSurfaceView.Holder;
            mHolder.AddCallback(this);
        }

        public void SwitchCamera(Camera camera)
        {
            if (PreviewCamera != null)
            {
                //PreviewCamera.StopPreview();
                PreviewCamera.Release();
            }

            PreviewCamera = camera;

            try
            {
                camera.SetPreviewDisplay(mHolder);
            }
            catch (Java.IO.IOException exception)
            {
                Android.Util.Log.Error(TAG, "IOException caused by setPreviewDisplay()", exception);
            }

            Camera.Parameters parameters = camera.GetParameters();
            parameters.SetPreviewSize(mPreviewSize.Width, mPreviewSize.Height);
            RequestLayout();

            camera.SetParameters(parameters);
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            // We purposely disregard child measurements because act as a
            // wrapper to a SurfaceView that centers the camera preview instead
            // of stretching it.
            int width = ResolveSize(SuggestedMinimumWidth, widthMeasureSpec);
            int height = ResolveSize(SuggestedMinimumHeight, heightMeasureSpec);
            SetMeasuredDimension(width, height);

            if (mSupportedPreviewSizes != null)
            {
                mPreviewSize = GetOptimalPreviewSize(mSupportedPreviewSizes, width, height);
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            if (changed && ChildCount > 0)
            {
                var child = GetChildAt(0);

                int width = r - l;
                int height = b - t;

                int previewWidth = width;
                int previewHeight = height;
                if (mPreviewSize != null)
                {
                    previewWidth = mPreviewSize.Width;
                    previewHeight = mPreviewSize.Height;
                }

                // Center the child SurfaceView within the parent.
                if (width * previewHeight > height * previewWidth)
                {
                    int scaledChildWidth = previewWidth * height / previewHeight;
                    child.Layout((width - scaledChildWidth) / 2, 0,
                        (width + scaledChildWidth) / 2, height);
                }
                else
                {
                    int scaledChildHeight = previewHeight * width / previewWidth;
                    child.Layout(0, (height - scaledChildHeight) / 2,
                        width, (height + scaledChildHeight) / 2);
                }
            }
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            // The Surface has been created, acquire the camera and tell it where
            // to draw.
            try
            {
                if (PreviewCamera != null)
                {
                    PreviewCamera.SetPreviewDisplay(holder);
                }
            }
            catch (Java.IO.IOException exception)
            {
                Android.Util.Log.Error(TAG, "IOException caused by setPreviewDisplay()", exception);
            }
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            // Surface will be destroyed when we return, so stop the preview.
            if (PreviewCamera != null)
            {
                PreviewCamera.StopPreview();
            }
        }

        private Camera.Size GetOptimalPreviewSize(IList<Camera.Size> sizes, int w, int h)
        {
            const double ASPECT_TOLERANCE = 0.1;
            double targetRatio = (double)w / h;

            if (sizes == null)
                return null;

            Camera.Size optimalSize = null;
            double minDiff = Double.MaxValue;

            int targetHeight = h;

            // Try to find an size match aspect ratio and size
            foreach (Camera.Size size in sizes)
            {
                double ratio = (double)size.Width / size.Height;

                if (Math.Abs(ratio - targetRatio) > ASPECT_TOLERANCE)
                    continue;

                if (Math.Abs(size.Height - targetHeight) < minDiff)
                {
                    optimalSize = size;
                    minDiff = Math.Abs(size.Height - targetHeight);
                }
            }

            // Cannot find the one match the aspect ratio, ignore the requirement
            if (optimalSize == null)
            {
                minDiff = Double.MaxValue;
                foreach (Camera.Size size in sizes)
                {
                    if (Math.Abs(size.Height - targetHeight) < minDiff)
                    {
                        optimalSize = size;
                        minDiff = Math.Abs(size.Height - targetHeight);
                    }
                }
            }

            return optimalSize;
        }

        public void SurfaceChanged(ISurfaceHolder holder, Android.Graphics.Format format, int w, int h)
        {
            // Now that the size is known, set up the camera parameters and begin
            // the preview.
            var parameters = PreviewCamera.GetParameters();
            parameters.SetPreviewSize(mPreviewSize.Width, mPreviewSize.Height);
            RequestLayout();

            PreviewCamera.SetParameters(parameters);
            PreviewCamera.StartPreview();
        }
    }
}

