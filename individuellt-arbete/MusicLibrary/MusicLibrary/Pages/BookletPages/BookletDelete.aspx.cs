using MusicLibrary.Model.BLL;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusicLibrary.Pages.BookletPages
{
    public partial class BookletDelete : System.Web.UI.Page
    {
        /// <summary>
        /// Service-class in the application. Used to fetch and save content to the database.
        /// </summary>
        private Service _service;
        private Service Service
        {
            get
            {
                return _service ?? (_service = new Service());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int bookletID = int.Parse(RouteData.Values["id"].ToString());
                Booklet booklet = Service.GetBooklet((bookletID));
                ConfirmLiteral.Text = String.Format(ConfirmLiteral.Text, "<span class=\"title\">" + booklet.Name + "</span>");
            }
            catch
            {
                ConfirmPlaceHolder.Visible = false;
                BackPlaceHolder.Visible = true;
                Page.ModelState.AddModelError(String.Empty, String.Format(Strings.SelectBookletRecordByIDErrorSwedish, RouteData.Values["id"]));
            }
        }

        protected void YesLinkButton_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int bookletID = int.Parse(e.CommandArgument.ToString());
                Service.DeleteBooklet(bookletID);

                // If success, a success message is shown
                Session["SuccessMessage"] = String.Format(Strings.DeleteBookletSuccessSwedish);
                Response.RedirectToRoute("Booklets", null);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch
            {
                Page.ModelState.AddModelError(String.Empty, Strings.DeleteBookletErrorSwedish);
            }
        }
    }
}