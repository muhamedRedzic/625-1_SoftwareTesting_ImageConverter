/****************************** Module Header ******************************\ 
Module Name:    TiffImageConverter.cs 
Project:        CSTiffImageConverter
Copyright (c) Microsoft Corporation. 

The class defines the helper methods for converting TIFF from or to JPEG 
file(s)

This source is subject to the Microsoft Public License.
See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
All other rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. 
\***************************************************************************/

using System;
using System.IO;
using System.Linq;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace CSTiffImageConverter
{
    public class TiffImageConverter
    {
        /// <summary>
        /// Service function for the image conversion, called by the MainForm.
        /// </summary>
        /// <param name="fileNames">Full name to images</param>
        /// <param name="targetExtension">String of the target image format</param>
        /// <returns>Int representing the number of images successfully converted</returns>
        public static int ConvertImages(string[] fileNames, string targetExtension, int choice)
        {
            int counter = 0;
            foreach (String fileName in fileNames)
            {
                switch (targetExtension)
                {
                    case ".jpeg":
                        ConvertImageToImage(fileName, ImageFormat.Jpeg, choice);
                        counter++;
                        break;

                    case ".tiff":
                        ConvertImageToTiff(new string[] { fileName }, false, choice);
                        counter++;
                        break;

                    case ".tiff (multipaged)":
                        return counter = ConvertImageToTiff(fileNames, true, choice);

                    case ".gif":
                        ConvertImageToImage(fileName, ImageFormat.Gif, choice);
                        counter++;
                        break;

                    case ".bmp":
                        ConvertImageToImage(fileName, ImageFormat.Bmp, choice);
                        counter++;
                        break;

                    default:
                        break;
                }
            }
            return counter;
        }

        /// <summary>
        /// Converts an image to image(s) in another format (jpeg, gif or bmp).
        /// </summary>
        /// <param name="fileName">Full name to image</param>
        /// <param name="targetFormat">ImageFormat of the target image format</param>
        private static void ConvertImageToImage(string fileName, ImageFormat targetFormat, int choice)
        {
            using (Image imageFile = Image.FromFile(fileName))
            {
                FrameDimension frameDimensions = new FrameDimension(
                    imageFile.FrameDimensionsList[0]);

                // Gets the number of pages from the image (if multipage)
                int frameNum = imageFile.GetFrameCount(frameDimensions);
                string[] imagePaths = new string[frameNum];

                for (int frame = 0; frame < frameNum; frame++)
                {
                    // Selects one frame at a time and save.
                    imageFile.SelectActiveFrame(frameDimensions, frame);
                    Bitmap bmp = Resizer(choice, imageFile);
                    using (bmp)
                    {
                        imagePaths[frame] = String.Format("{0}\\{1}{2}." + targetFormat.ToString().ToLower(),
                            Path.GetDirectoryName(fileName),
                            Path.GetFileNameWithoutExtension(fileName),
                            frame);
                        bmp.Save(imagePaths[frame], targetFormat);
                    }
                }
            }
        }

        /// <summary>
        /// Converts image(s) to tiff image(s).
        /// </summary>
        /// <param name="fileNames">
        /// String array having full name to images.
        /// </param>
        /// <param name="isMultipage">
        /// true to create single multipage tiff file otherwise, false.
        /// </param>
        /// <returns>Int representing the number of images successfully converted if using multipaged setting</returns>
        public static int ConvertImageToTiff(string[] fileNames, bool isMultipage, int choice)
        {
            int counter = 0;
            EncoderParameters encoderParams = new EncoderParameters(1);
            ImageCodecInfo tiffCodecInfo = ImageCodecInfo.GetImageEncoders()
                .First(ie => ie.MimeType == "image/tiff");

            string[] tiffPaths = null;
            if (isMultipage)
            {
                counter = fileNames.Length;
                tiffPaths = new string[1];
                Image tiffImg = null;
                try
                {
                    for (int i = 0; i < fileNames.Length; i++)
                    {
                        if (i == 0)
                        {
                            tiffPaths[i] = String.Format("{0}\\{1}0.tif",
                                Path.GetDirectoryName(fileNames[i]),
                                Path.GetFileNameWithoutExtension(fileNames[i]));

                            // Initialize the first frame of multipage tiff.
                            tiffImg = Image.FromFile(fileNames[i]);
                            encoderParams.Param[0] = new EncoderParameter(
                                Encoder.SaveFlag, (long)EncoderValue.MultiFrame);
                            tiffImg.Save(tiffPaths[i], tiffCodecInfo, encoderParams);
                        }
                        else
                        {
                            // Add additional frames.
                            encoderParams.Param[0] = new EncoderParameter(
                                Encoder.SaveFlag, (long)EncoderValue.FrameDimensionPage);
                            using (Image frame = Image.FromFile(fileNames[i]))
                            {
                                tiffImg.SaveAdd(frame, encoderParams);
                            }
                        }

                        if (i == fileNames.Length - 1)
                        {
                            // When it is the last frame, flush the resources and closing.
                            encoderParams.Param[0] = new EncoderParameter(
                                Encoder.SaveFlag, (long)EncoderValue.Flush);
                            tiffImg.SaveAdd(encoderParams);
                        }
                    }
                }
                finally
                {
                    if (tiffImg != null)
                    {
                        tiffImg.Dispose();
                        tiffImg = null;
                    }
                }
            }
            else
            {
                tiffPaths = new string[fileNames.Length];

                for (int i = 0; i < fileNames.Length; i++)
                {
                    tiffPaths[i] = String.Format("{0}\\{1}.tif",
                        Path.GetDirectoryName(fileNames[i]),
                        Path.GetFileNameWithoutExtension(fileNames[i]));

                    // Save as individual tiff files.
                    Image tiffImg = Image.FromFile(fileNames[i]);
                    tiffImg = (Image)Resizer(choice, tiffImg);
                    using (tiffImg)
                    {
                        
                        tiffImg.Save(tiffPaths[i], ImageFormat.Tiff);
                    }
                }
            }
            return counter;
        }
        public static Bitmap Resizer(int choice, Image myImage)
        {
            Bitmap bmp = new Bitmap(myImage);
            switch (choice)
            {
                case 1:
                    bmp = ResizeCrop(myImage);
                    break;
                case 2:
                    bmp = ResizeKeepRatio(myImage);
                    break;
                case 3:
                    bmp = new Bitmap(myImage, new Size(1024, 768));
                    break;
                default:
                    break;

            }

            return bmp;
        }

        private static Bitmap ResizeKeepRatio(Image myImage)
        {
            int Width = 1024;
            int Height = 768;
            int sourceWidth = myImage.Width;
            int sourceHeight = myImage.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Width -
                              (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Height -
                              (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height,
                              PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(myImage.HorizontalResolution,
                             myImage.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Black);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(myImage,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        private static Bitmap ResizeCrop(Image image)
        {
            int Width = 1024;
            int Height = 768;
            int sourceWidth = image.Width;
            int sourceHeight = image.Height;
            int sourceX = 0;
            int sourceY = 0;
            double destX = 0;
            double destY = 0;

            double nScale = 0;
            double nScaleW = 0;
            double nScaleH = 0;

            nScaleW = ((double)Width / (double)sourceWidth);
            nScaleH = ((double)Height / (double)sourceHeight);

                nScale = Math.Max(nScaleH, nScaleW);
                destY = (Height - sourceHeight * nScale) / 2;
                destX = (Width - sourceWidth * nScale) / 2;
            

            if (nScale > 1)
                nScale = 1;

            int destWidth = (int)Math.Round(sourceWidth * nScale);
            int destHeight = (int)Math.Round(sourceHeight * nScale);


            System.Drawing.Bitmap bmPhoto = null;
            try
            {
                bmPhoto = new System.Drawing.Bitmap(destWidth + (int)Math.Round(2 * destX), destHeight + (int)Math.Round(2 * destY));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("destWidth:{0}, destX:{1}, destHeight:{2}, desxtY:{3}, Width:{4}, Height:{5}",
                    destWidth, destX, destHeight, destY, Width, Height), ex);
            }
            using (System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto))
            {


                Rectangle to = new System.Drawing.Rectangle((int)Math.Round(destX), (int)Math.Round(destY), destWidth, destHeight);
                Rectangle from = new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight);
                //Console.WriteLine("From: " + from.ToString());
                //Console.WriteLine("To: " + to.ToString());
                grPhoto.DrawImage(image, to, from, System.Drawing.GraphicsUnit.Pixel);

                return bmPhoto;
            }
        }

    }


}