using System;
using System.ComponentModel.DataAnnotations;

namespace MusicLibrary.Model.BLL
{
    /// <summary>
    /// The class represent a record in table appSchema.Piece.
    /// </summary>
    public class Piece
    {
        public int PieceID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name can't be null.")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters.")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "CatalogueNumber can't be null.")]
        [StringLength(12, ErrorMessage = "CatalogueNumber can't be longer than 12 characters")]
        public string CatalogueNumber { get; set; }

        [Required(ErrorMessage = "ComposerID can't be null.")]
        [Range(0, Int32.MaxValue, ErrorMessage = "ComposerID can't be smaller than 0.")]
        public int ComposerID { get; set; }

        [Required(ErrorMessage = "ScaleID can't be null.")]
        [Range(0, Int32.MaxValue, ErrorMessage = "ScaleID can't be smaller than 0.")]
        public int ScaleID { get; set; }

        [Required(ErrorMessage = "GenreID can't be null.")]
        [Range(0, Int32.MaxValue, ErrorMessage = "GenreID can't be smaller than 0.")]
        public int GenreID { get; set; }

        [Required(ErrorMessage = "YearOfComposition can't be null.")]
        [CheckDateTimeAttribute(ErrorMessage = "YearOfCompoition can't be a year in the future")]
        public DateTime YearOfComposition { get; set; }
    }
}