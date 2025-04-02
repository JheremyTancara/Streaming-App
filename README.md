# Streaming Project

## Description

This project is an API for a streaming system that uses Entity Framework Core
to interact with a MySQL database. It allows for the management of users and
movies, as well as functionalities related to managing watchlists.

Advantages of Using Microservices

By dividing the project into microservices, several benefits can be achieved:

* Scalability: Each microservice can be scaled independently, allowing better resource allocation based on usage patterns. For instance, if the streaming service experiences heavy traffic, it can be scaled without affecting the user management service.

* Maintainability: Each microservice is small and focuses on a specific domain (user management, movie management, etc.), making it easier to maintain and develop features without affecting other parts of the system.

* Flexibility: Microservices can be developed and deployed independently. This allows for the use of different technologies in different services if necessary, optimizing the performance of each component.

* Resilience: In a microservice architecture, failure in one service doesn't affect the others, ensuring the overall system remains functional.

* Faster Development: Different teams can work on different services concurrently, leading to faster development cycles.

Microservice Responsibilities

User Microservice:

* Manages user data (e.g., registration, login, profile).

* Handles the creation, updating, and retrieval of user information.

Streaming Microservice:

* Manages the movies available on the platform.

* Allows the creation, retrieval, and update of movie details, including metadata like genre, title, and description.

* Manages watchlists for users, allowing them to add or remove movies from their lists.

API Gateway:

* ggregates requests from clients to the appropriate microservices.

* Simplifies communication by abstracting the underlying microservices from the client.

* Provides a centralized point for authentication and routing of requests.

## Prerequisites

Make sure you have the following components installed on your machine:

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or higher)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)
- [MySQL Workbench](https://dev.mysql.com/downloads/workbench/) (Optional)

## Project Setup

### 1. Clone the repository

```bash
    git clone https://github.com/JheremyTancara/TheBoys-MonolithicLayers.git
    cd TheBoys-MonolithicLayers
```

### 2. Start MySQL Server with docker

```bash
docker compose up -d
```

### 3. Update the database

Run the following command to apply migrations and update the database:

```bash
dotnet ef database update
```

### 4. Run the application

To start the application, use the following command:

```bash
dotnet run
```

The API will be available at `https://localhost:5270/swagger/index.html`

Run from all the microservices

```bash
sudo docker-compose up --build
