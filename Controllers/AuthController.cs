using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HelperDesk.API.Data;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;


namespace HelperDesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        private readonly IConfiguration _config;

        private readonly ISessionRepository _repoSession;
        public AuthController(IAuthRepository repo, ISessionRepository _repoSession, IConfiguration config){
            this._repo = repo;
            this._config = config;
            this._repoSession = _repoSession;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDTO)
        {
            userForRegisterDTO.Username = userForRegisterDTO.Email;

            if (await _repo.UserExists(userForRegisterDTO.Username))
                return Ok(new { 
                    status = "error",
                    message = "El usuario ya existe"
                });

            var AuthyAPIKey = "bet16crXxHw6keUAc5zAR0g1O8qWCJn2";
            var code = "502";
            var result = new JObject();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Authy-API-Key", AuthyAPIKey);

                var requestContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("user[email]", userForRegisterDTO.Email),
                    new KeyValuePair<string, string>("user[cellphone]", userForRegisterDTO.Phone),
                    new KeyValuePair<string, string>("user[country_code]", code),
                });

                HttpResponseMessage response = client.PostAsync(
                    "https://api.authy.com/protected/json/users/new",
                    requestContent).Result;

                HttpContent responseContent = response.Content;
                result = JObject.Parse(responseContent.ReadAsStringAsync().Result);
            }

            if ((string)result["sucess"] != "true")
                BadRequest("Ocurrió un error al registar el usuario");

            var id = (string)result["user"]["id"];

	        var userToCreate = new User
            {
                Names = userForRegisterDTO.Names,
                LastName = userForRegisterDTO.LastName,
                Email = userForRegisterDTO.Email,
                Phone = userForRegisterDTO.Phone,
                GenderId = userForRegisterDTO.GenderId,
                RoleId = userForRegisterDTO.RoleId,
                CompanyId = userForRegisterDTO.CompanyId,
                status = userForRegisterDTO.status,
                Username = userForRegisterDTO.Username,
                Authy_id = id
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDTO.Password);            

            // return StatusCode(201);
            return Ok(new { 
                    status = "OK",
                    message = "Usuario registrado correctamente"
                });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var usr = await _repo.UserActive(userForLoginDto.Username.ToLower());

            if (usr == null) {
                return Unauthorized();
            }

            if (usr.Active)
                // return StatusCode(422, "Usuario ya tiene sesión activa");
                return BadRequest("Usuario ya tiene sesión activa");

            var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            if (userFromRepo == null)
                return Unauthorized();

            // var claims = new[]
            // {
            //     new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
            //     new Claim(ClaimTypes.Name, userFromRepo.Username)
            // };

            // var key = new SymmetricSecurityKey(Encoding.UTF8
            //     .GetBytes(_config.GetSection("AppSettings:Token").Value));

            // var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // var tokenDescriptor = new SecurityTokenDescriptor
            // {
            //     Subject = new ClaimsIdentity(claims),
            //     Expires = DateTime.Now.AddDays(1),
            //     SigningCredentials = creds
            // };

            // var tokenHandler = new JwtSecurityTokenHandler();

            // var token = tokenHandler.CreateToken(tokenDescriptor);

            var userToUpdate = new UserForActivateDto
            {
                Active = true
            };

            var AuthyAPIKey = "bet16crXxHw6keUAc5zAR0g1O8qWCJn2";
            var result = new JObject();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Authy-API-Key", AuthyAPIKey);

                // var authy_id = usr.authy_id;
                var authy_id = usr.Authy_id;

                HttpResponseMessage response = client.GetAsync(
                    $"https://api.authy.com/protected/json/sms/{authy_id}").Result;

                HttpContent responseContent = response.Content;
                result = JObject.Parse(responseContent.ReadAsStringAsync().Result);
                // Console.WriteLine(responseContent.ReadAsStringAsync().Result);
            }

            if ((string)result["sucess"] != "true")
                BadRequest("Ocurrió un error al registar el usuario");

            return Ok(new {
                status = "OK",
                data = usr.Id
                // token = tokenHandler.WriteToken(token)
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(User user)
        {
            var usr = await _repo.ChangeActive(user.Id);

            return Ok(new {
                message = "Sesión cerrada"
            });
        }

        [HttpPost("create")]
        public async Task<IActionResult> createAuthy(UserForRegisterDto userForRegisterDTO)
        {
            var AuthyAPIKey = "bet16crXxHw6keUAc5zAR0g1O8qWCJn2";
            var result = new JObject();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Authy-API-Key", AuthyAPIKey);

                var requestContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("user[email]", userForRegisterDTO.Email),
                    new KeyValuePair<string, string>("user[cellphone]", userForRegisterDTO.Phone),
                    new KeyValuePair<string, string>("user[country_code]", "502"),
                });

                HttpResponseMessage response = client.PostAsync(
                    "https://api.authy.com/protected/json/users/new",
                    requestContent).Result;

                HttpContent responseContent = response.Content;
                result = JObject.Parse(responseContent.ReadAsStringAsync().Result);
            }

            return Ok(new {
                data = result
            });
        }

        [HttpGet("send/{id}")]
        public async Task<IActionResult> sendVerification(int id) {
            
            var usr = await _repo.GetUser(id);

            if (usr == null)
                BadRequest("Usuario no existe");

            var AuthyAPIKey = "bet16crXxHw6keUAc5zAR0g1O8qWCJn2";
            var result = new JObject();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Authy-API-Key", AuthyAPIKey);

                // var authy_id = usr.authy_id;
                var authy_id = 287051348;

                HttpResponseMessage response = client.GetAsync(
                    $"https://api.authy.com/protected/json/sms/{authy_id}").Result;

                HttpContent responseContent = response.Content;
                result = JObject.Parse(responseContent.ReadAsStringAsync().Result);
                // Console.WriteLine(responseContent.ReadAsStringAsync().Result);
            }

            return Ok(new {
                data =  result
            });
        }

        [HttpPost("verify")]
        public async Task<IActionResult> verificate(Verification user) {

            var usr = await _repo.GetUser(user.Id);

            if (usr == null)
                BadRequest("Usuario no existe");

            var AuthyAPIKey = "bet16crXxHw6keUAc5zAR0g1O8qWCJn2";
            var result = new JObject();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Authy-API-Key", AuthyAPIKey);

                var authy_id = usr.Authy_id;
                var tokenId = user.Token;

                HttpResponseMessage response = client.GetAsync(
                    $"https://api.authy.com/protected/json/verify/{tokenId}/{authy_id}").Result;

                HttpContent responseContent = response.Content;
                result = JObject.Parse(responseContent.ReadAsStringAsync().Result);
            }

            if ((string)result["success"] != "true")
                return Ok(new {
                    status = "error",
                    message = "Token Inválido"
                });

            var usrs = await _repo.UserActive(usr.Username.ToLower());

            if (usrs == null) {
                return Unauthorized();
            }

            if (usrs.Active)
                // return StatusCode(422, "Usuario ya tiene sesión activa");
                return Ok(new {
                    status = "error",
                    message = "Usuario ya tiene sesión activa"
                });


            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usr.Id.ToString()),
                new Claim(ClaimTypes.Name, usr.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            Sessions session = new Sessions(
                usr.Id,
                Request.Headers["User-Agent"].ToString(),
                0,
                new DateTime()
            );
            // session.UserId = usr.Id;
            // session.Information = Request.Headers["User-Agent"].ToString();
            // session.Active = 0;

            await this._repoSession.Add(session);

            return Ok(new {
                status = "OK",
                message = "Bienvenido de nuevo",
                data = tokenHandler.WriteToken(token)
            });
        }
    }
}
