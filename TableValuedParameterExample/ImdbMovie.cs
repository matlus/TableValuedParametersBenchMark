using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public sealed class ImdbMovie
    {
        public Genre Genre { get; }
        public string ImageUrl { get; }
        public string Title { get; }
        public int Year { get; }

        public ImdbMovie(string title, Genre genre, int year, string imageUrl)
        {
            Title = title;
            Genre = genre;
            Year = year;
            ImageUrl = imageUrl;
        }
    }
}
