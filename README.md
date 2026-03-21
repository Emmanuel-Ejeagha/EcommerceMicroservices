# E‑Commerce Microservices

A modular, scalable e‑commerce backend built with .NET microservices, following **Vertical Slice Architecture** and **CQRS** patterns. The system is containerized with Docker and designed for high flexibility and maintainability.

## 📦 Services Overview

| Service       | Description                                                                 | Tech Stack                          | Ports (Local / Docker External / Internal) |
|---------------|-----------------------------------------------------------------------------|-------------------------------------|--------------------------------------------|
| **Catalog API** | Product catalog management (CRUD, category filtering)                       | PostgreSQL + Marten, Carter, MediatR | `5000-5050` / `6000-6060` / `8080-8081`   |
| **Basket API**   | Shopping cart management, integrates with Discount.gRPC for price discounts | Redis, StackExchangeRedis, Scrutor  | `5001-5051` / `6001-6061` / `8080-8081`   |
| **Discount.gRPC**| gRPC service for coupon-based discount calculations                         | SQLite, gRPC                        | `5002-5052` / `6002-6062` / `8080-8081`   |
| **Ordering API** | *(Planned)* Order processing and management                                 | *TBD*                               | `5003-5053` / `6003-6063` / `8080-8081`   |

## 🏗 Architecture

The solution is split into two main components:

- **BuildingBlocks** – A shared class library containing cross‑cutting concerns:
  - Common NuGet packages (FluentValidation, Mapster, MediatR, Marten, Carter, etc.)
  - CQRS abstractions (`ICommand`, `IQuery`, handlers)
  - Pipeline behaviors (`LoggingBehavior`, `ValidationBehavior`)
  - Custom exceptions (`BadRequestException`, `NotFoundException`, `InternalServerException`)
  - Global exception handling (`CustomExceptionHandler`)

- **Microservices** – Each service is self‑contained, with its own database, business logic, and API surface. Services communicate via HTTP (REST) or gRPC.

### Vertical Slice Architecture

Each feature (e.g., `CreateProduct`, `GetBasketByUsername`) lives in its own folder, containing the API endpoint, handler, validation, and any feature‑specific models. This keeps code cohesive and simplifies maintenance.

### CQRS & MediatR

Commands and queries are dispatched through MediatR, enabling clean separation between read and write operations. Validation is applied via pipeline behaviors.

## 🛠 Technologies

- **.NET 10.0** 
- **PostgreSQL** + **Marten** (document database for Catalog)
- **Redis** (distributed cache for Basket)
- **SQLite** (lightweight DB for Discount.gRPC)
- **gRPC** (inter‑service communication for discounts)
- **Docker** & **Docker Compose**
- **Carter** (minimal API endpoints)
- **FluentValidation**, **Mapster**, **Scrutor**, **HealthChecks**

## 🚀 Getting Started

### Prerequisites

- [.NET SDK 10.0+](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (for containerized setup)
- [Postman](https://www.postman.com/) (for testing, optional)

### Running Locally (without Docker)

Each microservice can be run independently. Before starting, ensure required databases are available:

1. **Catalog API** – Requires PostgreSQL. Update connection string in `appsettings.json`.
2. **Basket API** – Requires Redis instance running locally (default `localhost:6379`).
3. **Discount.gRPC** – SQLite file database will be created automatically.

Then, navigate to each service folder and run:

```bash
dotnet run --project src/Services/Catalog.API/Catalog.API.csproj
dotnet run --project src/Services/Basket.API/Basket.API.csproj
dotnet run --project src/Services/Discount.gRPC/Discount.gRPC.csproj
Ports are configured in each service’s appsettings.json and launchSettings.json.

Running with Docker Compose
The entire stack can be launched with a single command:

bash
docker-compose up -d
This will start all services, PostgreSQL, Redis, and the SQLite volume. The API endpoints will be available on the Docker external ports defined above (e.g., http://localhost:6000 for Catalog).

To stop:

bash
docker-compose down
📡 API Endpoints
Catalog API (/products)
Method	Endpoint	Description
GET	/products	Get all products
GET	/products/{id}	Get product by ID
GET	/products/category/{category}	Get products by category
POST	/products	Create a new product
PUT	/products/{id}	Update an existing product
DELETE	/products/{id}	Delete a product
Basket API (/basket)
Method	Endpoint	Description
GET	/basket/{username}	Retrieve basket by username
POST	/basket	Store or update a basket
DELETE	/basket/{username}	Delete basket by username
Note: The Basket API calls the Discount.gRPC service to apply coupon discounts to each shopping cart item automatically.

Discount.gRPC (gRPC)
Service Method	Description
GetDiscount	Get a coupon by product name
CreateDiscount	Create a new coupon
UpdateDiscount	Update an existing coupon
DeleteDiscount	Delete a coupon
The Basket API consumes the GetDiscount method to retrieve applicable discounts.

🧪 Testing
All endpoints have been tested using Postman both in local and Docker environments. Sample requests and responses can be found in the docs/ folder (if you include them).

📁 Project Structure
src
├── BuildingBlocks/                 # Shared libraries
│   ├── Behaviors/                  # MediatR pipeline behaviors
│   ├── CQRS/                       # Command/Query interfaces
│   ├── Exceptions/                 # Custom exceptions & handler
│   └── GlobalUsings.cs
├── Services/
│   ├── Catalog.API/                # Product catalog service
│   │   ├── Data/                   # Marten document store config & initial data
│   │   ├── Models/                 # Domain models
│   │   ├── Products/               # Vertical slices
│   │   ├── Exceptions/             # Service-specific exceptions
│   │   ├── Dockerfile
│   │   └── GlobalUsings.cs
│   ├── Basket.API/                 # Shopping cart service
│   │   ├── Models/                 # ShoppingCart, ShoppingCartItem
│   │   ├── Endpoints/              # Carter endpoints
│   │   ├── Services/               # gRPC client for Discount
│   │   ├── Dockerfile
│   │   └── GlobalUsings.cs
│   ├── Discount.gRPC/              # Discount calculation service
│   │   ├── Models/                 # Coupon entity
│   │   ├── Services/               # gRPC service implementation
│   │   ├── Protos/                 # .proto files
│   │   ├── Data/                   # SQLite database context
│   │   ├── Dockerfile
│   │   └── GlobalUsings.cs
│   └── Ordering.API/               # (Planned)
├── docker-compose.yml              # Orchestrates all services
└── README.md
🔧 Configuration
Environment variables – Used for connection strings, port bindings, and service URLs. See docker-compose.yml and appsettings.*.json for details.

Health Checks – All services expose health endpoints for monitoring (e.g., /health).

📄 License
This project is licensed under the MIT License – see the LICENSE file for details.

🤝 Contributing
Contributions are welcome! Please open an issue or pull request for any improvements or bug fixes.8