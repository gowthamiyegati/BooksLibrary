using BookLibraryAPI.Controllers;
using BookLibraryAPI.Data;
using BookLibraryAPI.Interfaces;
using BookLibraryAPI.Models;
using BookLibraryAPI.Services;
using BookLibraryAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace BookLibraryTests
{
    internal class PublishersControllerTest
    {
        private static readonly DbContextOptions<AppDbContext> _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BookLibraryDbTest")
            .Options;

        private AppDbContext _dbContext;
        private IPublishersService _publishersService;
        private PublishersController _controller;

        [OneTimeSetUp]
        public void Setup()
        {
            _dbContext = new AppDbContext(_dbContextOptions);
            _dbContext.Database.EnsureCreated();

            SeedDatabase();

            _publishersService = new PublishersService(_dbContext);
            _controller = new PublishersController(_publishersService, new NullLogger<PublishersController>());
        }

        [Test, Order(1)]
        public void GetAllPublishersNoSortByNoSearchStringNoPageNumberNoPageSizeReturnOkTest()
        {
            IActionResult actionResult = _controller.GetAll();
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
            List<Publisher>? actionResultData = (actionResult as OkObjectResult)?.Value as List<Publisher>;

            Assert.That(actionResultData, Has.Count.EqualTo(5));
            Assert.That(actionResultData.LastOrDefault()!.Id, Is.EqualTo(5));
        }

        [Test, Order(2)]
        public void GetAllPublishersReturnOkTest()
        {
            IActionResult actionResult = _controller.GetAll("desc", "publisher", 2, 4);
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            List<Publisher>? actionResultData = (actionResult as OkObjectResult)?.Value as List<Publisher>;
            Assert.That(actionResultData, Has.Count.EqualTo(2));
            Assert.That(actionResultData.LastOrDefault()!.Name, Is.EqualTo("Publisher 1"));
        }

        [Test, Order(3)]
        public void GetPublisherByIdOk()
        {
            IActionResult actionResult = _controller.GetPublisherById(1);
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var actionData = (actionResult as OkObjectResult)?.Value as Publisher;
            Assert.Multiple(() =>
            {
                Assert.That(actionData?.Name, Is.EqualTo("Publisher 1"));
                Assert.That(actionData?.Id, Is.EqualTo(1));
            });
        }

        [Test, Order(4)]
        public void GetPublisherByIdNotFound()
        {
            IActionResult actionResult = _controller.GetPublisherById(99);
            Assert.That(actionResult, Is.TypeOf<NotFoundResult>());
        }

        [Test, Order(5)]
        public void AddPublisherCreated()
        {
            var newPublisherVM = new PublisherVM()
            {
                Name = "PublisherCreated"
            };

            IActionResult actionResult = _controller.AddPublisher(newPublisherVM);
            Assert.That(actionResult, Is.TypeOf<CreatedResult>());
        }

        [Test, Order(6)]
        public void AddPublisherBadRequest()
        {
            var newPublisherVM = new PublisherVM()
            {
                Name = "111111Publisher"
            };

            IActionResult actionResult = _controller.AddPublisher(newPublisherVM);
            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test, Order(7)]
        public void DeletePublisherOk()
        {
            var getAll = _controller.GetAll("asc", "null", 1, 99);
            List<Publisher>? pubsList = (getAll as OkObjectResult)?.Value as List<Publisher>;
            Publisher? pub = pubsList?.Find(p => p.Name == "PublisherCreated");
            if(pub != null)
            {
                IActionResult actionResult = _controller.DeletePublisherById(pub.Id);
                Assert.That(actionResult, Is.TypeOf<OkResult>());
            }
        }

        [Test, Order(8)]
        public void DeletePublisherBadRequest()
        {
            IActionResult actionResult = _controller.DeletePublisherById(99);
            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());
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