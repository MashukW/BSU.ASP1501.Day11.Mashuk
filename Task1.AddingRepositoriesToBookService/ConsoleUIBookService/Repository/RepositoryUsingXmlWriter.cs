using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookService;
using System.Xml;

namespace ConsoleUIBookService.Repository
{
    class RepositoryUsingXmlWriter : IRepositoryBookListService
    {
        private readonly string path;

        public RepositoryUsingXmlWriter(string path)
        {
            if(string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Path is invalid");

            this.path = path;
        }

        public List<Book> ReadFromFileToListBooks()
        {
            XmlReaderSettings settings = new XmlReaderSettings
            {
                IgnoreComments = true,
                IgnoreProcessingInstructions = true,
                IgnoreWhitespace = true,
            };

            List<Book> result = new List<Book>();

            string author = "", title = "", publisher = "";
            int yearIssued, numberOfPages;

            using (XmlReader reader = XmlReader.Create(path, settings))
            {
                while (reader.Read())
                {
                    if (reader.Name == "Author")
                    {
                        author = reader.ReadElementContentAsString("Author", "");
                        title = reader.ReadElementContentAsString("Title", "");
                        publisher = reader.ReadElementContentAsString("Publisher", "");
                        yearIssued = reader.ReadElementContentAsInt("YearIssued", "");
                        numberOfPages = reader.ReadElementContentAsInt("NumberOfPages", "");

                        result.Add(new Book(author, title, numberOfPages, publisher, yearIssued));
                    }
                }
            }

            return result;
        }

        public void WriteListBooksInFile(BookListService listBookForWrite)
        {
            XmlWriterSettings settings = new XmlWriterSettings {Indent = true};
            using (XmlWriter writer = XmlWriter.Create(path, settings))
            {
                writer.WriteStartElement("books");
                foreach (var book in listBookForWrite.ListBooks)
                {
                    writer.WriteStartElement("book");
                    writer.WriteElementString("Author", book.Author);
                    writer.WriteElementString("Title", book.Title);
                    writer.WriteElementString("Publisher", book.Publisher);
                    writer.WriteElementString("YearIssued", book.YearIssued.ToString());
                    writer.WriteElementString("NumberOfPages", book.NumberOfPages.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
        }
    }
}
