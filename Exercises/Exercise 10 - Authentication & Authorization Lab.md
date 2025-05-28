# Exercise 10: Authentication & Authorization Lab

## Lab Overview

In this hands-on lab you will secure your Minimal API by:

- Configuring JWT Bearer authentication.  
- Securing endpoints with `RequireAuthorization()`.  
- Generating tokens via the `dotnet user-jwts` tool and programmatically.  
- Implementing role-based authorization policies.  
- Validating tokens and extracting claims at runtime.

## Prerequisites

- A Minimal API project from previous exercises.  
- .NET 7 SDK or later installed.  

---

## Lab Steps

### 1. Install Authentication Packages  
Install the JWT bearer authentication package:  
```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

## 2. Configure JWT Bearer Tokens

In **Program.cs**, register authentication and authorization, configuring issuer, audience, signing key, and validation parameters:

```csharp
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.AddAuthorization();
```


## 3. Secure Endpoints with RequireAuthorization()

Apply authentication to endpoints:

```csharp
app.MapGet("/secure-data", () => "Protected data")
   .RequireAuthorization();
```

## 4. Generate JWTs with the `dotnet user-jwts` Tool

Install and use the tool to create a token for testing:

```bash
dotnet user-jwts create --name test-token --role Admin --claims sub=alice
```


## 5. Implement Role-Based Authorization Policies

In **Program.cs**, configure your authorization policies and apply them to endpoints:

1. **Define Policies**  
   ```csharp
   builder.Services.AddAuthorization(options =>
   {
       options.AddPolicy("AdminOnly", policy =>
           policy.RequireRole("Admin"));
       
       options.AddPolicy("ManagerOrAdmin", policy =>
           policy.RequireRole("Manager", "Admin"));
   });
   ```

## Protect Endpoints

Use `.RequireAuthorization("<PolicyName>")` on your Minimal API endpoints to enforce the corresponding authorization policy:

```csharp
app.MapDelete("/customers/{id}", (Guid id, ICustomerService svc) => svc.DeleteAsync(id))
   .RequireAuthorization("AdminOnly");
```

## Using JWT Tokens in Your `.http` File

After generating a token with the `dotnet user-jwts` tool, you can include it in your HTTP requests to test secured endpoints.

1. **Generate a JWT**  
   ```bash
   dotnet user-jwts create --name test-token --role Admin --claims sub=alice
   ```

Copy the resulting token string.

2. **Define a Variable for the Token**  
   At the top of your `.http` file, add: 
   @token = eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9…<rest of token>

3. **Add the Authorization Header**  
For any request that requires authentication, include: 
```
GET {{host}}/secure-data
Authorization: Bearer {{token}}
```

