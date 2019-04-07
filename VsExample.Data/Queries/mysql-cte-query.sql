WITH RECURSIVE cte (Id, FromPerson, ToPerson, Round) 
AS (
      SELECT Id, FromPerson, ToPerson, 1 as Round
      From vsexamples.FriendShip
      WHERE FromPerson = 13
      UNION ALL
      SELECT f.Id as Id, f.FromPerson as FromPerson, f.ToPerson as ToPerson, cte.Round + 1 as Round
      FROM vsexamples.FriendShip f
      INNER JOIN  cte
      ON f.FromPerson = cte.ToPerson And cte.Round < 3
    )
SELECT * 
FROM cte