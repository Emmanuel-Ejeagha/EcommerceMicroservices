# E‑Commerce Microservices

A production‑ready, containerized e‑commerce backend built with .NET microservices, following **Vertical Slice**, **Clean Architecture**, and **CQRS** patterns. The system uses asynchronous messaging (RabbitMQ + MassTransit) for order checkout and an API Gateway (YARP) for unified access and routing.

## 📦 Services & Technologies

| Service            | Description                                   | Database / Storage   | Ports (Local / Docker External) |
|--------------------|-----------------------------------------------|----------------------|----------------------------------|
| **Catalog.API**    | Product catalog (CRUD, category filtering)    | PostgreSQL + Marten  | `5000-5050` / `6000`             |
| **Basket.API**     | Shopping cart with discount integration       | Redis                | `5001-5051` / `6001`             |
| **Discount.gRPC**  | Coupon‑based discount calculation             | SQLite               | `5002-5052` / `6002`             |
| **Ordering.API**   | Order management (create, update, delete, query) | SQL Server       | `5003-5053` / `6003`             |
| **YarpApiGateway** | Reverse proxy gateway (YARP)                  | –                    | `5004-5054` / `6004`             |

> All services are fully containerised with Docker and orchestrated via `docker-compose`.

## 🏗 Architecture Highlights

- **Vertical Slice Architecture** – Features are self‑contained (endpoint, handler, validation, models).
- **CQRS & MediatR** – Commands and queries separated; MediatR used in Catalog, Basket, and Ordering (application layer).
- **Clean Architecture + REPR** – Ordering.API follows Clean Architecture with Request‑Endpoint‑Response pattern.
- **API Gateway** – YARP reverse proxy provides a single entry point and route aggregation.
- **Async Communication** – RabbitMQ + MassTransit for checkout event handling (Basket → Ordering).
- **Rate Limiting** – RateLimiterPolicy applied to Ordering.API.

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Postman](https://www.postman.com/) (optional, for testing)

### Run with Docker Compose (Recommended)

Clone the repository and start all services:

```bash
git clone https://github.com/your-repo/ecommerce-microservices.git
cd ecommerce-microservices
docker-compose up -d
```
This will spin up:

PostgreSQL, Redis, SQL Server, RabbitMQ

All microservices

YARP API Gateway

Access the Gateway
Once running, all APIs are accessible through the YARP gateway at:

text
http://localhost:6004
Run Locally (without Docker)
Each service can be run individually. Update appsettings.json with correct database connection strings and RabbitMQ address. Then:

```bash
dotnet run --project Services/Catalog.API/Catalog.API.csproj
dotnet run --project Services/Basket.API/Basket.API.csproj
dotnet run --project Services/Discount.gRPC/Discount.gRPC.csproj
dotnet run --project Services/Ordering.API/Ordering.API.csproj
dotnet run --project ApiGateways/YarpApiGateway/YarpApiGateway.csproj
```

## 📡 API Endpoints
### Direct Service Endpoints <br>
**Catalog API (/products)**<br>
Method	Endpoint	Description<br>
GET	/products?pageNumber=1&pageSize=5	Get all products (paginated)<br>
GET	/products/{id}	Get product by ID<br>
GET	/products/category/phone/{category}	Get products by category <br>
POST	/products	Create product<br>
PUT	/products	Update product<br>
DELETE	/products/{id}	Delete product<br>
GET	/health	Health check<br>

### Basket API (/basket)
**Method	Endpoint	Description**<br>
GET	/basket/{username}	Get basket by username<br>
POST	/basket	Store/update basket<br>
DELETE	/basket/{username}	Delete basket<br>
POST	/basket/checkout	Checkout basket (async via MassTransit)<br>

### Ordering API (/orders)
**Method	Endpoint	Description**<br>
POST	/orders	Create order<br>
PUT	/orders	Update order<br>
DELETE	/orders/{id}	Delete order<br>
GET	/orders?PageIndex=0&PageSize=2	Get orders with pagination<br>
GET	/orders/ORD_1	Get order by name<br>
GET	/health	Health check<br>

### API Gateway Endpoints (via YARP)
Base URL: http://localhost:6004

Service	Gateway Route	Equivalent Direct Endpoint<br>
Catalog	/catalog-service/products?...	/products?...<br>
Catalog	/catalog-service/products/{id}	/products/{id}<br>
Catalog	/catalog-service/products/category/phone/{category}	/products/category/...<br>
Catalog	/catalog-service/products (POST/PUT/DELETE)	same<br>

Basket	/basket-service/basket/{username}	/basket/{username}<br>
Basket	/basket-service/basket (POST/DELETE)	same<br>
Basket	/basket-service/basket/checkout	/basket/checkout<br>

Ordering	/ordering-service/orders...	/orders...<br>
All endpoints have been tested with Postman in both local and Docker environments.

### 🔄 Asynchronous Checkout Flow
Client sends a POST to /basket/checkout (direct or via gateway).

Basket.API publishes a BasketCheckoutEvent to RabbitMQ using MassTransit.

Ordering.API consumes the event and creates an order asynchronously.

The client receives an immediate 202 Accepted response; the order is created in the background.

🛡 Rate Limiting
The Ordering.API is protected with a RateLimiterPolicy to prevent abuse. Default limits are configurable in appsettings.json.

### 🐳 Docker Composition
The docker-compose.yml orchestrates all services, databases, and RabbitMQ. Services communicate via internal Docker network. Environment variables control connection strings and ports.<br>

```
Sample docker-compose.override.yml (excerpt)
yaml
catalog.api:
  environment:
    - ConnectionStrings__Default=Host=postgres;Database=CatalogDb;...
basket.api:
  environment:
    - ConnectionStrings__Redis=redis:6379
    - GrpcSettings__DiscountUrl=http://discount.grpc
ordering.api:
  environment:
    - ConnectionStrings__Ordering=Server=sqlserver;Database=OrderDb;...
rabbitmq:
  ports:
    - "5672:5672"
    - "15672:15672"
```
### 🧪 Testing
All endpoints have been manually tested using Postman. A Postman collection is available in /docs/postman_collection.json (if you add it). Automated tests can be added per service.

```
📁 Project Structure
text
├── BuildingBlocks/                 # Shared kernel (CQRS, behaviours, exceptions)
├── Services/
│   ├── Catalog.API/                # Product catalog (PostgreSQL + Marten)
│   ├── Basket.API/                 # Cart service (Redis, gRPC client)
│   ├── Discount.gRPC/              # Discount calculation (SQLite, gRPC server)
│   ├── Ordering.API/               # Order management (SQL Server, Clean Architecture)
│   └── YarpApiGateway/             # YARP reverse proxy
├── docker-compose.yml
├── docker-compose.override.yml
└── README.md
```
### 🔧 Configuration
Key configuration is managed via environment variables and appsettings.json. Common settings:

Connection strings for PostgreSQL, Redis, SQL Server, SQLite

gRPC service URLs (e.g., GrpcSettings__DiscountUrl)

RabbitMQ host and queue names

Rate limiting options for Ordering.API

YARP routes – defined in YarpApiGateway/appsettings.json

📄 License
MIT – see LICENSE file.

🤝 Contributing
Issues and pull requests are welcome. Please ensure all endpoints remain functional after changes.

Maintainer: [Your Name]<br>
Repository: [GitHub Repo Link]
