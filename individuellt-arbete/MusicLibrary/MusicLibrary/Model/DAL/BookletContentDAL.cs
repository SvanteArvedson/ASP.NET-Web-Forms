using MusicLibrary.Model.BLL;
using Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MusicLibrary.Model.DAL
{
    /// <summary>
    /// Select, delete and insert records in table appSchema.BookletContent.
    /// </summary>
    public class BookletContentDAL : DALBase
    {
        /// <summary>
        /// Delete a records from table appSchema.BookletContent.
        /// </summary>
        /// <param name="bookletID">Part of the Primary Key in the deleted records.</param>
        public void DeleteBookletContentsByBookletID(int bookletID)
        {
            using (var con = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("appSchema.usp_DeleteBookletContentsByBookletID", con);
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
        /// Select and return all records from table appSchema.BookletContent.
        /// </summary>
        /// <returns>A collection of instances of MusicLibrary.Model.BLL.BookletContent.</returns>
        public IEnumerable<BookletContent> GetBookletContents()
        {
            using (var con = CreateConnection())
            {
                try
                {
                    var bookletContents = new List<BookletContent>(100);
                    var cmd = new SqlCommand("appSchema.usp_GetBookletContents", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var bookletIDIndex = reader.GetOrdinal("BookletID");
                        var pieceIDIndex = reader.GetOrdinal("PieceID");

                        while (reader.Read())
                        {
                            bookletContents.Add(new BookletContent
                            {
                                BookletID = reader.GetInt32(bookletIDIndex),
                                PieceID = reader.GetInt32(pieceIDIndex)
                            });
                        }
                    }

                    bookletContents.TrimExcess();
                    return bookletContents;
                }
                catch
                {
                    throw new ApplicationException(Strings.SelectRecordError);
                }
            }
        }

        /// <summary>
        /// Select and return all records belonging to bookletID from table appSchema.BookletContent.
        /// </summary>
        /// <param name="bookletID">Part of the Primary Key in table BookletContent.</param>
        /// <returns>A collection of instances of MusicLibrary.Model.BLL.BookletContent.</returns>
        public IEnumerable<BookletContent> GetBookletContentsByBookletID(int bookletID) 
        {
            using (var con = CreateConnection())
            {
                try
                {
                    var bookletContents = new List<BookletContent>(100);
                    var cmd = new SqlCommand("appSchema.usp_GetBookletContentsByBookletID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@BookletID", SqlDbType.Int, 4).Value = bookletID;

                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var bookletIDIndex = reader.GetOrdinal("BookletID");
                        var pieceIDIndex = reader.GetOrdinal("PieceID");

                        while (reader.Read())
                        {
                            bookletContents.Add(new BookletContent
                            {
                                BookletID = reader.GetInt32(bookletIDIndex),
                                PieceID = reader.GetInt32(pieceIDIndex)
                            });
                        }
                    }

                    bookletContents.TrimExcess();
                    return bookletContents;
                }
                catch
                {
                    throw new ApplicationException(Strings.SelectRecordError);
                }
            }
        }

        /// <summary>
        /// Select and return all records belonging to pieceID from table appSchema.BookletContent.
        /// </summary>
        /// <param name="bookletID">Part of the Primary Key in table BookletContent.</param>
        /// <returns>A collection of instances of MusicLibrary.Model.BLL.BookletContent.</returns>
        public IEnumerable<BookletContent> GetBookletContentsByPieceID(int pieceID)
        {
            using (var con = CreateConnection())
            {
                try
                {
                    var bookletContents = new List<BookletContent>(100);
                    var cmd = new SqlCommand("appSchema.usp_GetBookletContentsByPieceID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PieceID", SqlDbType.Int, 4).Value = pieceID;

                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var bookletIDIndex = reader.GetOrdinal("BookletID");
                        var pieceIDIndex = reader.GetOrdinal("PieceID");

                        while (reader.Read())
                        {
                            bookletContents.Add(new BookletContent
                            {
                                BookletID = reader.GetInt32(bookletIDIndex),
                                PieceID = reader.GetInt32(pieceIDIndex)
                            });
                        }
                    }

                    bookletContents.TrimExcess();
                    return bookletContents;
                }
                catch
                {
                    throw new ApplicationException(Strings.SelectRecordError);
                }
            }
        }

        /// <summary>
        /// Insert a record in table appSchema.BookletContent.
        /// </summary>
        /// <param name="bookletContent">An instance of MusicLibrary.Model.BLL.BookletContent.</param>
        public void InsertBookletContent(BookletContent bookletContent)
        {
            using (var con = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("appSchema.usp_InsertBookletContent", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@BookletID", SqlDbType.Int, 4).Value = bookletContent.BookletID;
                    cmd.Parameters.Add("@PieceID", SqlDbType.Int, 4).Value = bookletContent.PieceID;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException(Strings.InsertRecordError);
                }
            }
        }
    }
}