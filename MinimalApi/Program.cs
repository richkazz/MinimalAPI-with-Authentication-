using MinimalApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterServices();
builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.RegisterExecption();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseAuthentication();
app.UseAuthorization();
app.RegisterEndPointDefinitions();


app.Run();

