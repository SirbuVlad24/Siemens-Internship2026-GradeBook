using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Repositories;
using Siemens.Internship2026.GradeBook.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Registering the External Repository as the primary data source
builder.Services.AddHttpClient<IGradeReader, ExternalGradeRepository>();

// Registering the Service Layer
builder.Services.AddScoped<IGradeService, GradeService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
