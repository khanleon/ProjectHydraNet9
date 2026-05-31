# Project Hydra - Intelligence Center

## Overview

Project Hydra is a multi-tier intelligence aggregation platform built to demonstrate modern enterprise architecture using .NET 10, Angular, Blazor, JWT authentication, API Gateway patterns, asynchronous service orchestration, API key security, and automated testing.

The system simulates an intelligence center that collects information from multiple backend services and presents a unified operational picture to end users.

---

## Architecture

Angular Frontend

Blazor Frontend

↓

JWT Authentication

↓

Hydra Gateway API

↓

API Key Authentication

↓

Threat Intelligence API

Operations Intelligence API

↓

Aggregated Intelligence Response

---

## Technologies

### Backend

* .NET 10
* ASP.NET Core Minimal APIs
* Dependency Injection
* HttpClientFactory
* JWT Authentication
* API Key Security
* Swagger/OpenAPI

### Frontend

* Angular
* Angular Signals
* Angular Routing
* Angular HttpClient
* Blazor WebAssembly

### Testing

* NUnit

---

## Security

### User Authentication

Users authenticate through JWT Bearer tokens.

### Service Authentication

Internal services communicate through API Key authentication.

---

## Reliability

### Asynchronous Aggregation

The Gateway API uses Task.WhenAll to call multiple downstream services concurrently.

### Graceful Degradation

If one backend service becomes unavailable, the Gateway can still return partial intelligence results.

---

## Features

* Threat Intelligence API
* Operations Intelligence API
* Gateway Aggregation API
* Angular Dashboard
* Blazor Dashboard
* JWT Authentication
* API Key Security
* Logging
* NUnit Testing

---

## Author

Stephen Leonard
