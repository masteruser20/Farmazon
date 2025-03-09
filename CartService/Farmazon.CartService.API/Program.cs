using Farmazon.CartService.App.Commands;
using Farmazon.CartService.App.Interfaces;
using Farmazon.CartService.Infrastructure.Repositories;
using Farmazon.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddScoped<ICartRepository, CartRepository>();

builder.Services.AddHttpClient<IProductProvider, ProductProvider>();
// with policies

app.UseHttpsRedirection();


app.Run();