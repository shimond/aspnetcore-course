
# Exercise 9: Exploring the New Visual Studio Feature — `.http` Files

## Project Overview

Visual Studio 2022 introduces first-class support for **`.http` files**, letting you define, execute, and inspect HTTP requests directly inside your API project. In this exercise you will:

- Add a `.http` file to your solution and use it to test your customer management endpoints.
- Leverage the full range of `.http` file features: request syntax, headers, bodies, variables, environments, request chaining, and secrets.
- Exercise working with both your JSON-backed and EF-backed repositories by selecting implementations via variables or headers.

## Requirements

1. **Add a `.http` File**  
   - In your API project, create a file named `requests.http` (or use the `.rest` extension).  
   - Observe that Visual Studio opens it in the HTTP editor UI.

2. **Define Multiple Requests**  
   - Write at least three requests separated by `###`, covering:  
     - `GET /customers`  
     - `GET /customers/{id}`  
     - `POST /customers` with a JSON body payload  

3. **Use Request Headers**  
   - For each request, add a `Content-Type: application/json` header where appropriate.  
   - Read the `datasourceType` header from your service: include both `datasourceType: json` and `datasourceType: ef` variants to test both repository implementations.

4. **Add Comments**  
   - Precede selected requests with `# @name <requestName>` comments so you can reference their responses later.

5. **Define Variables**  
   - At the top of the file, define variables like:
     ```
     @host = https://localhost:5001
     @jsonSource = json
     @efSource = ef
     ```
   - Reference them in your requests using `{{host}}` and `{{jsonSource}}` / `{{efSource}}`.

6. **Use an Environment File**  
   - Create a `http-client.env.json` alongside your `.http` file containing at least two named environments (e.g. `"dev"` and `"prod"`) with different `Host` URLs.  
   - Switch environments via the dropdown in the HTTP editor and re-run your requests.

7. **Shared Variables**  
   - In the same environment file, use the special `$shared` section to define values common to all environments.

8. **Request Variables & Chaining**  
   - Name one request (e.g. `# @name createCustomer`) and then in a subsequent `GET` use `{{createCustomer.response.headers.Location}}` (or JSONPath for response body) to fetch the newly created resource.

9. **User-Specific Environment File**  
   - Create a file named `http-client.env.json.user` (ignored by source control) to override a shared value (e.g. an API key or path) without affecting the committed environment file.

10. **Secret Management**  
    - In your environment JSON, experiment with the ASP.NET Core user secrets provider or Azure Key Vault provider to supply an `X-API-KEY` or similar header without committing secrets to source control.

## Points of Emphasis

- **`.http` Syntax & Editor**  
  Learn the Visual Studio editor features: request execution buttons, environment selector, response previews, and request history.

- **Variable Scoping**  
  Understand how file-level variables, environment variables, and shared variables interact and override one another.

- **Environment-Driven Testing**  
  Switch between “dev”/“prod” or “json”/“ef” scenarios by changing environment or header values—no code changes required.

- **Chaining Requests**  
  Use named requests and response extraction to simulate real-world workflows (e.g. create → retrieve → delete).

- **Safe Secrets Handling**  
  Practice storing sensitive values outside your committed files using `.env.user` and external secret providers.

- **Rapid Feedback Loop**  
  See how `.http` files accelerate debugging and manual testing, complementing your automated test suite without leaving the IDE.
