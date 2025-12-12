using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Web.Models;

namespace ExpenseTracker.Web.Data;

public class ExpenseTrackerContext : DbContext
{
    public ExpenseTrackerContext(DbContextOptions<ExpenseTrackerContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Expense> Expenses => Set<Expense>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Category configuration
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.HasIndex(e => e.Name)
                .IsUnique();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Color)
                .IsRequired()
                .HasMaxLength(7)
                .HasDefaultValue("#6c757d");

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .IsRequired();
        });

        // Expense configuration
        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.ExpenseDate)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .IsRequired();

            // Expense-Category relationship with cascade delete
            entity.HasOne(e => e.Category)
                .WithMany(c => c.Expenses)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        });

        // Seed data
        SeedData(modelBuilder);
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => (e.Entity is Category || e.Entity is Expense) 
                && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            if (entry.Entity is Category category)
            {
                if (entry.State == EntityState.Added)
                {
                    category.CreatedAt = DateTime.UtcNow;
                }
                category.UpdatedAt = DateTime.UtcNow;
            }
            else if (entry.Entity is Expense expense)
            {
                if (entry.State == EntityState.Added)
                {
                    expense.CreatedAt = DateTime.UtcNow;
                }
                expense.UpdatedAt = DateTime.UtcNow;
            }
        }
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        var seedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        var categories = new[]
        {
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Food",
                Description = "Groceries, dining out, and food delivery",
                Icon = "fas fa-utensils",
                Color = "#28a745",
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new Category
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Transportation",
                Description = "Gas, parking, public transit, ride-sharing",
                Icon = "fas fa-car",
                Color = "#007bff",
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new Category
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = "Entertainment",
                Description = "Movies, concerts, hobbies, subscriptions",
                Icon = "fas fa-film",
                Color = "#e83e8c",
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new Category
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Name = "Shopping",
                Description = "Clothing, electronics, household items",
                Icon = "fas fa-shopping-bag",
                Color = "#fd7e14",
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new Category
            {
                Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                Name = "Healthcare",
                Description = "Medical expenses, pharmacy, insurance",
                Icon = "fas fa-heartbeat",
                Color = "#dc3545",
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            }
        };

        modelBuilder.Entity<Category>().HasData(categories);
    }
}
