using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSTiffImageConverter;
using System.Drawing;

namespace ImageConverterTest
{
    [TestClass]
    public class TestResize
    {

        [TestMethod]
        public void TestResizeCrop()
        {
            int myWidth = 1024;
            int myHeight = 768;

            Image img = Image.FromFile(@"C:\Users\Muhamed\Documents\Visual Studio 2013\Projects\625-1_SoftwareTest\TIFF_image_--_JPEG_image_converter_CSTiffImageConverter_\testfiles\Soviet_BMP-1_IFV.jpeg");
            Bitmap bmp = new Bitmap(img);
            bmp = CSTiffImageConverter.TiffImageConverter.Resizer(1, img);
            Assert.AreEqual(bmp.Height, myHeight);
            Assert.AreEqual(bmp.Width, myWidth);
        }

        [TestMethod]
        public void TestResizeKeepRatio()
        {
            int myWidth = 1024;
            int myHeight = 768;

            Image img = Image.FromFile(@"C:\Users\Muhamed\Documents\Visual Studio 2013\Projects\625-1_SoftwareTest\TIFF_image_--_JPEG_image_converter_CSTiffImageConverter_\testfiles\Soviet_BMP-1_IFV.jpeg");
            Bitmap bmp = new Bitmap(img);
            bmp = CSTiffImageConverter.TiffImageConverter.Resizer(2, img);
            Assert.AreEqual(bmp.Height, myHeight);
            Assert.AreEqual(bmp.Width, myWidth);
        }

        [TestMethod]
        public void TestResizeDontKeepRatio()
        {
            int myWidth = 1024;
            int myHeight = 768;

            Image img = Image.FromFile(@"C:\Users\Muhamed\Documents\Visual Studio 2013\Projects\625-1_SoftwareTest\TIFF_image_--_JPEG_image_converter_CSTiffImageConverter_\testfiles\Soviet_BMP-1_IFV.jpeg");
            Bitmap bmp = new Bitmap(img);
            bmp = CSTiffImageConverter.TiffImageConverter.Resizer(3, img);
            Assert.AreEqual(bmp.Height, myHeight);
            Assert.AreEqual(bmp.Width, myWidth);
        }
    }
}
