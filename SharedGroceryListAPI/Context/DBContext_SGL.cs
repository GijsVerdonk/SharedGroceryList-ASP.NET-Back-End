using Microsoft.EntityFrameworkCore;
using SharedGroceryListAPI.Models;

namespace SharedGroceryListAPI.Context;

public partial class DBContext_SGL : DbContext
{
    public DBContext_SGL()
    {
    }

    public DBContext_SGL(DbContextOptions<DBContext_SGL> options)
        : base(options)
    {
    }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<List> Lists { get; set; }

    public virtual DbSet<ListItem> ListItems { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeItem> RecipeItems { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserList> UserLists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("items");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(55)
                .HasColumnName("name");
        });

        modelBuilder.Entity<List>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("lists");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(6)
                .HasColumnName("code");
            entity.Property(e => e.CodeActiveSince)
                .HasColumnType("datetime")
                .HasColumnName("code_active_since");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(55)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ListItem>(entity =>
        {
            entity.HasKey(e => new { e.ListId, e.ItemId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("list_items");

            entity.HasIndex(e => e.ItemId, "item_id");

            entity.Property(e => e.ListId).HasColumnName("list_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Quantity)
                .HasMaxLength(55)
                .HasColumnName("quantity");

            entity.HasOne(d => d.Item).WithMany(p => p.ListItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("list_items_ibfk_2");

            entity.HasOne(d => d.List).WithMany(p => p.ListItems)
                .HasForeignKey(d => d.ListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("list_items_ibfk_1");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("recipes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IsFeatured).HasColumnName("is_featured");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Text)
                .HasMaxLength(255)
                .HasColumnName("text");
        });

        modelBuilder.Entity<RecipeItem>(entity =>
        {
            entity.HasKey(e => new { e.RecipeId, e.ItemId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("recipe_items");

            entity.HasIndex(e => e.ItemId, "item_id");

            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Quantity)
                .HasMaxLength(55)
                .HasColumnName("quantity");

            entity.HasOne(d => d.Item).WithMany(p => p.RecipeItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_items_ibfk_2");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeItems)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_items_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IsAdmin).HasColumnName("is_admin");
            entity.Property(e => e.Nickname)
                .HasMaxLength(255)
                .HasColumnName("nickname");
            entity.Property(e => e.Sub)
                .HasMaxLength(255)
                .HasColumnName("sub");
        });

        modelBuilder.Entity<UserList>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ListId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("user_lists");

            entity.HasIndex(e => e.ListId, "list_id");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ListId).HasColumnName("list_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IsCreator).HasColumnName("is_creator");

            entity.HasOne(d => d.List).WithMany(p => p.UserLists)
                .HasForeignKey(d => d.ListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_lists_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.UserLists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_lists_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
