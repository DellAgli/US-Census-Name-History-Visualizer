CREATE PROCEDURE [dbo].[spGetNamesList]
@search varchar(200)
AS
	SELECT Distinct Name from tblNames
	Where @search Is Null or Name like @search+'%'