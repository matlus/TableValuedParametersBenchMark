using System.Collections.Generic;
using System.Data.Common;

namespace ConsoleApp2.ModelAdapters
{
    internal static class ModelAdapterMovies
    {
        public static IEnumerable<ImdbMovie> ToImdbMovies(DbDataReader dbDataReader)
        {
            while (dbDataReader.Read())
            {
                yield return new ImdbMovie(
                    title: (string)dbDataReader[0],
                    genre: GenreParser.Parse((string)dbDataReader[1]),
                    year: (int)dbDataReader[2],
                    imageUrl: (string)dbDataReader[3]
                    );
            }
        }

        public static List<ImdbMovie> ToImdbMovieList(DbDataReader dbDataReader)
        {
            var imdbMovies = new List<ImdbMovie>();
            while (dbDataReader.Read())
            {
                imdbMovies.Add(new ImdbMovie(
                    title: (string)dbDataReader[0],
                    genre: GenreParser.Parse((string)dbDataReader[1]),
                    year: (int)dbDataReader[2],
                    imageUrl: (string)dbDataReader[3]
                    ));
            }

            return imdbMovies;
        }
    }
}
