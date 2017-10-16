CREATE PROCEDURE [dbo].[spGetStatesList]
AS
	SELECT x.[State],y.[StateName]
	from (Select Distinct State from tblNames) x  Join tblStateDetails y
	On x.State = y.State
	Order by y.StateName

