using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashRegister.Model
{
    /// <summary>
    /// Enumerates the different possible DiscountRates in %.
    /// </summary>
    public enum DiscountRates
    {
        None = 0,
        Small = 5,
        Medium = 10,
        Big = 15
    }

    /// <summary>
    /// Represent a receipt with Subtotal sum, Total sum, Discount and money off.
    /// </summary>
    public class Receipt
    {
        private double _subtotal;

        public double DiscountRate { get; private set; }
        public double MoneyOff { get; private set; }
        public double Subtotal
        {
            get
            {
                return _subtotal;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("The value can not be less than or equal to 0");
                }
                else
                {
                    _subtotal = value;
                }
            }
        }
        public double Total { get; private set; }

        /// <summary>
        /// Constructor for Receipt. Calls the public method Calculate.
        /// </summary>
        /// <param name="subtotal">The sum before discount.</param>
        public Receipt(double subtotal)
        {
            Calculate(subtotal);
        }

        /// <summary>
        /// Calculate and initiate the properties of Receipt.
        /// </summary>
        /// <param name="subtotal">The sum before discount.</param>
        public void Calculate(double subtotal)
        {
            // Set Subtotal
            Subtotal = subtotal;

            // Set DiscountRate
            if (Subtotal < 500)
            {
                DiscountRate = (double)DiscountRates.None;
            }
            else if (Subtotal < 1000)
            {
                DiscountRate = (double)DiscountRates.Small / 100;
            }
            else if (Subtotal < 5000)
            {
                DiscountRate = (double)DiscountRates.Medium / 100;
            }
            else
            {
                DiscountRate = (double)DiscountRates.Big / 100;
            }

            // Set MoneyOff
            MoneyOff = Subtotal * DiscountRate;

            // Set Total
            Total = Subtotal - MoneyOff;

        }
    }
}