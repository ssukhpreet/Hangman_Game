using Android.App;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace Hangman.Classes
{
    public class DataAdapter : BaseAdapter<HangmanScore>
    {
        private readonly Activity mcontext;
        private readonly List<HangmanScore> mItems;

        public DataAdapter(Activity context, List<HangmanScore> Items)
        {
            this.mcontext = context;
            this.mItems = Items;
        }

        public override int Count
        {
            get { return mItems.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override HangmanScore this[int position]
        {
            get { return mItems[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mcontext).Inflate(Resource.Layout.Listview, null, false);
            }

            //Set the txtRowName.Text to the players name
            TextView txtRowName = row.FindViewById<TextView>(Resource.Id.txtRowName);
            txtRowName.Text = mItems[position].Name;
            //Set the txtRowScore.Text to the players score
            TextView txtScore = row.FindViewById<TextView>(Resource.Id.txtRowScore);
            txtScore.Text = mItems[position].Score.ToString();

            return row;
        }
    }
}
