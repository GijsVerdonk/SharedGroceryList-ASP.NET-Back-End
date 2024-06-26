﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SharedGroceryListAPI.Context;

#nullable disable

namespace SharedGroceryListAPI.Migrations
{
    [DbContext(typeof(DBContext_SGL))]
    partial class DBContext_SGLModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");

            modelBuilder.Entity("SharedGroceryListAPI.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_active");

                    b.Property<string>("Name")
                        .HasMaxLength(55)
                        .HasColumnType("varchar(55)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("items", (string)null);
                });

            modelBuilder.Entity("SharedGroceryListAPI.Models.List", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)")
                        .HasColumnName("code");

                    b.Property<DateTime?>("CodeActiveSince")
                        .HasColumnType("datetime")
                        .HasColumnName("code_active_since");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_active");

                    b.Property<string>("Name")
                        .HasMaxLength(55)
                        .HasColumnType("varchar(55)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("lists", (string)null);
                });

            modelBuilder.Entity("SharedGroceryListAPI.Models.ListItem", b =>
                {
                    b.Property<int>("ListId")
                        .HasColumnType("int")
                        .HasColumnName("list_id");

                    b.Property<int>("ItemId")
                        .HasColumnType("int")
                        .HasColumnName("item_id");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_active");

                    b.Property<string>("Quantity")
                        .HasMaxLength(55)
                        .HasColumnType("varchar(55)")
                        .HasColumnName("quantity");

                    b.HasKey("ListId", "ItemId")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.HasIndex(new[] { "ItemId" }, "item_id");

                    b.ToTable("list_items", (string)null);
                });

            modelBuilder.Entity("SharedGroceryListAPI.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_active");

                    b.Property<bool?>("IsFeatured")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_featured");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.Property<string>("Text")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("text");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("recipes", (string)null);
                });

            modelBuilder.Entity("SharedGroceryListAPI.Models.RecipeItem", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int")
                        .HasColumnName("recipe_id");

                    b.Property<int>("ItemId")
                        .HasColumnType("int")
                        .HasColumnName("item_id");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_active");

                    b.Property<string>("Quantity")
                        .HasMaxLength(55)
                        .HasColumnType("varchar(55)")
                        .HasColumnName("quantity");

                    b.HasKey("RecipeId", "ItemId")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.HasIndex(new[] { "ItemId" }, "item_id")
                        .HasDatabaseName("item_id1");

                    b.ToTable("recipe_items", (string)null);
                });

            modelBuilder.Entity("SharedGroceryListAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_active");

                    b.Property<bool?>("IsAdmin")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_admin");

                    b.Property<string>("Nickname")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("nickname");

                    b.Property<string>("Sub")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("sub");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("SharedGroceryListAPI.Models.UserList", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<int>("ListId")
                        .HasColumnType("int")
                        .HasColumnName("list_id");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_active");

                    b.Property<bool?>("IsCreator")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_creator");

                    b.HasKey("UserId", "ListId")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.HasIndex(new[] { "ListId" }, "list_id");

                    b.ToTable("user_lists", (string)null);
                });

            modelBuilder.Entity("SharedGroceryListAPI.Models.ListItem", b =>
                {
                    b.HasOne("SharedGroceryListAPI.Models.Item", "Item")
                        .WithMany("ListItems")
                        .HasForeignKey("ItemId")
                        .IsRequired()
                        .HasConstraintName("list_items_ibfk_2");

                    b.HasOne("SharedGroceryListAPI.Models.List", "List")
                        .WithMany("ListItems")
                        .HasForeignKey("ListId")
                        .IsRequired()
                        .HasConstraintName("list_items_ibfk_1");

                    b.Navigation("Item");

                    b.Navigation("List");
                });

            modelBuilder.Entity("SharedGroceryListAPI.Models.RecipeItem", b =>
                {
                    b.HasOne("SharedGroceryListAPI.Models.Item", "Item")
                        .WithMany("RecipeItems")
                        .HasForeignKey("ItemId")
                        .IsRequired()
                        .HasConstraintName("recipe_items_ibfk_2");

                    b.HasOne("SharedGroceryListAPI.Models.Recipe", "Recipe")
                        .WithMany("RecipeItems")
                        .HasForeignKey("RecipeId")
                        .IsRequired()
                        .HasConstraintName("recipe_items_ibfk_1");

                    b.Navigation("Item");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("SharedGroceryListAPI.Models.UserList", b =>
                {
                    b.HasOne("SharedGroceryListAPI.Models.List", "List")
                        .WithMany("UserLists")
                        .HasForeignKey("ListId")
                        .IsRequired()
                        .HasConstraintName("user_lists_ibfk_2");

                    b.HasOne("SharedGroceryListAPI.Models.User", "User")
                        .WithMany("UserLists")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("user_lists_ibfk_1");

                    b.Navigation("List");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SharedGroceryListAPI.Models.Item", b =>
                {
                    b.Navigation("ListItems");

                    b.Navigation("RecipeItems");
                });

            modelBuilder.Entity("SharedGroceryListAPI.Models.List", b =>
                {
                    b.Navigation("ListItems");

                    b.Navigation("UserLists");
                });

            modelBuilder.Entity("SharedGroceryListAPI.Models.Recipe", b =>
                {
                    b.Navigation("RecipeItems");
                });

            modelBuilder.Entity("SharedGroceryListAPI.Models.User", b =>
                {
                    b.Navigation("UserLists");
                });
#pragma warning restore 612, 618
        }
    }
}
