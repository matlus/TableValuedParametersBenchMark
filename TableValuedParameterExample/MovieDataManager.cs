using ConsoleApp2.CommandFactories;
using ConsoleApp2.ModelAdapters;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp2.Extensions;

namespace ConsoleApp2
{
    internal sealed class MovieDataManager
    {
        private DbProviderFactory _dbProviderFactory;

        public MovieDataManager()
        {
            _dbProviderFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        }

        private DbConnection CreateDbConnection()
        {
            var dbConnection = _dbProviderFactory.CreateConnection();
            dbConnection.ConnectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=MovieDb;Integrated Security=True";
            return dbConnection;
        }

        public void CreateMovies(IEnumerable<ImdbMovie> imdbMovies)
        {
            DbConnection dbConnection = null;
            DbTransaction dbTransaction = null;
            DbCommand dbCommand = null;
            try
            {
                dbConnection = CreateDbConnection();
                dbConnection.Open();
                dbTransaction = dbConnection.BeginTransaction();
                dbCommand = CommandFactoryMovies.CreateCommandForCreateMovies(dbConnection, dbTransaction, imdbMovies);
                dbCommand.ExecuteNonQuery();
                dbTransaction.Commit();
            }
            catch (DbException)
            {
                if (dbTransaction != null)
                {
                    dbTransaction.Rollback();
                }
                throw;
            }
            finally
            {
                dbCommand.DisposeIfNotNull();
                dbTransaction.DisposeIfNotNull();
                dbConnection.CloseAndDispose();
            }
        }

        public IEnumerable<ImdbMovie> GetAllMovies()
        {
            DbConnection dbConnection = null;
            DbCommand dbCommand = null;
            try
            {
                dbConnection = CreateDbConnection();
                dbConnection.Open();
                dbCommand = CommandFactoryMovies.CreateCommandForGetAllMovies(dbConnection);
                var dbDataReader = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
                return ModelAdapterMovies.ToImdbMovies(dbDataReader);
            }
            finally
            {
                dbCommand.DisposeIfNotNull();
            }
        }

        public IEnumerable<ImdbMovie> GetMoviesGenre(Genre genre)
        {
            DbConnection dbConnection = null;
            DbCommand dbCommand = null;
            try
            {
                dbConnection = CreateDbConnection();
                dbConnection.Open();
                dbCommand = CommandFactoryMovies.CreateCommandForGetMoviesByGenre(dbConnection, genre);
                var dbDataReader = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
                return ModelAdapterMovies.ToImdbMovies(dbDataReader);
            }
            finally
            {
                dbCommand.DisposeIfNotNull();
            }
        }

        public IEnumerable<ImdbMovie> GetMoviesYear(int year)
        {
            DbConnection dbConnection = null;
            DbCommand dbCommand = null;
            try
            {
                dbConnection = CreateDbConnection();
                dbConnection.Open();
                dbCommand = CommandFactoryMovies.CreateCommandForGetMoviesByYear(dbConnection, year);
                var dbDataReader = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
                return ModelAdapterMovies.ToImdbMovies(dbDataReader);
            }
            finally
            {
                dbCommand.DisposeIfNotNull();
            }
        }
    }
}
