using Application.Posts.Commands;
using Application.Posts.Queries;
using DataAccess.DataAccessException.AuthenticationException;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Options;
using MinimalApi.Extensions;
using MinimalApi.OptionsSetup;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterServices();

var app = builder.Build();

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

