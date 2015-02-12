using MusicLibrary.Model.BLL;
using Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MusicLibrary.Model.DAL
{
    /// <summary>
    /// Select, update, delete and insert records in table appSchema.Piece.
    /// </summary>
    public class PieceDAL : DALBase
    {
        /// <summary>
        /// Delete a record from table appSchema.Piece.
        /// </summary>
        /// <param name="bookletID">Record to be deleted.</param>
        public void DeletePiece(int pieceID)
        {
            using (var con = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("appSchema.usp_DeletePieceByID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PieceID", SqlDbType.Int, 4).Value = pieceID;

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
        /// Select and return records from table appSchema.Piece with a foreign key in table appSchema.BookletContent.
        /// </summary>
        /// <param name="bookletID">Primary key for a Booklet-object.</param>
        /// <returns>A collection of MusicLibrary.Model.BLL.Piece instances.</returns>
        public IEnumerable<Piece> GetPiecesByBookletID(int bookletID)
        {
            using (var con = CreateConnection())
            {
                try
                {
                    var pieces = new List<Piece>(100);
                    var cmd = new SqlCommand("appSchema.usp_GetPiecesByBookletID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@BookletID", SqlDbType.Int, 4).Value = bookletID;

                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var pieceIDIndex = reader.GetOrdinal("PieceID");
                        var nameIndex = reader.GetOrdinal("Name");
                        var catalogueNumberIndex = reader.GetOrdinal("CatalogueNumber");
                        var composerIDIndex = reader.GetOrdinal("ComposerID");
                        var scaleID = reader.GetOrdinal("ScaleID");
                        var genreID = reader.GetOrdinal("GenreID");
                        var yearOfCompositionIndex = reader.GetOrdinal("YearOfComposition");

                        while (reader.Read())
                        {
                            pieces.Add(new Piece
                            {
                                PieceID = reader.GetInt32(pieceIDIndex),
                                Name = reader.GetString(nameIndex),
                                CatalogueNumber = reader.GetString(catalogueNumberIndex),
                                ComposerID = reader.GetInt16(composerIDIndex),
                                ScaleID = reader.GetByte(scaleID),
                                GenreID = reader.GetByte(genreID),
                                YearOfComposition = reader.GetDateTime(yearOfCompositionIndex)
                            });
                        }
                    }

                    pieces.TrimExcess();
                    return pieces;
                }
                catch
                {
                    throw new ApplicationException(Strings.SelectRecordError);
                }
            }
        }

        /// <summary>
        /// Select and return a record from table appSchema.Piece.
        /// </summary>
        /// <param name="bookletID">Record to be selected.</param>
        /// <returns>An instance of MusicLibrary.Model.BLL.Piece.</returns>
        public Piece GetPieceByID(int pieceID)
        {
            using (var con = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("appSchema.usp_GetPieceByID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PieceID", SqlDbType.Int, 4).Value = pieceID;

                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var pieceIDIndex = reader.GetOrdinal("PieceID");
                        var nameIndex = reader.GetOrdinal("Name");
                        var catalogueNumberIndex = reader.GetOrdinal("CatalogueNumber");
                        var composerIDIndex = reader.GetOrdinal("ComposerID");
                        var scaleID = reader.GetOrdinal("ScaleID");
                        var genreID = reader.GetOrdinal("GenreID");
                        var yearOfCompositionIndex = reader.GetOrdinal("YearOfComposition");

                        if (reader.Read())
                        {
                            return new Piece
                            {
                                PieceID = reader.GetInt32(pieceIDIndex),
                                Name = reader.GetString(nameIndex),
                                CatalogueNumber = reader.GetString(catalogueNumberIndex),
                                ComposerID = reader.GetInt16(composerIDIndex),
                                ScaleID = reader.GetByte(scaleID),
                                GenreID = reader.GetByte(genreID),
                                YearOfComposition = reader.GetDateTime(yearOfCompositionIndex)
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
        /// Select and return all records from table appSchema.Piece.
        /// </summary>
        /// <returns>A collection of instances of MusicLibrary.Model.BLL.Piece.</returns>
        public IEnumerable<Piece> GetPieces()
        {
            using (var con = CreateConnection())
            {
                try
                {
                    var pieces = new List<Piece>(100);
                    var cmd = new SqlCommand("appSchema.usp_GetPieces", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var pieceIDIndex = reader.GetOrdinal("PieceID");
                        var nameIndex = reader.GetOrdinal("Name");
                        var catalogueNumberIndex = reader.GetOrdinal("CatalogueNumber");
                        var composerIDIndex = reader.GetOrdinal("ComposerID");
                        var scaleID = reader.GetOrdinal("ScaleID");
                        var genreID = reader.GetOrdinal("GenreID");
                        var yearOfCompositionIndex = reader.GetOrdinal("YearOfComposition");

                        while (reader.Read())
                        {
                            pieces.Add(new Piece
                            {
                                PieceID = reader.GetInt32(pieceIDIndex),
                                Name = reader.GetString(nameIndex),
                                CatalogueNumber = reader.GetString(catalogueNumberIndex),
                                ComposerID = reader.GetInt16(composerIDIndex),
                                ScaleID = reader.GetByte(scaleID),
                                GenreID = reader.GetByte(genreID),
                                YearOfComposition = reader.GetDateTime(yearOfCompositionIndex)
                            });
                        }
                    }

                    pieces.TrimExcess();
                    return pieces;
                }
                catch
                {
                    throw new ApplicationException(Strings.SelectRecordError);
                }
            }
        }

        /// <summary>
        /// Select and return records page wise from table appSchema.Piece.
        /// </summary>
        /// <param name="startRowIndex">Start Row Index of the page.</param>
        /// <param name="maximumRows">Maximum number of Rows on the page.</param>
        /// <param name="totalRowCount">Total number of rows in table appSchema.Piece.</param>
        /// <returns>A collection of instances of MusicLibrary.Model.BLL.Piece.</returns>
        public IEnumerable<Piece> GetPieces(int startRowIndex, int maximumRows, out int totalRowCount)
        {
            using (var con = CreateConnection())
            {
                try
                {
                    var pieces = new List<Piece>(100);
                    var cmd = new SqlCommand("appSchema.usp_GetPiecesPageWise", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@startRowIndex", SqlDbType.Int, 4).Value = startRowIndex;
                    cmd.Parameters.Add("@maximumRows", SqlDbType.Int, 4).Value = maximumRows;
                    cmd.Parameters.Add("@totalRowCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var pieceIDIndex = reader.GetOrdinal("PieceID");
                        var nameIndex = reader.GetOrdinal("Name");
                        var catalogueNumberIndex = reader.GetOrdinal("CatalogueNumber");
                        var composerIDIndex = reader.GetOrdinal("ComposerID");
                        var scaleID = reader.GetOrdinal("ScaleID");
                        var genreID = reader.GetOrdinal("GenreID");
                        var yearOfCompositionIndex = reader.GetOrdinal("YearOfComposition");

                        while (reader.Read())
                        {
                            pieces.Add(new Piece
                            {
                                PieceID = reader.GetInt32(pieceIDIndex),
                                Name = reader.GetString(nameIndex),
                                CatalogueNumber = reader.GetString(catalogueNumberIndex),
                                ComposerID = reader.GetInt16(composerIDIndex),
                                ScaleID = reader.GetByte(scaleID),
                                GenreID = reader.GetByte(genreID),
                                YearOfComposition = reader.GetDateTime(yearOfCompositionIndex)
                            });
                        }
                    }

                    totalRowCount = (int)cmd.Parameters["@totalRowCount"].Value;
                    pieces.TrimExcess();
                    return pieces;
                }
                catch
                {
                    throw new ApplicationException(Strings.SelectRecordError);
                }
            }
        }

        /// <summary>
        /// Insert a record in table appSchema.Piece.
        /// </summary>
        /// <param name="booklet">An instance of MusicLibrary.Model.BLL.Piece.</param>
        public void InsertPiece(Piece piece)
        {
            using (var con = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("appSchema.usp_InsertPiece", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = piece.Name;
                    cmd.Parameters.Add("@CatalogueNumber", SqlDbType.VarChar, 12).Value = piece.CatalogueNumber;
                    cmd.Parameters.Add("@ComposerID", SqlDbType.SmallInt, 2).Value = piece.ComposerID;
                    cmd.Parameters.Add("@ScaleID", SqlDbType.TinyInt, 1).Value = piece.ScaleID;
                    cmd.Parameters.Add("@GenreID", SqlDbType.TinyInt, 1).Value = piece.GenreID;
                    cmd.Parameters.Add("@YearOfComposition", SqlDbType.DateTime2).Value = piece.YearOfComposition;
                    cmd.Parameters.Add("@PieceID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    piece.PieceID = (int)cmd.Parameters["@PieceID"].Value;
                }
                catch
                {
                    throw new ApplicationException(Strings.InsertRecordError);
                }
            }
        }

        /// <summary>
        /// Update a record in table appSchema.Piece.
        /// </summary>
        /// <param name="booklet">An instance of MusicLibrary.Model.BLL.Piece.</param>
        public void UpdatePiece(Piece piece)
        {
            using (var con = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("appSchema.usp_UpdatePiece", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PieceID", SqlDbType.Int, 4).Value = piece.PieceID;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = piece.Name;
                    cmd.Parameters.Add("@CatalogueNumber", SqlDbType.VarChar, 12).Value = piece.CatalogueNumber;
                    cmd.Parameters.Add("@ComposerID", SqlDbType.SmallInt, 2).Value = piece.ComposerID;
                    cmd.Parameters.Add("@ScaleID", SqlDbType.TinyInt, 1).Value = piece.ScaleID;
                    cmd.Parameters.Add("@GenreID", SqlDbType.TinyInt, 1).Value = piece.GenreID;
                    cmd.Parameters.Add("@YearOfComposition", SqlDbType.DateTime2).Value = piece.YearOfComposition;

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