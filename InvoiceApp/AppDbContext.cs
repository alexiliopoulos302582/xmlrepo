using InvoiceApp.Models;
using Microsoft.EntityFrameworkCore;



namespace InvoiceApp

{
    public class AppDbContext : DbContext
    {


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customer { get; set; }

    }
}
