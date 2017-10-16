CREATE TABLE [dbo].[tblPronounciation]
(
	[Name] VARCHAR(64) NOT NULL, 
    [Pronounciation] VARCHAR(64) NOT NULL, 
    CONSTRAINT [PK_tblPronounciation] PRIMARY KEY ([Name], [Pronounciation]), 
    
)
