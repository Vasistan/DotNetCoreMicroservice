using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApplicationService.DataRepository;
using ToDoApplicationService.Models;

namespace ToDoApplicationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoRepository repository;

        public ToDoController(IToDoRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IList<ToDoItemDetail>> GetAll()
        {
            return await this.repository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ToDoItemDetail> GetById([FromRoute] string id)
        {
            return await this.repository.GetById(id);
        }

        [HttpPost]
        public async Task<ToDoItemDetail> AddNew([FromBody] ToDoItemDetail todoItem)
        {
            return await this.repository.AddNew(todoItem);
        }

        [HttpPut("MarkAsDone")]
        public async Task<ToDoItemDetail> MarkAsDone([FromBody] ToDoItemDetail todoItem)
        {
            return await this.repository.MarkAsDone(todoItem);
        }
    }
}
