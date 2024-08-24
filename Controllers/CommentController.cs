using BlogAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly Context _context;
        public CommentController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Comment([FromBody] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Comments.Add(comment);
                _context.SaveChanges();
                return Ok(new { message = "new Comment has been added successfully" });
            }
            return BadRequest();

        }
        [HttpPut("{id}")]
        public IActionResult EditComment(int id, [FromBody] Comment comment)
        {
            Comment comment1 = _context.Comments.FirstOrDefault(x => x.Id == id);
            if (ModelState.IsValid)
            {
                if (comment == null)
                {
                    return NotFound();
                }
                comment.Content = comment1.Content;
            }
            return StatusCode(StatusCodes.Status204NoContent);

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            Comment comment = _context.Comments.FirstOrDefault(x => x.Id == id);

            if (comment == null)
            {
                return NotFound();
            }
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return BadRequest();
        }
    }
}
