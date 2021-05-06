using Library.Models.BookModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services.BookService
{
    public interface IBookService
    {
        public List<BookModel> GetAllBooks();
        public BookModel GetBookDetailsByID(int id);
        public Boolean AddBookData(BookModel book);
    }
}
