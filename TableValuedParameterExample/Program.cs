using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var movieDataManager = new MovieDataManager();

            var allMovies = GetAllMovies();
            movieDataManager.CreateMovies(allMovies);

            var imdbMovies = movieDataManager.GetMoviesGenre(Genre.SciFi);

            foreach (var movie in imdbMovies)
            {
                Console.WriteLine($"Title: {movie.Title}\tGenre: {movie.Genre}\tYear: {movie.Year}\tImageUrl:{movie.ImageUrl}");
            }

            imdbMovies = movieDataManager.GetMoviesYear(2013);

            Console.WriteLine("");

            foreach (var movie in imdbMovies)
            {
                Console.WriteLine($"Title: {movie.Title}\tGenre: {movie.Genre}\tYear: {movie.Year}\tImageUrl:{movie.ImageUrl}");
            }
        }

        public static IEnumerable<ImdbMovie> GetAllMovies()
        {
            yield return new ImdbMovie("Star Wars Episode IV: A New Hope", Genre.SciFi, 1977, "StarWarsEpisodeIV.jpg");
            yield return new ImdbMovie("Star Wars Episode V: The Empire Strikes Back", Genre.SciFi, 1980, "StarWarsEpisodeV.jpg");
            yield return new ImdbMovie("Star Wars Episode VI: Return of the Jedi", Genre.SciFi, 1983, "StarWarsEpisodeVI.jpg");
            yield return new ImdbMovie("Star Wars: Episode I: The Phantom Menace", Genre.SciFi, 1999, "StarWarsEpisodeI.jpg");
            yield return new ImdbMovie("Star Wars Episode II: Attack of the Clones", Genre.SciFi, 2002, "StarWarsEpisodeII.jpg");
            yield return new ImdbMovie("Star Wars: Episode III: Revenge of the Sith", Genre.SciFi, 2005, "StarWarsEpisodeIII.jpg");
            yield return new ImdbMovie("Olympus Has Fallen", Genre.Action, 2013, "Olympus_Has_Fallen_poster.jpg");
            yield return new ImdbMovie("G.I. Joe: Retaliation", Genre.Action, 2013, "GIJoeRetaliation.jpg");
            yield return new ImdbMovie("Jack the Giant Slayer", Genre.Action, 2013, "jackgiantslayer4.jpg");
            yield return new ImdbMovie("Drive", Genre.Action, 2011, "FileDrive2011Poster.jpg");
            yield return new ImdbMovie("Sherlock Holmes", Genre.Action, 2009, "FileSherlock_Holmes2Poster.jpg");
            yield return new ImdbMovie("The Girl with the Dragon Tatoo", Genre.Drama, 2011, "FileThe_Girl_with_the_Dragon_Tattoo_Poster.jpg");
            yield return new ImdbMovie("Saving Private Ryan", Genre.Drama, 1998, "SavingPrivateRyan.jpg");
            yield return new ImdbMovie("Schindlers List", Genre.Drama, 1993, "SchindlersList.jpg");
            yield return new ImdbMovie("Good Will Hunting", Genre.Drama, 1997, "FileGood_Will_Hunting_theatrical_poster.jpg");
            yield return new ImdbMovie("Citizen Kane", Genre.Drama, 1941, "Citizenkane.jpg");
            yield return new ImdbMovie("Shawshank Redemption", Genre.Drama, 1994, "FileShawshankRedemption.jpg");
            yield return new ImdbMovie("Forest Gump", Genre.Drama, 1994, "ForrestGump.jpg");
            yield return new ImdbMovie("We Bought a Zoo", Genre.Drama, 2011, "FileWe_Bought_a_Zoo_Poster.jpg");
            yield return new ImdbMovie("A Beautiful Mind", Genre.Drama, 2001, "FileAbeautifulmindposter.jpg");
            yield return new ImdbMovie("Avatar", Genre.SciFi, 2009, "Avatar.jpg");
            yield return new ImdbMovie("Iron Man", Genre.SciFi, 2008, "IronMan.jpg");
            yield return new ImdbMovie("Terminator 2", Genre.SciFi, 1991, "Terminator2.jpg");
            yield return new ImdbMovie("The Dark Knight", Genre.SciFi, 2001, "TheDarkKnight.jpg");
            yield return new ImdbMovie("The Matrix", Genre.SciFi, 1999, "TheMatrix.jpg");
            yield return new ImdbMovie("Transformers", Genre.SciFi, 2007, "Transformers.jpg");
            yield return new ImdbMovie("Revenge Of The Fallen", Genre.SciFi, 2009, "TransformersRevengeOfTheFallen.jpg");
            yield return new ImdbMovie("The Dark of the Moon", Genre.SciFi, 2011, "TransformersTheDarkoftheMoon.jpg");
            yield return new ImdbMovie("X-Men First Class", Genre.SciFi, 2011, "XMenFirstClass.jpg");
            yield return new ImdbMovie("Snitch", Genre.Thriller, 2013, "Snitch.jpg");
            yield return new ImdbMovie("Life Of Pi", Genre.Drama, 2012, "LifeOfPi.jpg");
            yield return new ImdbMovie("The Call", Genre.Thriller, 2013, "TheCall.jpg");
            yield return new ImdbMovie("Wake in Fright", Genre.Thriller, 1971, "WakeInFright.jpg");
            yield return new ImdbMovie("Oblivion", Genre.SciFi, 2013, "Oblivion.jpg");
            yield return new ImdbMovie("American Sniper", Genre.Thriller, 2015, "AmericanSniper.jpg");
            yield return new ImdbMovie("Run All Night", Genre.Thriller, 2015, "RunAllNight.jpg");
            yield return new ImdbMovie("Mission: Impossible - Rogue Nation", Genre.Thriller, 2015, "MissionImpossibleRogueNation.jpg");
            yield return new ImdbMovie("Spectre", Genre.Thriller, 2015, "Spectre.jpg");
            yield return new ImdbMovie("Insurgent", Genre.Thriller, 2015, "Insurgent.jpg");
            yield return new ImdbMovie("Kill Me Three Times", Genre.Thriller, 2014, "KillMeThreeTimes.jpg");
            yield return new ImdbMovie("Batman v Superman: Dawn of Justice", Genre.Action, 2016, "BatmanVSupermanDawnofJustice.jpg");
            yield return new ImdbMovie("Avengers: Age of Ultron", Genre.Action, 2015, "AvengersAgeofUltron.jpg");
            yield return new ImdbMovie("Guardians of the Galaxy", Genre.Action, 2015, "GuardiansoftheGalaxy.jpg");
            yield return new ImdbMovie("Kingsman: The Secret Service", Genre.Action, 2014, "KingsmanTheSecretService.jpg");
            yield return new ImdbMovie("Seventh Son", Genre.Action, 2014, "SeventhSon.jpg");
            yield return new ImdbMovie("Maze Runner: The Scorch Trials", Genre.Thriller, 2015, "MazeRunnerTheScorchTrials.jpg");
        }
    }


}
