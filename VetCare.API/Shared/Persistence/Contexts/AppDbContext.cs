using VetCare.API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using VetCare.API.Appointment.Domain.Models;
using VetCare.API.Faq.Domain.Models;
using VetCare.API.Identification.Domain.Models;
using VetCare.API.Store.Domain.Models;

namespace VetCare.API.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{
    
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    
    public DbSet<Question> Questions { get; set; }
    
    

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Pet>().ToTable("Pets");
        builder.Entity<Pet>().HasKey(p => p.Id);
        builder.Entity<Pet>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Pet>().Property(p => p.Name).IsRequired().HasMaxLength(30);
        builder.Entity<Pet>().Property(p => p.Breed).IsRequired().HasMaxLength(30);
        builder.Entity<Pet>().Property(p => p.Weight).IsRequired();
        builder.Entity<Pet>().Property(p => p.Type).IsRequired();
        builder.Entity<Pet>().Property(p => p.photoUrl).IsRequired();
        builder.Entity<Pet>().Property(p => p.Color).IsRequired().HasMaxLength(30);
        builder.Entity<Pet>().Property(p => p.Date).IsRequired();
        
        // Relationships
        builder.Entity<Pet>()
            .HasMany(p => p.Prescriptions)
            .WithOne(p => p.Pet)
            .HasForeignKey(p => p.CategoryId);
        
        builder.Entity<Prescription>().ToTable("Prescriptions");
        builder.Entity<Prescription>().HasKey(p => p.Id);
        builder.Entity<Prescription>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Prescription>().Property(p => p.Title).IsRequired().HasMaxLength(50);
        builder.Entity<Prescription>().Property(p => p.Description).HasMaxLength(120);
        builder.Entity<Prescription>().Property(p => p.Published).IsRequired();
        
        
        
        builder.Entity<Product>().ToTable("Products");
        builder.Entity<Product>().HasKey(p => p.Id);
        builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Product>().Property(p => p.Description).HasMaxLength(200);
        builder.Entity<Product>().Property(p => p.Price).IsRequired();
        builder.Entity<Product>().Property(p => p.Image).HasMaxLength(250);
        builder.Entity<Product>().Property(p => p.Stock).IsRequired();
        
        
        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasKey(p => p.Id);
        builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(p => p.Email).IsRequired();
        builder.Entity<User>().Property(p => p.FirstName).IsRequired();
        builder.Entity<User>().Property(p => p.LastName).IsRequired();
        builder.Entity<User>().Property(p => p.PasswordHash).IsRequired();

        
        builder.Entity<Question>().ToTable("Questions");
        builder.Entity<Question>().HasKey(p => p.Id);
        builder.Entity<Question>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Question>().Property(p => p.Name).IsRequired();
        builder.Entity<Question>().Property(p => p.Description).IsRequired();
        
        
        
        // Apply Snake Case Naming Convention
        
        builder.UseSnakeCaseNamingConvention();
    }
}