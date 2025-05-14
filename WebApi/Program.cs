using Persistence;
using WebApi.Dependency;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

//AGREGAMOS LA CLASE ESTATICA PARA LA CONFIGURACION DE DEPENDENCIAS Y MANTENER EL PROGRAM.CS LIMPIO
builder.Services.AggregatedDependencyConfiguration(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
           

var app = builder.Build();

//Crear usuario admin al iniciar
await InsertTestData.InsertInformationInDbTest(app.Services);

app.UseMiddleware<ExecuteExceptionCapture>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();https://localhost:7021

app.UseAuthorization();

app.MapControllers();

app.Run();
