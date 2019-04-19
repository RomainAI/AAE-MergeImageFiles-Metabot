using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeImageFiles
{
    public class MergeImage
    {
        /// <summary>
        /// Merge file and create a new image file
        /// </summary>
        /// <param name="files">List of image file to add</param>
        /// <param name="isPortaitMode">Portait or landscape mode</param>
        /// <returns></returns>
        private static Bitmap CombineBitmap(string[] files, bool isPortaitMode)
        {
            //read all images into memory
            List<Bitmap> images = new List<Bitmap>();
            Bitmap finalImage = null;

            try
            {
                int width = 0;
                int height = 0;

                foreach (string image in files)
                {
                    //create a Bitmap from the file and add it to the list
                    Bitmap bitmap = new Bitmap(image);

                    //update the size of the final bitmap
                    if (isPortaitMode)
                    {
                        width = bitmap.Width > width ? bitmap.Width : width;
                        height += bitmap.Height;
                    }
                    else
                    {
                        width += bitmap.Width;
                        height = bitmap.Height > height ? bitmap.Height : height;
                    }

                    images.Add(bitmap);
                }

                //create a bitmap to hold the combined image
                finalImage = new Bitmap(width, height);

                //get a graphics object from the image so we can draw on it
                using (Graphics g = Graphics.FromImage(finalImage))
                {
                    //set background color
                    g.Clear(Color.Black);

                    //go through each image and draw it on the final image
                    int offset = 0;
                    foreach (Bitmap image in images)
                    {
                        if (isPortaitMode)
                        {
                            g.DrawImage(image, new Rectangle(0, offset, image.Width, image.Height));
                            offset += image.Height;
                        }
                        else
                        {
                            g.DrawImage(image, new Rectangle(offset, 0, image.Width, image.Height));
                            offset += image.Width;
                        }
                        
                    }
                }

                return finalImage;
            }
            catch (Exception ex)
            {
                if (finalImage != null)
                    finalImage.Dispose();

                throw ex;
            }
            finally
            {
                //clean up memory
                foreach (Bitmap image in images)
                {
                    image.Dispose();
                }
            }
        }

        /// <summary>
        /// Merge image files into one
        /// </summary>
        /// <param name="listFiles">List of image files, separated by ";"</param>
        /// <param name="isPortraitMode">1=yes ; 0=no</param>
        /// <param name="resultFilePath">Target file path</param>
        /// <returns>Success or Error</returns>
        public static string MergeImageFiles(string listFiles, int isPortraitMode, string resultFilePath)
        {
            string returnValue = string.Empty;
            bool isPortrait = true;
            
            try
            {
                string[] ListFile = listFiles.Split(';');

                if (isPortraitMode == 0)
                    isPortrait = false;

                Bitmap resultImage = CombineBitmap(ListFile, isPortrait);
                resultImage.Save(@resultFilePath);

                returnValue = "Success";
            }
            catch
            {
                returnValue = "Error";
            }

            return returnValue;
        }
    }
}

