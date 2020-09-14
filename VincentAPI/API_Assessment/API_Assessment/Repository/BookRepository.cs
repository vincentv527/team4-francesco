using API_Assessment.Controllers;
using API_Assessment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_assessment_with_repo_.Repository
{
    public class BookRepository
    {
        private readonly BookContext _context;

        public BookRepository(BookContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
        //Main page that returns a list
        //you can classify books by setting a minprice, by author, and by title
        public DbSet<Book> GetBooks()
        {
            return _context.Books;
        }

        public async Task<Book> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            return book;
        }
        public async Task<bool> PutBook(int id, [FromBody] Book book)
        {
            bool isCompleted = false;
            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                isCompleted = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Books.Any(e => e.Id == id))
                {
                    return isCompleted;
                }
                else
                {
                    throw;
                }
            }

            return isCompleted;
        }

        public async Task<Book> PostBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book;
        }

        //deletes books by id
        public async Task<Book> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return null;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }
        public async Task<bool> DeleteBooks()
        {
            bool isCompleted = false;
            try
            {
                foreach (Book book in _context.Books)
                {
                    var bk = await _context.Books.FindAsync(book.Id);
                    _context.Books.Remove(bk);
                    await _context.SaveChangesAsync();

                }
                isCompleted = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return isCompleted;
            }
            return isCompleted;
        }
    }
}
