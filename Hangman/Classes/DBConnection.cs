using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace Hangman.Classes
{
    public class DBConnection
    {


        public SQLiteConnection db; // sqlite connection string
        public string databasePath; // variable to hold database path
        public string databaseName; //variable to hold database name
        HangmanScore field = new HangmanScore();

        public DBConnection()
        {
            //Using Xamarin Essentials to set the path. AppdataDirectory gets backed up by Android 
            databaseName = "HangmanDB.sqlite";
            databasePath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, databaseName);
            if (databasePath != null)
            {
                db = new SQLiteConnection(databasePath);
                db.CreateTable<HangmanScore>();
            }

        }

        //below function will used to perform the select operation
        public List<HangmanScore> ViewAll()
        {
            try
            {
                return db.Query<HangmanScore>("select * from HangmanScore ORDER BY Score DESC");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
                return null;
            }
        }

        public string InsertNewPlayer(string name, int score, DateTime datetime)
        {
            try
            {
                //Set up the DB connection
                db = new SQLiteConnection(databasePath);
                //Instantiate the HangmanScore DB class and pass through the fields in the database
                field.Name = name;
                field.Score = score;
                field.Date = datetime;
                //Insert into the database with the field and return a message
                db.Insert(field);
                return "Nice. You have been added.";
            }
            catch (Exception e)
            {
                return "Error : " + e.Message;
            }
        }


        public string UpdateScore(int id, string name, int score, DateTime datetime)
        {
            try
            {
                //Set up the DB connection
                db = new SQLiteConnection(databasePath);
                //Instantiate the HangmanScore DB class and pass through the fields in the database
                field.Id = id;
                field.Name = name;
                field.Score = score;
                field.Date = datetime;
                //Update the database with the item and return a message
                db.Update(field);
                return "Record Updated...";
            }
            catch (Exception e)
            {
                return "Error : " + e.Message;
            }
        }

        public string DeletePlayer(int id)
        {
            try
            {
                //Set up the DB connection
                db = new SQLiteConnection(databasePath);
                //Instantiate the HangmanScore DB class and pass through the fields in the database
                field.Id = id;
                //Delete from the database using the field id number and return a message
                db.Delete(field);
                return "ID# " + field + " has been deleted successfully.";
            }
            catch (Exception e)
            {
                return "Error : " + e.Message;
            }
        }

    }
}
