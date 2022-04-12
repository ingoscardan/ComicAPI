using ComicAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicAPI.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) :base(options){ }
    public DbSet<Series> Series { get; set; }
}