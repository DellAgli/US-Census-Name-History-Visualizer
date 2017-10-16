CREATE TABLE [dbo].[tblNames]
(
	[State] VARCHAR(2) NOT NULL , 
    [Gender] VARCHAR NOT NULL, 
    [Year] INT NOT NULL, 
    [Name] VARCHAR(64) NOT NULL, 
    [Number] INT NOT NULL, 
    PRIMARY KEY ([Name], [Year], [State], [Gender]), 
	
    
)
