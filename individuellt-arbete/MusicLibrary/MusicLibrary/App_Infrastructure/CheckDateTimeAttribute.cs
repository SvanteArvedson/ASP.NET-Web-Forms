using System;
using System.ComponentModel.DataAnnotations;

namespace MusicLibrary
{
    /// <summary>
    /// Checks that the DateTime object isn't in the future.
    /// </summary>
    public class CheckDateTimeAttribute : ValidationAttribute
    {
        private DateTime Current { get; set; }

        public CheckDateTimeAttribute()
        {
            this.Current = DateTime.Now;
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                DateTime dateTimeValue = (DateTime)value;
                return (dateTimeValue <= Current);
            }
            return true;
        }
    }
}