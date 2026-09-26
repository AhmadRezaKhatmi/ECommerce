using API.DTOs;
using API.Extensions;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Controllers
{
    public class AccountController(SignInManager<AppUser> signInManager) : BaseApiController
    {
        //ثبت نام کاربر
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterDTO registerDTO)
        {
            var user = new AppUser
            {
                Email = registerDTO.Email,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                UserName = registerDTO.Email
            };

            var result = await signInManager.UserManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();

        }



        //ورود کاربر
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDTO loginDTO)
        {

            var user = await signInManager.UserManager.FindByEmailAsync(loginDTO.Email);

            if (user == null)
                return Unauthorized("Email is incorrect");


            var result = await signInManager.PasswordSignInAsync(
                user,
                loginDTO.Password,
                false,
                false);

            if (!result.Succeeded)
                return Unauthorized("Email or password is incorrect");

            return Ok();
        }




        //خروج از سیستم
        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return NoContent();
        }



        // دریافت اطلاعات کاربر جاری 
        [HttpGet("user-info")]
        public async Task<ActionResult> GetUserInfo()
        {
            if (User.Identity?.IsAuthenticated == false)
                return NoContent();

            var user = await signInManager.UserManager.GetUserByEmail(User);

            if (user == null)
                return Unauthorized();


            return Ok(new
            {
                user.FirstName,
                user.LastName,
                user.Email
            });

        }





        [HttpGet("auth-status")]
        public ActionResult GetAuthState()
        {
            return Ok(new
            {
                IsAuthenticated = User.Identity?.IsAuthenticated ?? false
            });
        }


    }
}
