using MusicLibrary.Model.BLL;
using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusicLibrary.Pages.BookletPages
{
    public partial class BookletCreate : System.Web.UI.Page
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

        /// <summary>
        /// Shows success messages and saves textbox-values between postbacks.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Show success message if there is one.
            if (Session["SuccessMessage"] != null)
            {
                SuccessMessagePlaceHolder.Visible = true;
                SuccessMessageLiteral.Text = Session["SuccessMessage"].ToString();
                Session.Remove("SuccessMessage");
            }

            // Saves TextBox-values between postbacks.
            if (IsPostBack)
            {
                TextBox textBox = (TextBox)BookletCreateFormView.FindControl("nameTextBox");
                string txt = textBox.Text;
                Session["name"] = txt;

                textBox = (TextBox)BookletCreateFormView.FindControl("yearOfPublicationTextBox");
                txt = textBox.Text;
                Session["year"] = txt;

                textBox = (TextBox)BookletCreateFormView.FindControl("placeTextBox");
                txt = textBox.Text;
                Session["place"] = txt;

                textBox = (TextBox)BookletCreateFormView.FindControl("notesTextBox");
                txt = textBox.Text;
                Session["notes"] = txt;

                textBox = (TextBox)BookletCreateFormView.FindControl("borrowedToTextBox");
                txt = textBox.Text;
                Session["borrowedTo"] = txt;
            }
        }

        /// <summary>
        /// SelectMethod for BookletContentListView.
        /// </summary>
        /// <returns>A collection of BookletContent items.</returns>
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
        /// SelectMethod for BookletContentListView_PieceDropDownList.
        /// </summary>
        /// <returns>A collection of Piece items.</returns>
        public IEnumerable<Piece> PieceDropDownList_GetData()
        {
            try
            {
                // Only the pieces that isn't already in the booklet content list should be returned.
                IEnumerable<Piece> allPieces = Service.GetPieces();
                if (TempBookletContentList == null)
                {
                    TempBookletContentList = new List<BookletContent>(100);
                }
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
        /// After the BookletContentListView has bound the data, this method creates a text presentation of the pieces.
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
        /// InsertMethod for BookletCreateFormView
        /// </summary>
        /// <param name="booklet">The booklet to be saved.</param>
        public void BookletCreateFromView_InsertItem(Booklet booklet)
        {
            if (ModelState.IsValid)
            {
                // Before updating the YearOfPublication-field in the record, the input in yearOfPublicationTextBox is 
                // converted to a DateTime-object.
                TextBox yearTextBox = (TextBox)BookletCreateFormView.FindControl("yearOfPublicationTextBox");
                booklet.YearOfPublication = new DateTime((Convert.ToInt32(yearTextBox.Text)), 1, 1);
                // For the moment, no data binding is activated on the publisherDropDownList.
                booklet.PublisherID = 1;

                // Booklet must validate and contain at least one piece.
                List<BookletContent> bookletContent = (List<BookletContent>)Session["TempBookletContentList"];
                ICollection<ValidationResult> validationResults = new List<ValidationResult>();
                if (booklet.Validate(out validationResults) && bookletContent.Count >= 1)
                {
                    try
                    {
                        Service.SaveBooklet(booklet);
                        // Saving content list
                        for (int i = 0; i < bookletContent.Count; i += 1)
                        {
                            bookletContent[i].BookletID = booklet.BookletID;
                            Service.SaveBookletContent(bookletContent[i]);
                        }
                        // If success, a success-message is shown.
                        CleanSession();
                        Session["SuccessMessage"] = String.Format(Strings.InsertNewBookletSuccessSwedish);
                        Response.RedirectToRoute("BookletDetails", new { id = booklet.BookletID });
                        Context.ApplicationInstance.CompleteRequest();
                    }
                    catch
                    {
                        Page.ModelState.AddModelError(String.Empty, Strings.InsertNewBookletErrorSwedish);
                        return;
                    }
                }
                else
                {
                    if (bookletContent.Count < 1)
                    {
                        Page.ModelState.AddModelError(String.Empty, Strings.InsertUpdateBookletNoContentErrorSwedish);
                    }
                    else
                    {
                        Page.ModelState.AddModelError(String.Empty, Strings.InsertNewBookletErrorSwedish);
                    }
                }
            }
        }

        /// <summary>
        /// InsertMethod for BookletCreateFormView_BookletPieceListView.
        /// </summary>
        /// <param name="bookletContent">The BookletContent to be saved.</param>
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

                    Response.Redirect(GetRouteUrl("BookletCreate", null));
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch
                {
                    Page.ModelState.AddModelError(String.Empty, Strings.InsertContentRecordErrorSwedish);
                }
            }
        }

        /// <summary>
        /// DeleteMethod for BookletFormView_BookletPieceListView.
        /// </summary>
        /// <param name="pieceID">The pieceID for the BookletContent that should be removed from the content list.</param>
        public void BookletPiecesListView_DeleteItem(int pieceID)
        {
            try
            {
                TempBookletContentList.RemoveAll(p => p.PieceID == pieceID);
                Session["SuccessMessage"] = Strings.DeleteContentRecordSuccessSwedish;

                Response.Redirect(GetRouteUrl("BookletCreate", null));
                Context.ApplicationInstance.CompleteRequest();
            }
            catch
            {
                Page.ModelState.AddModelError(String.Empty, Strings.DeleteContentRecordErrorSwedish);
            }
        }

        /// <summary>
        /// Before the BookletCreateFormView is rendered, this method recreates the textbox 
        /// values that came with the postback.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BookletCreateFromView_PreRender(object sender, EventArgs e)
        {
            if (Session["name"] != null)
            {
                TextBox nameTextBox = (TextBox)BookletCreateFormView.FindControl("nameTextBox");
                nameTextBox.Text = Session["name"].ToString();
                Session.Remove("name");
            }
            if (Session["year"] != null)
            {
                TextBox yearTextBox = (TextBox)BookletCreateFormView.FindControl("yearOfPublicationTextBox");
                yearTextBox.Text = Session["year"].ToString();
                Session.Remove("year");
            }
            if (Session["place"] != null)
            {
                TextBox placeTextBox = (TextBox)BookletCreateFormView.FindControl("placeTextBox");
                placeTextBox.Text = Session["place"].ToString();
                Session.Remove("place");
            }
            if (Session["notes"] != null)
            {
                TextBox notesTextBox = (TextBox)BookletCreateFormView.FindControl("notesTextBox");
                notesTextBox.Text = Session["notes"].ToString();
                Session.Remove("notes");
            }
            if (Session["borrowedTo"] != null)
            {
                TextBox borrowedToTextBox = (TextBox)BookletCreateFormView.FindControl("borrowedToTextBox");
                borrowedToTextBox.Text = Session["borrowedTo"].ToString();
                Session.Remove("borrowedTo");
            }
        }

        /// <summary>
        /// This method create text presentations of the pieces in the PieceDropDownList.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PieceDropDownList_PreRender(object sender, EventArgs e)
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
        /// This method creates the Listitems in the PublisherDropDownList.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PublisherDropDownList_PreRender(object sender, EventArgs e)
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
        /// Activates if user cancel without saving the new booklet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelLinkButton_Click(object sender, EventArgs e)
        {
            CleanSession();
            Response.RedirectToRoute("Booklets", null);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}