using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using AutoMapper;
using FirstNetMongo.Domain.Models;
using FirstNetMongo.Domain.Dtos;
using FirstNetMongo.Services;

namespace FirstNetMongo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly MongoDBServices _mongoDBServices;
    private readonly IMapper _mapper;

    public UsersController(MongoDBServices mongoDBServices, IMapper mapper)
    {
        _mongoDBServices = mongoDBServices;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType<UserResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<UserResponseDto>>> Get()
    {
        var users = await _mongoDBServices.GetAsync();
        var userResponse = _mapper.Map<IEnumerable<Users>, IEnumerable<UserResponseDto>>(users);
        return Ok(userResponse);
    }

    [HttpPost]
    [ProducesResponseType<UserResponseDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] UserRequestDto user)
    {
        var userRequest = _mapper.Map<UserRequestDto, Users>(user);
        var userCreated = await _mongoDBServices.CreateAsync(userRequest);
        var userResponse = _mapper.Map<Users, UserResponseDto>(userCreated);
        return CreatedAtAction(nameof(Get), new { id = userResponse.Id }, userResponse);
    }

    [HttpPut("{id}")]
    [ProducesResponseType<UserResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(string id, [FromBody] UserRequestDto user)
    {
        var userRequest = _mapper.Map<UserRequestDto, Users>(user);
        var userUpdated = await _mongoDBServices.PutAsync(id, userRequest);
        var userResponse = _mapper.Map<Users, UserResponseDto>(userUpdated);
        return CreatedAtAction(nameof(Get), new { id = userResponse.Id }, userResponse);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        await _mongoDBServices.DeleteAsync(id);
        return Ok();
    }
}
