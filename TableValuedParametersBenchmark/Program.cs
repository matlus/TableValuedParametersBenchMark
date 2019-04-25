using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using TableValuedParameterExample;

namespace ConsoleApp2
{
    [SimpleJob(RunStrategy.Throughput, launchCount: 1, warmupCount: 3, targetCount: 20)]
    [MemoryDiagnoser]
    public class Program
    {
        private static readonly int[] s_validYears = new int[] { 2000, 2001, 2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009, 2010, 2011, 2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019 };
        private static readonly string[] s_validGenres = GenreParser.ValidGenres().ToArray();

        private readonly MovieDataManager _movieDataManager = new MovieDataManager();
        private string[] s_uniqueMovieTitles;
        private IEnumerable<ImdbMovie> _allMovies;

        [Params(/*5, 10, 20, 30, 40, 50, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 2000, 3000, 4000, 5000, 10000, */1000000)]
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

        ////[Benchmark]
        ////public void CreateMoviesTvpMergeMerge()
        ////{            
        ////    _movieDataManager.CreateMoviesTvpMergeMerge(_allMovies);
        ////}

        ////[Benchmark]
        ////public void CreateMoviesTvpMergeInsertInto()
        ////{
        ////    _movieDataManager.CreateMoviesTvpMergeInsertInto(_allMovies);
        ////}

        ////[Benchmark]
        ////public void CreateMoviesTvpDistinctInsertInto()
        ////{
        ////    _movieDataManager.CreateMoviesTvpDistinctInsertInto(_allMovies);
        ////}

        [Benchmark]
        public void CreateMoviesTvpUsingCursor()
        {
            _movieDataManager.CreateMoviesTvpUsingCursor(_allMovies);
        }

        ////[Benchmark]
        ////public void CreateMoviesWithoutTvp()
        ////{
        ////    _movieDataManager.CreateMoviesWithoutTvp(_allMovies);
        ////}

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

