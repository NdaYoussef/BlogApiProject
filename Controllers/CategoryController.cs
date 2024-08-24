using BlogAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly Context _context;
        public CategoryController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetCategory(int  id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if(id==null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public IActionResult AddCategory([FromBody] Category category)
        {
            if (!User.IsInRole("Admin"))
            {
                return Forbid();
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok(category);
        }

        [HttpPut("{id}")]
        public IActionResult EditCategory([FromRoute]int id, [FromBody] Category category)
        {
            Category category1 = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (ModelState.IsValid)
            {
                if (category1 == null)
                {
                    return NotFound();
                }
                category.Name = category1.Name;
                return Ok (category1);
            }
            return StatusCode(StatusCodes.Status204NoContent);

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
           var category = _context.Comments.FirstOrDefault(x => x.Id == id);

            if (category == null)
            {
                return NotFound();
            }
            _context.Comments.Remove(category);
            _context.SaveChanges();
            return BadRequest();
        }



    }
}
