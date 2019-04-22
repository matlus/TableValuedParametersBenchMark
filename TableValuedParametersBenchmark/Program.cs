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
    ////[Config(typeof(Config))]
    public class Program
    {
        ////private class Config : ManualConfig
        ////{
        ////    public Config()
        ////    {
        ////        Add(
        ////            Job.Dry
        ////            .WithWarmupCount(2)
        ////            .WithIterationCount(20));
        ////    }
        ////}

        private readonly MovieDataManager _movieDataManager = new MovieDataManager();

        [Params(10, 100, 1000, 10000)]
        public int NumberOfRecords { get; set; }

        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Program>();
        }

        [IterationSetup]
        public void IterationStep()
        {
            _movieDataManager.DropAndRecreateMovieDb();
        }

        [Benchmark]
        public void CreateMoviesUsingTableValuesParameters()
        {
            var allMovies = GetRandomImdbMovies(NumberOfRecords);
            _movieDataManager.CreateMovies(allMovies);
        }

        [Benchmark]
        public void CreateMoviesWithoutTvp()
        {
            var allMovies = GetRandomImdbMovies(NumberOfRecords);
            _movieDataManager.CreateMoviesWithoutTvp(allMovies);
        }

        private static IEnumerable<ImdbMovie> GetRandomImdbMovies(int requiredCount)
        {
            var count = 0;
            do
            {
                yield return GetRandomMovie();
                count++;
            } while (count < requiredCount);
        }

        private static ImdbMovie GetRandomMovie()
        {
            var shuffledYears = Randomizer.ShuffleArray(new int[] { 2000, 2001, 2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009, 2010, 2011, 2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019 });
            var shuffledGenres = Randomizer.ShuffleArray(GenreParser.ValidGenres().ToArray());

            return new ImdbMovie(
                title: Randomizer.GetRandomAciiString(10) + " " + Randomizer.GetRandomAciiString(10),
                genre: GenreParser.Parse(shuffledGenres[0]),
                year: shuffledYears[0],
                imageUrl: Randomizer.GetRandomAciiString(50));
        }
    }
}

