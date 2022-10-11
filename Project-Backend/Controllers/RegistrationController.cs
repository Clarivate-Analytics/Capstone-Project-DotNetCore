using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Project_Backend.Db_Context;
using Project_Backend.Models;
using System.Collections;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : Controller
    {
        private readonly RegistrationDbContext registrationDbContext;
        private readonly IConfiguration _config;

        public RegistrationController(RegistrationDbContext registrationDbContext, IConfiguration config)
        {
            this.registrationDbContext = registrationDbContext;
            _config = config;
        }

        //GET all data
        [HttpGet]
        [ActionName(nameof(GetSingleEmp))]
        public async Task<IActionResult> GetEmp()
        {
            var register = await registrationDbContext.Registration.ToListAsync();
            return Ok(register);
        }

        //GET single data
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName(nameof(GetSingleEmp))]
        public async Task<IActionResult> GetSingleEmp([FromRoute] Guid id)
        {

            var register = await registrationDbContext.Registration.FirstOrDefaultAsync(x => x.Id == id);
            if (register == null)
            {
                Console.WriteLine("id not found");
                return NotFound("id not found");
            }
            Console.WriteLine("email found");
            return Ok(register);
        }


        //GET employee id
        [HttpGet]
        [Route("/id/{id:guid}")]
        [ActionName(nameof(GetSingleEmp))]
        public async Task<IActionResult> GetEmpId([FromRoute] Guid id)
        {
            var register = await registrationDbContext.Registration.FirstOrDefaultAsync(x => x.Id == id);
            if (register == null)
            {
                Console.WriteLine("id not found");
                return NotFound("id not found");
            }
            Console.WriteLine("email found");
            return Ok(register.EmpID);
        }

        //POST single data
        [HttpPost]
        [ActionName("AddEmp")]
        public async Task<IActionResult> AddEmp([FromBody]Registration registration)
        {
            await registrationDbContext.Registration.AddAsync(registration);
            await registrationDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingleEmp), registration.Id, registration);
        }


        [HttpPost]
        [Route("login")]
        [ActionName("Login/{mailid}")]
        public IActionResult Login([FromBody]ArrayList cred)
        {
            string email = cred[0].ToString();
            string pswd = cred[1].ToString();
            Console.WriteLine();
            //return Ok("");
            if (email == null && pswd == null)
            {
                return BadRequest();
            }
            else
            {
                var user = registrationDbContext.Registration.Where(a => a.Email == email).FirstOrDefault();
                if (user != null && user.Password == pswd)
                {
                    var token = GenerateToken(user);
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Logged in successfully",
                        EmpId = user.Id,
                        Role = user.Role,
                        JwtToken = token
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "User not found"
                    });
                }
            }
        }


        private string GenerateToken(Registration user)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Employee ID", user.EmpID),
                new Claim("Email", user.Email),
                new Claim("Role", user.Role),
                new Claim("Password", user.Password)
            };
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credential);

            return tokenhandler.WriteToken(token);
        }

    }
}