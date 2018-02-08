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

        [HttpGet]

        public IActionResult GetAll()
        {
            return Ok(db.Books);
        }

    }
    
    }

