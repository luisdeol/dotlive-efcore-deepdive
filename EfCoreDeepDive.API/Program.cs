using EfCoreDeepDive.API.Entities;
using EfCoreDeepDive.API.Models;
using EfCoreDeepDive.API.Persistence;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("StoreCs");

builder.Services.AddDbContext<StoreDbContext>(o =>
    o.UseSqlServer(connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Paginação
app.MapGet("/api/products", (StoreDbContext db) =>
{
    var products = db.Products.ToList();

    return Results.Ok(products);
});

app.MapPost("/api/categories", (CategoryInputModel model, StoreDbContext db) =>
{
    var category = new Category(model.Title);

    db.Categories.Add(category);
    db.SaveChanges();

    return Results.Ok(category.Id);
});

// Testando Overriding SaveChanges
app.MapPut("/api/products/{id}", (Guid id, ProductInputModel model, StoreDbContext db) =>
{
    var product = db.Products.SingleOrDefault(p => p.Id == id);

    if (product is null)
        return Results.NotFound();

    product.Update(model.Title, model.Description, model.Price);

    db.SaveChanges();

    return Results.NoContent();
});


// ExecuteUpdate / ExecuteDelete
app.MapDelete("/api/products", (Guid categoryId, StoreDbContext db) =>
{
    var products = db.Products.Where(p => p.IdCategory == categoryId).ToList();

    foreach (var product in products)
        product.IsDeleted = true;

    db.SaveChanges();

    return Results.NoContent();
});

app.MapPost("/api/products", (StoreDbContext db, ProductInputModel model) =>
{
    var product = new Product(model.Title, model.Description, model.Price, model.IdCategory);

    db.Products.Add(product);

    db.SaveChanges();

    return Results.Created("/api/products", model);
});


app.Run();