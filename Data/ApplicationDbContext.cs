using EventRegistrationApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventRegistrationApp.Data;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    public DbSet<Event> Events { get; set; }
    public object EventRegistrations { get; set; }
}