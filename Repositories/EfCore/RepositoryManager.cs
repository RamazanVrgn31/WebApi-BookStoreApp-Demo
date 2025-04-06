using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Contrats;

namespace Repositories.EfCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;

        //Lazy Loading
        private readonly Lazy<IBookRepository> _bookRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _bookRepository = new Lazy<IBookRepository>(()=> new BookRepository(_context));
        }

        public IBookRepository Book => _bookRepository.Value;


        public async Task SaveAsync()
        {
             await _context.SaveChangesAsync();
        }
    }
}
