using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusicLibrary.Pages.Shared
{
    public partial class Site : System.Web.UI.MasterPage
    {
        private void CleanSession()
        {
            if (Session["TempBookletContentList"] != null)
            {
                Session.Remove("TempBookletContentList");
            }
            if (Session["name"] != null)
            {
                Session.Remove("name");
            }
            if (Session["year"] != null)
            {
                Session.Remove("year");
            }
            if (Session["place"] != null)
            {
                Session.Remove("place");
            }
            if (Session["notes"] != null)
            {
                Session.Remove("notes");
            }
            if (Session["borrowedTo"] != null)
            {
                Session.Remove("borrowedTo");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// On click on "Skapa nytt nothäfte."
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CreateBookletLinkButton_Click(object sender, EventArgs e)
        {
            CleanSession();
            Response.RedirectToRoute("BookletCreate", null);
            Context.ApplicationInstance.CompleteRequest();
        }

        /// <summary>
        /// On click on "Nothäften".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BookletsLinkButton_Click(object sender, EventArgs e)
        {
            CleanSession();
            Response.RedirectToRoute("Booklets", null);
            Context.ApplicationInstance.CompleteRequest();
        }

        /// <summary>
        /// On click on "Startsida".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void StartpageLinkButton_Click(object sender, EventArgs e)
        {
            CleanSession();
            Response.RedirectToRoute("Default", null);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}