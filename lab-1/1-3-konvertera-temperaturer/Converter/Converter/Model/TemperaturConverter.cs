using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Converter.Model
{
    /// <summary>
    /// Includes methods for converting temperatures.
    /// </summary>
    public static class TemperaturConverter
    {
        /// <summary>
        /// Converts Celcius to Farenheit.
        /// </summary>
        /// <param name="degreesC">The Celcius degree to be converted.</param>
        /// <returns>The matching Farenheit degree.</returns>
        public static int CelciusToFarenheit(int degreesC)
        {
            return Convert.ToInt32(degreesC * 1.8 + 32);
        }

        /// <summary>
        /// Converts Farenheit to Celcius.
        /// </summary>
        /// <param name="degreesF">Tha Farenheit degree to be converted.</param>
        /// <returns>The matching Celcius degree.</returns>
        public static int FarenheitToCelcius(int degreesF) 
        {
            return Convert.ToInt32((degreesF - 32) * ((double)5 / 9));
        }
    }
}