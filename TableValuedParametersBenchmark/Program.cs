using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using TableValuedParameterExample;

namespace ConsoleApp2
{
    [MemoryDiagnoser]
    public class Program
    {
        private static readonly int[] s_validYears = new int[] { 2000, 2001, 2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009, 2010, 2011, 2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019 };
        private static readonly string[] s_validGenres = GenreParser.ValidGenres().ToArray();

        private readonly MovieDataManager _movieDataManager = new MovieDataManager();
        private string[] s_uniqueMovieTitles;
        private IEnumerable<ImdbMovie> _allMovies;

        [Params(10/*, 20, 30, 40, 50, 100, 1000, 10000, 100000*/)]
        public int NumberOfRecords { get; set; }

        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Program>();
        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            s_uniqueMovieTitles = Randomizer.GenerateUniqueAsciiStrings(NumberOfRecords);
            _allMovies = GetRandomImdbMovies(NumberOfRecords);
        }

        [IterationSetup]
        public void IterationStep()
        {
            _movieDataManager.DropAndRecreateMovieDb();
        }

        [Benchmark]
        public void CreateMoviesUsingTvp()
        {            
            _movieDataManager.CreateMovies(_allMovies);
        }

        [Benchmark]
        public void CreateMoviesWithoutTvp()
        {
            _movieDataManager.CreateMoviesWithoutTvp(_allMovies);
        }

        private IEnumerable<ImdbMovie> GetRandomImdbMovies(int requiredCount)
        {
            var count = 0;
            do
            {
                yield return GetRandomMovie(s_uniqueMovieTitles[count]);
                count++;
            } while (count < requiredCount);
        }

        private static ImdbMovie GetRandomMovie(string title)
        {
            var shuffledYears = Randomizer.ShuffleArray(s_validYears);
            var shuffledGenres = Randomizer.ShuffleArray(s_validGenres);

            return new ImdbMovie(
                title: title,
                genre: GenreParser.Parse(shuffledGenres[0]),
                year: shuffledYears[0],
                imageUrl: Randomizer.GetRandomAciiString(50));
        }
    }
}

