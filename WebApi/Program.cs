using WebApi.Dependency;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//AGREGAMOS LA CLASE ESTATICA PARA LA CONFIGURACION DE DEPENDENCIAS Y MANTENER EL PROGRAM.CS LIMPIO
builder.Services.AggregatedDependencyConfiguration(builder.Configuration);

var app = builder.Build();

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
