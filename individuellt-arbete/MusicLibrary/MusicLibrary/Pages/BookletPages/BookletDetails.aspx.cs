using MusicLibrary.Model.BLL;
using Resources;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;

namespace MusicLibrary.Pages.BookletPages
{
    public partial class BookletDetails : System.Web.UI.Page
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

        /// <summary>
        /// Returns the id stored in the URL.
        /// </summary>
        private int BookletID
        {
            get
            {
                return int.Parse(Page.RouteData.Values["id"].ToString());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Show success message if there is one.
            if (Session["SuccessMessage"] != null)
            {
                SuccessMessagePlaceHolder.Visible = true;
                SuccessMessageLiteral.Text = Session["SuccessMessage"].ToString();
                Session.Remove("SuccessMessage");
            }
        }

        /// <summary>
        /// SelectMethod for BookletdetailFormView.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Booklet-object</returns>
        public Booklet BookletDetailFormView_GetItem([RouteData]int id)
        {
            try
            {
                return Service.GetBooklet(id);
            }
            catch
            {
                Page.ModelState.AddModelError(String.Empty, String.Format(Strings.SelectBookletRecordByIDErrorSwedish, id));
                return null;
            }
        }

        /// <summary>
        /// Creates a text presentation of the Booklet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BookletDetailFormView_DataBound(object sender, EventArgs e)
        {
            // Marks out default field-values in ReadOnly-mode.
            Literal literal = Page.FindControl("yearOfPublicationLiteral") as Literal;
            if (literal != null && literal.Text == "1")
            {
                literal.Text = "<span class=\"defaultValue\">Okänt publiseringsår</span>";
            }
            literal = Page.FindControl("placeLiteral") as Literal;
            if (literal != null && literal.Text == "ZZ0000")
            {
                literal.Text = "<span class=\"defaultValue\">Ingen hyllplats</span>";
            }
        }

        /// <summary>
        /// SelectMethod for BookletPiecesListView.
        /// </summary>
        /// <returns>A collection of BookletContent-objects.</returns>
        public IEnumerable<BookletContent> BookletPiecesListView_GetData()
        {
            try
            {
                return Service.GetBookletContentsByBookletID(BookletID);
            }
            catch
            {
                Page.ModelState.AddModelError(String.Empty, Strings.SelectBookletContentRecordsErrorSwedish);
                return null;
            }
        }

        /// <summary>
        /// Creates a text presentation of the BookletContent-objects in BookletPiecesListView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BookletPiecesListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                BookletContent bc = e.Item.DataItem as BookletContent;
                IEnumerable<Piece> pieces = Service.GetPieces();
                Piece piece = pieces.FirstOrDefault(p => p.PieceID == bc.PieceID);

                Literal literal = e.Item.FindControl("composerLiteral") as Literal;
                if (literal != null)
                {
                    literal.Text = "Kompositör";
                }
                literal = e.Item.FindControl("catalogueNumberLiteral") as Literal;
                if (literal != null && literal.Text == "Inget nummer")
                {
                    literal.Text = "<span class=\"defaultValue\">Inget nummer</span>";
                }
                literal = e.Item.FindControl("nameLiteral") as Literal;
                if (literal != null)
                {
                    literal.Text = piece.Name;
                }
                literal = e.Item.FindControl("catalogueNumberLiteral") as Literal;
                if (literal != null)
                {
                    if (piece.CatalogueNumber == "Inget nummer")
                    {
                        literal.Text = "<span class=\"defaultValue\">Inget nummer</span>";
                    }
                    else
                    {
                        literal.Text = piece.CatalogueNumber;
                    }
                }
                literal = e.Item.FindControl("scaleLiteral") as Literal;
                if (literal != null)
                {
                    literal.Text = "Tonart";
                }
                literal = e.Item.FindControl("instrumentsLiteral") as Literal;
                if (literal != null)
                {
                    literal.Text = "Instrument, Instrument, Instrument";
                }
                literal = e.Item.FindControl("genreLiteral") as Literal;
                if (literal != null)
                {
                    literal.Text = "Genre";
                }
            }
            catch
            {
                Page.ModelState.AddModelError(String.Empty, Strings.GeneralErrorSwedish);
            }
        }
    }
}