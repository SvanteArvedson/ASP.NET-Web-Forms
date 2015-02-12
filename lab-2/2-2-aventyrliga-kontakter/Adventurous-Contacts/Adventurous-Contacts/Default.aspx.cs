using Adventurous_Contacts.Model.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adventurous_Contacts
{
    public partial class Default : System.Web.UI.Page
    {
        // Stores a referens to a Service-instance.
        private Service _service;

        // Encapsulate _service.
        private Service Service
        {
            get
            {
                return _service ?? (_service = new Service());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // If Session[Sucess] have some content, a right message is shown.
            if (Session["Success"] != null)
            {
                RightBoxPlaceHolder.Visible = true;
                RightMessage.Text = Session["Success"] as string;
                Session.Remove("Success");
            }
        }

        // Supplies ListView with data.
        public IEnumerable<Contact> ContactListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return Service.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        // When ListView is changing, this method updates the DataPager-properties.
        protected void ContactListView_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            ((DataPager)ContactListView.FindControl("ContactDataPager")).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        }

        // On click on "Spara".
        public void ContactListView_InsertItem(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Service.SaveContact(contact);
                    Session["Success"] = "Kontakten har sparats i databasen";
                    Response.Redirect("~/Default.aspx");
                }
                catch (Exception ex)
                {
                    if (ex.Data["ValidationResult"] != null)
                    {
                        foreach (ValidationResult res in ((List<ValidationResult>)ex.Data["ValidationResult"]))
                        {
                            ModelState.AddModelError(String.Empty, res.ErrorMessage);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, "Kontaktuppgifterna kunde inte sparas.");
                    }
                }
            }
        }

        // On click on "Ändra".
        public void ContactListView_UpdateItem(int contactId)
        {
            // If contactId is invalid or an error occurs while updating the contact, an error message is shown.
            var contact = Service.GetContact(contactId);
            if (contact == null)
            {
                ModelState.AddModelError(String.Empty, String.Format("Kontaktpost {0} hittades inte.", contactId));
                return;
            }
            if (TryUpdateModel(contact))
            {
                
                try
                {
                    Service.SaveContact(contact);
                    Session["Success"] = "Kontaktuppgifterna har uppdaterats.";
                    Response.Redirect("~/Default.aspx");
                }
                catch (Exception ex)
                {
                    if (ex.Data["ValidationResult"] != null)
                    {
                        foreach (ValidationResult res in ((List<ValidationResult>)ex.Data["ValidationResult"]))
                        {
                            ModelState.AddModelError(String.Empty, res.ErrorMessage);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, "Kontaktuppgifterna kunde inte sparas.");
                    }
                }
            }
        }

        // On click on "Radera".
        public void ContactListView_DeleteItem(int contactId)
        {
            try
            {
                Service.DeleteContact(contactId);
                Session["Success"] = "Kontakten har raderats.";
                Response.Redirect("~/Default.aspx");
            }
            catch
            {
                ModelState.AddModelError(String.Empty, "Kontakten kan inte raderas.");
            }
        }
    }
}