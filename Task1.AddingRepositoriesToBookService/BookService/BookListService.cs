using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BookService;

namespace BookService
{
    [Serializable]
    public class BookListService : ISerializable
    {
        private List<Book> listBook;
        public List<Book> ListBooks { get { return listBook; }}

        private IRepositoryBookListService repository;
        
        public BookListService(IRepositoryBookListService repository)
        {
            if (repository == null)
                throw new ArgumentNullException("Class SaveAndLoad File is null");

            this.repository = repository;
            listBook = new List<Book>();
        }

        #region Public Methods (AddBook, RemoveBook, FindByTag, SortsBooksByTag)
        public void AddBook(Book book)
        {
            if (book == null)
                return;
            if (listBook.Contains(book))
                throw new AddBookException("This book already exists in the list");

            listBook.Add(book);
        }

        public void AddBook(IEnumerable<Book> books)
        {
            if (books == null)
                throw new ArgumentException("Books collection is null");

            foreach (Book book in books)
                AddBook(book);
        }

        public void RemoveBook(Book book)
        {
            if (book == null)
                return;
            if (!listBook.Contains(book))
                throw new RemoveBookException("Book to be deleted was not found");

            listBook.Remove(book);
        }

        public List<Book> FindByTag(Predicate<Book> tag)
        {
            return listBook.FindAll(tag);
        }

        public void SortsBooksByTag()
        {
            if (listBook.Count == 0)
                return;

            listBook.Sort();
        }

        public List<Book> SortsBooksByTag(Func<Book, object> keySelector)
        {
            return listBook.OrderBy(keySelector).ToList();
        }

        public void SaveListBook()
        {
            try
            {
                repository.WriteListBooksInFile(this);
            }
            catch (Exception ex)
            {
                throw new BookListServiceException("Unable to download data from file", ex);
            }
        }

        public void LoadListBook()
        {
            try
            {
                listBook = repository.ReadFromFileToListBooks();
            }
            catch (Exception ex)
            {
                throw new BookListServiceException("Unable to download data from file", ex);
            }
        }
        #endregion

        #region These methods are used to Serialize and Deserialize
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("listBook", listBook);
        }

        protected BookListService(SerializationInfo info, StreamingContext context)
        {
            listBook = (List<Book>) info.GetValue("listBook", typeof (List<Book>));
        }
        #endregion
    }
}
