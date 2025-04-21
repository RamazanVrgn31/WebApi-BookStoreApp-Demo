using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories.Contrats;
using Services.Contrats;

namespace Services.Concrete
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> _bookService;
        private readonly Lazy<IAuthenticationService> _authenticationService;


        public ServiceManager(IRepositoryManager repositoryManager,ILoggerService logger, IMapper mapper,UserManager<User> userManager,IConfiguration configuration,IBookLinks bookLinks)
        {
            _bookService = new Lazy<IBookService> (() => new BookManager(repositoryManager, logger, mapper, bookLinks));
            _authenticationService = new Lazy<IAuthenticationService> (() => new AuthenticationManager(logger, mapper, userManager,configuration));
        }

        public IBookService BookService =>_bookService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
