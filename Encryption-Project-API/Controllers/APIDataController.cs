using Encryption_Project_API.Repositories;
using Encryption_Project_LIB.DTOs;
using Encryption_Project_LIB.Interfaces;
using Encryption_Project_LIB.ViewModels;
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
        private IHashAndSalting _hashAndSalting;
        public APIDataController(Encryption_Project_LIB.Interfaces.IAuthentication authentication, IEncryptedUserService encryptedUserService, IHashAndSalting hashAndSalting)
        {
            _authentication = authentication;
            _encryptedUserService = encryptedUserService;
            _hashAndSalting = hashAndSalting;
        }


        [HttpGet("{username}/{password}")]
        public IActionResult Login(string username, string password)
        {
            try
            {
                var item = _authentication.Login(username, password);
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
        [HttpGet()]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _encryptedUserService.GetAllUsers();
                return Ok(users);
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

        [HttpGet()]
        [Route("GetAllSecrets")]
        public IActionResult GetAllSecrets(int id, string secretname)
        {
            try
            {
                var secrets = _encryptedUserService.GetSecrets(secretname, _encryptedUserService.GetAllUsers().Where(a => a.Id == id).FirstOrDefault().Priveleges);
                return Ok(secrets);
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

        [HttpGet()]
        [Route("GetSecret/{id}/{secretname}")]
        public IActionResult GetSecret(int id,string secretname)
        {
            try
            {
                var secrets = _encryptedUserService.GetSecrets(secretname, _encryptedUserService.GetAllUsers().Where(a => a.Id == id).FirstOrDefault().Priveleges);
                return Ok(secrets);
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

        // POST api/<AccessDataController>
        [HttpPost]
        public void Register([FromBody] RegisterUserVM VM)
        {
            _encryptedUserService.Add(VM);
        }

        // PUT api/<AccessDataController>/5
        [HttpGet("{id}")]
        public void Block(int id)
        {
            _encryptedUserService.Block(_encryptedUserService.GetAllUsers().Where(a => a.Id == id).FirstOrDefault());
        }

        // DELETE api/<AccessDataController>/5
        [HttpGet()]
        [Route("Unblock/{id}")]
        public void Unblock(int id)
        {
            _encryptedUserService.Unblock(_encryptedUserService.GetAllUsers().Where(a => a.Id == id).FirstOrDefault());
        }

    }
}
