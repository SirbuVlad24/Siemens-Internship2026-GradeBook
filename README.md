## Getting Started
1. Run application: `dotnet run`
2. Local address: `https://localhost:7069` or `http://localhost:5030`.

## Main Endpoints
- All grades and statistics: `GET /api/Grades`
- Grade by ID: `GET /api/Grades/{id}`
- Filter passing grades (N): `GET /api/Grades/passing?count={N}`
- Visual interface for testing N: `/api/Grades/view`

## Technical Details
- **SOLID Refactoring**: All initial violations have been corrected (see Documentation.md).
- **Service Layer**: Business logic implementation in GradeService.
- **Repository Pattern**: Integration with external endpoint (GitHub Gist) via ExternalGradeRepository.
- **Dependency Injection**: Decoupling components through interfaces.
- **Domain**: Renamed 'Item' entities to 'Grade' for better clarity.
