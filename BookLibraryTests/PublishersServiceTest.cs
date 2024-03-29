using BookLibraryAPI.Data;
using BookLibraryAPI.Exceptions;
using BookLibraryAPI.Interfaces;
using BookLibraryAPI.Models;
using BookLibraryAPI.Services;
using BookLibraryAPI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryTests
{
    public class PublishersServiceTest
    {
        private static readonly DbContextOptions<AppDbContext> _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BookLibraryDbTest")
            .Options;

        private AppDbContext _dbContext;
        private IPublishersService _service;

        [OneTimeSetUp]
        public void Setup()
        {
            _dbContext = new AppDbContext(_dbContextOptions);
            _dbContext.Database.EnsureCreated();

            SeedDatabase();

            _service = new PublishersService(_dbContext);
        }

        [Test, Order(1)]
        public void GetAllPublishersNoSortByNoSearchStringNoPageNumberNoPageSize()
        {
            List<Publisher>? result = _service.GetAll("", "", null, null);

            Assert.That(result, Has.Count.EqualTo(5));
        }

        [Test, Order(2)]
        public void GetAllPublishersNoSortByNoSearchStringNoPageNumber()
        {
            List<Publisher>? result = _service.GetAll("", "", null, 6);

            Assert.That(result, Has.Count.EqualTo(6));
        }

        [Test, Order(3)]
        public void GetAllPublishersNoSortByNoSearchStringNoPageSize()
        {
            List<Publisher> result = _service.GetAll("", "", 2, null);

            Assert.That(result, Has.Count.EqualTo(1));
        }

        [Test, Order(4)]
        public void GetAllPublishersNoSortByNoSearchStringNoPageSizePage3()
        {
            List<Publisher>? result = _service.GetAll("", "", 3, null);

            Assert.That(result, Is.Empty);
        }

        [Test, Order(5)]
        public void GetAllPublishersNoSortByNoPageNumberNoPageSize()
        {
            List<Publisher>? result = _service.GetAll("", "3", null, null);

            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result.FirstOrDefault()!.Name, Is.EqualTo("Publisher 3"));
        }

        [Test, Order(6)]
        public void GetAllPublishersNoSearchStringNoPageNumberNoPageSize()
        {
            var result = _service.GetAll("desc", "", null, null);

            Assert.That(result, Has.Count.EqualTo(5));
            Assert.That(result.FirstOrDefault()!.Name, Is.EqualTo("Publisher 6"));
        }

        [Test, Order(7)]
        public void GetPublisherById()
        {
            for (int i = 1; i <= _service.GetAll("", "", null, 6).Count; i++)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(_service.GetPublisherById(i)!.Id, Is.EqualTo(i));
                    Assert.That(_service.GetPublisherById(i)!.Name, Is.EqualTo($"Publisher {i}"));
                });
            }
        }

        [Test, Order(8)]
        public void GetPublisherByIdNull()
        {
            Assert.That(_service.GetPublisherById(99), Is.Null);
        }

        [Test, Order(98)]
        public void AddPublisherWithException()
        {
            var publisher = new PublisherVM()
            {
                Name = "123 With Exception"
            };

            Assert.That(() => _service.AddPublisher(publisher), Throws.TypeOf<PublisherNameException>().With.Message.EqualTo("Publisher is not allowed to start with number"));
        }

        [Test, Order(99)]
        public void AddPublisherWithExceptionNoException()
        {
            var publisher = new PublisherVM()
            {
                Name = "No Exception"
            };

            var result = _service.AddPublisher(publisher);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Does.StartWith("No"));
        }

        [Test, Order(9)]
        public void GetPublisherDataTest()
        {
            PublisherWithBooksAndAuthorsVM? result = _service.GetPublisherData(1);
            Assert.Multiple(() =>
            {
                Assert.That(result!.Name, Is.EqualTo("Publisher 1"));
                Assert.That(result.BookAuthors, Is.Not.Empty);
                Assert.That(result.BookAuthors.OrderBy(ba => ba.BookName).FirstOrDefault()!.BookName, Is.EqualTo("Book 1 Title"));
            });
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _dbContext.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var publishers = new List<Publisher>
            {
                    new Publisher() {
                        Id = 1,
                        Name = "Publisher 1"
                    },
                    new Publisher() {
                        Id = 2,
                        Name = "Publisher 2"
                    },
                    new Publisher() {
                        Id = 3,
                        Name = "Publisher 3"
                    },
                    new Publisher() {
                        Id = 4,
                        Name = "Publisher 4"
                    },
                    new Publisher() {
                        Id = 5,
                        Name = "Publisher 5"
                    },
                    new Publisher() {
                        Id = 6,
                        Name = "Publisher 6"
                    },
            };
            _dbContext.Publishers.AddRange(publishers);

            var authors = new List<Author>()
            {
                new Author()
                {
                    Id = 1,
                    FullName = "Author 1"
                },
                new Author()
                {
                    Id = 2,
                    FullName = "Author 2"
                }
            };
            _dbContext.Authors.AddRange(authors);

            var books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Book 1 Title",
                    Description = "Book 1 Description",
                    IsRead = false,
                    Genre = "Genre",
                    Cover = "https://...",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                },
                new Book()
                {
                    Id = 2,
                    Title = "Book 2 Title",
                    Description = "Book 2 Description",
                    IsRead = false,
                    Genre = "Genre",
                    Cover = "https://...",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                }
            };
            _dbContext.Books.AddRange(books);

            var books_authors = new List<BookAuthor>()
{
                new BookAuthor()
                {
                    Id = 1,
                    BookId = 1,
                    AuthorId = 1
},
                new BookAuthor()
                {
                    Id = 2,
                    BookId = 1,
                    AuthorId = 2
                },
                new BookAuthor()
                {
                    Id = 3,
                    BookId = 2,
                    AuthorId = 2
                },
            };
            _dbContext.BooksAuthors.AddRange(books_authors);

            _dbContext.SaveChanges();
        }
    }
}