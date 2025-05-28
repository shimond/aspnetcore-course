# Exercise 7: Validation with FluentValidation

## Introduction

In this exercise you will add robust request validation to the customer API you built in Exercise 6. You’ll use FluentValidation to enforce rules on your DTOs, leverage automatic DI registration, integrate validation into your Minimal API endpoints, and create both built-in and custom validation rules.

---

## Installation

From your project root, install the FluentValidation packages:

```bash
dotnet add package FluentValidation
dotnet add package FluentValidation.AspNetCore
```

## Project Overview

Enhance your existing Minimal API so that:

- Every incoming `CustomerDto` is validated before any business logic runs.  
- Validation rules live in dedicated validator classes, not in controllers or endpoints.  
- Validators are automatically discovered and registered via DI.  
- Endpoints fail fast with clear validation errors (e.g. HTTP 400 with details) when input is invalid.  
- You demonstrate both built-in rules (e.g. non-empty, length limits) and a custom rule (e.g. email domain must match `@example.com`).

## Requirements

1. **Configure FluentValidation**  
   Ensure FluentValidation’s ASP.NET Core integration is set up so that all validators in your assembly are registered automatically.

2. **Implement `CustomerDtoValidator`**  
   Create a class that inherits from `AbstractValidator<CustomerDto>` and add rules for:  
   - **`Uid`**: must not be the empty GUID.  
   - **`Name`**: must not be null or whitespace; length between 3–100 characters.  
   - **`Email`**: must not be empty; must match a valid email pattern.

3. **Add a Custom Validation Rule**  
   In `CustomerDtoValidator`, use FluentValidation’s `Must(...)` to ensure `Email` ends with `@example.com`, and provide a clear error message when this rule fails.

4. **Integrate Validation into Minimal API**  
   In each endpoint that accepts `CustomerDto`, resolve `IValidator<CustomerDto>` from DI, call `ValidateAsync(dto)`, and:  
   - If invalid, return `TypedResults.ValidationProblem(...)` with the error details.  
   - Otherwise, proceed with mapping and service calls.

5. **Demonstrate Built-In vs. Custom Rules**  
   Ensure your validator class shows both:  
   - FluentValidation’s built-in rules: `.NotEmpty()`, `.Length(...)`, `.EmailAddress()`.  
   - Your custom `.Must(...)` rule for domain-specific logic.

## Points of Emphasis

- **Separation of Concerns**  
  Keep validation logic in validator classes, not mixed into controllers or services.

- **FluentValidation Fundamentals**  
  Learn how `AbstractValidator<T>` and fluent rule definitions replace scattered `if`-checks.

- **Automatic DI Registration**  
  Wire up all validators via assembly scanning so you don’t have to register each one manually.

- **Minimal API Integration**  
  Decide between manual `ValidateAsync` calls in your handlers or a global validation middleware/filter.

- **Error Response Formatting**  
  Return standard `ValidationProblemDetails` so clients receive structured information about every validation failure.

- **Custom Validation**  
  Practice writing domain-specific rules with `Must(...)`, and learn how to craft user-friendly error messages.

