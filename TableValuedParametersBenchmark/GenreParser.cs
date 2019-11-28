using System.Collections.Generic;
using System.Reflection;

namespace ConsoleApp2
{
    public static class GenreParser
    {
        private static readonly Dictionary<string, Genre> stringToGenreMappings = new Dictionary<string, Genre>();
        private static readonly Dictionary<Genre, string> genreToStringMappings = new Dictionary<Genre, string>();

        private static readonly string joinedGenres;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S3963:\"static\" fields should be initialized inline", Justification = "There is too much going on here and so we can't put all of this in the member initializer")]
        static GenreParser()
        {
            var fieldInfos = typeof(Genre).GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (var fieldInfo in fieldInfos)
            {
                var genre = (Genre)fieldInfo.GetValue(null);

                var enumDescriptionAttributes = (EnumDescriptionAttribute[])fieldInfo
                    .GetCustomAttributes(typeof(EnumDescriptionAttribute), false);

                if (enumDescriptionAttributes.Length == 0)
                {
                    stringToGenreMappings.Add(fieldInfo.Name.ToLower(), genre);
                    genreToStringMappings.Add(genre, fieldInfo.Name);
                }
                else
                {
                    genreToStringMappings.Add(genre, enumDescriptionAttributes[0].Description);

                    foreach (var enumDescriptionAttribute in enumDescriptionAttributes)
                    {
                        stringToGenreMappings.Add(enumDescriptionAttribute.Description.ToLower(), genre);
                    }
                }
            }

            joinedGenres = string.Join(",", GetGenreValues());
        }

        public static Genre Parse(string genreAsString)
        {
            if (string.IsNullOrEmpty(genreAsString))
            {
                throw new InvalidGenreException($"The string can not be null or empty. Valid values are: {joinedGenres}");
            }

            var genreAsStringLowered = genreAsString.ToLower();
            if (!stringToGenreMappings.ContainsKey(genreAsStringLowered))
            {
                throw new InvalidGenreException($"The string: {genreAsString} is not a valid Genre. Valid values are: {joinedGenres}");
            }

            return stringToGenreMappings[genreAsStringLowered];
        }

        public static string ToString(Genre genre)
        {
            Validate(genre);
            return genreToStringMappings[genre];
        }

        public static void Validate(Genre genre)
        {
            if (!genreToStringMappings.ContainsKey(genre))
            {
                throw new InvalidGenreException($"The Genre: {genre} is not valid Genre. Valid values are: {joinedGenres}");
            }
        }

        public static IEnumerable<string> GetGenreValues()
        {
            foreach (var kvp in genreToStringMappings)
            {
                yield return kvp.Value;
            }
        }
    }
}
