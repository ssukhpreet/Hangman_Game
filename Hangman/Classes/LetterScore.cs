using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Hangman.Activities;

namespace Hangman.Classes
{
    class LetterScore
    {
        //Sets the property for the score
        public int score { get; set; } = 0;

        //Constructor for LetterScore
        public LetterScore()
        {
        }

        public void GetScore(string word) //Passes 'word' back to the Game class where it passes back as 'letter'
        {
            // If any of these letters are correct, their score is increased by 2
            switch (word)
            {
                case "A":
                case "E":
                case "I":
                case "O":
                case "U":
                    score = score + 2;
                    // These letters increase the score by 4 and this increases below the harder the letter is to get, etc
                    break;
                case "L":
                case "N":
                case "R":
                case "S":
                case "T":
                    score = score + 4;

                    break;
                case "D":
                case "G":
                case "B":
                case "C":
                case "M":
                case "P":
                    score = score + 6;

                    break;
                case "F":
                case "H":
                case "W":
                case "Y":
                case "V":
                    score = score + 7;

                    break;
                case "K":
                case "J":
                case "X":
                    score = score + 8;

                    break;
                case "Q":
                case "Z":
                    score = score + 10;

                    break;
            }
        }
    }
}