<h1>Chiến Lược Làm Bài</h1>

CREATE PROJECT
```csharp
dotnet new webapi -n YourProjectName.API
dotnet new classlib -n YourProjectName.REPO
dotnet new classlib -n YourProjectName.DAO
dotnet new classlib -n YourProjectName.BO
```

SCAFFOLD EF COMMAND IN DAO <br/>
```csharp
dotnet ef dbcontext scaffold "Server=(local);Database=SilverJewelry2023DB;Uid=SA;Pwd=MyStrongPass123;Trust Server Certificate=True;" Microsoft.EntityFrameworkCore.SqlServer
```

=> Add Entity Qua BO

DAO LAYER <br/>
```csharp
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.7
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.7
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.7
dotnet add package Microsoft.Extensions.Configuration.Json --version 8.0.1
```

REPO LAYER <br/>
```csharp
dotnet add package Microsoft.Extensions.Configuration.Binder --version 8.0.1
```

API LAYER <br/>
```csharp
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.7
dotnet add package Microsoft.AspNetCore.OData --version 9.0.0
dotnet add package Microsoft.OpenApi.OData --version 9.0.0
dotnet add package Swashbuckle.AspNetCore.Annotations --version 6.9.0
```

Add Connection String Trong DAO

```csharp
private static string? GetConnectionString()
{
    IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .Build();
    return configuration["ConnectionStrings:DefaultConnectionString"];
}

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer(GetConnectionString());
```

APP.SETTINGS <br/>
```json
{
  "ConnectionStrings": {
    "DefaultConnectionString": "Server=(local);Database=SilverJewelry2023DB;Uid=SA;Pwd=MyStrongPass123;Trust Server Certificate=True;"
  },
  "JwtOptions": {
    "SecretKey": "IRanUIwukUBzSauFsZnr7AjV7XS96moon",
    "Issuer": "bentran1vn",
    "Audience": "bentran1vn",
    "ExpireMinutes": 120
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

SetUp Trong DAO <br/>
SetUp Trong REPO <br/>
SetUp Trong API <br/>

