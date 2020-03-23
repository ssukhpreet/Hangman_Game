using Android.App;
using Android.OS;
using Android.Widget;
using Hangman.Activities;
using Hangman.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hangman
{
    [Activity(Label = "SelectPlayer")]
    public class SelectPlayer : Activity
    {
        private List<HangmanScore> myList;
        public TextView txtEnterPlayerName;
        private Spinner spnPlayerName;
        private DBConnection myConnection;
        private DataAdapter da;
        private Button btnStartNewGame;
        private Button btnAddPlayer;
        private Button btnSelectPlayerMainMenu;
        private ImageView imgHangman;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SelectPlayer);
            myConnection = new DBConnection();
            myList = myConnection.ViewAll();
            AssignEvents();
        }

        private void AssignEvents()
        {
            //Assign the button, spinner, and click events
            spnPlayerName = FindViewById<Spinner>(Resource.Id.selectPlayerSpinner);
            da = new DataAdapter(this, myList);
            spnPlayerName.Adapter = da;
            spnPlayerName.ItemSelected += spnPlayerName_ItemSelected;
            txtEnterPlayerName = FindViewById<TextView>(Resource.Id.txtEnterName);
            btnStartNewGame = FindViewById<Button>(Resource.Id.btnStartGame);
            btnStartNewGame.Click += btnStartNewGame_Click;
            btnAddPlayer = FindViewById<Button>(Resource.Id.btnAddProfile);
            btnAddPlayer.Click += btnAddPlayer_Click;
            btnSelectPlayerMainMenu = FindViewById<Button>(Resource.Id.btnselectplayerMainMenu);
            btnSelectPlayerMainMenu.Click += btnSelectPlayerMainMenu_Click;
            imgHangman = FindViewById<ImageView>(Resource.Id.imgHangmanTitle);
            imgHangman.SetImageResource(Resource.Drawable.Life7);

        }

        private void btnSelectPlayerMainMenu_Click(object sender, EventArgs e)
        {
            //Run the main activity class (main menu)
            StartActivity(typeof(MainActivity));
        }

        private void btnAddPlayer_Click(object sender, EventArgs e)
        {
            if (txtEnterPlayerName.Text.Length > 0)
            {
                //Set the player name to the textbox
                Game.PlayerName = txtEnterPlayerName.Text;
                //Start with a score of 0
                Game.Score = 0;
                myConnection = new DBConnection();
                //Insert the player into the database using the InsertNewPlayer method and passing across the Game fields
                myConnection.InsertNewPlayer(Game.PlayerName, Game.Score, Game.DateTime);
                //Update the list and display on the spinner
                myList = myConnection.ViewAll();
                da = new DataAdapter(this, myList);
                spnPlayerName.Adapter = da;
                Toast.MakeText(this, Game.PlayerName + " has been added", ToastLength.Short).Show();
            }
            else // If the text field is empty, display error message
            {
                Toast.MakeText(this, "Please enter at least 1 character for your name.", ToastLength.Short).Show();
            }
        }

        private void btnStartNewGame_Click(object sender, EventArgs e)
        {
            //Start the game activity (start the game itself) and start the score with 0
            if (!string.IsNullOrWhiteSpace(Game.PlayerName))
            {
                Toast.MakeText(this, "Loading Player " + Game.PlayerName, ToastLength.Short).Show();
                StartActivity(typeof(Game));
                Game.Score = 0;
            }
            else
            {
                Toast.MakeText(this, "Please select a player", ToastLength.Short).Show();
            }
        }

        private void spnPlayerName_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            //The player name and score is taken from this
            Game.Id = myList.ElementAt(e.Position).Id;
            Game.PlayerName = myList.ElementAt(e.Position).Name;
            Game.Score = myList.ElementAt(e.Position).Score;
            Game.DateTime = myList.ElementAt(e.Position).Date;
        }
    }
}
