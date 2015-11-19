using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookService
{
    public class RemoveBookException : BookListServiceException
    {
        public RemoveBookException(string message) : base(message) { }
    }
}
