\# Project Hydra – Intelligence Center



\## Overview



Project Hydra is an enterprise-style intelligence aggregation platform built to demonstrate modern software architecture, security, asynchronous programming, API integration, and frontend development using .NET, Angular, and Blazor.



The system simulates an intelligence center that gathers information from multiple backend services and presents a unified operational picture to authenticated users.



\---



\## Architecture



```text

&#x20;               Angular Frontend

&#x20;                       |

&#x20;                       | JWT Authentication

&#x20;                       |

&#x20;               Hydra Gateway API

&#x20;                       |

&#x20;                       | API Key Authentication

&#x20;                       |

&#x20;       --------------------------------

&#x20;       |                              |

Threat Intelligence API      Operations Intelligence API

&#x20;       |                              |

&#x20;       --------------------------------

&#x20;                       |

&#x20;               Aggregated Intelligence

&#x20;                       |

&#x20;               Angular / Blazor UI

```



\---



\## Features



\### Security



\* JWT Bearer Authentication

\* API Key Service-to-Service Security

\* Protected API Endpoints

\* Authorization Enforcement



\### Backend



\* ASP.NET Core Minimal APIs

\* API Gateway Pattern

\* Dependency Injection

\* HttpClientFactory

\* DTO-Based Architecture

\* Structured Logging



\### Reliability



\* Asynchronous API Aggregation

\* Task.WhenAll Parallel Processing

\* Graceful Degradation

\* Partial Response Handling



\### Frontend



\* Angular

\* Angular Signals

\* Angular Routing

\* Angular HttpClient

\* Blazor WebAssembly



\### Testing



\* NUnit Unit Tests

\* Service Layer Validation



\---



\## Technology Stack



\### Backend



\* .NET

\* ASP.NET Core

\* JWT Authentication

\* Swagger / OpenAPI

\* HttpClientFactory

\* Dependency Injection



\### Frontend



\* Angular

\* TypeScript

\* Angular Signals

\* Blazor WebAssembly



\### Testing



\* NUnit



\---



\## Request Flow



\### User Login



1\. User enters credentials.

2\. Gateway API validates credentials.

3\. JWT token is generated.

4\. JWT token is returned to the frontend.

5\. Frontend includes JWT in future requests.



\### Intelligence Briefing



1\. Frontend calls Gateway API.

2\. Gateway validates JWT.

3\. Gateway calls Threat Intelligence API.

4\. Gateway calls Operations Intelligence API.

5\. Calls execute concurrently using Task.WhenAll.

6\. Results are aggregated.

7\. Combined response is returned to the frontend.



\---



\## Key Design Decisions



\### Why JWT?



JWT allows stateless authentication and is commonly used for securing APIs exposed to clients.



\### Why API Keys?



API Keys provide lightweight service-to-service authentication between trusted internal APIs.



\### Why a Gateway API?



The Gateway centralizes authentication, orchestration, aggregation, and security concerns while hiding internal services from clients.



\### Why Task.WhenAll?



Task.WhenAll allows multiple downstream services to execute concurrently, reducing total response time.



\---



\## Future Enhancements



\* Role-Based Authorization

\* Refresh Tokens

\* Distributed Caching

\* Database Persistence

\* Containerization with Docker

\* Azure Deployment

\* CI/CD Pipelines

\* Health Checks

\* Metrics and Monitoring



\---



\## Author



Stephen Leonard



GitHub: https://github.com/khanleon



