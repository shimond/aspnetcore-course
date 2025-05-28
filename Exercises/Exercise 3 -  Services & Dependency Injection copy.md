# Exercise 4: Asynchronous Services & Controllers

## Project Overview

Refactor the customer management API from **Exercise 3** so that every data-access, service, and controller method is fully asynchronous. All operations—reading and writing the `customers.json` file, service calls, and controller actions—must return `Task` or `Task<T>` and use `async`/`await`.

## Requirements

1. **Asynchronous Repository Interface**  
   - Change `ICustomerRepository` so that:
     - `GetAll()` becomes `Task<IEnumerable<Customer>> GetAllAsync()`
     - `Add(Customer c)` becomes `Task AddAsync(Customer c)`, etc.

2. **Async File I/O Implementation**  
   - In `CustomerJsonRepository`, use the asynchronous JSON APIs (e.g. `JsonSerializer.DeserializeAsync`, `FileStream.ReadAsync`, `FileStream.WriteAsync`) to load and save the list of customers.
   - Ensure each public method on the repository is `async` and returns the appropriate `Task` or `Task<IEnumerable<Customer>>`.

3. **Async Service Layer**  
   - Update `CustomerService` so that all methods return `Task<T>` or `Task` and internally `await` repository calls.
   - Propagate `async`/`await` in any business-logic methods you’ve added.

4. **Async Controller Actions**  
   - Modify your controller so that:
     - `GET /customers` action returns `Task<IActionResult>` (or `Task<IEnumerable<Customer>>`) and `await`s the service.
     - `POST /customers` action returns `Task<IActionResult>` and `await`s the service call to add a new customer.

5. **Dependency Injection**  
   - Ensure your DI registrations still work—no changes needed here beyond matching the updated async method signatures.

## Points of Emphasis

- **Async All the Way**  
  Once you make the repository methods asynchronous, your service and controller signatures must also change. Learn how `async`/`await` propagates through your layers.

- **Task vs. Task<T>**  
  Understand when to return `Task` (no result) and when to return `Task<T>` (with a return value).

- **Asynchronous File I/O**  
  Use non-blocking APIs to read and write `customers.json` so that the thread pool isn’t tied up during I/O.

- **Controller Best Practices**  
  Return `Task<IActionResult>` (or another `Task<...>`) from actions instead of blocking on `.Result` or `.Wait()`.

- **Error Handling in Async**  
  Be mindful of exception propagation in async methods—use `try`/`catch` as needed and return appropriate HTTP status codes.

- **Testing Async Methods**  
  (Optional) Consider how you would unit-test your service and repository now that they return `Task`/`Task<T>`—you can `await` them directly in test methods.

