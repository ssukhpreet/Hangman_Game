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
using SQLite;

namespace Hangman.Classes
{
    [Table("HangmanScore")]
    public class HangmanScore
    {
        // Class for the HangmanScore SQLite DB
        [PrimaryKey, AutoIncrement]

        //Fields that are in the DB
        public int Id { get; set; }
        [MaxLength(25)]
        public string Name { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}