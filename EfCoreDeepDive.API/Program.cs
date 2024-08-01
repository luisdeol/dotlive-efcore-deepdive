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
app.MapGet("/api/products", (StoreDbContext db, int page = 0, int size = 3) =>
{
    var products = db.Products
        .Skip(size * page)
        .Take(size)
        //.IgnoreQueryFilters()
        .ToList();

    var totalRecords = db.Products.Count();

    var pagedProducts = new PagedProducts(products, page, size, totalRecords);

    return Results.Ok(pagedProducts);
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
    //var products = db.Products.Where(p => p.IdCategory == categoryId).ToList();

    //db.Products.RemoveRange(products);

    //foreach (var product in products)
    //    product.IsDeleted = true;

    // db.SaveChanges();

    db.Products
       .Where(p => p.IdCategory == categoryId)
       .ExecuteDelete();

    db.Products
        .Where(p => p.IdCategory == categoryId)
        .ExecuteUpdate(s => s.SetProperty(p => p.IsDeleted, true));

    return Results.NoContent();
});

app.MapPost("/api/products", (StoreDbContext db, ProductInputModel model) =>
{
    var manufacturer = new Manufacturer("Samsung", DateTime.Now.AddYears(-2), "Endereço ABC");
    var product = new Product(model.Title, model.Description, model.Price, model.IdCategory, manufacturer);

    db.Products.Add(product);

    db.SaveChanges();

    return Results.Created("/api/products", model);
});


app.Run();