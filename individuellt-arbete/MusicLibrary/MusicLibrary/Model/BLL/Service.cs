using MusicLibrary.Model.DAL;
using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicLibrary.Model.BLL
{
    /// <summary>
    /// Service class for applicaton MusicLibrary.
    /// </summary>
    public class Service
    {
        private BookletContentDAL _bookletContentDAL;
        private BookletContentDAL BookletContentDAL
        {
            get
            {
                return _bookletContentDAL ?? (_bookletContentDAL = new BookletContentDAL());
            }
        }

        private BookletDAL _bookletDAL;
        private BookletDAL BookletDAL
        {
            get
            {
                return _bookletDAL ?? (_bookletDAL = new BookletDAL());
            }
        }

        private PieceDAL _pieceDAL;
        private PieceDAL PieceDAL
        {
            get
            {
                return _pieceDAL ?? (_pieceDAL = new PieceDAL());
            }
        }

        /// <summary>
        /// Delete a record from table appSchema.Booklet.
        /// </summary>
        /// <param name="bookletID">Record to be deleted.</param>
        public void DeleteBooklet(int bookletID)
        {
            BookletDAL.DeleteBooklet(bookletID);
        }

        /// <summary>
        /// Delete records from table appSchema.BookletContent.
        /// </summary>
        /// <param name="bookletID">Part of the Primary Key in the selected records.</param>
        public void DeleteBookletContent(int bookletID)
        {
            BookletContentDAL.DeleteBookletContentsByBookletID(bookletID);
        }

        /// <summary>
        /// Delete a record from table appSchema.Piece.
        /// </summary>
        /// <param name="pieceID">Record to be deleted.</param>
        public void DeletePiece(int pieceID)
        {
            PieceDAL.DeletePiece(pieceID);
        }

        /// <summary>
        /// Select and return a record from table appSchema.Booklet.
        /// </summary>
        /// <param name="bookletID">Record to be selected.</param>
        /// <returns>An instance of MusicLibrary.Model.BLL.Booklet.</returns>
        public Booklet GetBooklet(int bookletID)
        {
            return BookletDAL.GetBookletByID(bookletID);
        }

        /// <summary>
        /// Select and return all records from table appSchema.Booklet.
        /// </summary>
        /// <returns>A collection of instances of MusicLibrary.Model.BLL.Booklet.</returns>
        public IEnumerable<Booklet> GetBooklets() 
        {
            return BookletDAL.GetBooklets();
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
            return BookletDAL.GetBooklets(startRowIndex, maximumRows, out totalRowCount);
        }

        /// <summary>
        /// Select and return all records from table appSchema.BookletContent.
        /// </summary>
        /// <returns>A collection of instances of MusicLibrary.Model.BLL.BookletContent.</returns>
        public IEnumerable<BookletContent> GetBookletContents()
        {
            return BookletContentDAL.GetBookletContents();
        }

        /// <summary>
        /// Select and return a part of all records from table appSchema.BookletContent.
        /// </summary>
        /// <param name="bookletID">Part of the Primary Key in the selected record.</param>
        /// <returns>A collection of instances of MusicLibrary.Model.BLL.BookletContent.</returns>
        public IEnumerable<BookletContent> GetBookletContentsByBookletID(int bookletID)
        {
            return BookletContentDAL.GetBookletContentsByBookletID(bookletID);
        }

        /// <summary>
        /// Select and return a part of all records from table appSchema.BookletContent.
        /// </summary>
        /// <param name="pieceID">Part of the Primary Key in the selected record.</param>
        /// <returns>A collection of instances of MusicLibrary.Model.BLL.BookletContent.</returns>
        public IEnumerable<BookletContent> GetBookletContentsByPieceID(int pieceID)
        {
            return BookletContentDAL.GetBookletContentsByPieceID(pieceID);
        }

        /// <summary>
        /// Select and return a record from table appSchema.Piece.
        /// </summary>
        /// <param name="pieceID">Record to be selected.</param>
        /// <returns>An instance of MusicLibrary.Model.BLL.Piece.</returns>
        public Piece GetPiece(int pieceID)
        {
            return PieceDAL.GetPieceByID(pieceID);
        }

        /// <summary>
        /// Select and return all records from table appSchema.Piece.
        /// </summary>
        /// <returns>A collection of instances of MusicLibrary.Model.BLL.Piece.</returns>
        public IEnumerable<Piece> GetPieces()
        {
            return PieceDAL.GetPieces();
        }

        /// <summary>
        /// Select and return records from table appSchema.Piece with a foreign key in table appSchema.BookletContent.
        /// </summary>
        /// <param name="bookletID">Primary key for a Booklet-object.</param>
        /// <returns>A collection of MusicLibrary.Model.BLL.Piece instances.</returns>
        public IEnumerable<Piece> GetPieces(int bookletID)
        {
            return PieceDAL.GetPiecesByBookletID(bookletID);
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
            return PieceDAL.GetPieces(startRowIndex, maximumRows, out totalRowCount);
        }

        /// <summary>
        /// Insert or update a record in table appSchema.Booklet.
        /// </summary>
        /// <param name="booklet">An instance of MusicLibrary.Model.BLL.Booklet.</param>
        public void SaveBooklet(Booklet booklet)
        {
            ICollection<ValidationResult> validationResults = new List<ValidationResult>();

            // Using extension method for validating.
            if (booklet.Validate(out validationResults))
            {
                if (booklet.BookletID == 0)
                {
                    BookletDAL.InsertBooklet(booklet);
                }
                else
                {
                    BookletDAL.UpdateBooklet(booklet);
                }
            }
            else
            {
                var ex = new ApplicationException(Strings.SaveDataError);
                ex.Data.Add("ValidationResult", validationResults);
                throw ex;
            }
        }

        /// <summary>
        /// Insert a record in table appSchema.BookletContent.
        /// </summary>
        /// <param name="bookletContent">An instance of MusicLibrary.Model.BLL.BookletContent.</param>
        public void SaveBookletContent(BookletContent bookletContent)
        {
            ICollection<ValidationResult> validatonResults = new List<ValidationResult>();

            // Using extension method for validating.
            if (bookletContent.Validate(out validatonResults))
            {
                BookletContentDAL.InsertBookletContent(bookletContent);
            }
            else
            {
                var ex = new ApplicationException(Strings.SaveDataError);
                ex.Data.Add("ValidationResult", validatonResults);
                throw ex;
            }
        }

        /// <summary>
        /// Insert or update a record in table appSchema.Piece.
        /// </summary>
        /// <param name="piece">An instance of MusicLibrary.Model.BLL.Piece.</param>
        public void SavePiece(Piece piece)
        {
            ICollection<ValidationResult> validatonResults = new List<ValidationResult>();

            // Using extension method for validating.
            if (piece.Validate(out validatonResults))
            {
                if (piece.PieceID == 0)
                {
                    PieceDAL.InsertPiece(piece);
                }
                else
                {
                    PieceDAL.UpdatePiece(piece);
                }
            }
            else
            {
                var ex = new ApplicationException(Strings.SaveDataError);
                ex.Data.Add("ValidationResult", validatonResults);
                throw ex;
            }
        }
    }
}