namespace EfCoreDeepDive.API.Entities;

public class Category : BaseEntity
{
    public Category(string title)
    {
        Title = title;
    }
    
    public string Title { get; private set; }
    public List<Product> Products { get; private set; }
}