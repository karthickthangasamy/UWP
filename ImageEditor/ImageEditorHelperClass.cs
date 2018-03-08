using Common;
using Syncfusion.SampleBrowser.UWP.ImageEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.SampleBrowser.UWP.ImageEditor
{
    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
            CollectSampleView();
        }
        public void CollectSampleView()
        {

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(FirstPage1).AssemblyQualifiedName,
                Product = "ImageEditor",
                ProductIcons = "ms-appx:///Syncfusion.SampleBrowser.UWP.ImageEditor/Assets/imageedit.png",
                DesktopImage = "ms-appx:///Syncfusion.SampleBrowser.UWP.ImageEditor/Assets/imageedit.png",
                MobileImage = "ms-appx:///Syncfusion.SampleBrowser.UWP.ImageEditor/Assets/imageedit.png",
                Header = "ImageEditor",
                Tag = Tags.None,
                Category = Categories.Editors,
                Description = "The image editor control lets users annotate images with freehand drawing, text and shapes. It is also possible to perform simple image manipulation operations like cropping,flipping and rotation.",
                HasOptions = false
            });
            SampleHelper.SetTagsForProduct("ImageEditor", Tags.None);

        }
    }
}

