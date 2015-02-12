using MusicLibrary.Model.BLL;
using Resources;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusicLibrary.Pages.BookletPages
{
    public partial class BookletsList : System.Web.UI.Page
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
            if (Session["SuccessMessage"] != null)
            {
                PlaceHolder successMessage = (PlaceHolder)ListViewBooklets.FindControl("SuccessMessagePlaceHolder");
                successMessage.Visible = true;
                Literal successMassageLiteral = (Literal)ListViewBooklets.FindControl("SuccessMessageLiteral");
                successMassageLiteral.Text = Session["SuccessMessage"].ToString();

                Session.Remove("SuccessMessage");
            }
        }

        /// <summary>
        /// SelectMethod for ListViewBooklets.
        /// </summary>
        /// <param name="maximumRows"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="totalRowCount"></param>
        /// <returns>A collection of Booklet-objects.</returns>
        public IEnumerable<Booklet> ListViewBooklets_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            try
            {
                return Service.GetBooklets(startRowIndex, maximumRows, out totalRowCount);
            }
            catch
            {
                Page.ModelState.AddModelError(String.Empty, Strings.SelectBookletsErrorSwedish);
                totalRowCount = 0;
                return null;
            }
        }

        /// <summary>
        /// After the dataitem has been bound this methods checks for default-values and mark
        /// them with font-style italic.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ListViewBooklets_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Literal literal = e.Item.FindControl("yearOfPubliationLabel") as Literal;
            if (literal != null && literal.Text == "1")
            {
                literal.Text = "<span class=\"defaultValue\">Okänt publiseringsår</span>";
            }
            literal = e.Item.FindControl("placeLabel") as Literal;
            if (literal != null && literal.Text == "ZZ0000")
            {
                literal.Text = "<span class=\"defaultValue\">Ingen hyllplats</span>";
            }
        }

        /// <summary>
        /// When user change page, this method update the DataPager properties.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ListViewBooklets_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            ((DataPager)ListViewBooklets.FindControl("BookletsDataPager")).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        }
    }
}