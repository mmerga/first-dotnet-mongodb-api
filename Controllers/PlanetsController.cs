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
public class PlanetsController : ControllerBase
{
    private readonly PlanetsService _planetsService;

    public PlanetsController(PlanetsService planetsService)
    {
        _planetsService = planetsService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Planets>>> Get()
    {
        return Ok(await _planetsService.GetAsync());
    }
}
