using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestTask.Services.Dto;
using TestTask.Services.Services.Contracts;
using TestTask.Web.Model.Position;

namespace TestTask.Web.Controllers
{
    /// <summary>
    /// Контроллер для должностей
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PosionController : ControllerBase
    {
        private readonly IPositionService positionService;
        private readonly IMapper mapper;

        public PosionController(IPositionService positionService, IMapper mapper)
        {
            this.positionService = positionService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Показать все должности
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public async Task<IEnumerable<PositionModel>> Get()
        {
            var lstUsers = await positionService.GetAllAsync();
            Console.WriteLine(" кол-во" + lstUsers.Count);
            var models = mapper.Map<IEnumerable<PositionModel>>(lstUsers);
            return models;
        }

        /// <summary>
        /// Вытянуть по идентификату
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<PositionModel> Get(Guid? id)
        {
            if (!id.HasValue)
            {
                return new PositionModel();
            }
            var user = await positionService.GetByIdAsync(id.Value);
            var model = mapper.Map<PositionModel>(user);
            return model;
        }

        /// <summary>
        /// Добавить должность 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST api/<UserController>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PositionCreateModel value)
        {
            if (value == null)
            {
                return NotFound();
            }
            var userDto = mapper.Map<PositionDto>(value);

            var result = await positionService.CreateAsync(userDto);

            if (result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return new JsonResult(result.GetErrorString());
            }
        }

        /// <summary>
        /// Редактировать должность 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // PUT api/<UserController>/5
        [HttpPut("edit")]
        public async Task<IActionResult> Update([FromBody] PositionEditModel value)
        {
            if (value == null)
            {
                return NotFound();
            }
            var userDto = mapper.Map<PositionDto>(value);

            var result = await positionService.UpdateAsync(userDto);

            if (result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return new JsonResult(result.GetErrorString());
            }
        }

        /// <summary>
        /// Удалить должность 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<UserController>/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var result = await positionService.DeleteItemAsync(id.Value);

            if (result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return new JsonResult(result.GetErrorString());
            }
        }
    }
}
