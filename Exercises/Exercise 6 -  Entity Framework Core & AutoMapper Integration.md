# Exercise 6: Entity Framework Core & AutoMapper Integration

## Installation

From your project root, install the required packages:

```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package AutoMapper
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection

---

## Project Overview
Extend the customer management API from previous exercises to:

1. **Use EF Core with an in-memory database for persistence.**  
 
2. **Implement a second ICustomerRepository using DbContext*  

3. **Switch your DI registration from the JSON file repository to the EF Core repository.**  

4. **Introduce a CustomerDto with a Uid property (to practice distinguishing DTOs from entities).**  
  
5. *Use AutoMapper to map between Customer entities and CustomerDto.**  
   - Annotate each endpoint with `.WithTags("Customers")` (or other logical group names) so they appear grouped in Swagger UI.


Register EfCore with InMemory mode
```csharp
builder.Services.AddDbContext<CustomerDbContext>(opts =>
    opts.UseInMemoryDatabase("CustomerDb"));
```

