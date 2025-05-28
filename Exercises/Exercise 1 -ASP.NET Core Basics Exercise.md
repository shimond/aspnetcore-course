# ASP.NET Core Basics Exercise

A minimal .NET  Web API solution demonstrating custom middleware behavior.

## Overview

This project showcases how to implement a simple middleware pipeline that:

1. Serves static files from a `wwwroot` folder.
2. Strips the default `Server` header from all HTTP responses.
3. Logs each request's `User-Agent` header value to the console.
4. Blocks requests with a `User-Agent` containing the substring `facebook` (case-insensitive), returning HTTP 400.

## Project Goals

By the end of this exercise, the application must:

- **Serve Static Files**: Any asset in the `wwwroot` folder (e.g., `index.html`) is returned by the server.
- **Remove Default Server Header**: No response includes the built-in `Server` header.
- **Log User-Agent**: Every incoming request prints its `User-Agent` header to the console.
- **Block Facebook Requests**: Requests from clients identifying as Facebook user agents are rejected with a 400 status and error message.

## Points of Emphasis

- **Minimal Hosting Model**  
  Use top-level statements (`WebApplication.CreateBuilder` → `app.Run()`) instead of a separate Startup class.

- **Static-Files Middleware**  
  Understand how `UseStaticFiles()` serves assets before custom logic.

- **Custom Middleware Responsibilities**  
  - Read and log `HttpContext.Request.Headers["User-Agent"]` to the console.  
  - Short-circuit the pipeline with a 400 response for blocked user agents.  
  - Remove the `Server` header from every response.

- **Middleware Order**  
  Register middleware in this order:  
  1. `UseStaticFiles()`  
  2. Custom user-agent middleware  
  3. Endpoint mappings  

- **Short-Circuit Logic**  
  Learn to terminate the pipeline early by writing directly to the response and **not** calling the next delegate.

- **Separation of Concerns**  
  Keep middleware focused solely on header inspection, logging, removal, and blocking logic.

## Usage

1. Create a new WebApi project  
2. Place any static assets in the `wwwroot` folder.  
3. Run the host project to start the API.  
4. Access:
   - `/index.html` for static content.  
   - `/health` (or any custom endpoint) to verify middleware behavior.
