namespace InvoiceApp
{
    using InvoiceApp.Models;
    using Microsoft.EntityFrameworkCore;




    public class ChaniaContext :DbContext
    {


        public ChaniaContext(DbContextOptions<ChaniaContext> options) : base(options)
        {
            
        }

        public DbSet<ChaniaCustomer> ChaniaCustomers { get; set; }
        public DbSet<ChaniaInvoice> ChaniaInvoices { get; set; }
        public DbSet<ChaniaTransaction> ChaniaTransactions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "Server=localhost;Database=chaniabank;User=root;Password= !@;",
                new MySqlServerVersion(new Version(8, 0, 2))  // Use your MySQL version here
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure primary keys and relationships
            modelBuilder.Entity<ChaniaCustomer>()
                .HasKey(c => c.ChaniaCustomerID); // Primary key for ChaniaCustomer

            modelBuilder.Entity<ChaniaInvoice>()
                .HasKey(i => i.ChaniaDocID); // Primary key for ChaniaInvoice

            modelBuilder.Entity<ChaniaTransaction>()
                .HasKey(t => t.ChaniaTransactionId); // Primary key for ChaniaTransaction

            // Configure one-to-many relationship between ChaniaInvoice and ChaniaTransaction
            modelBuilder.Entity<ChaniaInvoice>()
                .HasMany(i => i.ChaniaTransactions)
                .WithOne(t => t.ChaniaInvoice)
                .HasForeignKey(t => t.ChaniaDocID) // Foreign key in ChaniaTransaction
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete transactions when invoice is deleted

            base.OnModelCreating(modelBuilder);
        }


    }
}
