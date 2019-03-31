SELECT 
Name,
count(Id) as Count,
avg(price) as AveragePrice
FROM vsexamples.Animals
Group By Name;