using System;
using System.ComponentModel.DataAnnotations;

namespace MusicLibrary.Model.BLL
{
    /// <summary>
    /// The class represent a record in table appSchema.BookletContent.
    /// </summary>
    public class BookletContent
    {
        [Required(ErrorMessage = "BookletID can't be null.")]
        [Range(0, Int32.MaxValue, ErrorMessage = "BookletID can't be smaller than 0.")]
        public int BookletID { get; set; }

        [Required(ErrorMessage = "PieceID can't be null.")]
        [Range(0, Int32.MaxValue, ErrorMessage = "PieceID can't be smaller than 0")]
        public int PieceID { get; set; }
    }
}