# Exercise 6: Entity Framework Core & AutoMapper Integration

## Installation

From your project root, install the required packages:

```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package AutoMapper
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
```
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

Create dto for the customer entity
```csharp
public class CustomerDto
{
    public Guid Uid { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
```

Create a mapping profile

## Points of Emphasis

- **DbContext & EF Core Basics**  
  Learn how `CustomerDbContext` manages entities and how to configure the in-memory provider.

- **In-Memory Database for Development**  
  Understand why an in-memory database is useful for prototyping and testing without external dependencies.

- **Repository Abstraction**  
  Compare the file-based repository (`CustomerJsonRepository`) with the EF-based `EfCustomerRepository` implementing the same interface.

- **DTO vs. Entity**  
  See how `CustomerDto` decouples your API contract from the domain model; practice mapping `Id` ↔ `Uid`.

- **AutoMapper Profiles**  
  Master configuring mapping rules in a `Profile` and how AutoMapper integrates via DI.

- **Minimal API with Typed DTOs**  
  Practice mapping DTOs in endpoint handlers for clearer, more maintainable API signatures.

- **Dependency Injection**  
  Observe how swapping implementations (JSON vs. EF Core) is trivial when both implement `ICustomerRepository` and are registered in DI.


