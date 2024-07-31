namespace EfCoreDeepDive.API.Entities;

public class Product : BaseEntity
{
    public Product(string title, string description, decimal price, Guid idCategory)
    {
        Title = title;
        Description = description;
        Price = price;
        IdCategory = idCategory;
    }
    
    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public Guid IdCategory { get; private set; }
    public Category Category { get; set; }
}