dotnet ef dbcontext scaffold "Server=localhost;Port=3306;Database=vsexamples;Uid=root;Pwd=root;" Pomelo.EntityFrameworkCore.MySql -o MySQLDbFirst -f
dotnet ef dbcontext scaffold "User ID=postgres;Password=root;Host=localhost;Port=5499;Database=vsexamples;" Npgsql.EntityFrameworkCore.PostgreSQL -o PostgresSQLDbFirst -f
dotnet ef dbcontext scaffold "Data Source=localhost,1433;Initial Catalog=vsexamples;User ID=SA;Password=rootR0@t;" Microsoft.EntityFrameworkCore.SqlServer -o SQLServerbFirst -f