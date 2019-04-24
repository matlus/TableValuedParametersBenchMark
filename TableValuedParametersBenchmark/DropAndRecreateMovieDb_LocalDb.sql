USE [master]
GO

DROP DATABASE IF EXISTS [MovieDb]

/****** Object:  Database [MovieDb]    Script Date: 4/18/2019 1:23:17 PM ******/
CREATE DATABASE [MovieDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MovieDb', FILENAME = N'C:\Users\c102116\AppData\Local\Microsoft\VisualStudio\SSDTMovieDb_Primary.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )

GO
ALTER DATABASE [MovieDb] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MovieDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MovieDb] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [MovieDb] SET ANSI_NULLS ON 
GO
ALTER DATABASE [MovieDb] SET ANSI_PADDING ON 
GO
ALTER DATABASE [MovieDb] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [MovieDb] SET ARITHABORT ON 
GO
ALTER DATABASE [MovieDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MovieDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MovieDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MovieDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MovieDb] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [MovieDb] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [MovieDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MovieDb] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [MovieDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MovieDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MovieDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MovieDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MovieDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MovieDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MovieDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MovieDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MovieDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MovieDb] SET RECOVERY FULL 
GO
ALTER DATABASE [MovieDb] SET  MULTI_USER 
GO
ALTER DATABASE [MovieDb] SET PAGE_VERIFY NONE  
GO
ALTER DATABASE [MovieDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MovieDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MovieDb] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [MovieDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MovieDb] SET QUERY_STORE = OFF
GO
USE [MovieDb]
GO
/****** Object:  UserDefinedTableType [dbo].[MovieTvp]    Script Date: 4/24/2019 3:20:42 PM ******/
CREATE TYPE [dbo].[MovieTvp] AS TABLE(
	[Title] [varchar](50) NOT NULL,
	[Genre] [varchar](50) NOT NULL,
	[Year] [int] NOT NULL,
	[ImageUrl] [varchar](200) NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Title] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[Assoc_MovieGenre]    Script Date: 4/24/2019 3:20:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assoc_MovieGenre](
	[MovieId] [int] NOT NULL,
	[GenreId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genre]    Script Date: 4/24/2019 3:20:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genre](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Genre] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movie]    Script Date: 4/24/2019 3:20:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movie](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Year] [int] NOT NULL,
	[ImageUrl] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[MovieVw]    Script Date: 4/24/2019 3:20:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MovieVw]
AS
	SELECT
			dbo.Movie.Title,
			dbo.Genre.Title AS Genre,
			dbo.Movie.Year,
			dbo.Movie.ImageUrl
	FROM	dbo.Movie
	INNER JOIN dbo.Assoc_MovieGenre ON dbo.Movie.Id = dbo.Assoc_MovieGenre.MovieId
	INNER JOIN dbo.Genre ON dbo.Genre.Id = dbo.Assoc_MovieGenre.GenreId
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Genre]    Script Date: 4/24/2019 3:20:42 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Genre] ON [dbo].[Genre]
(
	[Title] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Movie]    Script Date: 4/24/2019 3:20:42 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Movie] ON [dbo].[Movie]
(
	[Title] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Assoc_MovieGenre]  WITH CHECK ADD  CONSTRAINT [FK_Assoc_MovieGenre_Genre] FOREIGN KEY([GenreId])
REFERENCES [dbo].[Genre] ([Id])
GO
ALTER TABLE [dbo].[Assoc_MovieGenre] CHECK CONSTRAINT [FK_Assoc_MovieGenre_Genre]
GO
ALTER TABLE [dbo].[Assoc_MovieGenre]  WITH CHECK ADD  CONSTRAINT [FK_Assoc_MovieGenre_Movie] FOREIGN KEY([MovieId])
REFERENCES [dbo].[Movie] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Assoc_MovieGenre] CHECK CONSTRAINT [FK_Assoc_MovieGenre_Movie]
GO
/****** Object:  StoredProcedure [dbo].[CreateMovie]    Script Date: 4/24/2019 3:20:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateMovie]
	@Title varchar(50),
	@Genre varchar(50),
	@Year int,
	@ImageUrl varchar(200)
AS
	SET NOCOUNT ON

	INSERT INTO dbo.Movie
	(Title, Year, ImageUrl)
	VALUES(@Title, @Year, @ImageUrl)
	
	DECLARE @MovieId int = SCOPE_IDENTITY()
	DECLARE @GenreId int;

	SELECT @GenreId = dbo.Genre.Id FROM dbo.Genre WHERE dbo.Genre.Title = @Genre

	IF (@GenreId IS NULL)
	BEGIN
		INSERT INTO dbo.Genre
		(Title)
		VALUES(@Genre)

		SET @GenreId = SCOPE_IDENTITY()
	END
	
	INSERT INTO dbo.Assoc_MovieGenre
	(MovieId, GenreId)
	VALUES(@MovieId, @GenreId)

	SET NOCOUNT OFF

RETURN @MovieId
GO
/****** Object:  StoredProcedure [dbo].[CreateMoviesTvpDistinctInsertInto]    Script Date: 4/24/2019 3:20:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateMoviesTvpDistinctInsertInto]
	@MovieTvp MovieTvp READONLY
AS
	SET NOCOUNT ON	

	-- Insert Distinct Genres into the Genre table
	INSERT
	INTO	dbo.Genre
	SELECT DISTINCT(Genre)
	FROM	@MovieTvp
	EXCEPT
	SELECT	dbo.Genre.Title
	FROM	dbo.Genre

	-- Insert Movies into the Movie Table
	INSERT
	INTO	dbo.Movie (Title, [Year], ImageUrl)
	SELECT	Title, [Year], ImageUrl
	FROM	@MovieTvp m
	
	-- Select the Movie.Id and the Genre.Id columns and insert into the Assoc_MovieGenre
	INSERT
	INTO	dbo.Assoc_MovieGenre
	SELECT	dbo.Movie.Id, dbo.Genre.Id
	FROM	@MovieTvp mtvp
	INNER
	JOIN	dbo.Genre
	ON		dbo.Genre.Title = mtvp.Genre
	INNER
	JOIN	dbo.Movie
	ON		dbo.Movie.Title = mtvp.Title

	SET NOCOUNT OFF
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[CreateMoviesTvpMergeInsertInto]    Script Date: 4/24/2019 3:20:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateMoviesTvpMergeInsertInto]
	@MovieTvp MovieTvp READONLY
AS
	SET NOCOUNT ON

	MERGE
	INTO	dbo.Genre as g 
	USING	(SELECT DISTINCT(Genre)
			FROM @MovieTvp) as mtvp 
			ON g.Title = mtvp.Genre 
	WHEN NOT MATCHED THEN 
	INSERT
	VALUES	(mtvp.Genre);


	-- Insert Movies into the Movie Table
	INSERT
	INTO	dbo.Movie (Title, [Year], ImageUrl)
	SELECT	Title, [Year], ImageUrl
	FROM	@MovieTvp m
	
	-- Select the Movie.Id and the Genre.Id columns and insert into the Assoc_MovieGenre
	INSERT
	INTO	dbo.Assoc_MovieGenre
	SELECT	dbo.Movie.Id, dbo.Genre.Id
	FROM	@MovieTvp mtvp
	INNER
	JOIN	dbo.Genre
	ON		dbo.Genre.Title = mtvp.Genre
	INNER
	JOIN	dbo.Movie
	ON		dbo.Movie.Title = mtvp.Title

	SET NOCOUNT OFF

RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[CreateMoviesTvpMergeMerge]    Script Date: 4/24/2019 3:20:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateMoviesTvpMergeMerge]
	@MovieTvp MovieTvp READONLY
AS
	SET NOCOUNT ON

	MERGE
	INTO	dbo.Genre as g 
	USING	(SELECT DISTINCT(Genre)
			FROM @MovieTvp) as mtvp 
			ON g.Title = mtvp.Genre 
	WHEN NOT MATCHED THEN 
	INSERT
	VALUES	(mtvp.Genre);

	-- Insert Movies into the Movie Table
	MERGE
	INTO	dbo.Movie as m 
	USING	(SELECT * FROM @MovieTvp) as mtvp 
			ON m.Title = mtvp.Title 
	WHEN NOT MATCHED THEN	
	INSERT
	VALUES	(mtvp.Title, mtvp.[Year], mtvp.ImageUrl);
	
	-- Select the Movie.Id and the Genre.Id columns and insert into the Assoc_MovieGenre
	INSERT
	INTO	dbo.Assoc_MovieGenre
	SELECT	dbo.Movie.Id, dbo.Genre.Id
	FROM	@MovieTvp mtvp
	INNER
	JOIN	dbo.Genre
	ON		dbo.Genre.Title = mtvp.Genre
	INNER
	JOIN	dbo.Movie
	ON		dbo.Movie.Title = mtvp.Title

	SET NOCOUNT OFF

RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[CreateMoviesTvpUsingCursor]    Script Date: 4/24/2019 3:20:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateMoviesTvpUsingCursor]
	@MovieTvp MovieTvp READONLY
AS

	SET NOCOUNT ON

	DECLARE @title varchar(50), @genre varchar(50), @year int, @imageurl varchar(200)

	DECLARE movie_cursor CURSOR FOR   
	SELECT Title, Genre, [Year], ImageUrl
	FROM @MovieTvp
  
	OPEN movie_cursor  

	FETCH NEXT FROM movie_cursor   
	INTO @title, @genre, @year, @imageUrl
  
	WHILE @@FETCH_STATUS = 0  
	BEGIN
		EXEC dbo.CreateMovie @title, @genre, @year, @imageUrl
		FETCH NEXT FROM movie_cursor
		INTO @title, @genre, @year, @imageUrl
	END
	CLOSE movie_cursor;  
	DEALLOCATE movie_cursor;  

	SET NOCOUNT OFF
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[GetAllMovies]    Script Date: 4/24/2019 3:20:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllMovies]
AS
	SET NOCOUNT ON;
	SELECT	Title, Genre, Year, ImageUrl FROM dbo.MovieVw

RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[GetMoviesByGenre]    Script Date: 4/24/2019 3:20:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetMoviesByGenre]
	@Genre varchar(50)
AS
	SELECT	Title, Genre, Year, ImageUrl FROM dbo.MovieVw
	WHERE	Genre = @Genre

RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[GetMoviesByYear]    Script Date: 4/24/2019 3:20:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetMoviesByYear]
	@Year int
AS
	SET NOCOUNT ON;
	
	SELECT	Title, Genre, Year, ImageUrl FROM dbo.MovieVw
	WHERE	Year = @Year

RETURN 0
GO
USE [master]
GO
ALTER DATABASE [MovieDb] SET  READ_WRITE 
GO