using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contrats
{
    public interface IRepositoryManager 
    {
        public IBookRepository Book { get; }

        void Save();

    }
}
