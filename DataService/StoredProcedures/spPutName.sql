CREATE PROCEDURE [dbo].[spPutName]
	@State varchar(2),
	@Gender varchar(1),
	@Year int,
	@Name varchar(64),
	@Number int
AS
	Insert Into [dbo].[tblNames] ([State],[Gender],[Year],[Name],[Number])
	Values (@State,@Gender,@Year,@Name,@Number);
	
RETURN 0
