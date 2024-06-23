using Microsoft.EntityFrameworkCore;

namespace ExchangeBook.Data
{
    public class BookExchangeDbContext : DbContext
    {
        public BookExchangeDbContext()
        {
            
        }
        public BookExchangeDbContext(DbContextOptions<BookExchangeDbContext> options)
          : base(options)
        {
        }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<StoreBook> StoreBooks { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Author> Notifications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(b => b.Title, "IX_TITLE");
                entity.Property(b => b.Title).HasMaxLength(50);
                entity.HasOne(b => b.Author)
                      .WithMany(a => a.Books)
                      .HasForeignKey(b => b.AuthorId)
                      .HasConstraintName("FK_BOOK_AUTHOR");
                entity.HasMany(b => b.Persons).WithMany(p => p.Books);
                entity.HasMany(b => b.StoreBooks)
                  .WithOne(sb => sb.Book)
                  .HasForeignKey(sb => sb.BookId);
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(a => a.Name).HasMaxLength(60);
                //entity.HasMany(a => a.Books)
                //      .WithOne(b => b.Author)
                //      .HasForeignKey(b => b.AuthorId);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasOne(n => n.User)
                      .WithMany(u => u.Notifications)
                      .HasForeignKey(n => n.InterestedUserId);
                entity.HasOne(n => n.HolderUser)
                      .WithMany()
                      .HasForeignKey(n => n.UserId);
                entity.HasOne(n => n.Book)
                      .WithMany(b => b.Notifications)
                      .HasForeignKey(n => n.BookId);
                entity.Property(n => n.Type)
                      .HasConversion<string>()
                      .IsRequired();
                entity.Property(n => n.IsSeen)
                      .IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");
                entity.HasIndex(e => e.Username, "UQ_USERNAME").IsUnique();
                entity.HasIndex(e => e.Email, "UQ_EMAIL").IsUnique();
                entity.Property(e => e.Username)
                    .HasMaxLength(50);
                entity.Property(e => e.Password)
                    .HasMaxLength(512);
                entity.Property(e => e.Email)
                    .HasMaxLength(50);
                entity.Property(e => e.UserRole)
                    .HasConversion<string>()
                    .HasMaxLength(50)
                    .IsRequired();

            });
            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Firstname)
                       .HasMaxLength(50);
                entity.Property(e => e.Lastname)
                       .HasMaxLength(50);
                entity.Property(p => p.PhoneNumber).HasMaxLength(15);
                entity.HasOne(p => p.User)
                  .WithOne(u => u.Person)
                  .HasForeignKey<Person>(p => p.UserId)
                  .HasConstraintName("FK_PERSON_USER");
                entity.HasMany(p => p.Books)
                    .WithMany(b => b.Persons)
                .UsingEntity(
                    "Person_Book");

            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasOne(s => s.User)
                      .WithOne(u => u.Store)
                      .HasForeignKey<Store>(p => p.UserId)
                      .HasConstraintName("FK_STORE_USERS");
                entity.Property(s => s.Name)
                .HasMaxLength(100);
                entity.HasMany(s => s.StoreBooks)
                      .WithOne(sb => sb.Store);
                      
            });
            modelBuilder.Entity<StoreBook>(entity =>
            {
                entity.HasKey(sb => new { sb.StoreId, sb.BookId });
                entity.HasOne(sb => sb.Store)
                      .WithMany(s => s.StoreBooks)
                      .HasForeignKey(sb => sb.StoreId);
                entity.HasOne(sb => sb.Book)
                .WithMany(b => b.StoreBooks)
                .HasForeignKey(sb => sb.BookId);
            });
        }
    }
}
