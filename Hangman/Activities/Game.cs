using Android.App;
using Android.OS;
using Android.Widget;
using Hangman.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Stream = System.IO.Stream;

namespace Hangman.Activities
{
    [Activity(Label = "Game")]
    public class Game : Activity
    {
        //Buttons A-Z
        private Button btnA;
        private Button btnB;
        private Button btnC;
        private Button btnD;
        private Button btnE;
        private Button btnF;
        private Button btnG;
        private Button btnH;
        private Button btnI;
        private Button btnJ;
        private Button btnK;
        private Button btnL;
        private Button btnM;
        private Button btnN;
        private Button btnO;
        private Button btnP;
        private Button btnQ;
        private Button btnR;
        private Button btnS;
        private Button btnT;
        private Button btnU;
        private Button btnV;
        private Button btnW;
        private Button btnX;
        private Button btnY;
        private Button btnZ;

        private TextView txtWordToGuess;
        private Button btnGameMainMenu;
        private Button btnNewGame;
        private ImageView imgHangman;
        private TextView txtCurrentScore;
        private TextView txtGuessesLeft;

        public static int Id;
        public static string PlayerName;
        public static int Score { set; get; }
        public static DateTime DateTime;

        public string letter;
        public string random;
        public int GuessesLeft = 8;
        public char[] WordToGuess;
        public char[] HiddenWord;
        public bool GuessCorrect;
        public List<string> wordList = new List<string>();

        public DBConnection myConnection;
        public DataAdapter da;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GameScreen);
            myConnection = new DBConnection();
            LoadWords();
            AssignButtons();
        }

        private void AssignButtons() //Assigns all buttons and events to the correct place
        {
            btnNewGame = FindViewById<Button>(Resource.Id.btnNewGame);
            btnNewGame.Click += btnNewGame_Click;
            btnGameMainMenu = FindViewById<Button>(Resource.Id.gamebtnMainMenu);
            btnGameMainMenu.Click += btnGameMainMenu_Click;
            imgHangman = FindViewById<ImageView>(Resource.Id.imgHangman);
            txtCurrentScore = FindViewById<TextView>(Resource.Id.txtCurrentScore);
            txtCurrentScore.Text = Score.ToString();
            txtGuessesLeft = FindViewById<TextView>(Resource.Id.txtGuessesLeft);
            txtGuessesLeft.Text = GuessesLeft.ToString();
            txtWordToGuess = FindViewById<TextView>(Resource.Id.txtWordToGuess);

            btnA = FindViewById<Button>(Resource.Id.btnA);
            btnB = FindViewById<Button>(Resource.Id.btnB);
            btnC = FindViewById<Button>(Resource.Id.btnC);
            btnD = FindViewById<Button>(Resource.Id.btnD);
            btnE = FindViewById<Button>(Resource.Id.btnE);
            btnF = FindViewById<Button>(Resource.Id.btnF);
            btnG = FindViewById<Button>(Resource.Id.btnG);
            btnH = FindViewById<Button>(Resource.Id.btnH);
            btnI = FindViewById<Button>(Resource.Id.btnI);
            btnJ = FindViewById<Button>(Resource.Id.btnJ);
            btnK = FindViewById<Button>(Resource.Id.btnK);
            btnL = FindViewById<Button>(Resource.Id.btnL);
            btnM = FindViewById<Button>(Resource.Id.btnM);
            btnN = FindViewById<Button>(Resource.Id.btnN);
            btnO = FindViewById<Button>(Resource.Id.btnO);
            btnP = FindViewById<Button>(Resource.Id.btnP);
            btnQ = FindViewById<Button>(Resource.Id.btnQ);
            btnR = FindViewById<Button>(Resource.Id.btnR);
            btnS = FindViewById<Button>(Resource.Id.btnS);
            btnT = FindViewById<Button>(Resource.Id.btnT);
            btnU = FindViewById<Button>(Resource.Id.btnU);
            btnV = FindViewById<Button>(Resource.Id.btnV);
            btnW = FindViewById<Button>(Resource.Id.btnW);
            btnX = FindViewById<Button>(Resource.Id.btnX);
            btnY = FindViewById<Button>(Resource.Id.btnY);
            btnZ = FindViewById<Button>(Resource.Id.btnZ);

            DisableLetterButtons();
            BlankImage();

            btnA.Click += OnLetterButtonClick;
            btnB.Click += OnLetterButtonClick;
            btnC.Click += OnLetterButtonClick;
            btnD.Click += OnLetterButtonClick;
            btnE.Click += OnLetterButtonClick;
            btnF.Click += OnLetterButtonClick;
            btnG.Click += OnLetterButtonClick;
            btnH.Click += OnLetterButtonClick;
            btnI.Click += OnLetterButtonClick;
            btnJ.Click += OnLetterButtonClick;
            btnK.Click += OnLetterButtonClick;
            btnL.Click += OnLetterButtonClick;
            btnM.Click += OnLetterButtonClick;
            btnN.Click += OnLetterButtonClick;
            btnO.Click += OnLetterButtonClick;
            btnP.Click += OnLetterButtonClick;
            btnQ.Click += OnLetterButtonClick;
            btnR.Click += OnLetterButtonClick;
            btnS.Click += OnLetterButtonClick;
            btnT.Click += OnLetterButtonClick;
            btnU.Click += OnLetterButtonClick;
            btnV.Click += OnLetterButtonClick;
            btnW.Click += OnLetterButtonClick;
            btnX.Click += OnLetterButtonClick;
            btnY.Click += OnLetterButtonClick;
            btnZ.Click += OnLetterButtonClick;
        }

        private void BlankImage()
        {
            //Reset the image to the starting image
            imgHangman.SetImageResource(Resource.Drawable.BlankScreen);
        }

        private void btnGameMainMenu_Click(object sender, EventArgs e)
        {
            //Run the main activity screen (main menu)
            StartActivity(typeof(MainActivity));
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            //Start a new game, disable the new game button, load a new random word and image
            btnNewGame.Enabled = false;
            LoadRandomWord();
            btnNewGame.Enabled = false;
            BlankImage();
        }

        private void LoadRandomWord()
        {
            //Enable the letter buttons
            EnableLetterButtons();
            //Set guesses left to 8
            GuessesLeft = 8;
            //Go through the wordlist file at random to select a word and set to upper case and convert to an array
            Random myRandom = new Random();
            random = wordList[myRandom.Next(wordList.Count)];
            random = random.ToUpper();
            WordToGuess = random.ToArray();

            HiddenWord = new char[WordToGuess.Length];

            for (int i = 0; i < HiddenWord.Length; i++)
            {
                HiddenWord[i] = '_';
                txtWordToGuess.Text = string.Join(" ", HiddenWord);
            }
        }

        private void OnLetterButtonClick(object sender, EventArgs e)
        {
            //Create a fakebutton buttonclick to hold all of the buttonclicks for the A-Z buttons
            var buttonclick = (Button)sender;
            //Disable the button that was clicked
            buttonclick.Enabled = false;
            //letter becomes the button that was clicked and put to upper case
            letter = buttonclick.Text;
            letter = letter.ToUpper();

            // loop through the array of the hidden word and see if the letter is present
            for (int i = 0; i < HiddenWord.Length; i++)
            {
                //if the field 'letter' matches the letter of the word we are trying to guess run this.
                if (letter == WordToGuess[i].ToString())
                {
                    //the position of the letter[i] in the word that is hidden is set
                    HiddenWord[i] = letter.ToCharArray()[0];
                    txtWordToGuess.Text = string.Join(" ", HiddenWord);
                    //Bool GuessedCorrect is set to true
                    GuessCorrect = true;
                    //Run the set score method which will determine the score based off which letter is guessed correctly and update textbox
                    UpdateScore(SetScore());
                }
            }

            //If the GuessCorrect bool is false, guessesleft is reduced by 1 and the textboxes and images are updated
            if (GuessCorrect == false)
            {
                GuessesLeft = GuessesLeft - 1;

                GuessFailed();
                GuessedWrongUpdateText();
            }
            else
            {
                //Set the GuessCorrect to false for the next round 
                GuessCorrect = false;
            }

            if (!HiddenWord.Contains('_')) //If the hiddenword does not contain an underscore (meaning no words are left to guess), run win method
            {
                WinGame();
            }

        }

        private void GuessedWrongUpdateText() //Update the guessesleft textbox
        {
            txtGuessesLeft.Text = GuessesLeft.ToString();
        }

        private void GuessFailed()
        {
            switch (GuessesLeft) //Run through this switch statement and update the image based off how many guesses are left from 'GuessesLeft'
            {
                case 7://(first life)
                    imgHangman.SetImageResource(Resource.Drawable.Life1);
                    break;

                case 6://(second life)... etc
                    imgHangman.SetImageResource(Resource.Drawable.Life2);
                    break;

                case 5:
                    imgHangman.SetImageResource(Resource.Drawable.Life3);
                    break;

                case 4:
                    imgHangman.SetImageResource(Resource.Drawable.Life4);
                    break;

                case 3:
                    imgHangman.SetImageResource(Resource.Drawable.Life5);
                    break;

                case 2:
                    imgHangman.SetImageResource(Resource.Drawable.Life6);
                    break;

                case 1:
                    imgHangman.SetImageResource(Resource.Drawable.Life7);
                    break;

                //Run this when you have lost the game
                case 0:
                    imgHangman.SetImageResource(Resource.Drawable.Life8);
                    Score = Score - 12;
                    //If score is less than 0, set to 0 - stop negative points
                    if (Score < 0)
                    {
                        Score = 0;
                    }
                    //Post a message to say you have lost and update your score accordingly, update the DB, and start the main activity (main menu)
                    Toast.MakeText(this, "You have run out of guesses, what a chump! You have lost. Your score was " + Score, ToastLength.Long).Show();
                    myConnection = new DBConnection();
                    myConnection.UpdateScore(Id, PlayerName, Score, DateTime);

                    StartActivity(typeof(MainActivity));
                    break;
            }
        }

        private int SetScore() //Sets the score from the LetterScore class and updates
        {
            LetterScore letterScore = new LetterScore();
            //Loop through the letters in the LetterScore class and update score
            foreach (char letters in HiddenWord)
            {
                string strletter = letters.ToString();
                letterScore.GetScore(strletter);
            }
            //Sets the score equal to the letterscore
            Score = letterScore.score;
            return letterScore.score;
        }

        private void LoadWords() //Load the words from the Words.txt file
        {
            Stream myStream = Assets.Open("Words.txt");
            using (StreamReader sr = new StreamReader(myStream))
            {
                string word;

                while ((word = sr.ReadLine()) != null)
                {
                    wordList.Add(word);
                }
            }
        }

        private void DisableLetterButtons() //Disable the letter buttons
        {
            btnA.Enabled = false;
            btnB.Enabled = false;
            btnC.Enabled = false;
            btnD.Enabled = false;
            btnE.Enabled = false;
            btnF.Enabled = false;
            btnG.Enabled = false;
            btnH.Enabled = false;
            btnI.Enabled = false;
            btnJ.Enabled = false;
            btnK.Enabled = false;
            btnL.Enabled = false;
            btnM.Enabled = false;
            btnN.Enabled = false;
            btnO.Enabled = false;
            btnP.Enabled = false;
            btnQ.Enabled = false;
            btnR.Enabled = false;
            btnS.Enabled = false;
            btnT.Enabled = false;
            btnU.Enabled = false;
            btnV.Enabled = false;
            btnW.Enabled = false;
            btnX.Enabled = false;
            btnY.Enabled = false;
            btnZ.Enabled = false;
        }

        private void EnableLetterButtons() //Enable the letter buttons and new game button
        {
            btnA.Enabled = true;
            btnB.Enabled = true;
            btnC.Enabled = true;
            btnD.Enabled = true;
            btnE.Enabled = true;
            btnF.Enabled = true;
            btnG.Enabled = true;
            btnH.Enabled = true;
            btnI.Enabled = true;
            btnJ.Enabled = true;
            btnK.Enabled = true;
            btnL.Enabled = true;
            btnM.Enabled = true;
            btnN.Enabled = true;
            btnO.Enabled = true;
            btnP.Enabled = true;
            btnQ.Enabled = true;
            btnR.Enabled = true;
            btnS.Enabled = true;
            btnT.Enabled = true;
            btnU.Enabled = true;
            btnV.Enabled = true;
            btnW.Enabled = true;
            btnX.Enabled = true;
            btnY.Enabled = true;
            btnZ.Enabled = true;
            btnNewGame.Enabled = true;
        }

        private void UpdateScore(int YourScore) //Update the current score text box
        {
            txtCurrentScore.Text = YourScore.ToString();
        }

        private void WinGame() //Run when the game is won
        {
            BlankImage();
            Toast.MakeText(this, "Congrats! You guessed the word correctly!", ToastLength.Long).Show();
            //Run the update score method to store in the DB
            myConnection = new DBConnection();
            myConnection.UpdateScore(Id, PlayerName, Score, DateTime);
            Toast.MakeText(this, myConnection.UpdateScore(Id, PlayerName, Score, DateTime), ToastLength.Long).Show();
            //Load a new word at random
            LoadRandomWord();
        }
    }
}