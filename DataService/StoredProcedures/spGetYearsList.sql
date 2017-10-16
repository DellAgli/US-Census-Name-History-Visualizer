CREATE PROCEDURE [dbo].[spGetYearsList]
AS
	SELECT Distinct [Year] from tblNames
	Order by [Year]