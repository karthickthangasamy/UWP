using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Syncfusion.SampleBrowser.UWP.Presentation
{

    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Getting Started",
                Tag = Tags.None,
                Product = "Presentation",
                ProductIcons = "Icons/presentation.png",
                Category = Categories.FileFormat,
                SampleCategory = "Default Functionalities",
                SearchKeys = new string[] { "Presentation", "PowerPoint", "PPTX", "hello world", "getting started" },
                SampleView = typeof(EssentialPresentation.GettingStartedPresentation).AssemblyQualifiedName,
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Slides",
                Tag = Tags.None,
                Product = "Presentation",
                Category = Categories.FileFormat,
                SampleCategory = "Slide Elements",
                SearchKeys = new string[] { "Presentation", "PowerPoint", "PPTX", "slide" },
                SampleView = typeof(EssentialPresentation.SlidesPresentation).AssemblyQualifiedName,
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Tables",
                Tag = Tags.None,
                Product = "Presentation",
                Category = Categories.FileFormat,
                SampleCategory = "Slide Elements",
                SearchKeys = new string[] { "Presentation", "PowerPoint", "PPTX", "table" },
                SampleView = typeof(EssentialPresentation.TablesPresentation).AssemblyQualifiedName,
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Images",
                Tag = Tags.None,
                Product = "Presentation",
                Category = Categories.FileFormat,
                SampleCategory = "Slide Elements",
                SearchKeys = new string[] { "Presentation", "PowerPoint", "PPTX", "image" },
                SampleView = typeof(EssentialPresentation.ImagesPresentation).AssemblyQualifiedName,
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Comments",
                Tag = Tags.None,
                Product = "Presentation",
                Category = Categories.FileFormat,
                SampleCategory = "Slide Elements",
                SearchKeys = new string[] { "Presentation", "PowerPoint", "PPTX", "table" },
                SampleView = typeof(EssentialPresentation.CommentsPresentation).AssemblyQualifiedName,
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Charts",
                Tag = Tags.None,
                Product = "Presentation",
                Category = Categories.FileFormat,
                SampleCategory = "Working With Charts",
                SearchKeys = new string[] { "Presentation", "PowerPoint", "PPTX", "chart" },
                SampleView = typeof(EssentialPresentation.ChartsPresentation).AssemblyQualifiedName,
            });
			 SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "PPTX To Image",
                Tag = Tags.New,
                Product = "Presentation",
                Category = Categories.FileFormat,
                SampleCategory = "Conversion",
                SearchKeys = new string[] { "Presentation", "PowerPoint", "PPTX", "image" },
                SampleView = typeof(EssentialPresentation.PPTXToImagePresentation).AssemblyQualifiedName,
            });
            SampleHelper.SetTagsForProduct("Presentation", Tags.Updated);
        }
    }
}