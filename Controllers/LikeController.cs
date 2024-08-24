using BlogAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase


    {
        private readonly Context _context;
        public LikeController(Context context)
        {
            _context = context;
        }
        [HttpGet("GetPostLikes/{id}")]
        public IActionResult GetPostLikes(int id)
        {
            var post = _context.Likes.Find(id);
            if (post == null)
            {
                return NotFound();
            }
            var likes = _context.Likes.Where(l => l.PostId == id).ToList();
            return Ok(likes);
        }
        [HttpGet("GetCommentLikes/{id}")]
        public IActionResult GetCommentLikes(int id)
        {
            var comment = _context.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }
            var likes = comment.Likes.Where(l => l.CommentId == id).ToList();
            return Ok(likes);
        }

        [HttpPost("Comment-likes-dislikes/{commentId}")]
        public IActionResult LikeorDislikeComment(int CommentId,bool islike)
        {
            var comment =  _context.Posts.Find(CommentId);
            if (comment == null)
            {
                return NotFound();
            }
            if(islike)
            {
                comment.Likes++;

            }
            else
                comment.Likes--;
            
            _context.Update(comment);
            _context.SaveChanges();
         
            return Ok(comment);

            }
        [HttpPost("posts-likes-dislikes/{postId}")]
        public IActionResult LikePost(int postId,bool islike)
        {
            var post = _context.Posts.Find(postId);
            if (post == null)
            {
                return NotFound();
            }
            if(islike)
            {
                post.Likes++;
            }
            else 
                post.Likes--;
            _context.Update(post);
            _context.SaveChangesAsync();

            return Ok(post);
        }
       
        
        
    }
}



