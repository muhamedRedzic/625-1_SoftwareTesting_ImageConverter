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

            Image img = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + "\\Soviet_BMP-1_IFV.jpeg");
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

            Image img = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + "\\Soviet_BMP-1_IFV.jpeg");
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

            Image img = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + "\\Soviet_BMP-1_IFV.jpeg");
            Bitmap bmp = new Bitmap(img);
            bmp = CSTiffImageConverter.TiffImageConverter.Resizer(3, img);
            Assert.AreEqual(bmp.Height, myHeight);
            Assert.AreEqual(bmp.Width, myWidth);
        }
    }
}
