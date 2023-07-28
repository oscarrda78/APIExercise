using APIExercise.Core.Entities;
using APIExercise.Core.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace APIExercise.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
            .Property(c => c.Id)
                .HasDefaultValueSql("newid()");

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Transactions)
                .WithOne(t => t.Account)
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Client>()
            .Property(c => c.Id)
                .HasDefaultValueSql("newid()");

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Accounts)
                .WithOne(a => a.Client)
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Address>()
            .Property(a => a.Id)
                .HasDefaultValueSql("newid()");

            modelBuilder.Entity<Person>()
                .HasOne(p => p.Address)
                .WithOne(a => a.Person)
                .HasForeignKey<Address>(a => a.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>()
            .Property(c => c.Id)
                .HasDefaultValueSql("newid()");

            modelBuilder.Entity<Transaction>()
            .HasOne(t => t.CounterpartyAccount)
            .WithMany(a => a.CounterpartyTransactions)
            .HasForeignKey(t => t.CounterpartyAccountId)
            .OnDelete(DeleteBehavior.NoAction);

            //Seed
            var joseGuid = Guid.NewGuid();
            var marianelaGuid = Guid.NewGuid();
            var juanGuid = Guid.NewGuid();

            modelBuilder.Entity<Client>().HasData(
                new Client { Id = joseGuid, FirstName = "Jose", LastName = "Lema", IdDocument = "12345678", PhoneNumber = "098254785", Password = "1234", Status = Status.Active },
                new Client { Id = marianelaGuid, FirstName = "Marianela", LastName = "Montalvo", IdDocument = "91011121", PhoneNumber = "097548965", Password = "5678", Status = Status.Active },
                new Client { Id = juanGuid, FirstName = "Juan", LastName = "Osorio", IdDocument = "31415161", PhoneNumber = "098874587", Password = "1245", Status = Status.Active }
            );

            modelBuilder.Entity<Address>().HasData(
                new Address { Street = "Jirón de la Unión", City = "Lima", State = "Lima", Country = "Perú", PostalCode = "15001", Id = joseGuid },
                new Address { Street = "Avenida Caminos del Inca", City = "Santiago de Surco", State = "Lima", Country = "Perú", PostalCode = "15023", Id = marianelaGuid },
                new Address { Street = "Avenida La Molina", City = "La Molina", State = "Lima", Country = "Perú", PostalCode = "15026", Id = juanGuid }
            );

            modelBuilder.Entity<Account>().HasData(
                new Account { Id= Guid.NewGuid(), AccountNumber = "478758", AccountType = AccountType.Savings, Balance = 2000, Status = Status.Active, ClientId = joseGuid },
                new Account { Id = Guid.NewGuid(), AccountNumber = "225487", AccountType = AccountType.Funds, Balance = 100, Status = Status.Active, ClientId = marianelaGuid },
                new Account { Id = Guid.NewGuid(), AccountNumber = "495878", AccountType = AccountType.Savings, Balance = 0, Status = Status.Active, ClientId = juanGuid },
                new Account { Id = Guid.NewGuid(), AccountNumber = "496825", AccountType = AccountType.Funds, Balance = 540, Status = Status.Active, ClientId = marianelaGuid }
            );

        }

    }

}
