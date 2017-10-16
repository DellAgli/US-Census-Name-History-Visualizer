CREATE PROCEDURE [dbo].[spPutPronounciation]
	@Name varchar(64),
	@Pronounciation varchar(64)
AS
	If Exists(Select 1 from tblNames Where [Name] = @Name)
		Insert into tblPronounciation(Name,Pronounciation)
		Values (@Name,@Pronounciation );
	
