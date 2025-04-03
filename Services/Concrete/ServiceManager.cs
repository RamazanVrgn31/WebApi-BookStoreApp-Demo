using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Contrats;
using Services.Contrats;

namespace Services.Concrete
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> _bookService;


        public ServiceManager(IRepositoryManager repositoryManager,ILoggerService logger)
        {
            _bookService = new Lazy<IBookService> (() => new BookManager(repositoryManager,logger));
        }

        public IBookService BookService =>_bookService.Value;
    }
}
