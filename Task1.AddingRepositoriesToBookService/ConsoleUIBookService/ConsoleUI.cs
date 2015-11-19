using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookService;
using ConsoleUIBookService.Repository;
using Microsoft.Win32;

namespace ConsoleUIBookService
{
    class ConsoleUI
    {
        static void Main(string[] args)
        {
            List<Book> startList = new List<Book>
            {
                new Book("Лев Толстой", "Анна Каренина", 562, "Минск-Пресс", 2003),
                new Book("Александр Островский", "Снегурочка", 102, "Питер-Пресс", 2000),
                new Book("Сергей Есенин", "На поле Куликовом", 86, "Киев-Пресс", 2001),
                new Book("Лев Толстой", "Путь жизни", 368, "Минск-Пресс", 2000),
                new Book("Михаил Лермонтов", "Мцыри", 153, "Киев-Пресс", 2001),
                new Book("Николай Некрасов", "Русские женщины", 385, "Киев-Пресс", 2006),
            };

            #region Using Binary Serializer
            string pathBS = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "books.bin");
            RepositoryUsingBinarySerializer bs = new RepositoryUsingBinarySerializer(pathBS);
            BookListService bookServiceFirst = new BookListService(bs);
            bookServiceFirst.AddBook(startList);

            bookServiceFirst.SaveListBook();
            bookServiceFirst.LoadListBook();
            #endregion

            #region Using XML Writer
            string pathXML = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "books.xml");
            RepositoryUsingXmlWriter xws = new RepositoryUsingXmlWriter(pathXML);
            BookListService bookServiceSecond = new BookListService(xws);
            bookServiceSecond.AddBook(startList);

            bookServiceSecond.SaveListBook();
            bookServiceSecond.LoadListBook();
            #endregion

            #region Using Linq2XML
            string pathLinqXML = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "booksLinq.xml");
            RepositoryUsingLINQ2XML xlis = new RepositoryUsingLINQ2XML(pathLinqXML);
            BookListService bookServiceThird = new BookListService(xlis);
            bookServiceSecond.AddBook(startList);

            bookServiceSecond.SaveListBook();
            bookServiceSecond.LoadListBook();
            #endregion
        }
    }
}
