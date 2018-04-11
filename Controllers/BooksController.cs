using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fisher.Bookstore.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fisher.Bookstore.Api.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller 
    {
        private readonly BookstoreContext db;
        public BooksController(BookstoreContext db){
            this.db = db;
            if(this.db.Books.Count()==0){
                this.db.Books.Add( new Book{
                    Id = 1,
                    Title = "The Lean Startup"
                });

                 this.db.Books.Add( new Book{
                    Id = 2,
                    Title = "The Lean Startup"
                });

                this.db.SaveChanges();
            }
            
        }

// this is the return method for book call
        [HttpGet]

        public IActionResult GetAll()
        {
            return Ok(db.Books);
        }

//Another Get method to get book by its id
    [HttpGet("{id}",Name="GetBook")]
   
    public IActionResult GetByID(int id)
    {
        var book = db.Books.Find(id);

        if(book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    //This is a  a Post method  to post  a book
    [HttpPost]
    public IActionResult Post([FromBody]Book book)
    {
        //if book returns null, there is a bad request wrning
        if(book == null)
        {
            return BadRequest();

        }
        //if there is  no bad request, then you can add a new book
        this.db.Books.Add(book);
        this.db.SaveChanges();

        return CreatedAtRoute("GetBook", new{IDesignTimeMvcBuilderConfiguration= book.Id}, book);
    } 

    [HttpPost("id")]

    // this method will insert a book into the database
    public IActionResult Put (int id, [FromBody]Book newBook)
    {
        if(newBook == null || newBook.Id != id)
        {
            return BadRequest();
        }
        var currentBook = this.db.Books.FirstOrDefault(x => x.Id == id);

        if (currentBook == null)
        {
            return NotFound();
        }

        //set current book to new book
        currentBook.Author = newBook.Author;
        currentBook.PublishDate = newBook.PublishDate;
        currentBook.Publisher= newBook.Publisher;

        //update and save
        this.db.Books.Update(currentBook);
        this.db.SaveChanges();

        return NoContent();
    }

        // this is a delete method to delete a book out of the the db

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            var book = this.db.Books.FirstOrDefault(x => x.Id==id);
            if(book == null)
            {
                return NotFound();
            }
            this.db.Books.Remove(book);
            this.db.SaveChanges();

            return NoContent();

        }



    }
    
    }

