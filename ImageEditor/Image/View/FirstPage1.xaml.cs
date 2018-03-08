#region Copyright Syncfusion Inc. 2001-2017.
// Copyright Syncfusion Inc. 2001-2017. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.ImageEditor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FirstPage1 : Page
    {
        public FirstPage1()
        {
            this.InitializeComponent();
        }

        public async void Gallery_Change(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();

            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".jpeg");

            StorageFile file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                var stream = await file.OpenAsync(FileAccessMode.Read);
                OpenImageEditor(stream);
            }


        }

        public async void Camera_Change(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();

            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.JpegXR;
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Png;

            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (photo != null)
            {
                var stream = await photo.OpenAsync(FileAccessMode.Read);
                OpenImageEditor(stream);
            }

            else if (photo == null)
            {
                return;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage image = new BitmapImage();
            image = (BitmapImage)((sender as Button).Content as Image).Source;

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(image.UriSource);
            if (file != null)
            {
                var stream = await file.OpenAsync(FileAccessMode.Read);
                OpenImageEditor(stream);
            }

        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BitmapImage image = new BitmapImage();
            image = (BitmapImage)((sender as Button).Content as Image).Source;

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(image.UriSource);
            if (file != null)
            {
                var stream = await file.OpenAsync(FileAccessMode.Read);
                OpenImageEditor(stream);
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BitmapImage image = new BitmapImage();
            image = (BitmapImage)((sender as Button).Content as Image).Source;

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(image.UriSource);
            if (file != null)
            {
                var stream = await file.OpenAsync(FileAccessMode.Read);
                OpenImageEditor(stream);

            }
        }

        private void OpenImageEditor(IRandomAccessStream stream)
        {
            if (secondPage.Children.Count > 1)
            {
                secondPage.Children.RemoveAt(1);
            }
            Syncfusion.UI.Xaml.ImageEditor.SfImageEditor imgedit = new Syncfusion.UI.Xaml.ImageEditor.SfImageEditor();
            imgedit.Image = stream as FileRandomAccessStream;
            secondPage.Children.Add(imgedit);
            Grid.SetRow(imgedit, 1);           

            firstPage.Visibility = Visibility.Collapsed;
            secondPage.Visibility = Visibility.Visible;
        }

        private void appBarButton_Click(object sender, RoutedEventArgs e)
        {
            firstPage.Visibility = Visibility.Visible;
            secondPage.Visibility = Visibility.Collapsed;

        }
    }
}
