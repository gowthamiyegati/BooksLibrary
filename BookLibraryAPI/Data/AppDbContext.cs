using BookLibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<BookAuthor> BooksAuthors => Set<BookAuthor>();
        public DbSet<Publisher> Publishers => Set<Publisher>();
        public DbSet<Log> Logs => Set<Log>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>().HasOne(ba => ba.Book).WithMany(ba => ba.BookAuthors).HasForeignKey(ba => ba.BookId);
            modelBuilder.Entity<BookAuthor>().HasOne(ba => ba.Author).WithMany(ba => ba.BookAuthors).HasForeignKey(ba => ba.AuthorId);

            modelBuilder.Entity<Log>().HasKey(l => l.Id);
        }
    }
}