# NavigationPlatform

# 🧭 Navigation Platform – Microservices Architecture (.NET + CQRS + Keycloak)

A navigation platform that allows users to record, manage, and share journeys. Built using **Clean Architecture**, **CQRS**, and **microservices** principles, with secure authentication and asynchronous messaging.

---

## 📦 Tech Stack

- **.NET 7+** with Clean Architecture
- **CQRS** pattern with MediatR
- **MediatR Validation Pipeline**
- **RabbitMQ** with **MassTransit**
- **Keycloak** for OIDC authentication (Authorization Code Flow with PKCE)
- **Docker + Docker Compose**
- **Serilog** with Correlation IDs
- **Microservices**: API Gateway, Journey Service, Notification Service, Reward Worker
- **Messaging**: Domain & Integration Events
- **Database**: EF Core with automatic migrations

---

## ✅ Features Implemented

- ✔️ Create, Update, Delete Journey
- ✔️ Mark journey as favourite / remove from favourites
- ✔️ Get journeys by user, paginated, and logged user
- ✔️ Domain Events: `JourneyCreated`, `JourneyUpdated`, `JourneyDeleted`
- ✔️ CQRS using Commands, Queries, and Handlers via MediatR
- ✔️ Clean Architecture separation: Domain → Application → Infrastructure → Presentation
- ✔️ Serilog with Correlation ID for traceability
- ✔️ Docker Compose setup for local development
- ✔️ Keycloak auth using Authorization Code Flow + PKCE
- ✔️ RabbitMQ + MassTransit configured
- ✔️ Microservices defined and scoped

---

## ❌ Missing or In Progress

- ⏳ Daily Distance Reward (background worker + `DailyGoalAchieved` event)
- ⏳ Journey Sharing (user-based and public links)
- ⏳ SignalR notifications for favourites (with offline fallback to email)
- ⏳ Secure Logout (refresh token revocation)
- ⏳ Minimal Frontend SPA (React/Angular/Vue)
- ⏳ Admin APIs: filtering, monthly stats, user status management
- ⏳ CI/CD with GitHub Actions (build, test, coverage)
- ⏳ Unit Testing and Integration Testing
- ⏳ OpenTelemetry, Prometheus metrics, Jaeger tracing

---

## 🧱 Clean Architecture Layers

| Layer         | Responsibilities |
|---------------|------------------|
| **Domain**     | Core business models, value objects, domain events |
| **Application**| CQRS commands/queries, interfaces, MediatR handlers |
| **Infrastructure** | Repositories, messaging config (MassTransit), logging (Serilog), DB |
| **Presentation** | API Controllers, Auth, Validation, SignalR (to be added) |

---

## 🐳 Local Development

To spin up the full stack locally:

```bash
docker compose up --build
```

Ensure Docker is running.

Visit RabbitMQ dashboard at: http://localhost:15672

Keycloak runs on: http://localhost:8080 (default realm/user setup required)

API and services available on configured ports (see docker-compose.yml)

## 🧪 Testing (To Do)

❌ Unit tests not yet implemented
❌ Reward badge logic edge cases (19.99, 20.00, 20.01 km) to be tested
❌ Integration tests via GitHub Actions
❌ 80%+ code coverage required in CI

## 📣 Messaging Flow

Domain events persist to an outbox table
MassTransit publishes events to RabbitMQ
Workers and consumers listen asynchronously
Planned: Reward Worker to track total daily distance

## 📋 API Endpoints Summary

Method	Endpoint	Description
- POST	/api/journeys	Create a journey
- GET	/api/journeys/{id}	Get journey by ID
- GET	/api/journeys?Page=1&PageSize=20	Paginated list
- PUT	/api/journeys/{id}	Update (owner only)
- DELETE	/api/journeys/{id}	Delete (owner only)
- POST	/api/journeys/{id}/favorite	Mark as favourite
- DELETE	/api/journeys/{id}/favorite	Remove from favourites

More (e.g., sharing, logout, admin, notifications) in progress.

## 🌐 Authentication

- Keycloak using Authorization Code Flow with PKCE
- Access and refresh tokens stored securely (planned via HTTP-only cookies)
- 401 on unauthorized access; refresh token support to be added
- Admin routes require Admin scope

## 🛠 Observability (Planned)

- Serilog structured logs with correlation ID propagation
- Health checks: /healthz, /readyz
- OpenTelemetry + Jaeger tracing
- Prometheus metrics: HTTP latency, DB latency, queue lag
- Alert rule: queue lag > 100 msgs for 5 min

## ⚙️ CI/CD Pipeline (Planned)

GitHub Actions

✅ Restore, Build, Lint (.NET)
⏳ Run Unit Tests + Coverage (≥80%)
⏳ StyleCop / Roslyn Analyzers
⏳ Docker image build and push (tagged with commit SHA)
⏳ Integration tests with full stack up

## ⚖️ Design Decisions

- Clean separation of concerns (Clean Architecture)
- CQRS for scalability and clear read/write logic
- Asynchronous domain & integration events
- Stateless microservices, scalable horizontally
- Easy local setup with Docker Compose
- Real-time and email-based notifications planned

## 🚀 Scaling & Production Strategy

- Stateless services allow horizontal scaling
- RabbitMQ decouples message producers and consumers
- Per-microservice databases ensure bounded contexts
- Propagate correlation/trace IDs across services
- Static front-end served via API or NGINX container

## 📌 Notes

This project is a work in progress. Missing features are clearly scoped and planned.

Front-end and admin tooling are yet to be integrated.

Daily badge and sharing logic will be implemented as a next priority.

📎 License

MIT or specify your licensing model here.
