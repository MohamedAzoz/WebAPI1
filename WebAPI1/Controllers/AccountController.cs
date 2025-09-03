using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI1.DTO;
using WebAPI1.Model;

namespace WebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IConfiguration config;

        public AccountController(UserManager<AppUser> userManager,IConfiguration config)
        {
            this.userManager = userManager;
            this.config = config;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO UserR)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser();
                user.UserName = UserR.UserName;
                user.Email = UserR.Email;
                user.PasswordHash = UserR.Password;
                IdentityResult result=
                    await userManager.CreateAsync(user,UserR.Password);
                if (result.Succeeded)
                {
                    return Created();
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("Password", item.Description);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO userDto)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await userManager.FindByNameAsync(userDto.UserName);
                if (appUser != null)
                {
                    bool found=await userManager.CheckPasswordAsync(appUser,userDto.Password);
                    if (found)
                    {
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.NameIdentifier,appUser.Id));
                        claims.Add(new Claim(ClaimTypes.Name,appUser.UserName));

                        var Roles = await userManager.GetRolesAsync(appUser);
                        foreach (var item in Roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role,item));
                        }

                        SymmetricSecurityKey key = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(config["JWT:SecritKey"]));

                        SigningCredentials signingCred = 
                            new SigningCredentials(key,SecurityAlgorithms.HmacSha256);


                        JwtSecurityToken token = new JwtSecurityToken(
                            issuer: config["JWT:IssuerIP"],
                            audience: config["JWT:AudienceIP"],
                            claims: claims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: signingCred
                            );

                        return Ok(new
                        {
                            token= new JwtSecurityTokenHandler().WriteToken(token),
                            expires= DateTime.Now.AddHours(1) // (or) ==>  token.ValidTo 
                        });

                       // return Ok(object from DTO content last property); // this is best practicies
                    
                    }
                }
                ModelState.AddModelError("userName", "UserName OR Password is Invalid");
            }
            return BadRequest(ModelState);

        }
    }
}
