IF OBJECT_ID( N'dbo.ParserConfiguration_Select', 'P' ) IS NOT NULL
BEGIN
	PRINT 'DROPPING PROCEDURE dbo.ParserConfiguration_Select'
	DROP PROCEDURE dbo.ParserConfiguration_Select
END
GO

CREATE PROCEDURE dbo.ParserConfiguration_Select
AS
BEGIN
	SELECT *
	FROM dbo.ParserConfiguration (nolock)
	ORDER BY Id ASC
END
GO
