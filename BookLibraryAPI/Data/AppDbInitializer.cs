using BookLibraryAPI.Models;

namespace BookLibraryAPI.Data
{
    public static class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();

            if (dbContext?.Books.Any() == false)
            {
                dbContext.AddRange(
                    new Book()
                    {
                        Title = "HowToNameABook",
                        Description = "How to name a book in short words",
                        IsRead = true,
                        DateRead = DateTime.Now.AddDays(-10),
                        Rate = 4,
                        Genre = "Manual",
                        Cover = "https://edit.org/images/cat/book-covers-big-2019101610.jpg",
                        DateAdded = DateTime.Now,
                    },
                    new Book()
                    {
                        Title = "ReadABook",
                        Description = "How to read a book in short words",
                        IsRead = true,
                        DateRead = DateTime.Now.AddDays(-20),
                        Rate = 5,
                        Genre = "Biography",
                        Cover = "https://img.freepik.com/free-vector/abstract-elegant-winter-book-cover_23-2148798745.jpg?w=2000",
                        DateAdded = DateTime.Now,
                    },
                    new Book()
                    {
                        Title = "TryMe",
                        Description = "Big words in small box",
                        IsRead = false,
                        Genre = "Drama",
                        Cover = "https://m.media-amazon.com/images/I/41gr3r3FSWL.jpg",
                        DateAdded = DateTime.Now,
                    }
                );

                dbContext.SaveChanges();
            }
        }
    }
}