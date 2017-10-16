CREATE PROCEDURE [dbo].[spGetNamesListByPronounciation]
@search varchar(200)
AS
	SELECT Distinct n.[Name]
	From tblNames n Inner Join tblPronounciation p on n.Name=p.Name
	Where p.Pronounciation = (Select Pronounciation
								From tblPronounciation
								Where [Name] Like @search)