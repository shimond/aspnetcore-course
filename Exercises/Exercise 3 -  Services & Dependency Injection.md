
## Exercise 3: Services & Dependency Injection

### Project Overview

Extend your minimal .NET  Web API to implement a simple customer management system. At the end of this exercise, your application will:

1. **Define a `Customer` record** with properties:
   - `Guid Id`
   - `string Name`
   - `string Email`

2. **Use the `with` expression** to clone a `Customer` and produce an updated copy.

3. **Manage customer data in a JSON file**:
   - Create `customers.json` at the application root.
   - Populate it with an array of customer objects matching your `Customer` record.
   - In a controller, read from and write to this file to handle **GET** and **POST** operations.

4. **Introduce Repository and Service**:
   - Define `ICustomerRepository` for file-based data access; implement it in `CustomerJsonRepository`.
  

5. **using Dependency Injection**:
   - Register `ICustomerRepository` and `CustomerService` in ASP.NET Core’s DI container.
   - Modify your controller to receive `CustomerService` via constructor injection.

6. **Experiment with Service Lifetimes**:
   - Configure your registrations in `Program.cs` with each of:
     - **Transient** (`services.AddTransient<…>()`)  
     - **Scoped** (`services.AddScoped<…>()`)  
     - **Singleton** (`services.AddSingleton<…>()`)  
   - **How to observe**:  
     - Set a breakpoint inside the constructor of `CustomerJsonRepository`.  
     - Send multiple requests to your API endpoints; note how often the breakpoint is hit under each lifetime:
       - **Transient**: once per resolution (every request or service injection).  
       - **Scoped**: once per HTTP request, even if resolved multiple times in that request.  
       - **Singleton**: only once when the application starts.

### Real-World POCO: `Customer`

Your record should be declared as:

```csharp
public record Customer(Guid Id, string Name, string Email);`
```


```json
[
  {
    "Id": "c56a4180-65aa-42ec-a945-5fd21dec0538",
    "Name": "Alice Johnson",
    "Email": "alice.johnson@example.com"
  },
  {
    "Id": "d2719d1f-fc2a-4d7d-9a8b-ae0f473c29bf",
    "Name": "Bob Smith",
    "Email": "bob.smith@example.com"
  }
]
```