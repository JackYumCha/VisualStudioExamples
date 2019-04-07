WITH RECURSIVE cte (FromPerson, ToPerson) 
AS (
      SELECT FromPerson, ToPerson
      From vsexamples.FriendShip
      WHERE FromPerson = 13
      UNION ALL
      SELECT cte.FromPerson as FromPerson, cte.ToPerson as ToPerson
      FROM vsexamples.FriendShip f
      INNER JOIN  cte
      ON cte.FromPerson = f.ToPerson
    )
SELECT * 
FROM cte