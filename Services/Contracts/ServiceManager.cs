using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> bookService;
        public ServiceManager(IRepositoryManager repositoryManager)
        {
            bookService = new Lazy<IBookService>(() => new BookMaanger(repositoryManager));

        }
        public IBookService BookService => bookService.Value;
    }
}
