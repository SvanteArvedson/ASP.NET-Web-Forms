using Adventurous_Contacts.Model.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Adventurous_Contacts.Model.DAL
{
    public class ContactDAL : DALBase
    {
        /// <summary>
        /// Deletes a record.
        /// </summary>
        /// <param name="contactId">The id of the record that should be deleted.</param>
        public void DeleteContact(int contactId)
        {
            using (var con = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Person.uspRemoveContact", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contactId;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("An error occured during the connection with the database.");
                }
            }
        }

        /// <summary>
        /// Selects a record.
        /// </summary>
        /// <param name="contactId">The id of the record that should be selected.</param>
        /// <returns>A Contact-instance with the data from the selected record or null.</returns>
        public Contact GetContactById(int contactId)
        {
            using (var con = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Person.uspGetContact", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contactId;

                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var contactIdIndex = reader.GetOrdinal("ContactID");
                        var firstNameIndex = reader.GetOrdinal("FirstName");
                        var lastNameIndex = reader.GetOrdinal("LastName");
                        var emailAddressIndex = reader.GetOrdinal("EmailAddress");

                        if (reader.Read())
                        {
                            return new Contact
                            {
                                ContactId = reader.GetInt32(contactIdIndex),
                                FirstName = reader.GetString(firstNameIndex),
                                LastName = reader.GetString(lastNameIndex),
                                EmailAddress = reader.GetString(emailAddressIndex)
                            };
                        }

                        return null;
                    }
                }
                catch
                {
                    throw new ApplicationException("An error occured during the connection with the database.");
                }
            }
        }

        /// <summary>
        /// Selects all records.
        /// </summary>
        /// <returns>A collection with the data from the selected records.</returns>
        public IEnumerable<Contact> GetContacts()
        {
            using (var con = CreateConnection())
            {
                try
                {
                    var contacts = new List<Contact>(100);
                    var cmd = new SqlCommand("Person.uspGetContacts", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var contactIdIndex = reader.GetOrdinal("ContactID");
                        var firstNameIndex = reader.GetOrdinal("FirstName");
                        var lastNameIndex = reader.GetOrdinal("LastName");
                        var emailAddressIndex = reader.GetOrdinal("EmailAddress");

                        while (reader.Read())
                        {
                            contacts.Add(new Contact
                            {
                                ContactId = reader.GetInt32(contactIdIndex),
                                FirstName = reader.GetString(firstNameIndex),
                                LastName = reader.GetString(lastNameIndex),
                                EmailAddress = reader.GetString(emailAddressIndex)
                            });
                        }
                    }

                    contacts.TrimExcess();
                    return contacts;
                }
                catch
                {
                    throw new ApplicationException("An error occured during the connection with the database.");
                }
            }
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
            using (var con = CreateConnection())
            {
                try
                {
                    var contacts = new List<Contact>(100);
                    var cmd = new SqlCommand("Person.uspGetContactsPageWise", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = (double)startRowIndex / maximumRows + 1;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var contactIdIndex = reader.GetOrdinal("ContactID");
                        var firstNameIndex = reader.GetOrdinal("FirstName");
                        var lastNameIndex = reader.GetOrdinal("LastName");
                        var emailAddressIndex = reader.GetOrdinal("EmailAddress");

                        while (reader.Read())
                        {
                            contacts.Add(new Contact
                            {
                                ContactId = reader.GetInt32(contactIdIndex),
                                FirstName = reader.GetString(firstNameIndex),
                                LastName = reader.GetString(lastNameIndex),
                                EmailAddress = reader.GetString(emailAddressIndex)
                            });
                        }
                    }

                    totalRowCount = (int)cmd.Parameters["@RecordCount"].Value;
                    contacts.TrimExcess();
                    return contacts;
                }
                catch
                {
                    throw new ApplicationException("An error occured during the connection with the database.");
                }
            }
        }

        /// <summary>
        /// Inserts a new record.
        /// </summary>
        /// <param name="contact">The record to be inserted.</param>
        public void InsertContact(Contact contact)
        {
            using (var con = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Person.uspAddContact", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = contact.LastName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 50).Value = contact.EmailAddress;
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    contact.ContactId = (int)cmd.Parameters["@ContactID"].Value;
                }
                catch
                {
                    throw new ApplicationException("An error occured during the connection with the database.");
                }
            }
        }

        /// <summary>
        /// Updates a record.
        /// </summary>
        /// <param name="contact">The record to be updated.</param>
        public void UpdateContact(Contact contact)
        {
            using (var con = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Person.uspUpdateContact", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contact.ContactId;
                    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = contact.LastName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 50).Value = contact.EmailAddress;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("An error occured during the connection with the database.");
                }
            }
        }
    }
}