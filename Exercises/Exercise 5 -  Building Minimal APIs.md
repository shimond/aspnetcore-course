# Exercise 5: Building Minimal APIs

## Project Overview

In this exercise you will replace or supplement your existing controller-based endpoints with .NET 7 Minimal APIs. By the end, your application should:

- Expose endpoints using top-level statements and the Minimal API style.  
- Return results via `TypedResults` for clear, self-documenting responses.  
- Implement a full CRUD endpoint set for your `Customer` model.  
- Organize endpoints with **Tags** for grouping in OpenAPI/Swagger.  
- Group related endpoints under a common route prefix using **MapGroup**.  
- Move endpoint definitions into separate files via extension methods to keep `Program.cs` clean.  
- (Optional) Refactor earlier controller-based exercises (e.g. exercises 3 & 4) to use Minimal APIs.

---

## Requirements

1. **Introduction to Minimal API**  
   - Understand the minimal-hosting model (`WebApplication.CreateBuilder` → `app.Run()`), no controllers or attributes required.

2. **Create Your First Minimal API**  
   - In `Program.cs`, map a simple `GET /hello` that returns `"Hello, world!"`.

3. **Use TypedResults**  
   - Return `TypedResults.Ok()`, `TypedResults.NotFound()`, `TypedResults.Created()` instead of raw status codes or anonymous objects.

4. **Create CRUD with Minimal API**  
   - For the `Customer` record (`Id`, `Name`, `Email`):
     - `GET /customers` → list all customers.  
     - `GET /customers/{id}` → get by ID or return 404.  
     - `POST /customers` → create new customer, return 201 with `Location` header.  
     - `PUT /customers/{id}` → update existing or return 404.  
     - `DELETE /customers/{id}` → remove or return 404.

5. **Minimal API – Use Tags**  
   - Annotate each endpoint with `.WithTags("Customers")` (or other logical group names) so they appear grouped in Swagger UI.

6. **MapGroup with Minimal API**  
   - Create a `var customerGroup = app.MapGroup("/customers")` and map your CRUD endpoints on `customerGroup` instead of `app`.

7. **Create Minimal API in a Separate File (Extension Methods)**  
   - Create a static class `CustomerEndpoints` in a file (e.g. `CustomerEndpoints.cs`) with an extension method:
     ```csharp
     public static WebApplication MapCustomerEndpoints(this WebApplication app) { … }
     ```
   - Move all `MapGet`/`MapPost`/etc. calls into this method.

8. **Create Minimal API in a Separate File – Continue**  
   - In a second file (e.g. `ProgramExtensions.cs`), define extension methods for other domain areas or cross-cutting concerns.
   - In `Program.cs`, invoke these extension methods to wire up all endpoints.

9. **Optional Refactor of Previous Exercises**  
   - Convert Controllers from Exercises 3 & 4 into Minimal API endpoints following the patterns above.

---

## Points of Emphasis

- **Minimal vs. Controller**  
  Compare the simplicity of Minimal APIs with the structure of controllers and attributes.

- **TypedResults Advantages**  
  Gain compile-time checking for common HTTP results and consistent response shapes.

- **Tags & OpenAPI**  
  Use `.WithTags()` to improve API documentation and discoverability.

- **MapGroup Organization**  
  Prefix and group related endpoints to reduce repetition and clearly scope your routes.

- **Extension Methods for Structure**  
  Keep `Program.cs` minimal by moving endpoint definitions into well-named extension methods in separate files.

- **Refactoring Practice**  
  Strengthen your understanding by refactoring existing controller-based code into Minimal API style.

- **Maintainability & Readability**  
  Observe how grouping and extension methods help large APIs stay organized and easy to navigate.

- **Code example**  
```csharp
app.MapGet("/customers/{id}", async (Guid id, ICustomerService service) =>
{
    var customer = await service.GetByIdAsync(id);
    return customer is not null
        ? TypedResults.Ok(customer)
        : TypedResults.NotFound($"Customer with ID '{id}' not found.");
})
.WithName("GetCustomerById")
.WithTags("Customers");
  ```

