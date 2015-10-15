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
        public static int ConvertImages(string[] fileNames, string targetExtension)
        {
            int counter = 0;
            foreach(String fileName in fileNames) {
                switch (targetExtension) {
                    case ".jpeg":
                        ConvertImageToImage(fileName, ImageFormat.Jpeg);
                        counter++;
                        break;

                    case ".tiff":
                        ConvertImageToTiff(new string[] { fileName }, false);
                        counter++;
                        break;

                    case ".tiff (multipaged)":
                        return counter = ConvertImageToTiff(fileNames, true);

                    case ".gif":
                        ConvertImageToImage(fileName, ImageFormat.Gif);
                        counter++;
                        break;

                    case ".bmp":
                        ConvertImageToImage(fileName, ImageFormat.Bmp);
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
        private static void ConvertImageToImage(string fileName, ImageFormat targetFormat)
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
                    using (Bitmap bmp = new Bitmap(imageFile))
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
        public static int ConvertImageToTiff(string[] fileNames, bool isMultipage)
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
                    using (Image tiffImg = Image.FromFile(fileNames[i]))
                    {
                        tiffImg.Save(tiffPaths[i], ImageFormat.Tiff);
                    }
                }
            }
            return counter;
        }
    }
}