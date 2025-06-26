using Finme.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Finme.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }
        public DbSet<UserBank> UsersBanks { get; set; }
        public DbSet<UserDocument> UsersDocuments { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<OperationComment> OperationComments { get; set; }
        public DbSet<OperationFile> OperationFiles { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<InvestmentStatus> InvestmentStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<VerificationCode>().ToTable("VerificationCodes");
            
            modelBuilder.Entity<UserBank>().ToTable("Banks");

            modelBuilder.Entity<UserAddress>().ToTable("Address");

            modelBuilder.Entity<UserDocument>().ToTable("Documents");

            modelBuilder.Entity<Operation>().ToTable("Operation");
            
            modelBuilder.Entity<OperationComment>().ToTable("OperationComment");
            
            modelBuilder.Entity<OperationFile>().ToTable("OperationFile");

            modelBuilder.Entity<Administrator>().ToTable("Administrator");

            modelBuilder.Entity<Investment>().ToTable("Investment");

            modelBuilder.Entity<InvestmentStatus>()
                .ToTable("InvestmentStatus")
                .Property(p => p.Status).HasConversion<string>(); ;

            modelBuilder.Entity<UserBank>()
                .HasOne(vc => vc.User)
                .WithMany(u => u.UserBanks) // Especifica a propriedade de navegação
                .HasForeignKey(vc => vc.UserId);

            modelBuilder.Entity<UserAddress>()
                .HasOne(vc => vc.User)
                .WithMany(u => u.UserAddreses) // Especifica a propriedade de navegação
                .HasForeignKey(vc => vc.UserId);

            modelBuilder.Entity<UserDocument>()
                .HasOne(vc => vc.User)
                .WithMany(u => u.UserDocuments) // Especifica a propriedade de navegação
                .HasForeignKey(vc => vc.UserId);

            modelBuilder.Entity<OperationComment>()
                .HasOne(vc => vc.Operation)
                .WithMany(o => o.Comments)
                .HasForeignKey(vc => vc.OperationId);

            modelBuilder.Entity<OperationComment>()
                .HasOne(vc => vc.User)
                .WithMany(u => u.OperationComments) // Assumes User has a Comments collection
                .HasForeignKey(vc => vc.UserId);

            modelBuilder.Entity<OperationFile>()
                .HasOne(vc => vc.Operation)
                .WithMany(u => u.Files) // Especifica a propriedade de navegação
                .HasForeignKey(vc => vc.OperationId);

            modelBuilder.Entity<InvestmentStatus>()
                .HasOne(vc => vc.Investment)
                .WithMany(u => u.InvestmentStatuses) // Especifica a propriedade de navegação
                .HasForeignKey(vc => vc.InvestmentId);

            modelBuilder.Entity<Investment>()
                .HasOne(vc => vc.Operation)
                .WithMany(u => u.Investments) // Especifica a propriedade de navegação
                .HasForeignKey(vc => vc.OperationId);
        }
    }
}
