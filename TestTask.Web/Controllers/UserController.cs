using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestTask.Services.Dto;
using TestTask.Services.Services.Contracts;
using TestTask.Web.Model.User;

namespace TestTask.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet("getall")]
        public async Task<IEnumerable<UserModel>> Get()
        {
            var lstUsers = await userService.GetAllAsync();
            Console.WriteLine(" кол-во" + lstUsers.Count);
            var models = mapper.Map<IEnumerable<UserModel>>(lstUsers);
            return models;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<UserModel> Get(Guid? id)
        {
            if (!id.HasValue)
            {
                return new UserModel();
            }
            var user = await userService.GetByIdAsync(id.Value);
            var model = mapper.Map<UserModel>(user);
            return model;
        }

        // POST api/<UserController>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserCreateModel value)
        {
            if (value == null)
            {
                return NotFound();
            }
            var userDto = mapper.Map<UserDto>(value);

            var result = await userService.CreateAsync(userDto);

            if (result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return new JsonResult(result.GetErrorString());
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("edit")]
        public async Task<IActionResult> Update([FromBody] UserEditModel value)
        {
            if (value == null)
            {
                return NotFound();
            }
            var userDto = mapper.Map<UserDto>(value);

            var result = await userService.UpdateAsync(userDto);

            if (result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return new JsonResult(result.GetErrorString());
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var result = await userService.DeleteItemAsync(id.Value);

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
