using Kitchen.Data.DAL;
using Kitchen.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public UsersController(ILogger<UsersController> logger, UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        [HttpGet]
        [Route("GetByID")]
        public async Task<IActionResult> GetByID(string uid)
        {
            var filter = Builders<User>.Filter.Where(p => p.uid == uid);

            var data = await _unitOfWork.UserRepository.GetAll(filter);
            return Ok(data.FirstOrDefault());
        }



        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> GetAsync(string clientID)
        {
            var filter = Builders<User>.Filter.Where(p => p.ClientID == clientID);

            var data = await _unitOfWork.UserRepository.GetAll(filter);
            return Ok(data);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(User model)
        {
            model.DateTime = DateTime.Now;
            await _unitOfWork.UserRepository.Add(model);
            await _unitOfWork.CommitAsync();
            return Ok(model);
        }


    }
}

