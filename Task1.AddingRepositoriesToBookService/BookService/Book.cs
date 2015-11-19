using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BookService
{
    [Serializable]
    public class Book : IEquatable<Book>, IComparable<Book>, ISerializable
    {
        #region Property
        public string Author { get; private set; }
        public string Title { get; private set; }
        public string Publisher { get; private set; }
        public int YearIssued { get; private set; }
        public int NumberOfPages { get; private set; }
        #endregion

        public Book(string author, string title, int numberOfPages, string publisher, int yearIssued)
        {
            if (String.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Author is not specified");
            if (String.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is not specified");
            if (numberOfPages <= 0)
                throw new ArgumentException("Number of pages cannot be equal to zero");

            Author = author;
            Title = title;
            Publisher = publisher;
            YearIssued = yearIssued;
            NumberOfPages = numberOfPages;
        }

        public int CompareTo(Book other)
        {
            if (this.Equals(other))
                return 0;
            return this.Author.CompareTo(other.Author);
        }

        public bool Equals(Book other)
        {
            if (other == null)
                return false;
            if (this.Author != other.Author)
                return false;
            if (this.Title != other.Title)
                return false;
            if (this.Publisher != other.Publisher)
                return false;
            if (this.YearIssued != other.YearIssued)
                return false;
            if (this.NumberOfPages != other.NumberOfPages)
                return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, this))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return GetType() == obj.GetType() && Equals((Book)obj);
        }

        public override string ToString()
        {
            StringBuilder resultStr = new StringBuilder();
            resultStr.Append("Author: " + Author + " ");
            resultStr.Append("Title: " + Title + " ");
            resultStr.Append("Publisher: " + Publisher + " ");
            resultStr.Append("YearIssued: " + YearIssued + " ");
            resultStr.Append("NumberOfPages: " + NumberOfPages);

            return resultStr.ToString();
        }

        #region These methods are used to Serialize and Deserialize
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Author", Author);
            info.AddValue("Title", Title);
            info.AddValue("Publisher", Publisher);
            info.AddValue("YearIssued", YearIssued);
            info.AddValue("NumberOfPages", NumberOfPages);
        }

        protected Book(SerializationInfo info, StreamingContext context)
        {
            Author = info.GetString("Author");
            Title = info.GetString("Title");
            Publisher = info.GetString("Publisher");
            YearIssued = info.GetInt32("YearIssued");
            NumberOfPages = info.GetInt32("NumberOfPages");
        }
        #endregion
    }
}
