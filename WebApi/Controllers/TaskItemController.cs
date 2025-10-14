using Application.Features.TaskItem.Queries;
using Application.Features.TaskItems.Command.Create;
using Application.Features.TaskItems.Command.Delete;
using Application.Features.TaskItems.Command.Update;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskItemController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Task oluşturur
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskItemCommand command)
        {
            if (!ModelState.IsValid)//Kullanıcı apıyı yanlış girerse hata döndürecez sonra api angular uı göstercez
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        /// <summary>
        /// Task Günceller
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UpdateTaskItemCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Id’yi command’a set et
            command.Id = id;

            var result = await _mediator.Send(command);
            return Ok(result);

        }


        /// <summary>
        /// Task'i Siler
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(Guid id)
        {
            try
            {
                var command = new DeleteTaskItemCommand { Id = id };
                var result = await _mediator.Send(command);

                return Ok(result);  
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });//Id yanlış verilirse KeyNotFoundException hatası  messajını gösterir api ye
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new { Message = ex.Message });
            }
        }
        /// <summary>
        /// Id'göre task getirir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            try
            {
                var query = new GetTaskItemByIdQuery { Id = id };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
        /// <summary>
        ///  Status göre task  TaskItem  listesine döner
        /// </summary>
        /// <param name="status">Filtrelenecek TaskItem durumu (örnek: Pending, Completed..)</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTaskByStatus([FromQuery] TaskItemStatus? status) 
        {
            try
            {
                var query = new GetTaskItemsByStatusQuery { Status = status };
                var result=await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception exception)
            {

                return StatusCode(500, new { Message = exception.Message });

            }
        }
    }
}


