using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Toyin_group_api.Core.Models.Payload;
using Toyin_group_api.Core.Models.Resources;
using Toyin_group_api.Core.Services;

namespace Toyin_group_api.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class TodoController  : BaseController
    {
        private readonly ITodoService todoService;
        public TodoController(ITodoService todoService)
        {
            this.todoService = todoService;
        }

        /// <summary>
        /// Creates a new Todo
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ObjectResource<TodoResource>>> CreateAsync([FromBody]
        CreateTodoPayload payload)
        {
            var response = await todoService.CreateTodoAsync(payload);
            return HandleResponse(response);
        }


        ///<description>Get All Todos</description>
        /// <summary>
        /// This endpoint its used to Get a List Of Todo From The Database
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        public async Task<ActionResult<ListResource<TodoResource>>> GetAllTodos()
        {

            var result = await todoService.GetAllTodosAsync();
            return HandleResponse(result);
        }


        /// <summary>
        /// Get a Todo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ObjectResource<TodoResource>>> GetAsync(Guid id)
        {
            var response = await todoService.GetTodoAsync(id);
            return HandleResponse(response);
        }


        /// <summary>
        /// Update a Todo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<StatusResource>> UpdateAsync(Guid id,[FromBody]
        UpdateTodoPayload payload)
        {
            var response = await todoService.UpdateTodoAsync(id,payload);
            return HandleResponse(response);
        }


        /// <summary>
        /// Delete a Todo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ObjectResource<string>>> DeleteAsync(Guid id)
        {
            var response = await todoService.DeleteTodoAsync(id);
            return HandleResponse(response);
        }
    }
}
