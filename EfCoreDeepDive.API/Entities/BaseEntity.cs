namespace EfCoreDeepDive.API.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
}