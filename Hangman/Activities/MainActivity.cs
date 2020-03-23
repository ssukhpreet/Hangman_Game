using Android.App;
using Android.OS;
using Android.Widget;
using Hangman.Classes;
using System;
using System.IO;

namespace Hangman
{
    [Activity(Label = "Hangman", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo.Light")] // Check theme
    public class MainActivity : Activity
    {
        private Button btnNewGame;
        private Button btnHighScores;
        private Button btnEditDB;
        private Button btnQuitGame;
        private DBConnection myConnection;
        private ImageView imgHangman;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            myConnection = new DBConnection();
            CopyTheDB();
            AssignButtons();
        }

        private void CopyTheDB()
        {
            //DB file name
            string dbName = "HangmanDB.sqlite";

            // Check if your DB has already been extracted. If the file does not exist move it
            if (!File.Exists(myConnection.databasePath))
            {
                using (BinaryReader br = new BinaryReader(Assets.Open(dbName)))
                {
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(myConnection.databasePath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int len = 0;
                        while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, len);
                        }
                    }
                }
            }
        }

        private void AssignButtons()
        {
            //Assigns all of the button click events
            btnNewGame = FindViewById<Button>(Resource.Id.btnNewGameScreen);
            btnNewGame.Click += btnNewGame_Click;
            btnHighScores = FindViewById<Button>(Resource.Id.btnHighScores);
            btnHighScores.Click += btnHighScores_Click;
            btnEditDB = FindViewById<Button>(Resource.Id.btnEditDB);
            btnEditDB.Click += btnEditDB_Click;
            btnQuitGame = FindViewById<Button>(Resource.Id.btnQuit);
            btnQuitGame.Click += btnQuitGame_Click;
            imgHangman = FindViewById<ImageView>(Resource.Id.imgHangmanTitle);
            imgHangman.SetImageResource(Resource.Drawable.Life7);
        }

        private void btnQuitGame_Click(object sender, EventArgs e)
        {
            //Exit the app if clicked
            System.Environment.Exit(0);
        }

        private void btnEditDB_Click(object sender, EventArgs e)
        {
            //Start the EditDB activity if clicked
            StartActivity(typeof(EditDB));
        }

        private void btnHighScores_Click(object sender, EventArgs e)
        {
            //Start the HighScores activity if clicked
            StartActivity(typeof(HighScores));
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            //Start the SelectPlayer activity if clicked
            StartActivity(typeof(SelectPlayer));
        }
    }
}
