using BlogAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly Context _context;
        public PostController(Context context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetPost()
        {
            List<Post> posts = new List<Post>();
            return Ok(posts);
        }

        [HttpGet("id")]
        public IActionResult GetPostById(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Post post = _context.Posts.FirstOrDefault(x => x.Id == id);
            return Ok(post);
            _context.SaveChanges();

        }

        [HttpPost]
        public IActionResult Post(Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Posts.Add(post);
                _context.SaveChanges();
                return Ok(new { message = "new Post has been added successfully" });
            }
            return BadRequest();

        }
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id,Post post)
        {
            Post post1 = _context.Posts.FirstOrDefault(x => x.Id == id);
            if (ModelState.IsValid)
            {
                if (post == null)
                {
                    return NotFound();
                }
                post.Title = post1.Title;
                post.User = post1.User;
                return Ok(post1);
            }
            return StatusCode(StatusCodes.Status204NoContent);

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            Post post = _context.Posts.FirstOrDefault(x => x.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            _context.Posts.Remove(post);
            _context.SaveChanges();
            return BadRequest();
        }
        [HttpGet("search-post/{id}")]
        public IActionResult SearchPost(int id)
        {
            var post = _context.Posts
                          .Where(x => x.Id == id)
                          .Select(p => new PostDto
                          {
                              Id = p.Id,
                              Content = p.Content,
                          }
                          )
                          .FirstOrDefault();
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }
        }

    }

