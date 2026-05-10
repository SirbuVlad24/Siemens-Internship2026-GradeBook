## Getting Started
1. Run application: `dotnet run` (default port 5030).
2. To enable HTTPS (port 7069):
   - Run `dotnet dev-certs https --trust`
   - Run `dotnet run --launch-profile https`
3. Local address: `http://localhost:5030` or `https://localhost:7069`.

## Main Endpoints
- All grades and statistics: `GET /api/Grades`
- Grade by ID: `GET /api/Grades/{id}`
- Filter passing grades (N): `GET /api/Grades/passing?count={N}`
- Visual dashboard: Access the root URL `http://localhost:5030` or `https://localhost:7069` (automatically redirects to the interactive UI)

## Technical Details
- **SOLID Refactoring**: All initial violations have been corrected (see Documentation.md).
- **Service Layer**: Business logic implementation in GradeService.
- **Repository Pattern**: Integration with external endpoint (GitHub Gist) via ExternalGradeRepository.
- **Dependency Injection**: Decoupling components through interfaces.
- **Domain**: Renamed 'Item' entities to 'Grade' for better clarity.
