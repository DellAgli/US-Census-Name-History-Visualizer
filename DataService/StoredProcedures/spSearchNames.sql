CREATE PROCEDURE [dbo].[spSearchNames]
	@statesarray varchar(256),
	@namesarray varchar(1024)
AS
	Select y.Year, IsNull(Males,0) as Males,IsNull(Females,0) as Females
	From (Select Distinct [Year] from tblNames) y
	Left Outer Join 
	(SELECT IsNull(m.[Year],f.[Year]) as [Year], m.Number as [Males], F.Number as [Females]
	from (Select  [Year], Sum(Number) as Number
		  From tblNames
		  Where [Gender] = 'M'
			And @statesarray like '%|'+[State]+'|%'
			And @namesarray like '%|'+[Name]+'|%'
		  Group by [Year]) m
		  Full Outer Join
		  (Select [Year], Sum(Number) as Number
		  From tblNames
		  Where [Gender] = 'F'
		    And @statesarray like '%|'+[State]+'|%'
			And @namesarray like '%|'+[Name]+'|%'
		
		  Group by [Year]) f
		  On m.Year = f.Year
	) d
	On y.Year = d.Year
	Order by y.Year
