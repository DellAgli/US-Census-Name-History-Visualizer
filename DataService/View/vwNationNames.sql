CREATE VIEW [dbo].[vwNationNames]
	AS
		SELECT [Year],[Name], Sum(Number) as Number 
		FROM [dbo].[tblNames]
		GROUP BY [Name],[Year]
