using FolhaPagamento.Models;
using Microsoft.EntityFrameworkCore;

namespace FolhaPagamento.Data;

public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions<AppDataContext> options) :
    base(options)
    { }
    public DbSet<Employee> Employees { get; set; } 

    public DbSet<Folha> Folhas { get; set; }
}