using System.ComponentModel.DataAnnotations;

namespace Adventurous_Contacts.Model.BLL
{
    /// <summary>
    /// Contains information from one record in databas table 1dv406_AdventureWorksAssignment.Contact.
    /// </summary>
    public class Contact
    {
        public int ContactId { get; set; }

        [Required(ErrorMessage = "You must give an email address.", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(50, ErrorMessage = "Email adrress can't be longer than 50 characters.")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "You must give a first name.", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "First Name can't be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must give a first name.", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "Last Name can't be longer than 50 characters.")]
        public string LastName { get; set; }
    }
}