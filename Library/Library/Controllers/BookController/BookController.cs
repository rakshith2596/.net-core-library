using Library.Models.BookModel;
using Library.Services.BookService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Library.Controllers.BookController
{
    public class BookController : Controller
    {
        private IBookService _bookService = new BookService();
        private ILogger _logger;
        public BookController(ILogger<BookController> logger)
        {
            _logger = logger;
        }
        // GET: BookController
        public ActionResult Index()
        {
            List<BookModel> bookList = _bookService.GetAllBooks();
            if (bookList is null)
            {
                _logger.LogError("Could not fetch the book details");
                return View();
            }
            else
            {
                _logger.LogInformation("Details of Book fetched successfully");
                return View(bookList);
            }
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            BookModel book = _bookService.GetBookDetailsByID(id);
            if (book is null)
            {
                _logger.LogError("Could not fetch the book details");
                return View();
            }
            else
            {
                _logger.LogInformation("Details of Book fetched successfully");
                return View(book);
            }
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            return View(new BookModel());
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookModel bookModel)
        {
            Boolean isSaved = _bookService.AddBookData(bookModel);
            if (isSaved)
            {
                _logger.LogInformation("Record Saved successfully");
                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogError("Error in saving the details");
                return View();
            }
        }
    }
}
