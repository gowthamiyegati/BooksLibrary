using BookLibraryAPI.Data;
using BookLibraryAPI.Exceptions;
using BookLibraryAPI.Interfaces;
using BookLibraryAPI.Models;
using BookLibraryAPI.ViewModels;
using System.Text.RegularExpressions;

namespace BookLibraryAPI.Services
{
    public class PublishersService : IPublishersService
    {
        private readonly AppDbContext _dbContext;

        public PublishersService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Publisher> GetAll(string sortBy, string searchString, int? pageNumber, int? pageSize)
        {
            var publishers = _dbContext.Publishers.ToList();
            if (!string.IsNullOrEmpty(searchString) && searchString != "null")
            {
                publishers = publishers.Where(p => p.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                publishers = sortBy switch
                {
                    "desc" => publishers.OrderByDescending(p => p.Name).ToList(),
                    _ => publishers.OrderBy(p => p.Name).ToList(),
                };
            }

            return PaginetedList<Publisher>.Create(publishers.AsQueryable(), pageNumber ?? 1, pageSize ?? 5);
        }

        public Publisher AddPublisher(PublisherVM publisherVM)
        {
            if (StringStartsWithNumber(publisherVM.Name))
            {
                throw new PublisherNameException("Publisher is not allowed to start with number", publisherVM.Name);
            }

            var publisher = new Publisher()
            {
                Name = publisherVM.Name
            };

            _dbContext.Add(publisher);
            _dbContext.SaveChanges();

            return publisher;
        }

        public Publisher? GetPublisherById(int id) => _dbContext.Publishers.FirstOrDefault(p => p.Id == id);

        public void DeletePublisherById(int id)
        {
            var publisher = _dbContext.Publishers.FirstOrDefault(p => p.Id == id);

            if (publisher != null)
            {
                _dbContext.Publishers.Remove(publisher);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception($"The publisher with id: {id} does not exist");
            }
        }

        public PublisherWithBooksAndAuthorsVM? GetPublisherData(int publisherId)
        {
            var data = _dbContext.Publishers.Where(p => p.Id == publisherId).Select(p => new PublisherWithBooksAndAuthorsVM()
            {
                Name = p.Name,
                BookAuthors = p.Books.Select(p => new BookAuthorVM()
                {
                    BookName = p.Title,
                    BookAuthors = p.BookAuthors.Select(p => p.Author.FullName).ToList()
                }).ToList()
            }).FirstOrDefault();

            return data;
        }

        private static bool StringStartsWithNumber(string name) => Regex.IsMatch(name, @"^\d");
    }
}