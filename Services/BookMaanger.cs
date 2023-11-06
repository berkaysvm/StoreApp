using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookMaanger : IBookService
    {
        private readonly IRepositoryManager manager;

        public BookMaanger(IRepositoryManager manager)
        {
            this.manager = manager;
        }

        public Book CreateOneBook(Book book)
        {
            if (book is null)
                throw new ArgumentNullException(nameof(book));

            manager.Book.CreateOneBook(book);
            manager.Save();
            return book;
        }

        public void DeleteOneBook(int id, bool trackChanges)
        {
            var entity = manager.Book.GetOneBook(id, trackChanges);

            if (entity is null)
                throw new Exception($"Book with id: {id} could not found.");

            manager.Book.DeleteOneBook(entity);
            manager.Save();
        }

        public IEnumerable<Book> GetAllBooks(bool trackChanges)
        {
           return manager.Book.GetAllBooks(trackChanges);
        }

        public Book GetOneBookById(int id, bool trackChanges)
        {
            return manager.Book.GetOneBook(id,trackChanges);    
        }

        public void UpdateOneBook(int id, Book book, bool trackChanges)
        {
            var entity = manager.Book.GetOneBook(id, trackChanges); 
            if (entity is null)
                throw new Exception($"Book with id: {id} could not found.");


        }
    }
}
