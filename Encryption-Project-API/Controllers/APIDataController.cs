using Encryption_Project_API.Repositories;
using Encryption_Project_LIB.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Encryption_Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIDataController : ControllerBase
    {
        private IAuthentication _authentication;
        private IEncryptedUserService _encryptedUserService;
        public APIDataController(Encryption_Project_LIB.Interfaces.IAuthentication authentication, IEncryptedUserService encryptedUserService)
        {
            _authentication = authentication;
            _encryptedUserService = encryptedUserService;
        }
  

        [HttpGet("{username}/{password}")]
        public IActionResult Login(string username, string password)
        {
            try
            {
                var item = _encryptedUserService.GetAllUsers().FirstOrDefault().Username;
                return Ok(item);
                //return Ok(new
                //{
                //    token = _authentication.Login(username, password)
                //});
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (UnauthorizedAccessException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return Problem("Incorrect input", statusCode: 401);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<AccessDataController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AccessDataController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
           
        }

        // PUT api/<AccessDataController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccessDataController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
