using API.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController(SignInManager<AppUser> signInManager) : BaseApiController
    {
        [HttpPost("Register")]
        public async Task<ActionResult> Register (RegisterDTO registerDTO)
        {
            var user = new AppUser
            {
                Email = registerDTO.Email,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                UserName=registerDTO.Email
            };

            var result = await signInManager.UserManager.CreateAsync(user,registerDTO.Password);

            if (!result.Succeeded) 
                return BadRequest(result.Errors);

            return Ok();

        }


    }
}
