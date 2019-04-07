WITH cte (FromPerson, ToPerson) 
AS (
      SELECT FromPerson, ToPerson
      From [vsexamples].[dbo].[FriendShip]
      WHERE FromPerson = 13
      UNION ALL
      SELECT cte.FromPerson as FromPerson, cte.ToPerson as ToPerson
      FROM [vsexamples].[dbo].[FriendShip] f
      INNER JOIN  cte
      ON cte.FromPerson = f.ToPerson
    )
SELECT * 
FROM cte