using MusicLibrary.Model.BLL;
using Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MusicLibrary.Model.DAL
{
    /// <summary>
    /// Select, update, delete and insert records in table appSchema.Booklet.
    /// </summary>
    public class BookletDAL : DALBase
    {
        /// <summary>
        /// Delete a record from table appSchema.Booklet.
        /// </summary>
        /// <param name="bookletID">Record to be deleted.</param>
        public void DeleteBooklet(int bookletID)
        {
            using (var con = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("appSchema.usp_DeleteBookletByID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@BookletID", SqlDbType.Int, 4).Value = bookletID;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException(Strings.DeleteRecordError);
                }
            }
        }

        /// <summary>
        /// Select and return a record from table appSchema.Booklet.
        /// </summary>
        /// <param name="bookletID">Record to be selected.</param>
        /// <returns>An instance of MusicLibrary.Model.BLL.Booklet.</returns>
        public Booklet GetBookletByID(int bookletID)
        {
            using (var con = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("appSchema.usp_GetBookletByID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@BookletID", SqlDbType.Int, 4).Value = bookletID;

                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var bookletIDIndex = reader.GetOrdinal("BookletID");
                        var nameIndex = reader.GetOrdinal("Name");
                        var publisherIDIndex = reader.GetOrdinal("PublisherID");
                        var yearOfPublicationIndex = reader.GetOrdinal("YearOfPublication");
                        var placeIndex = reader.GetOrdinal("Place");

                        if (reader.Read())
                        {
                            return new Booklet
                            {
                                BookletID = reader.GetInt32(bookletIDIndex),
                                Name = reader.GetString(nameIndex),
                                PublisherID = reader.GetInt16(publisherIDIndex),
                                YearOfPublication = reader.GetDateTime(yearOfPublicationIndex),
                                Place = reader.GetString(placeIndex)
                            };
                        }

                        return null;
                    }
                }
                catch
                {
                    throw new ApplicationException(Strings.SelectRecordError);
                }
            }
        }

        /// <summary>
        /// Select and return all records from table appSchema.Booklet.
        /// </summary>
        /// <returns>A collection of instances of MusicLibrary.Model.BLL.Booklet.</returns>
        public IEnumerable<Booklet> GetBooklets()
        {
            using (var con = CreateConnection())
            {
                try
                {
                    var booklets = new List<Booklet>(100);
                    var cmd = new SqlCommand("appSchema.usp_GetBooklets", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var bookletIDIndex = reader.GetOrdinal("BookletID");
                        var nameIndex = reader.GetOrdinal("Name");
                        var publisherIDIndex = reader.GetOrdinal("PublisherID");
                        var yearOfPublicationIndex = reader.GetOrdinal("YearOfPublication");
                        var placeIndex = reader.GetOrdinal("Place");

                        while (reader.Read())
                        {
                            booklets.Add(new Booklet
                            {
                                BookletID = reader.GetInt32(bookletIDIndex),
                                Name = reader.GetString(nameIndex),
                                PublisherID = reader.GetInt16(publisherIDIndex),
                                YearOfPublication = reader.GetDateTime(yearOfPublicationIndex),
                                Place = reader.GetString(placeIndex)
                            });
                        }
                    }

                    booklets.TrimExcess();
                    return booklets;
                }
                catch
                {
                    throw new ApplicationException(Strings.SelectRecordError);
                }
            }
        }

        /// <summary>
        /// Select and return records page wise from table appSchema.Booklet.
        /// </summary>
        /// <param name="startRowIndex">Start Row Index of the page.</param>
        /// <param name="maximumRows">Maximum number of Rows on the page.</param>
        /// <param name="totalRowCount">Total number of rows in table appSchema.Booklet.</param>
        /// <returns>A collection of instances of MusicLibrary.Model.BLL.Booklet.</returns>
        public IEnumerable<Booklet> GetBooklets(int startRowIndex, int maximumRows, out int totalRowCount)
        {
            using (var con = CreateConnection())
            {
                try
                {
                    var booklets = new List<Booklet>(100);
                    var cmd = new SqlCommand("appSchema.usp_GetBookletsPageWise", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@startRowIndex", SqlDbType.Int, 4).Value = startRowIndex;
                    cmd.Parameters.Add("@maximumRows", SqlDbType.Int, 4).Value = maximumRows;
                    cmd.Parameters.Add("@totalRowCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var bookletIDIndex = reader.GetOrdinal("BookletID");
                        var nameIndex = reader.GetOrdinal("Name");
                        var publisherIDIndex = reader.GetOrdinal("PublisherID");
                        var yearOfPublicationIndex = reader.GetOrdinal("YearOfPublication");
                        var placeIndex = reader.GetOrdinal("Place");

                        while (reader.Read())
                        {
                            booklets.Add(new Booklet
                            {
                                BookletID = reader.GetInt32(bookletIDIndex),
                                Name = reader.GetString(nameIndex),
                                PublisherID = reader.GetInt16(publisherIDIndex),
                                YearOfPublication = reader.GetDateTime(yearOfPublicationIndex),
                                Place = reader.GetString(placeIndex)
                            });
                        }
                    }

                    totalRowCount = (int)cmd.Parameters["@totalRowCount"].Value;
                    booklets.TrimExcess();
                    return booklets;
                }
                catch
                {
                    throw new ApplicationException(Strings.SelectRecordError);
                }
            }
        }

        /// <summary>
        /// Insert a record in table appSchema.Booklet.
        /// </summary>
        /// <param name="booklet">An instance of MusicLibrary.Model.BLL.Booklet.</param>
        public void InsertBooklet(Booklet booklet)
        {
            using (var con = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("appSchema.usp_InsertBooklet", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = booklet.Name;
                    cmd.Parameters.Add("@PublisherID", SqlDbType.SmallInt, 2).Value = booklet.PublisherID;
                    cmd.Parameters.Add("@YearOfPublication", SqlDbType.DateTime2).Value = booklet.YearOfPublication;
                    cmd.Parameters.Add("@Place", SqlDbType.Char, 6).Value = booklet.Place;
                    cmd.Parameters.Add("@BookletID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    booklet.BookletID = (int)cmd.Parameters["@BookletID"].Value;
                }
                catch
                {
                    throw new ApplicationException(Strings.InsertRecordError);
                }
            }
        }

        /// <summary>
        /// Update a record in table appSchema.Booklet.
        /// </summary>
        /// <param name="booklet">An instance of MusicLibrary.Model.BLL.Booklet.</param>
        public void UpdateBooklet(Booklet booklet)
        {
            using (var con = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("appSchema.usp_UpdateBooklet", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@BookletID", SqlDbType.Int, 4).Value = booklet.BookletID;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = booklet.Name;
                    cmd.Parameters.Add("@PublisherID", SqlDbType.SmallInt, 2).Value = booklet.PublisherID;
                    cmd.Parameters.Add("@YearOfPublication", SqlDbType.DateTime2).Value = booklet.YearOfPublication;
                    cmd.Parameters.Add("@Place", SqlDbType.Char, 6).Value = booklet.Place;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException(Strings.UpdateRecordError);
                }
            }
        }
    }
}