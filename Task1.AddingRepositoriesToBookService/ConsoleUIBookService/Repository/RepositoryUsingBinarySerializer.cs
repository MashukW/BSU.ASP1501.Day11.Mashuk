using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BookService;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleUIBookService.Repository
{
    class RepositoryUsingBinarySerializer : IRepositoryBookListService
    {
        private readonly string path;
        
        private BookListService bookList;

        public RepositoryUsingBinarySerializer(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Path is invalid");

            this.path = path;
        }

        public List<Book> ReadFromFileToListBooks()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = File.OpenRead(path))
            {
                bookList = (BookListService) formatter.Deserialize(stream);
            }

            return bookList.ListBooks;
        }

        public void WriteListBooksInFile(BookListService listBookForWrite)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream stream = File.Create(path))
            {
                formatter.Serialize(stream, listBookForWrite);
            }
        }
    }
}
