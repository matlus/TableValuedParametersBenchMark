using System.Collections.Generic;
using System.Reflection;

namespace ConsoleApp2
{
    public static class GenreParser
    {
        private static Dictionary<string, Genre> s_stringToGenreDictionary = new Dictionary<string, Genre>();
        private static Dictionary<Genre, string> s_genreToStringDictionary = new Dictionary<Genre, string>();

        static GenreParser()
        {
            FieldInfo[] fieldInfos = typeof(Genre).GetFields(BindingFlags.GetField | BindingFlags.Public | BindingFlags.Static);

            foreach (var fieldInfo in fieldInfos)
            {
                var genre = (Genre)fieldInfo.GetValue(null);
                var enumDescriptionAttributes = (EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);

                if (enumDescriptionAttributes.Length > 0)
                {
                    foreach (var attib in enumDescriptionAttributes)
                    {
                        s_stringToGenreDictionary.Add(attib.Description.ToLower(), genre);

                        if (!s_genreToStringDictionary.ContainsKey(genre))
                        {
                            s_genreToStringDictionary.Add(genre, attib.Description);
                        }
                    }
                }
                else
                {
                    s_stringToGenreDictionary.Add(genre.ToString().ToLower(), genre);
                    s_genreToStringDictionary.Add(genre, genre.ToString());
                }
            }
        }

        public static Genre Parse(string genreAsString)
        {
            var genreLowercase = genreAsString.ToLower();
            if (s_stringToGenreDictionary.ContainsKey(genreLowercase))
            {
                return s_stringToGenreDictionary[genreLowercase];
            }

            throw new InvalidGenreException($"The value: {genreAsString}, is not a valid Genre. Valid values are {string.Join(", ", ValidGenres())}");
        }

        public static string ToString(Genre genre)
        {
            return s_genreToStringDictionary[genre];
        }

        public static IEnumerable<string> ValidGenres()
        {
            foreach (var kvp in s_stringToGenreDictionary)
            {
                yield return kvp.Key;
            }
        }
    }


}
