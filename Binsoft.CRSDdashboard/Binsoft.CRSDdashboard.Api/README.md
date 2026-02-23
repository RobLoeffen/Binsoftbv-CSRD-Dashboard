# Ecoparts Dashboard API - SOLID Principles Implementation

## Overview
This API demonstrates **SOLID principles** with a focus on **Single Responsibility Principle (SRP)** using SQLite with Entity Framework Core.

## Architecture

### SOLID Principles Applied

#### 1. **Single Responsibility Principle (SRP)**
Each class has **one reason to change**:

- **Controllers** ? Handle HTTP requests/responses only
- **Services** ? Business logic orchestration
- **Repositories** ? Data access only
- **Mappers** ? Object transformation only
- **DbContext** ? Database configuration only

#### 2. **Dependency Inversion Principle (DIP)**
- High-level modules depend on abstractions (interfaces)
- All dependencies are injected via constructor

```
Controller ? IService ? IRepository
     ?           ?           ?
   (Uses)    (Uses)      (Uses)
```

### Folder Structure

```
Binsoft.CRSDdashboard.Api/
??? Controllers/              # HTTP Layer (API endpoints)
?   ??? EcopartsController.cs
??? Services/                 # Business Logic Layer
?   ??? Interfaces/
?   ?   ??? IEcopartService.cs
?   ?   ??? ICo2CalculationService.cs
?   ??? Implementations/
?       ??? EcopartService.cs
?       ??? Co2CalculationService.cs
??? Repositories/            # Data Access Layer
?   ??? Interfaces/
?   ?   ??? IEcopartRepository.cs
?   ?   ??? IEmissionFactorRepository.cs
?   ??? Implementations/
?       ??? EcopartRepository.cs
?       ??? EmissionFactorRepository.cs
??? Models/
?   ??? Entities/            # Database models
?   ?   ??? Ecopart.cs
?   ?   ??? EmissionFactor.cs
?   ??? Responses/           # API response DTOs
?       ??? EcopartResponse.cs
?       ??? EcopartWithEmissionsResponse.cs
??? Mappers/                 # Object Mapping
?   ??? IEcopartMapper.cs
?   ??? EcopartMapper.cs
??? Data/                    # Database Context
?   ??? AppDbContext.cs
??? Program.cs              # DI Configuration
```

## Database

### SQLite Database
- **File**: `ecoparts.db` (created automatically)
- **Provider**: Entity Framework Core with SQLite

### Tables

#### **Ecoparts**
| Column | Type | Description |
|--------|------|-------------|
| Id | int | Primary key |
| Name | string | Part name |
| Length | double | Length in mm |
| Width | double | Width in mm |
| Height | double | Height in mm |
| Weight | double | Weight in kg |
| Material | string | Material type |
| CreatedAt | DateTime | Creation timestamp |

#### **EmissionFactors**
| Column | Type | Description |
|--------|------|-------------|
| Id | int | Primary key |
| Material | string | Material name (unique) |
| Co2PerKg | double | CO2 emissions per kg |

### Seeded Data

**Emission Factors:**
- Steel: 1.85 kg CO2/kg
- Aluminum: 8.24 kg CO2/kg
- Plastic: 3.5 kg CO2/kg
- Wood: 0.45 kg CO2/kg
- Copper: 2.7 kg CO2/kg
- Glass: 0.85 kg CO2/kg

**Sample Ecoparts:**
- Steel Beam (15.5 kg)
- Aluminum Frame (8.2 kg)
- Plastic Housing (2.3 kg)
- Wooden Panel (5.6 kg)
- Copper Wire Bundle (3.4 kg)

## API Endpoints

### 1. Get All Ecoparts
```http
GET /api/ecoparts
```

**Response:**
```json
[
  {
    "id": 1,
    "name": "Steel Beam",
    "length": 200,
    "width": 50,
    "weight": 15.5,
    "material": "Steel"
  }
]
```

### 2. Get Ecopart by ID
```http
GET /api/ecoparts/{id}
```

**Example:**
```http
GET /api/ecoparts/1
```

**Response:**
```json
{
  "id": 1,
  "name": "Steel Beam",
  "length": 200,
  "width": 50,
  "weight": 15.5,
  "material": "Steel"
}
```

### 3. Get Ecopart with CO2 Emissions
```http
GET /api/ecoparts/{id}/emissions
```

**Example:**
```http
GET /api/ecoparts/1/emissions
```

**Response:**
```json
{
  "id": 1,
  "name": "Steel Beam",
  "length": 200,
  "width": 50,
  "weight": 15.5,
  "material": "Steel",
  "totalCo2Equivalent": 28.675,
  "co2PerKg": 1.85
}
```

**Calculation:**
```
Total CO2 = (Weight × CO2 per kg) / 1 kg
          = (15.5 kg × 1.85 kg CO2/kg) / 1
          = 28.675 kg CO2
```

## How to Run

1. **Build the project:**
   ```bash
   dotnet build
   ```

2. **Run the API:**
   ```bash
   dotnet run --project Binsoft.CRSDdashboard.Api
   ```

3. **Access Swagger/OpenAPI:**
   ```
   https://localhost:<port>/openapi/v1.json
   ```

4. **Test endpoints:**
   ```bash
   # Get all ecoparts
   curl https://localhost:<port>/api/ecoparts

   # Get specific ecopart with emissions
   curl https://localhost:<port>/api/ecoparts/1/emissions
   ```

## SOLID Principles in Action

### Example Flow: GET /api/ecoparts/1/emissions

```
1. EcopartsController (HTTP layer)
   ??? Receives HTTP request
   ??? Calls IEcopartService.GetEcopartWithEmissionsAsync(1)

2. EcopartService (Business logic)
   ??? Calls IEcopartRepository.GetByIdAsync(1)
   ??? Calls ICo2CalculationService.CalculateCo2EquivalentAsync()
   ??? Calls IEcopartMapper.ToResponseWithEmissions()

3. EcopartRepository (Data access)
   ??? Queries AppDbContext.Ecoparts
   ??? Returns Ecopart entity

4. Co2CalculationService (Calculation logic)
   ??? Calls IEmissionFactorRepository.GetByMaterialAsync()
   ??? Performs calculation: (weight × co2PerKg) / 1

5. EmissionFactorRepository (Data access)
   ??? Queries AppDbContext.EmissionFactors
   ??? Returns EmissionFactor entity

6. EcopartMapper (Object transformation)
   ??? Converts Ecopart ? EcopartWithEmissionsResponse

7. EcopartsController
   ??? Returns HTTP 200 with JSON response
```

### Key Benefits

? **Testability**: Each component can be unit tested independently  
? **Maintainability**: Changes in one layer don't affect others  
? **Flexibility**: Easy to swap implementations (e.g., JSON ? SQL ? MongoDB)  
? **Readability**: Clear separation of concerns  
? **Scalability**: Easy to add new features following the same pattern

## Next Steps for Learning

1. **Add more SOLID principles:**
   - **Open/Closed Principle (OCP)**: Strategy pattern for different calculation methods
   - **Liskov Substitution Principle (LSP)**: Multiple repository implementations
   - **Interface Segregation Principle (ISP)**: Split large interfaces

2. **Add validation:**
   - FluentValidation for request validation

3. **Add error handling:**
   - Global exception handler middleware

4. **Add logging:**
   - ILogger integration

5. **Add caching:**
   - In-memory cache for emission factors

6. **Add unit tests:**
   - xUnit + Moq for testing each layer

## Technologies Used

- **.NET 10**
- **ASP.NET Core Web API**
- **Entity Framework Core 10**
- **SQLite**
- **C# 14**

## Database File

The SQLite database (`ecoparts.db`) is created automatically in the project root when you first run the application.
