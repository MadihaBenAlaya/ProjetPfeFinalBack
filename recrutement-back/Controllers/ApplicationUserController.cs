using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AppRecrutement.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace AppRecrutement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _singInManager;
        private readonly ApplicationSettings _appSettings;

        public ApplicationUserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _singInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        
        [HttpGet]
        [Route("GetRecruteurs")]
        public async Task<IList<ApplicationUser>> GetAllRecruteurs()
        {
            return await _userManager.GetUsersInRoleAsync("RECRUTEUR");


        }

        [HttpGet]
        [Route("GetCandidats")]
        public async Task<IList<ApplicationUser>> GetAllCandidats()
        {
            return await _userManager.GetUsersInRoleAsync("CANDIDAT");


        }

        [HttpPost]
        [Route("RegisterRecruteur")]
        //POST : /api/ApplicationUser/RegisterRecruteur
        public async Task<Object> PostApplicationUserRecruteur(ApplicationUserModel model)
        {
            
            model.Role = "RECRUTEUR";
           
            var applicationUser = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
                
            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                await _userManager.AddToRoleAsync(applicationUser, model.Role);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        

        [HttpPost]
        [Route("RegisterAdmin")]
        //POST : /api/ApplicationUser/RegisterAdmin
        public async Task<Object> PostApplicationUserAdmin(ApplicationUserModel model)
        {
            model.Role = "ADMINISTRATEUR";
            
            var applicationUser = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName

            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                await _userManager.AddToRoleAsync(applicationUser, model.Role);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        [Route("RegisterCandidat")]
        //POST : /api/ApplicationUser/RegisterCandidat
        public async Task<Object> PostApplicationUserCandidat(ApplicationUserModel model)
        {

            model.Role = "CANDIDAT";

            var applicationUser = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName

            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                await _userManager.AddToRoleAsync(applicationUser, model.Role);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [Route("Login")]
        //POST : /api/ApplicationUser/Login
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                //Get role assigned to the user
                var role = await _userManager.GetRolesAsync(user);
                IdentityOptions _options = new IdentityOptions();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.Id.ToString()),
                        new Claim(_options.ClaimsIdentity.RoleClaimType,role.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(59),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect. " });
        }


        // PUT api/<CandidatureController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, ApplicationUser model)
        {

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
           
           


            try
            {
                user.UserName = model.UserName;
                //user.PasswordHash = model.Surname;
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.Date_naissance = model.Date_naissance;
                user.PhoneNumber = model.PhoneNumber;
                user.Pays = model.Pays;
                user.Date_naissance = model.Date_naissance;
                user.diplome = model.diplome;
                user.Nb_annees_experience = model.Nb_annees_experience;
                user.Specialite = model.Specialite;
                user.Ville = model.Ville;
                var result = await _userManager.UpdateAsync(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return NoContent();
        }


      
    }
}

