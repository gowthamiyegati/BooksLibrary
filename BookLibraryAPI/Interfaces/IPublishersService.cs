using BookLibraryAPI.Models;
using BookLibraryAPI.ViewModels;

namespace BookLibraryAPI.Interfaces
{
    public interface IPublishersService
    {
        Publisher AddPublisher(PublisherVM publisherVM);

        PublisherWithBooksAndAuthorsVM? GetPublisherData(int publisherId);

        Publisher? GetPublisherById(int id);

        void DeletePublisherById(int id);

        List<Publisher> GetAll(string sortBy, string searchString, int? pageNumber, int? pageSize);
    }
}