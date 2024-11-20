using Kitchen.Data.DAL;
using Kitchen.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Kitchen.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public ClientsController(ILogger<ClientsController> logger, UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> GetAsync()
        {
            var filter = Builders<Client>.Filter.Empty;

            var data = await _unitOfWork.ClientRepository.GetAll(filter);
            return Ok(data);

        }


        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(Client model)
        {
            model.ClientID = Guid.NewGuid().ToString();
            model.DateTime = DateTime.Now;
            await _unitOfWork.ClientRepository.Add(model);
            await _unitOfWork.CommitAsync();
            return Ok(model);
        }

    }
}
