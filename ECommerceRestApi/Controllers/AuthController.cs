namespace ECommerceRestApi.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using global::ECommerceRestApi.Models;
    using global::ECommerceRestApi.Services.Concrete;
    using global::ECommerceRestApi.Dto;

    namespace ECommerceRestApi.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class AuthController : ControllerBase
        {
            private readonly UserManager<User> _userManager;
            private readonly SignInManager<User> _signInManager;
            private readonly JwtTokenGenerator _jwtTokenGenerator;

            public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, JwtTokenGenerator jwtTokenGenerator)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtTokenGenerator = jwtTokenGenerator;
            }

            [HttpPost("register")]
            public async Task<IActionResult> Register([FromBody] RegisterModel model)
            {
                var newUser = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname,
                    UserType = UserType.User 
                };

                var result = await _userManager.CreateAsync(newUser, model.Password);

                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                return Ok("Kayıt başarılı.");
            }
            [HttpPost("registeradmin")]
            public async Task<IActionResult> Register([FromBody] RegisterAdminModel model)
            {
                var newUser = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname,
                    UserType = UserType.Admin
                };

                var result = await _userManager.CreateAsync(newUser, model.Password);

                if (!result.Succeeded)
                    return BadRequest(result.Errors);

               
                var token = _jwtTokenGenerator.GenerateToken(newUser);
                return Ok(new { token });
            }



            [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] LoginDto model)
            {
                // Email ile kullanıcıyı bul
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                    return Unauthorized("Kullanıcı bulunamadı");

                // Parola doğrulama
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (!result.Succeeded)
                    return Unauthorized("Hatalı giriş");

                // Token üret
                var token = _jwtTokenGenerator.GenerateToken(user);
                return Ok(new { token });
            }

        }
    }

}
