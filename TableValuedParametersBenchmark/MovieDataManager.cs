﻿using ConsoleApp2.CommandFactories;
using ConsoleApp2.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;

namespace ConsoleApp2
{
    internal sealed class MovieDataManager
    {
        private readonly DbProviderFactory _dbProviderFactory;

        public MovieDataManager()
        {
            _dbProviderFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        }

        public void DropAndRecreateMovieDb()
        {
            var fileContent = File.ReadAllText(@"..\..\DropAndRecreateMovieDb_LocalDb.sql");
            var sqlqueries = fileContent.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

            SqlConnection.ClearAllPools();

            DbConnection dbConnection = null;
            DbCommand dbCommand = null;
            try
            {
                dbConnection = CreateDbConnection();
                dbCommand = new SqlCommand("query", (SqlConnection)dbConnection);
                dbConnection.Open();
                foreach (var query in sqlqueries)
                {
                    dbCommand.CommandText = query;
                    dbCommand.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                if (e.Message.Contains("provider: Named Pipes Provider, error: 0 - No process is on the other end of the pipe"))
                {
                    throw new DatasbeDoesNotExistException("Target Database MovieDb does not Exist. Please Publish the database using the  MovieDb.publish.xml Publish script from the MovieDb Project", e);
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                dbCommand.DisposeIfNotNull();
                dbConnection.CloseAndDispose();
            }
        }

        private DbConnection CreateDbConnection()
        {
            var dataSource = @"(localdb)\ProjectsV13";
            var dbConnection = _dbProviderFactory.CreateConnection();
            dbConnection.ConnectionString = $@"Data Source={dataSource};Initial Catalog=MovieDb;Integrated Security=True";
            return dbConnection;
        }

        public void CreateMoviesTvpMergeMerge(IEnumerable<ImdbMovie> imdbMovies)
        {
            DbConnection dbConnection = null;
            DbTransaction dbTransaction = null;
            DbCommand dbCommand = null;
            try
            {
                dbConnection = CreateDbConnection();
                dbConnection.Open();
                dbTransaction = dbConnection.BeginTransaction();
                dbCommand = CommandFactoryMovies.CreateCommandForCreateMoviesTvpMergeMerge(dbConnection, dbTransaction, imdbMovies);
                dbCommand.ExecuteNonQuery();
                dbTransaction.Commit();
            }
            catch (DbException)
            {
                dbTransaction.RollbackIfNotNull();
                throw;
            }
            finally
            {
                dbCommand.DisposeIfNotNull();
                dbTransaction.DisposeIfNotNull();
                dbConnection.CloseAndDispose();
            }
        }

        public void CreateMoviesTvpMergeInsertInto(IEnumerable<ImdbMovie> imdbMovies)
        {
            DbConnection dbConnection = null;
            DbTransaction dbTransaction = null;
            DbCommand dbCommand = null;
            try
            {
                dbConnection = CreateDbConnection();
                dbConnection.Open();
                dbTransaction = dbConnection.BeginTransaction();
                dbCommand = CommandFactoryMovies.CreateCommandForCreateMoviesTvpMergeInsertInto(dbConnection, dbTransaction, imdbMovies);
                dbCommand.ExecuteNonQuery();
                dbTransaction.Commit();
            }
            catch (DbException)
            {
                dbTransaction.RollbackIfNotNull();
                throw;
            }
            finally
            {
                dbCommand.DisposeIfNotNull();
                dbTransaction.DisposeIfNotNull();
                dbConnection.CloseAndDispose();
            }
        }

        public void CreateMoviesTvpDistinctInsertInto(IEnumerable<ImdbMovie> imdbMovies)
        {
            DbConnection dbConnection = null;
            DbTransaction dbTransaction = null;
            DbCommand dbCommand = null;
            try
            {
                dbConnection = CreateDbConnection();
                dbConnection.Open();
                dbTransaction = dbConnection.BeginTransaction();
                dbCommand = CommandFactoryMovies.CreateCommandForCreateMoviesTvpDistinctInsertInto(dbConnection, dbTransaction, imdbMovies);
                dbCommand.ExecuteNonQuery();
                dbTransaction.Commit();
            }
            catch (DbException)
            {
                dbTransaction.RollbackIfNotNull();
                throw;
            }
            finally
            {
                dbCommand.DisposeIfNotNull();
                dbTransaction.DisposeIfNotNull();
                dbConnection.CloseAndDispose();
            }
        }

        public void CreateMoviesTvpUsingCursor(IEnumerable<ImdbMovie> imdbMovies)
        {
            DbConnection dbConnection = null;
            DbTransaction dbTransaction = null;
            DbCommand dbCommand = null;
            try
            {
                dbConnection = CreateDbConnection();
                dbConnection.Open();
                dbTransaction = dbConnection.BeginTransaction();
                dbCommand = CommandFactoryMovies.CreateCommandForCreateMoviesTvpUsingCursor(dbConnection, dbTransaction, imdbMovies);
                dbCommand.CommandTimeout = 120;
                dbCommand.ExecuteNonQuery();
                dbTransaction.Commit();
            }
            catch (DbException)
            {
                dbTransaction.RollbackIfNotNull();
                throw;
            }
            finally
            {
                dbCommand.DisposeIfNotNull();
                dbTransaction.DisposeIfNotNull();
                dbConnection.CloseAndDispose();
            }
        }

        public void CreateMoviesWithoutTvp(IEnumerable<ImdbMovie> imdbMovies)
        {
            DbConnection dbConnection = null;
            DbTransaction dbTransaction = null;
            DbCommand dbCommand = null;
            try
            {
                dbConnection = CreateDbConnection();
                dbConnection.Open();
                dbTransaction = dbConnection.BeginTransaction();

                foreach (var imdbMovie in imdbMovies)
                {
                    dbCommand = CommandFactoryMovies.CreateCommandForCreateMovie(dbConnection, dbTransaction, imdbMovie);
                    dbCommand.ExecuteNonQuery();
                }

                dbTransaction.Commit();
            }
            catch (DbException)
            {
                dbTransaction.RollbackIfNotNull();
                throw;
            }
            finally
            {
                dbCommand.DisposeIfNotNull();
                dbTransaction.DisposeIfNotNull();
                dbConnection.CloseAndDispose();
            }
        }
    }


    [Serializable]
    public sealed class DatasbeDoesNotExistException : Exception
    {
        public DatasbeDoesNotExistException() { }
        public DatasbeDoesNotExistException(string message) : base(message) { }
        public DatasbeDoesNotExistException(string message, Exception inner) : base(message, inner) { }
        private DatasbeDoesNotExistException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
