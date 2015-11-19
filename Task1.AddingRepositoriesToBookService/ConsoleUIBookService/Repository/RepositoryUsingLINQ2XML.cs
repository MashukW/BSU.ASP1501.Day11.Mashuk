using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookService;

namespace ConsoleUIBookService.Repository
{
    class RepositoryUsingLINQ2XML : IRepositoryBookListService
    {
        private readonly string path;

        public RepositoryUsingLINQ2XML(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException("Path is invalid");
            this.path = path;
        }

        public List<Book> ReadFromFileToListBooks()
        {
            throw new NotImplementedException();
        }

        public void WriteListBooksInFile(BookListService listBookForWrite)
        {
            throw new NotImplementedException();
        }
    }
}
