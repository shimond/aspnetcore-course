
# Exercise 2: Configuration & Options Pattern with a Real-World Example

## Project Overview

Enhance your minimal .NET  Web API to demonstrate configuration management using a real-world scenario: notification settings for an email service.

When complete, your application will:

- Read configuration in middleware, logging the current **ApplicationName** setting on every request.  
- Bind a strongly-typed Options model representing your notification settings (including properties for sender email address, retry count, and administrator email list).  
- Demonstrate the difference between **IOptions<T>** and **IOptionsSnapshot<T>**, using reload-on-change to show how settings can update at runtime.  
- Expose a **GET** endpoint that returns the current notification settings.  
- Expose a **POST** endpoint that accepts part of the settings (e.g. retry count) in the request body and returns it.

## Real-World POCO Description: NotificationSettings

Define a class named `NotificationSettings` that includes:
- A **string** property for the sender email address used by the notification system.  
- An **int** property for the number of retry attempts on failure.  
- A **string[]** property for a list of administrator email addresses to notify on critical errors.

## Points of Emphasis

- **Reading in Middleware**  
  Inject the configuration service into your middleware to log the `ApplicationName` setting on each request.

- **Strongly-Typed Binding**  
  Understand the benefits of binding configuration sections to a POCO rather than using literal strings sprinkled throughout your code.

- **Options Pattern Lifetimes**  
  - `IOptions<T>` is a singleton: values are fixed at startup.  
  - `IOptionsSnapshot<T>` is scoped: values refresh on each request when reload-on-change is enabled.

- **reloadOnChange Demonstration**  
  Show that modifying your configuration file while the app is running only affects components using `IOptionsSnapshot<T>`.

- **Controller & Model Binding**  
  Create:  
  - A **GET** endpoint that returns the bound settings as JSON.  
  - A **POST** endpoint that binds incoming JSON payloads to your model (or a subset) and returns the result.

- **Separation of Concerns**  
  Keep configuration registration and binding in the application startup, logging in middleware, and business logic in some endpoint.
