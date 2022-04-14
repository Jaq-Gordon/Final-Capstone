using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookDAO bookDAO;

        public BookController(IBookDAO bookDAO)
        {
            this.bookDAO = bookDAO;
        }

        [HttpGet]
        public ActionResult<List<Book>> GetAllBooks()
        {
            List<Book> books = bookDAO.GetAllBooks();

            return books;
        }

        [HttpGet("isbn")]
        public ActionResult<Book> GetSpecificBook(long isbn)
        {
            Book specificBook = bookDAO.GetSpecificBook(isbn);

            if (specificBook == null)
            {
                return NotFound();
            }
            else
            {
                return specificBook;
            }
        }


        [HttpPost("{familyID}/AddBook/")]
        public ActionResult<List<Book>> AddBookToFamilyLibrary(int familyID, Book book)
        {
            int bookID = bookDAO.CheckIfBookExistsOnDB(book);
            List<Book> toReturn = new List<Book>();
            bool completed = false;

            if (bookID == 0)
            {
                book.BookId = bookDAO.AddNewBook(book);
                completed = bookDAO.AddToFamilyLibrary(book.BookId, familyID);
                toReturn = bookDAO.GetAllFamilyBooks(familyID);
            }
            else
            {
                completed = bookDAO.AddToFamilyLibrary(bookID, familyID);
                toReturn = bookDAO.GetAllFamilyBooks(familyID);

            }

            if (completed)
            {
                return Ok(toReturn);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}/GetFamilyBooks")]
        public ActionResult<List<Book>> GetFamilyBooks(int id)
        {

            List<Book> famBooks = bookDAO.GetAllFamilyBooks(id);

            if (famBooks == null)
            {
                return BadRequest();
            }
            else
            {
                return famBooks;
            }


        }
        [HttpGet("{userId}/GetPersonalBooks")]
        public ActionResult<List<PersonalBook>> GetPersonalBooks(int userId)
        {
            int user_id = int.Parse(this.User.FindFirst("sub").Value);
            if (userId == user_id)
            {

                List<PersonalBook> BookItems = bookDAO.GetPersonalBooks(userId);

                return Ok(BookItems);
            }
            return Forbid();
        }

        [HttpPost("{userId}/AddPersonalBook/{bookId}")]
        public ActionResult<List<PersonalBook>> AddPersonalBook(int userId, int bookId)
        {
            if(bookId == 0)
            {
                return BadRequest();
            }
            int user_id = int.Parse(this.User.FindFirst("sub").Value);
            if (userId == user_id)
            {

                bool isCompleted = bookDAO.AddPersonalBook(userId, bookId);
                if (isCompleted)
                {
                    List<PersonalBook> BookItems = bookDAO.GetPersonalBooks(userId);
                    return Ok(BookItems);

                }

            }

            return BadRequest();
        }
    }
}
