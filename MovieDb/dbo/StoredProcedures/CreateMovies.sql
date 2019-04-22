CREATE PROCEDURE [dbo].[CreateMovies]
	@MovieTvp MovieTvp READONLY
AS
	SET NOCOUNT ON
	SET IDENTITY_INSERT dbo.Movie ON

	-- Insert Distinct Genres into the Genre table
	INSERT
	INTO	dbo.Genre
	SELECT DISTINCT(Genre)
	FROM	@MovieTvp
	EXCEPT
	SELECT	dbo.Genre.Title
	FROM	dbo.Genre


	--SELECT	DISTINCT Genre
	--FROM	@MovieTvp

	-- Insert Movies into the Movie Table
	DECLARE	@MaxMovieId int = 0;
	SELECT	@MaxMovieId = ISNULL(MAX(Id), 0)
	FROM	dbo.Movie

	INSERT
	INTO	dbo.Movie (Id, Title, [Year], ImageUrl)
	SELECT	@MaxMovieId + ROW_Number() OVER (ORDER BY Title) Id, Title, [Year], ImageUrl
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

	SET IDENTITY_INSERT dbo.Movie OFF
	SET NOCOUNT OFF


RETURN 0
