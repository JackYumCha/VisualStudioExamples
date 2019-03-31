SELECT 
Name,
count(Id) as Count
FROM vsexamples.Animals
Group By Name;