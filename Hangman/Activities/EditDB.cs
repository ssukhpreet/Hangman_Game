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
using Hangman.Classes;

namespace Hangman
{
    [Activity(Label = "EditDB")]
    public class EditDB : Activity
    {
        private List<HangmanScore> myList;
        private Button btnDelete;
        private Spinner spnEditDB;
        private Button btnEditDBBacktoMenu;
        private DBConnection myConnection;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditDB);
            myConnection = new DBConnection();
            myList = myConnection.ViewAll();

            AssignButtons();
        }

        private void AssignButtons()
        {
            //Assigns the buttons and click events for this screen
            btnEditDBBacktoMenu = FindViewById<Button>(Resource.Id.btnEditDBBackToMenu);
            btnEditDBBacktoMenu.Click += btnEditDBBacktoMenu_Click;
            spnEditDB = FindViewById<Spinner>(Resource.Id.spinnereditDB);
            DataAdapter da = new DataAdapter(this, myList); // check this
            spnEditDB.Adapter = da;
            spnEditDB.ItemSelected += spnEditDB_ItemSelected;
            btnDelete = FindViewById<Button>(Resource.Id.btnDeleteEntry);
            btnDelete.Click += btnDelete_Click;
            btnDelete.Enabled = false;
        }

        private void spnEditDB_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            //Sets up the spinner and enables the delete button
            Spinner spinner = (Spinner)sender;
            Game.Id = myList.ElementAt(e.Position).Id;
            btnDelete.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Sets up the DB connection
            myConnection = new DBConnection();
            //Runs the delete player method from the DBConnection class using the ID number from the Game class
            myConnection.DeletePlayer(Game.Id);
            myList = myConnection.ViewAll();
            var da = new DataAdapter(this, myList);
            spnEditDB.Adapter = da;
            Toast.MakeText(this, "Succesfully deleted", ToastLength.Long).Show();
        }

        private void btnEditDBBacktoMenu_Click(object sender, EventArgs e)
        {
            // Runs the main activity class (Main Menu)
            StartActivity(typeof(MainActivity));
        }
    }
}