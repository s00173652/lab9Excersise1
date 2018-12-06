using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab9Excersise1
{
    public class Movie
    {

        public string Title { get; set; }
        public string Genre { get; set; }
        public int Rating { get; set; }

        public Movie(string title, string genre, int rating)
        {
            Title = title;
            Genre = genre;
            Rating = rating;
        }
        public Movie(string title):this(title, "Unknown", 0)
        {

        }
        public Movie():this("Unknown")
        {

        }

        public override string ToString()
        {
            return string.Format($"{Title}, Genre: {Genre}, Rating: {Rating}");
        }
    }
}
