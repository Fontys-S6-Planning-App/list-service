using list_service.Models;
using Microsoft.EntityFrameworkCore;
namespace list_service.DBContexts;

public class ToDoListContext : DbContext
{
    public DbSet<ToDoList> ToDoLists { get; set; }
    
    public ToDoListContext(DbContextOptions<ToDoListContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // map entity to table
        modelBuilder.Entity<ToDoList>().ToTable("to-do-list");
        
        // configure primary key
        modelBuilder.Entity<ToDoList>().HasKey(t => t.Id).HasName("pk_to_do_list");

        // configure indexes
        modelBuilder.Entity<ToDoList>().HasIndex(t => t.Name).IsUnique().HasDatabaseName("idx_id");
        
        // configure columns
        modelBuilder.Entity<ToDoList>().Property(t => t.Id).HasColumnName("id");
        modelBuilder.Entity<ToDoList>().Property(t => t.BoardId).HasColumnName("board_id");
        modelBuilder.Entity<ToDoList>().Property(t => t.Name).HasColumnName("name");
    }
}