using EfCoreDeepDive.API.Entities;
using EfCoreDeepDive.API.Models;
using EfCoreDeepDive.API.Persistence;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<StoreDbContext>(o =>
    o.UseInMemoryDatabase("StoreDb"));
    //o.UseSqlServer("Server=localhost,1433;Database=StoreDb;User ID=sa;Password=MyPass@word"));

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
app.MapGet("/api/products", (StoreDbContext db, int page = 0, int size = 20) =>
{
    var products = db.Products.ToList();
    var totalRecords = db.Products.Count();

    var pagedProducts = new PagedProducts(products, page, size, totalRecords);
    
    return Results.Ok(new
    {
        products,
        pagedProducts
    });
});

app.MapPost("/api/categories", (CategoryInputModel model, StoreDbContext db) =>
{
    var category = new Category(model.Title);

    db.Categories.Add(category);
    db.SaveChanges();

    return Results.Ok(category.Id);
});

// Atualizar todos produtos de category
app.MapPut("/api/products", (Guid categoryId, StoreDbContext db) =>
{
    db.Products
        .Where(p => p.IdCategory == categoryId)
        .ExecuteUpdate(s => s.SetProperty(p => p.IsDeleted, false));

    return Results.NoContent();
});


// Deletar todos produtos de category
app.MapDelete("/api/products", (Guid categoryId, StoreDbContext db) =>
{
    db.Products
        .Where(p => p.IdCategory == categoryId)
        .ExecuteDelete();

    return Results.NoContent();
});


// Transação
app.MapPost("/api/products", (StoreDbContext db, ProductInputModel model) =>
{
    var product = new Product(model.Title, model.Description, model.Price, model.IdCategory);

    /*using var transaction = db.Database.BeginTransaction();

    try
    {*/
        db.Products.Add(product);
        

        var categoryAleatoria = new Category("Teste");
        
        db.SaveChanges();
        
        // E se precisar reutilizar o Id de Category?
        
       /* transaction.Commit();
    }
    catch (Exception)
    {
        
    }
*/
    return Results.Created("/api/products", product);
});


// Configuração normal

// Configuração BaseEntity Ids

// Global Query Filters


app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}