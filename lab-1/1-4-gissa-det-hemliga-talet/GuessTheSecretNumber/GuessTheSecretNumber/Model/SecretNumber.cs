using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace GuessTheSecretNumber.Model
{
    /// <summary>
    /// The possible return values from MakeGuess() in the class SecretNumber. 
    /// </summary>
    public enum Outcome
    {
        Indefinite,
        Low,
        High,
        Correct,
        NoMoreGuesses,
        PreviousGuess
    }

    [Serializable]
    public class SecretNumber
    {
        private const int MaxNumberOfGuesses = 7;
        private int _number;
        private List<int> _previousGuesses;

        /// <summary>
        /// Returns ture if it is possible to make more guesses.
        /// </summary>
        public bool CanMakeGuess
        {
            get
            {
                return !(Count >= MaxNumberOfGuesses || Outcome == Outcome.Correct);
            }
        }

        /// <summary>
        /// Returns the number of made guesses.
        /// </summary>
        public int Count
        {
            get
            {
                return PreviousGuesses.Count;
            }
        }

        /// <summary>
        /// If it isn't possible to make more guesses the property returns the secret number, otherwise it returns null. 
        /// </summary>
        public int? Number
        {
            get
            {
                if (CanMakeGuess)
                {
                    return null;
                }
                else
                {
                    return _number;
                }
            }
        }

        /// <summary>
        /// Returns the result of the last guess.
        /// </summary>
        public Outcome Outcome
        {
            get;
            private set;
        }

        /// <summary>
        /// Return a ReadOnlyCollection with the previous guesses.
        /// </summary>
        public ReadOnlyCollection<int> PreviousGuesses
        { 
            get 
            {
                return new ReadOnlyCollection<int>(_previousGuesses);
            } 
        }

        /// <summary>
        /// Constructor method.
        /// </summary>
        public SecretNumber()
        {
            _previousGuesses = new List<int>(MaxNumberOfGuesses);
            Initialize();
        }

        /// <summary>
        /// Initialize the instance to the default state.
        /// </summary>
        public void Initialize()
        {
            _number = new Random().Next(1, 101);
            _previousGuesses.Clear();
            Outcome = Outcome.Indefinite;
        }

        /// <summary>
        /// The method compare the guess with the previous guesses and the secret number. 
        /// If the guess is valid the guess is added to _previous guesses.
        /// </summary>
        /// <param name="guess">The guess.</param>
        /// <returns>The outcome of the guess.</returns>
        /// <exception>Throws ArgumentOutOfRangeException.</exception>
        public Outcome MakeGuess(int guess)
        {
            // Controls if the user can make more guesses.
            if (CanMakeGuess)
            {
                if (guess >= 1 && guess <= 100) 
                {
                    // Controls if the guess is the same as an old guess.
                    foreach (int previousGuess in PreviousGuesses) 
                    {
                        if (previousGuess == guess) 
                        {
                            Outcome = Outcome.PreviousGuess;
                            return Outcome.PreviousGuess;
                        }
                    }

                    // If it's a valid guess it's added to _previousGuesses.
                    _previousGuesses.Add(guess);
                    if (guess < _number) 
                    {
                        Outcome = Outcome.Low;
                        return Outcome.Low;
                    }
                    else if (guess > _number)
                    {
                        Outcome = Outcome.High;
                        return Outcome.High;
                    }
                    else 
                    {
                        Outcome = Outcome.Correct;
                        return Outcome.Correct;
                    }
                }
                else 
                {
                    throw new ArgumentOutOfRangeException("The guess must be in the range 1 to 100.");
                }
            }
            else 
            {
                Outcome = Outcome.NoMoreGuesses;
                return Outcome.NoMoreGuesses;
            }
        }
    }
}