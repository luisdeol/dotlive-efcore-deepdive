using System.Linq.Expressions;
using EfCoreDeepDive.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace EfCoreDeepDive.API.Persistence;

public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Product>(e =>
        {
            e.HasKey(p => p.Id);

            e.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.IdCategory)
                .OnDelete(DeleteBehavior.Restrict);

            e.OwnsOne(p => p.Manufacturer, e =>
            {
                e.Property(m => m.Name);//.HasColumnName("ManufacturerName");
                e.Property(m => m.ProductFullAddress);//.HasColumnName("ProducerFullAddress");
                e.Property(m => m.ProductionDate);//.HasColumnName("ProductionDate");
            });
        });

        builder.Entity<Category>(e =>
        {
            e.HasKey(c => c.Id);
        });
        
        // Global Query Filter
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (!typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                continue;
            }

            var param = Expression.Parameter(entityType.ClrType);
            var prop = Expression.PropertyOrField(param, nameof(BaseEntity.IsDeleted));
            var entityNotDeleted = Expression.Lambda(Expression.Equal(prop, Expression.Constant(false)), param);

            entityType.SetQueryFilter(entityNotDeleted);
        }
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>().Where(x => x.State == EntityState.Modified))
        {
            if (entry.State == EntityState.Modified)
                entry.Entity.SetUpdatedDate();
        }

        return base.SaveChanges();
    }
}