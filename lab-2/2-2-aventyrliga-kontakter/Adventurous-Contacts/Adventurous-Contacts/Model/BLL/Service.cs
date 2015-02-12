using Adventurous_Contacts.App_Infrastructure;
using Adventurous_Contacts.Model.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Adventurous_Contacts.Model.BLL
{
    /// <summary>
    /// A part of the Buissnes Logic Layer for the application "Adventurous Contacts". Contains CRUD-functionality.
    /// </summary>
    public class Service
    {
        private ContactDAL _contactDAL;

        private ContactDAL ContactDAL
        {
            get
            {
                return _contactDAL ?? (_contactDAL = new ContactDAL());
            }
        }

        /// <summary>
        /// Deletes a record.
        /// </summary>
        /// <param name="contact">The record that should be deleted.</param>
        public void DeleteContact(Contact contact)
        {
            ContactDAL.DeleteContact(contact.ContactId);
        }

        /// <summary>
        /// Deletes a record.
        /// </summary>
        /// <param name="contactId">The id of the record that should be deleted.</param>
        public void DeleteContact(int contactId)
        {
            ContactDAL.DeleteContact(contactId);
        }

        /// <summary>
        /// Selects a record.
        /// </summary>
        /// <param name="contactId">The id of the record that should be selected.</param>
        /// <returns>A Contact-instance with the data from the selected record.</returns>
        public Contact GetContact(int contactId)
        {
            return ContactDAL.GetContactById(contactId);
        }

        /// <summary>
        /// Selects all records.
        /// </summary>
        /// <returns>A collection with the data from the selected records.</returns>
        public IEnumerable<Contact> GetContacts()
        {
            return ContactDAL.GetContacts();
        }

        /// <summary>
        /// Selects records pagewise.
        /// </summary>
        /// <param name="maximumRows">Maximum number of records.</param>
        /// <param name="startRowIndex">Startindex of teh selected records.</param>
        /// <param name="totalRowCount">Out parameter with the total records in the relation.</param>
        /// <returns>A collection with the data from the selected records.</returns>
        public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return ContactDAL.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        /// <summary>
        /// Inserts a new record or to update a record.
        /// </summary>
        /// <param name="contact">The Contact-instance to be saved.</param>
        public void SaveContact(Contact contact)
        {
            ICollection<ValidationResult> validatonResults = new List<ValidationResult>();

            if (contact.Validate(out validatonResults))
            {
                if (contact.ContactId == 0)
                {
                    ContactDAL.InsertContact(contact);
                }
                else
                {
                    ContactDAL.UpdateContact(contact);
                }
            }
            else
            {
                var ex = new ApplicationException("An error occured while saveing the contact.");
                ex.Data.Add("ValidationResult", validatonResults);
                throw ex;
            }
        }
    }
}