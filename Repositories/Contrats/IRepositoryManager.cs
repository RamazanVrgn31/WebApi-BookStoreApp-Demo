using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contrats
{
    public interface IRepositoryManager 
    {
        public ICategoryRepository Category { get; }
        public IBookRepository Book { get; }

        Task SaveAsync();

    }
}
