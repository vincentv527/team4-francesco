using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Assessment.Models;
using Api_assessment_with_repo_.Repository;

namespace API_Assessment.Controllers
{
    [Route("books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookRepository _context;

        public BooksController(BookRepository context)
        {
            _context = context;
        }

        //Main page that returns a list
        //you can classify books by setting a minprice, by author, and by title
        [HttpGet]
        public async Task<ActionResult> GetBooks([FromQuery] BookQueryParameters queryParameters)
        {
            IQueryable<Book> products = _context.GetBooks();
            if (queryParameters.MinPrice != null)
            {
                products = products.Where(
                    p => p.Price >= queryParameters.MinPrice.Value);
            }
            if (!string.IsNullOrEmpty(queryParameters.Author))
            {
                products = products.Where(
                    p => p.Author.ToLower().Contains(queryParameters.Author.ToLower()));
            }
            if (!string.IsNullOrEmpty(queryParameters.Title))
            {
                products = products.Where(
                    p => p.Title.ToLower().Contains(queryParameters.Title.ToLower()));
            }

            return Ok(await products.ToListAsync());
        }

        //helper method for postbook to send back the book that was added
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.GetBook(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        //used to update books
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook([FromRoute] int id, [FromBody] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            bool isCompleted = await _context.PutBook(id, book);
            if (!isCompleted)
            {
                return NotFound();
            }


            return NoContent();
        }

        //adds book to inmemory cache
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            Book retBook = await _context.PostBook(book);

            return CreatedAtAction("GetBook", new { id = retBook.Id }, retBook);
        }

        //deletes books by id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await _context.DeleteBook(id);
            if (book == null)
            {
                return NotFound();
            }

            return book;
        }
        //Deletes books by just not entering an id
        [HttpDelete]
        public async Task<ActionResult<Book>> DeleteBooks()
        {
            bool isCompleted = await _context.DeleteBooks();
            if (!isCompleted)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
