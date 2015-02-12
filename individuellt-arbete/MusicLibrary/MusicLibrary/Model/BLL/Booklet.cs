using System;
using System.ComponentModel.DataAnnotations;

namespace MusicLibrary.Model.BLL
{
    /// <summary>
    /// The class represent a record in table appSchema.Booklet.
    /// </summary>
    public class Booklet
    {
        public int BookletID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name can't be null.")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters,")]
        public string Name { get; set; }

        [Required(ErrorMessage = "PublisherID can't be null.")]
        [Range(0, Int32.MaxValue, ErrorMessage = "PublisherID can't be smaller than 0.")]
        public int PublisherID { get; set; }

        [Required(ErrorMessage = "YearOfPublication can't be null.")]
        [CheckDateTimeAttribute(ErrorMessage = "YearOfPublication can't be a year in the future")]
        public DateTime YearOfPublication { get; set; }

        [Required(ErrorMessage="Place can't be null.")]
        [StringLength(6, ErrorMessage="Place can't be longer than 6 characters.")]
        [RegularExpression(@"^[A-Z]{2}\d{4}$", ErrorMessage="Place must be in format [AA0000]")]
        public string Place { get; set; }
    }
}