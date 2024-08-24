using BlogAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly Context _context;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        public AccountController(Context context)
        {
            _context = context;
        }
        public AccountController
           (
           UserManager<User> userManager,
           SignInManager<User> signInManager,
           IEmailSender emailSender
           )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Ok("Login successful");
            }
            else
            {
                return Unauthorized();
            }
        }
            
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] DtoNewUser user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user1 = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            if (user1 != null)
            {
                return BadRequest("Email already exists!!");
            }

            var user2 = new User
            {
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password)
            };

            _context.Users.Add(user2);
            _context.SaveChanges();

            return Ok(new { Message = "User registered successfully" });
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest User)
        {
            var user = await _userManager.FindByEmailAsync(User.Email);
            if (user == null)
            {
                return BadRequest();
            }

            var result = await _userManager.ResetPasswordAsync(user, User.ResetCode, User.NewPassword);
            if (result.Succeeded)
            {
                return Ok("Password reset");
            }

            return BadRequest(result.Errors);
        }
        [HttpPost("send-email")]
        public async Task<IActionResult> ResendConfirmationEmail(ResendConfirmationEmailRequest mail)
        {
            var user = await _userManager.FindByEmailAsync(mail.Email);
            if (user == null)
            {
                return BadRequest();
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
            await _emailSender.SendEmailAsync(mail.Email, "Confirm your email", $" confirm your account<a href='{callbackUrl}'>here</a>.");

            return Ok("Confirmation mail resent");
        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest("Invalid email");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return Ok("Email confirmed ");
            }

            return BadRequest("Email confirmation failed");
        }
  
    }
}

