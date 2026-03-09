# 🧟‍♂️ Zombie Horde Defense System API

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![Azure](https://img.shields.io/badge/Deploy-Azure-0078D4?logo=microsoftazure)
![SQL Server](https://img.shields.io/badge/Database-SQL_Server-CC2927?logo=microsoftsqlserver)

API RESTful diseñada para calcular la estrategia de defensa táctica más eficiente contra oleadas de zombies. El sistema evalúa diferentes tipos de amenazas y recursos disponibles para determinar la combinación óptima de defensas, almacenando el historial de simulaciones para análisis posteriores.

## 🏗️ Arquitectura del Sistema

El proyecto está construido siguiendo los principios de la **Arquitectura Hexagonal (Puertos y Adaptadores)** y el **Diseño Orientado al Dominio (DDD)**, garantizando un alto nivel de desacoplamiento, testabilidad y mantenibilidad.

* **Domain:** Contiene la lógica central del negocio y las entidades puras (`Simulacion` como *Aggregate Root*). Sin dependencias externas.
* **Application:** Orquesta los casos de uso a través de servicios (`DefenseStrategyService`). Define los puertos (interfaces) y los DTOs para la transferencia de datos.
* **Infrastructure:** Adaptadores secundarios. Implementa el acceso a datos mediante Entity Framework Core (Database First/Scaffolding) y SQL Server.
* **API (Presentation):** Adaptador primario. Expone los endpoints HTTP y maneja la seguridad de entrada.

## 🗂️ Estructura del Proyecto

📁 ZombieHordeDefenseSystem.sln
├── ZombieHordeDefenseSystem.Api/             # Capa de Presentación (REST API)
│   ├── Controllers/                          # Controladores (ej. DefenseZombieController)
│   ├── Middleware/                           # Interceptores (ej. ApiKeyMiddleware)
│   └── Program.cs                            # Composición de dependencias y pipeline
├── ZombieHordeDefenseSystem.Application/     # Casos de Uso y Orquestación
│   ├── Dtos/                                 # Objetos de Transferencia de Datos
│   ├── Ports/                                # Interfaces para repositorios (Inversión de Dependencias)
│   └── Services/                             # Lógica de aplicación (ej. Algoritmo Knapsack)
├── ZombieHordeDefenseSystem.Domain/          # Núcleo del Negocio (DDD)
│   └── Entities/                             # Entidades del dominio (Eliminado, Simulacion, ZombieType)
└── ZombieHordeDefenseSystem.Infraestructure/ # Persistencia y Acceso a Datos
    ├── Persistence/                          # DbContext y Entidades mapeadas (Scaffold)
    └── Repositories/                         # Implementación de los puertos (SimulationRepository, etc.)
