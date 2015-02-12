using Converter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Converter
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StartTemperature.Focus();
        }

        protected void ConvertTemperatureButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                int Start = int.Parse(StartTemperature.Text);
                int End = int.Parse(EndTemperature.Text);
                int Step = int.Parse(TemperatureStep.Text);
                int NbrOfSteps = (End - Start) / Step;
                int[,] Degrees = new int[NbrOfSteps, 2];

                // Creating and storing all temperature values.
                for (int i = Start, k = 0; k <= NbrOfSteps - 1; i += Step, k += 1)
                {
                    Degrees[k, 0] = i;
                    if (RadioButtonConvertDirection.SelectedValue == "CtoF")
                    {
                        Degrees[k, 1] = TemperaturConverter.CelciusToFarenheit(i);
                    }
                    else 
                    {
                        Degrees[k, 1] = TemperaturConverter.FarenheitToCelcius(i);
                    }
                }

                // Creating the Table Header.
                TableHeaderRow tHeadRow = new TableHeaderRow();
                TableHeaderCell tHeadCellCelcius = new TableHeaderCell();
                TableHeaderCell tHeadCellFarenheit = new TableHeaderCell();
                tHeadCellCelcius.Text = "&deg;C";
                tHeadCellFarenheit.Text = "&deg;F";
                if (RadioButtonConvertDirection.SelectedValue == "CtoF")
                {
                    tHeadRow.Cells.Add(tHeadCellCelcius);
                    tHeadRow.Cells.Add(tHeadCellFarenheit);
                }
                else
                {
                    tHeadRow.Cells.Add(tHeadCellFarenheit);
                    tHeadRow.Cells.Add(tHeadCellCelcius);
                }
                // Adds the header row to the table.
                ResultTable.Rows.Add(tHeadRow);

                // Creating the Table Cells
                for (int i = 0; i <= NbrOfSteps - 1; i += 1) 
                {
                    TableRow tRow = new TableRow();
                    for (int k = 0; k <= 1; k += 1) 
                    {
                        TableCell tCell = new TableCell();
                        tCell.Text = Degrees[i, k].ToString();
                        tRow.Cells.Add(tCell);
                    }
                    ResultTable.Rows.Add(tRow);
                }

                // Emptying the TextBoxes and make the table visible.
                ResultTable.Visible = true;
                StartTemperature.Text = "";
                EndTemperature.Text = "";
                TemperatureStep.Text = "";
            }
        }
    }
}