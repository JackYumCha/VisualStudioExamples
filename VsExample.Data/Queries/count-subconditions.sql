SELECT 
Name,
count(Id) as Count,
avg(price) as AveragePrice,
sum(case when Age <= 6 then 1 else 0 end) as NumberNotOlderThan6,
sum(case when Age > 6 then 1 else 0 end) as NumberOlderThan6,
sum(case when Age <= 6 then Price else 0 end)/sum(case when Age <= 6 then 1 else 0 end) as AvgPriceNotOlderThan6,
sum(case when Age > 6 then Price else 0 end)/sum(case when Age > 6 then 1 else 0 end) as AvgPriceOlderThan6
FROM vsexamples.Animals
Group By Name;