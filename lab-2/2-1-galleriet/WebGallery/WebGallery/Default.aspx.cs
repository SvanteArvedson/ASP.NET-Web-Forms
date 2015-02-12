using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebGallery.Model;

namespace WebGallery
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If a right message shoud be shown, Session[UploadedFile] exists.
            if (Session["UploadedFile"] != null)
            {
                // Display the RightMessageDiv
                RightMessageLabel.Text = String.Format(RightMessageLabel.Text, Session["UploadedFile"]);
                RightMessagePlaceHolder.Visible = true;
                Session.Remove("UploadedFile");
            }

            // If the url doesn't have a image parameter or the image parameter is invalid, the first pictures is shown.
            // If the parameter is valid, that image is shown.
            if (Request.QueryString["image"] != null)
            {
                if (Gallery.ImageExists(Request.QueryString["image"]))
                {
                    BigImage.ImageUrl = String.Format("Content/Images/{0}", Request.QueryString["image"]);
                    RightMessageCloseAnchor.NavigateUrl = String.Format("~/?images={0}", Request.QueryString["image"]);
                }
                else
                {
                    try
                    {
                        Response.Redirect(String.Format("~/?image={0}", ((List<string>)new Gallery().GetImagesNames())[0]));
                    }
                    catch
                    {
                        BigImagePlaceHolder.Visible = false;
                        NoImagesPlaceHolder.Visible = true;
                    }
                }
            }
            else
            {
                try
                {
                    Response.Redirect(String.Format("~/?image={0}", ((List<string>)new Gallery().GetImagesNames())[0]));
                }
                catch
                {
                    BigImagePlaceHolder.Visible = false;
                    NoImagesPlaceHolder.Visible = true;
                }
            }
        }

        // Returns the Items for the Repeater control.
        public IEnumerable<ImageNames> ThumbnailRepeater_GetData()
        {
            List<ImageNames> imageUrlList = (List<ImageNames>)new Gallery().GetAllImagesNames();

            for (var i = 0; i < imageUrlList.Count; i += 1)
            {
                if (String.Format("Content/Images/{0}", imageUrlList[i].imageName) == BigImage.ImageUrl)
                {
                    imageUrlList[i].cssClass = "Selected";
                }

                imageUrlList[i].imageName = String.Format("~/?image={0}", imageUrlList[i].imageName);
                imageUrlList[i].thumbnailImageName = String.Format("Content/Images/Thumbnails/{0}", imageUrlList[i].thumbnailImageName);
            }
            return imageUrlList;
        }

        // Handler for the UploadFile-button.
        protected void UploadFileButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    var fileName = new Gallery().SaveImage(FileUpload1.FileContent, FileUpload1.FileName);
                    Session["UploadedFile"] = fileName;
                    Response.Redirect(String.Format("~/?image={0}", fileName));
                }
                catch
                {
                    var validator = new CustomValidator
                    {
                        IsValid = false,
                        ErrorMessage = "Ett fel inträffade då bilden skulle överföras."
                    };
                    Page.Validators.Add(validator);
                }
            }
        }
    }
}