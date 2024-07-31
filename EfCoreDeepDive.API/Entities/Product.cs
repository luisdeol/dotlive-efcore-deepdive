using System.Security.Cryptography;

namespace EfCoreDeepDive.API.Entities;

public class Product : BaseEntity
{
    protected Product() { }
    public Product(string title, string description, decimal price, Guid idCategory, Manufacturer manufacturer)
    {
        Title = title;
        Description = description;
        Price = price;
        IdCategory = idCategory;

        Manufacturer = manufacturer;
    }
    
    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public Guid IdCategory { get; private set; }
    public Category Category { get; set; }
    public Manufacturer Manufacturer { get; set; }

    public void Update(string title, string description, decimal price)
    {
        Title = title;
        Description = description;
        Price = price;
    }
}