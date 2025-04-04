using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Repositories.Contrats;
using Services.Contrats;

namespace Services.Concrete
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> _bookService;


        public ServiceManager(IRepositoryManager repositoryManager,ILoggerService logger, IMapper mapper)
        {
            _bookService = new Lazy<IBookService> (() => new BookManager(repositoryManager,logger, mapper));
        }

        public IBookService BookService =>_bookService.Value;
    }
}
