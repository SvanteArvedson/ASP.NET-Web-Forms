using GuessTheSecretNumber.Model;
using System;
using System.Text;

namespace GuessTheSecretNumber
{
    public partial class Default : System.Web.UI.Page
    {
        private SecretNumber SecretNumber
        {
            get
            {
                return Session["SecretNumber"] as SecretNumber;
            }
            set
            {
                Session["SecretNumber"] = value;
            }
        }

        protected void MakeGuessButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                int guess = int.Parse(MakeGuessTextBox.Text);
                string previousGuesses = "";
                string resultMessage = "";

                // Makeing the guess and creating the ResultMessage.
                switch (SecretNumber.MakeGuess(guess))
                {
                    case Outcome.Low:
                        resultMessage = "Du gissade för lågt.";
                        break;
                    case Outcome.High:
                        resultMessage = "Du gissade för högt.";
                        break;
                    case Outcome.Correct:
                        resultMessage = String.Format("Grattis! Du klarade det på {0} gissningar.", SecretNumber.Count);
                        break;
                    case Outcome.NoMoreGuesses:
                        resultMessage = "Du kan inte göra fler gissningar.";
                        break;
                    case Outcome.PreviousGuess:
                        resultMessage = String.Format("Du har redan gissat på {0}.", guess);
                        break;
                }

                // Create the GuessesMessage.
                StringBuilder builder = new StringBuilder();
                foreach (int madeGuess in SecretNumber.PreviousGuesses)
                {
                    builder.Append(madeGuess.ToString()).Append(", ");
                }
                previousGuesses = builder.ToString().TrimEnd(',', ' ');

                // Controls if user can make more guesses.
                if (SecretNumber.CanMakeGuess)
                {
                    NewGameButton.Enabled = false;
                    MakeGuessTextBox.Enabled = true;
                    MakeGuessButton.Enabled = true;
                }

                // Shows Messages. If user lost the game a GameOver message is shown.
                if (SecretNumber.Count >= 7 && SecretNumber.Outcome != Outcome.Correct)
                {
                    GameOverLabel.Text = String.Format("Du har inga gissningar kvar. Det hemliga talet var {0}.", SecretNumber.Number);
                    GameOverLabel.Visible = true;
                }
                else
                {
                    ResultLabel.Text = resultMessage;
                }
                PreviousGuessesLabel.Text = previousGuesses;
                ResultPlaceholder.Visible = true;
                MakeGuessTextBox.Text = "";
            }
        }

        protected void NewGameButton_Click(object sender, EventArgs e)
        {
            // Set SecretNumber to default values.
            if (SecretNumber == null)
            {
                SecretNumber = new SecretNumber();
            }
            else
            {
                SecretNumber.Initialize();
            }

            NewGameButton.Enabled = false;
            MakeGuessTextBox.Enabled = true;
            MakeGuessButton.Enabled = true;
        }
    }
}