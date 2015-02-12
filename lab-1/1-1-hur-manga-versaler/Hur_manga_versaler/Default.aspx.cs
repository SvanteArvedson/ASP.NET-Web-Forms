using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hur_manga_versaler
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Code running when user click on the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SendButton_Click(object sender, EventArgs e)
        {
            // Examines the status of the page.
            if (TextBox.Enabled)
            {
                int nbrOfCapitals = Model.TextAnalyzer.GetNumberOfCapitals(TextBox.Text);
                TextBox.Enabled = false;
                SendButton.Text = "Återställ";
                ResultLabel.Text = String.Format("Texten innehåller {0} versaler.", nbrOfCapitals);
                ResultLabel.Visible = true;
            }
            else
            {
                TextBox.Text = "";
                TextBox.Enabled = true;
                SendButton.Text = "Bestäm antalet versaler";
                ResultLabel.Text = "";
                ResultLabel.Visible = false;
            }
        }
    }
}