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
using Hangman.Classes;

namespace Hangman
{
    [Activity(Label = "HighScores")]
    public class HighScores : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Set the HighScores layout
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HighScores);
            //Instantiate the DBConnection and SQLite query
            ViewAllList();
        }

        private void ViewAllList()
        {
            Button HighScoresBackToMenu;
            List<HangmanScore> myList;
            DBConnection myConnection;
            myConnection = new DBConnection();
            myList = myConnection.ViewAll();
            //Assign the button click event
            HighScoresBackToMenu = FindViewById<Button>(Resource.Id.btnHighScoresBacktoMenu);
            HighScoresBackToMenu.Click += HighScoresBackToMenu_Click;
            //Display the names and highscores
            DataAdapter myDataAdapter = new DataAdapter(this, myList);
            ListView HighScoresListView = FindViewById<ListView>(Resource.Id.HighScoresListView);
            HighScoresListView.Adapter = myDataAdapter;
        }

        private void HighScoresBackToMenu_Click(object sender, EventArgs e)
        {
            //Back to main screen
            StartActivity(typeof(MainActivity));
        }
    }
}