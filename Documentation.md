# SOLID Principles Review & Refactoring Report

This document outlines the SOLID principle violations identified in the initial codebase of the **Siemens.Internship2026.GradeBook** project and the refactoring steps taken to address them.

## SOLID Analysis & Improvements

### 1. Single Responsibility Principle (SRP)
- **Violation**: The `ItemController` (now `GradesController`) was responsible for multiple high-level and low-level tasks: handling HTTP requests, performing console logging, and calculating grade statistics.
- **Location**: `Controllers/ItemController.cs`
    - **GetAll() Method (Lines 20-28)**: Contained logic for calculating `AverageValue` and `TotalCount`.
    - **GetById() Method (Lines 45-56)**: Handled manual logging and orchestration logic.
- **Why it is a violation**: A controller should only manage the request/response flow. Business logic and infrastructure concerns should be isolated in separate services to ensure the class has only one reason to change.
- **Fix**: Created the `IGradeService` and `GradeService` classes to handle statistics and business logic. Replaced `Console.WriteLine` with the standard `ILogger` abstraction.

### 2. Open/Closed Principle (OCP)
- **Violation**: Logging and statistics logic were hardcoded inside the controller methods.
- **Location**: `Controllers/ItemController.cs`
    - **Liniile 26, 33-38**: The logic for calculating and returning statistics was fixed.
    - **Multiple Lines (20, 28, 45, 49, 56)**: Direct dependency on the `Console` class for logging.
- **Why it is a violation**: To change the logging target or modify how statistics are calculated, the controller's source code had to be modified. This makes the code closed for extension but open for modification.
- **Fix**: Injected `IGradeService` and `ILogger` interfaces. Now, new implementations can be introduced via Dependency Injection without modifying the controller.

### 3. Liskov Substitution Principle (LSP)
- **Violation**: The repository used generic naming and exposed internal state (`protected readonly List<Item> _items`) in a way that allowed subclasses to violate the contract.
- **Location**: `Repositories/ItemRepository.cs`
    - **Lines 8-9**: `protected readonly List<Item> _items` and `protected int _nextId`.
- **Why it is a violation**: Subclasses could modify the internal list directly, potentially bypassing filters or logic intended by the base class/interface.
- **Fix**: Renamed the class to `InMemoryGradeRepository` and ensured that the implementation strictly adheres to the `IGradeReader` interface, hiding internal state from unnecessary modification.

### 4. Interface Segregation Principle (ISP)
- **Violation**: The class was named `ItemRepository` while only implementing a read-only interface (`IItemReader`).
- **Location**: `Repositories/ItemRepository.cs`
    - **Line 6**: `public class ItemRepository : IItemReader`.
- **Why it is a violation**: A "Repository" normally implies full CRUD operations. By naming it this way while only implementing a reader, it creates a misleading interface for the client.
- **Fix**: Renamed the domain entity to `Grade` and the implementation to `InMemoryGradeRepository`. This keeps the "Reader" interface specific and the implementation name accurate.

### 5. Dependency Inversion Principle (DIP)
- **Violation**: The controller depended directly on the concrete `Console` class, and the application lacked proper dependency registration.
- **Location**: `Controllers/ItemController.cs` and `Program.cs`.
- **Why it is a violation**: High-level modules should depend on abstractions. The direct dependency on `Console` and the lack of DI registration meant the application was rigid and would fail at runtime.
- **Fix**: Refactored the code to depend on `IGradeService` and `ILogger` abstractions. Registered all dependencies in the `Program.cs` file.

---

## General Refactoring & Fixes
- **Domain Renaming**: Renamed `Item` to `Grade` across the project for better clarity and alignment with the "GradeBook" domain.
- **Dependency Registration**: Fixed the runtime error by correctly registering all services in the DI container.
- **Data Initialization**: Added seed data to the in-memory repository to ensure a functional application state for testing.
