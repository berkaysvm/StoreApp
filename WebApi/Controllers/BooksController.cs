using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Repositories.EFCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepositoryManager manager;

        public BooksController(IRepositoryManager manager)
        {
            this.manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = manager.Book.GetAllBooks(false);
                return Ok(books);
            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);
            }

        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var book = manager
                  .Book
                  .GetOneBook(id, false);
                if (book is null)
                    return NotFound();

                return Ok(book);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                    return BadRequest();
                manager.Book.Create(book);
                manager.Save();
                return StatusCode(201, book);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            try
            {
                var entity = manager.Book.GetOneBook(id, true);
                if (entity is null)
                    return NotFound();
                if (id != book.Id)
                    return BadRequest();
                entity.Title = book.Title;
                entity.Price = book.Price;
                manager.Save();
                return Ok(book);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var entity = manager
                    .Book.GetOneBook(id, false);

                if (entity is null)
                    return NotFound(
                        new { StatusCode = 404,
                            message = $"Book with id:{id} could not found." });

                manager.Book.DeleteOneBook(entity);
                manager.Save();
                return NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            try
            {
                var entity = manager.Book.GetOneBook(id, true);
                if(entity is null)
                    return NotFound();
                bookPatch.ApplyTo(entity);
                manager.Book.Update(entity);
                return NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }

        }
    }
  
}
