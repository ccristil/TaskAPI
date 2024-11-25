using Microsoft.EntityFrameworkCore;
using TaskAPI.Models;

namespace TaskAPI.Data;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<TodoItem> TodoItems { get; set; }
    // test
}