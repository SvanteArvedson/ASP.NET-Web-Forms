
namespace WebGallery.Model
{
    public class ImageNames
    {
        public string imageName { get; set; }
        public string thumbnailImageName { get; set; }
        public string cssClass { get; set; }

        /// <summary>
        /// Create a new instance of ImageNames.
        /// </summary>
        /// <param name="image">Name of the full size image</param>
        /// <param name="thumbnail">Name of the thumbnail image</param>
        /// <param name="className">Image CssClassName</param>
        public ImageNames(string image, string thumbnail, string className) 
        {
            imageName = image;
            thumbnailImageName = thumbnail;
            cssClass = className;
        }
    }
}