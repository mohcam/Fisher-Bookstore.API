using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fisher.Bookstore.Api.Models;

namespace Fisher.Bookstore.Api.Controllers{

    [Route("api/[controller]")]
    public class AuthorController : Controller{
        private readonly BookstoreContext db;
        public AuthorController(BookstoreContext db){
            this.db = db;

            if (this.db.Author.Count() == 0){
                this.db.Author.Add(new Author {
                    Id = 1,
                    Fname = "Jon",
                    Lname = "Snow"
                });

                this.db.Author.Add(new Author {
                    Id = 2,
                    Fname = "Douglas",
                    Lname = "Adams"
                });

                this.db.SaveChanges();
            }
        }

        [HttpGet]
        public IActionResult GetAll(){
            return Ok(db.Author);
        }

        [HttpGet("{id}", Name="GetAuthor")]
        public IActionResult GetById(int id){
            var author = db.Author.Find(id);

            if(author == null){
                return NotFound();
            }

            return Ok(author);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Author author){
            if(author == null){
                return BadRequest();
            }

            this.db.Author.Add(author);
            this.db.SaveChanges();

            return CreatedAtRoute("GetAuthor", new { id = author.Id}, author);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Author newAuthor){
            if(newAuthor == null || newAuthor.Id != id){
                return BadRequest();
            }
            var currentAuthor = this.db.Author.FirstOrDefault(x => x.Id == id);

            if(currentAuthor == null){
                return NotFound();
            }

            currentAuthor.Fname = newAuthor.Fname;
            currentAuthor.Lname = newAuthor.Lname;

            this.db.Author.Update(currentAuthor);
            this.db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            var author = this.db.Author.FirstOrDefault(x => x.Id == id);

            if (author == null){
                return NotFound();
            }

            this.db.Author.Remove(author);
            this.db.SaveChanges();

            return NoContent();
        }


    }

}