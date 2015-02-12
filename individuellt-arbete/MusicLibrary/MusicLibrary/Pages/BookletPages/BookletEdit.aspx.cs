using MusicLibrary.Model.BLL;
using Resources;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;

namespace MusicLibrary.Pages.BookletPages
{
    public partial class BookletEdit : System.Web.UI.Page
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

        /// <summary>
        /// Temporary container for selected BookletContent.
        /// </summary>
        private List<BookletContent> TempBookletContentList
        {
            get
            {
                return Session["TempBookletContentList"] as List<BookletContent>;
            }
            set
            {
                Session["TempBookletContentList"] = value;
            }
        }

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

        /// <summary>
        /// Server validation method.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        public void CheckYear(object source, ServerValidateEventArgs args)
        {
            try
            {
                DateTime dt = new DateTime(Convert.ToInt32(args.Value), 1, 1);
                args.IsValid = dt.Year <= DateTime.Now.Year;
            }
            catch
            {
                args.IsValid = false;
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

            // ListView is working against a temporary list.
            if (TempBookletContentList == null)
            {
                TempBookletContentList = (List<BookletContent>)Service.GetBookletContentsByBookletID(BookletID);
            }

            // Saves TextBox-values between postbacks.
            if (IsPostBack)
            {
                TextBox textBox = (TextBox)BookletEditFormView.FindControl("nameTextBox");
                string txt = textBox.Text;
                Session["name"] = txt;

                textBox = (TextBox)BookletEditFormView.FindControl("yearOfPublicationTextBox");
                txt = textBox.Text;
                Session["year"] = txt;

                textBox = (TextBox)BookletEditFormView.FindControl("placeTextBox");
                txt = textBox.Text;
                Session["place"] = txt;

                textBox = (TextBox)BookletEditFormView.FindControl("notesTextBox");
                txt = textBox.Text;
                Session["notes"] = txt;

                textBox = (TextBox)BookletEditFormView.FindControl("borrowedToTextBox");
                txt = textBox.Text;
                Session["borrowedTo"] = txt;
            }
        }

        /// <summary>
        /// SelectMethod for BookletEditFormView.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Booklet-object</returns>
        public Booklet BookletEditFormView_GetItem([RouteData]int id)
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
        /// SelectMethod for BookletpieceListView.
        /// </summary>
        /// <returns>A collection of BookletContent-objects.</returns>
        public IEnumerable<BookletContent> BookletPiecesListView_GetData()
        {
            try
            {
                return (IEnumerable<BookletContent>)TempBookletContentList;
            }
            catch
            {
                Page.ModelState.AddModelError(String.Empty, Strings.SelectBookletContentRecordsErrorSwedish);
                return null;
            }
        }

        /// <summary>
        /// SelectMethod for PieceDropDownList.
        /// </summary>
        /// <returns>A collection of Piece-objects.</returns>
        public IEnumerable<Piece> PieceDropDownList_GetData()
        {
            try
            {
                // Only the pieces that isn't already in the booklet content list should be returned.
                IEnumerable<Piece> allPieces = Service.GetPieces();
                IEnumerable<BookletContent> bookletContentsInBooklet = (IEnumerable<BookletContent>)TempBookletContentList;

                List<int> li = bookletContentsInBooklet.Select(bc => bc.PieceID).ToList();
                return (from p in allPieces where !(li).Contains(p.PieceID) select p).ToList();
            }
            catch
            {
                Page.ModelState.AddModelError(String.Empty, Strings.SelectPiecesToDropDownListErrorSwedish);
                return null;
            }
        }

        /// <summary>
        /// Makes a text presentation of the pieces in BookletPieceListView.
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

        /// <summary>
        /// UpdateMethod for BookletEditFormView.
        /// </summary>
        /// <param name="bookletID">ID of the booklet-object to be updated.</param>
        public void BookletEditFormView_UpdateItem(int bookletID)
        {
            try
            {
                Booklet booklet = Service.GetBooklet(bookletID);
                if (booklet == null)
                {
                    Page.ModelState.AddModelError(String.Empty, String.Format(Strings.SelectBookletRecordByIDErrorSwedish, bookletID));
                    return;
                }

                // Booklet must conatin at least one piece
                if (TempBookletContentList.Count < 1)
                {
                    Page.ModelState.AddModelError(String.Empty, Strings.InsertUpdateBookletNoContentErrorSwedish);
                    return;
                }
                // Before updating the YearOfPublication-field in the record, the input in yearOfPublicationTextBox is 
                // converted to a DateTime-object.
                TextBox yearTextBox = (TextBox)BookletEditFormView.FindControl("yearOfPublicationTextBox");
                booklet.YearOfPublication = new DateTime((Convert.ToInt32(yearTextBox.Text)), 1, 1);

                if (TryUpdateModel(booklet))
                {
                    Service.SaveBooklet(booklet);

                    // Saving the new content.
                    Service.DeleteBookletContent(booklet.BookletID);
                    for (int i = 0; i < TempBookletContentList.Count; i += 1)
                    {
                        TempBookletContentList[i].BookletID = booklet.BookletID;
                        Service.SaveBookletContent(TempBookletContentList[i]);
                    }

                    CleanSession();
                    Session["SuccessMessage"] = String.Format(Strings.UpdateBookletRecordSuccessSwedish);
                    Response.RedirectToRoute("BookletDetails", new { id = booklet.BookletID });
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception)
            {
                Page.ModelState.AddModelError(String.Empty, Strings.UpdateBookletRecordErrorSwedish);
            }
        }

        /// <summary>
        /// DeleteMethod for BookletPieceListView.
        /// </summary>
        /// <param name="pieceID">ID of the piece to be deleted.</param>
        public void BookletPiecesListView_DeleteItem(int pieceID)
        {
            try
            {
                TempBookletContentList.RemoveAll(p => p.PieceID == pieceID);
                Session["SuccessMessage"] = Strings.DeleteContentRecordSuccessSwedish;

                Response.Redirect(GetRouteUrl("BookletEdit", BookletID));
                Context.ApplicationInstance.CompleteRequest();
            }
            catch
            {
                Page.ModelState.AddModelError(String.Empty, Strings.DeleteContentRecordErrorSwedish);
            }
        }

        /// <summary>
        /// InsertMethod for BookletPiecesListView.
        /// </summary>
        /// <param name="bookletContent">The object to be inserted.</param>
        public void BookletPiecesListView_InsertItem(BookletContent bookletContent)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Only PieceID is Bound from the control.
                    bookletContent.BookletID = 0;

                    TempBookletContentList.Add(bookletContent);
                    Session["SuccessMessage"] = Strings.InsertContentRecordSuccessSwedish;

                    Response.Redirect(GetRouteUrl("BookletEdit", BookletID));
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch
                {
                    Page.ModelState.AddModelError(String.Empty, Strings.InsertContentRecordErrorSwedish);
                }
            }
        }

        /// <summary>
        /// Recreates the values in the textboses.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BookletEditFormView_PreRender(object sender, EventArgs e)
        {
            if (Session["name"] != null)
            {
                TextBox nameTextBox = (TextBox)BookletEditFormView.FindControl("nameTextBox");
                nameTextBox.Text = Session["name"].ToString();
                Session.Remove("name");
            }
            if (Session["year"] != null)
            {
                TextBox yearTextBox = (TextBox)BookletEditFormView.FindControl("yearOfPublicationTextBox");
                yearTextBox.Text = Session["year"].ToString();
                Session.Remove("year");
            }
            if (Session["place"] != null)
            {
                TextBox placeTextBox = (TextBox)BookletEditFormView.FindControl("placeTextBox");
                placeTextBox.Text = Session["place"].ToString();
                Session.Remove("place");
            }
            if (Session["notes"] != null)
            {
                TextBox notesTextBox = (TextBox)BookletEditFormView.FindControl("notesTextBox");
                notesTextBox.Text = Session["notes"].ToString();
                Session.Remove("notes");
            }
            if (Session["borrowedTo"] != null)
            {
                TextBox borrowedToTextBox = (TextBox)BookletEditFormView.FindControl("borrowedToTextBox");
                borrowedToTextBox.Text = Session["borrowedTo"].ToString();
                Session.Remove("borrowedTo");
            }
        }

        /// <summary>
        /// Make a text presentation of the pie´ce-objects in the PieceDropDownList.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PieceDropDownList_PreRender(object sender, EventArgs e)
        {
            try
            {
                DropDownList dp = (DropDownList)sender;
                IEnumerable<Piece> pieces = Service.GetPieces();
                // For the moment the values for composer, scale and genre are static.
                for (int i = 0; i < dp.Items.Count; i += 1)
                {
                    Piece piece = pieces.FirstOrDefault(p => p.PieceID.ToString() == dp.Items[i].Text);
                    dp.Items[i].Text = String.Format("{0}, {1}, {2}, {3}, {4}", "Kompositör", piece.Name, "Tonart", piece.CatalogueNumber, "Genre");
                }
            }
            catch
            {
                Page.ModelState.AddModelError(String.Empty, Strings.GeneralErrorSwedish);
            }
        }

        /// <summary>
        /// Creates values for the publisherdropDownList.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void publisherDropDownList_PreRender(object sender, EventArgs e)
        {
            DropDownList dp = (DropDownList)sender;

            for (int i = 1; i <= 4; i += 1)
            {
                dp.Items.Add(new ListItem
                {
                    Text = String.Format("Förlag {0}", i),
                    Value = "1"
                });
            }
        }

        /// <summary>
        /// Called if user clicks on "Cancel"-button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelLinkButton_Click(object sender, EventArgs e)
        {
            CleanSession();
            Response.RedirectToRoute("BookletDetails", BookletID);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}