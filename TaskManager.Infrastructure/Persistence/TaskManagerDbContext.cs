using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Persistence.DbEntities;

namespace TaskManager.Infrastructure.Persistence;

public partial class TaskManagerDbContext : DbContext
{
    public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<DbEntities.Task> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AspNetRo__3214EC07F0D8A8CE");

            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysutcdatetime())");
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AspNetRo__3214EC07E3D33350");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasConstraintName("FK__AspNetRol__RoleI__59FA5E80");
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AspNetUs__3214EC070139D893");

            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK__AspNetUse__RoleI__5DCAEF64"),
                    l => l.HasOne<AspNetUser>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__AspNetUse__UserI__5CD6CB2B"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK__AspNetUs__AF2760AD74DF4FC6");
                        j.ToTable("AspNetUserRoles");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AspNetUs__3214EC07918C7C1C");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasConstraintName("FK__AspNetUse__UserI__5AEE82B9");
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey }).HasName("PK__AspNetUs__2B2C5B52BE49A73F");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasConstraintName("FK__AspNetUse__UserI__5BE2A6F2");
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name }).HasName("PK__AspNetUs__8CC49841E85007A1");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasConstraintName("FK__AspNetUse__UserI__5EBF139D");
        });

        modelBuilder.Entity<DbEntities.Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tasks__3214EC079ED97AA7");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysutcdatetime())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
