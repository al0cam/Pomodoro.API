using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class PomodorDbContext : IdentityDbContext<ApplicationUser> // Change to ApplicationUser
{
    public required DbSet<TaskItem> TaskItems { get; set; }

    public PomodorDbContext(DbContextOptions<PomodorDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.TaskItems)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.ToTable("TaskItems");

            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id)
                .ValueGeneratedOnAdd();

            entity.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(t => t.Description)
                .HasMaxLength(1000);

            entity.Property(t => t.IsCompleted)
                .HasDefaultValue(false);

            entity.Property(t => t.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(t => t.DueAt)
                .IsRequired(false);

            entity.Property(t => t.EstimatedPomodoros);

            entity.Property(t => t.CompletedPomodoros)
                .HasDefaultValue(0);
        });
    }
}
