using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
            string author = "", title = "", publisher = "";
            int yearIssued = 0, numberOfPages;
            List<Book> result = new List<Book>();

            XElement xmlBooks = XElement.Load(path);
            var book = from b in xmlBooks.Elements()
                where b.Name == "book"
                select b;
            
            foreach (var item in book.Elements())
            {
                switch (item.Name.ToString())
                {
                    case "Author":
                        author = item.Value;
                        break;
                    case "Title":
                        title = item.Value;
                        break;
                    case "Publisher":
                        publisher = item.Value;
                        break;
                    case "YearIssued":
                        yearIssued = int.Parse(item.Value.ToString());
                        break;
                    case "NumberOfPages":
                        numberOfPages = int.Parse(item.Value.ToString());
                        result.Add(new Book(author, title, numberOfPages, publisher, yearIssued));
                        break;
                }
            }

            return result;
        }

        public void WriteListBooksInFile(BookListService listBookForWrite)
        {
            XElement books = new XElement("books");
            foreach (var writeBook in listBookForWrite.ListBooks)
            {
                books.Add(new XElement("book", 
                    new XElement("Author", writeBook.Author),
                    new XElement("Title", writeBook.Title),
                    new XElement("Publisher", writeBook.Publisher),
                    new XElement("YearIssued", writeBook.YearIssued),
                    new XElement("NumberOfPages", writeBook.NumberOfPages)
                    ));
            }

            books.Save(path);
        }
    }
}
