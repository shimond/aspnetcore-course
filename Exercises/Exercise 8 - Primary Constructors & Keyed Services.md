# Exercise 8: Primary Constructors & Keyed Services

## Project Overview

Extend your customer management API to:

- **Use C# Primary Constructors** for your service and repository classes, reducing boilerplate and simplifying DI (see [C# primary constructors refactoring guide](https://devblogs.microsoft.com/dotnet/csharp-primary-constructors-refactoring/)).  
- **Register two implementations** of `ICustomerRepository`—one JSON‐file based, one EF‐Core based—each keyed by a string.  
- **Provide a resolver delegate** (`Func<string, ICustomerRepository>`) that returns the correct repository based on the key.  
- **Read a `datasourceType` header** in your Minimal API endpoints to choose between the “json” or “ef” implementation at runtime.

## Requirements

1. **Refactor to Primary Constructors**  
   - Convert `CustomerJsonRepository` and `EfCustomerRepository` into primary‐constructor classes that accept any required dependencies (e.g. `ILogger`, `CustomerDbContext`).  
   - Convert `CustomerService` into a primary‐constructor class that accepts the repository resolver (`Func<string, ICustomerRepository>`) and `IMapper`.

2. **Configure Keyed Services**  
   - In `Program.cs`, register both repositories as scoped implementations of `ICustomerRepository`.  
   - Also register a factory delegate:
     ```csharp
     builder.Services.AddScoped<CustomerJsonRepository>();
     builder.Services.AddScoped<EfCustomerRepository>();
     builder.Services.AddScoped<Func<string, ICustomerRepository>>(sp => key =>
         key switch
         {
             "ef"  => sp.GetRequiredService<EfCustomerRepository>(),
             _     => sp.GetRequiredService<CustomerJsonRepository>()
         });
     builder.Services.AddScoped<CustomerService>();
     ```
   
3. **Select Implementation at Runtime**  
   - In each Minimal API handler, read the `datasourceType` header:
     ```csharp
     var sourceKey = (httpContext.Request.Headers["datasourceType"].FirstOrDefault() ?? "json").ToLower();
     var repository = repoResolver(sourceKey);
     ```
   - Use the selected `repository` for data operations (`GetAllAsync`, `AddAsync`, etc.).

## Points of Emphasis

- **C# Primary Constructors**  
  Combine constructor parameters and field declarations succinctly, improving readability and reducing boilerplate.

- **Keyed Service Registration**  
  Practice registering multiple implementations of one interface and resolving them by a runtime key.

- **Header-Based Selection**  
  Learn to read HTTP headers in Minimal API endpoints and switch service behavior accordingly.

- **Clean DI Setup**  
  Centralize all service registrations in `Program.cs`, keeping resolver logic isolated in the factory delegate.

- **Separation of Concerns**  
  Endpoint handlers focus on request/response flow; the resolver delegate encapsulates implementation selection logic.
