using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebGallery.Model
{
    /// <summary>
    /// Buisness logic layer for WebGallery application.
    /// </summary>
    public class Gallery
    {
        #region Fields

        private static readonly Regex ApprovedExtensions;
        public static readonly string PhysicalUploadImagePath;
        private static readonly Regex SantizePath;

        #endregion

        #region Constructors

        /// <summary>
        /// Initiates the static readonly fields.
        /// </summary>
        static Gallery()
        {
            ApprovedExtensions = new Regex(@"^.*\.(gif|jpg|png)$");
            PhysicalUploadImagePath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Content/Images");
            SantizePath = new Regex(String.Format("[{0}]", Regex.Escape(new String(Path.GetInvalidFileNameChars()))));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a collection with file names for both fullsize image and thumbnail image.
        /// </summary>
        /// <returns>A collecion with all images file names.</returns>
        public IEnumerable<ImageNames> GetAllImagesNames()
        {
            List<string> images = (List<string>)GetImagesNames();
            List<string> thumbnails = (List<string>)GetThumbnailsNames();

            List<ImageNames> imageNames = new List<ImageNames>(images.Count);
            for (int i = 0; i < images.Count; i += 1)
            {
                imageNames.Add(new ImageNames(images[i], thumbnails[i], ""));
            }

            return imageNames;
        }

        /// <summary>
        /// Gets a collection with thumbnail image file names in ascending order.
        /// </summary>
        /// <returns>A collecion with all thumbnail image file names.</returns>
        public IEnumerable<string> GetThumbnailsNames()
        {
            var di = new DirectoryInfo(Path.Combine(PhysicalUploadImagePath, "Thumbnails"));
            var files = di.GetFiles().Where(f => ApprovedExtensions.IsMatch(f.Extension.ToLower()));

            List<string> names = files.Select(f => f.Name).ToList();
            names.Sort();

            return names;
        }

        /// <summary>
        /// Gets a collection with image file names in ascending order.
        /// </summary>
        /// <returns>A collecion with all image file names.</returns>
        public IEnumerable<String> GetImagesNames()
        {
            var di = new DirectoryInfo(PhysicalUploadImagePath);
            var files = di.GetFiles().Where(f => ApprovedExtensions.IsMatch(f.Extension.ToLower()));
            List<string> names = files.Select(f => f.Name).ToList();
            names.Sort();

            return names;
        }

        /// <summary>
        /// Checks if the given file name exists in the PhysicalUploadImagePath folder.
        /// </summary>
        /// <param name="name">The file to check.</param>
        /// <returns>True if file exists, else false.</returns>
        public static bool ImageExists(string name)
        {
            return File.Exists(Path.Combine(PhysicalUploadImagePath, name));
        }

        /// <summary>
        /// Checks if the file is a GIF, JPEG or PNG.
        /// </summary>
        /// <param name="image">The image to check.</param>
        /// <returns>True if image is GIF, JPEG or PNG, else false.</returns>
        private bool IsValidImage(Image image)
        {
            if (image.RawFormat.Guid == ImageFormat.Gif.Guid ||
                image.RawFormat.Guid == ImageFormat.Jpeg.Guid ||
                image.RawFormat.Guid == ImageFormat.Png.Guid)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Saves the image file in the image folder.
        /// </summary>
        /// <param name="stream">The file to save.</param>
        /// <param name="fileName">The name for the image file.</param>
        /// <returns></returns>
        public string SaveImage(Stream stream, string fileName)
        {
            fileName = SantizePath.Replace(fileName, "");
            Image image = Image.FromStream(stream);
            Image thumbnail = null;
            int i = 0;

            if (IsValidImage(image) && ApprovedExtensions.IsMatch(fileName.ToLower()))
            {
                // Controls and if needed change the fileName.
                if (ImageExists(fileName))
                {
                    i += 1;
                    do
                    {
                        fileName = String.Format("{0}({1}).{2}", Path.GetFileNameWithoutExtension(fileName), i, Path.GetExtension(fileName));
                        i += 1;
                    } while (ImageExists(fileName));
                }

                // Creates the thumbnail
                int thumbnailHeight = 100;
                int thumbnailWidth = Convert.ToInt32(thumbnailHeight * (image.Width / (double)image.Height));
                thumbnail = image.GetThumbnailImage(thumbnailWidth, thumbnailHeight, null, IntPtr.Zero);

                try
                {
                    // Saves the images.
                    image.Save(Path.Combine(PhysicalUploadImagePath, fileName));
                    thumbnail.Save(Path.Combine(PhysicalUploadImagePath, "Thumbnails", String.Format("thumbnail_{0}", fileName)));
                }
                catch
                {
                    throw new Exception("The files could not be saved.");
                }

                // The return string
                return fileName;
            }
            else
            {
                throw new ArgumentException("The stream didn't point to a image file.");
            }
        }

        #endregion
    }
}