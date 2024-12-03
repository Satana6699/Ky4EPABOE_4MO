using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using CourseProject.Application.Dtos;
using CourseProject.Application.Requests.Queries;
using CourseProject.Application.Requests.Commands;
using Microsoft.AspNetCore.Authorization;

namespace CourseProject.Web.Controllers;

[Route("api/serviceContracts")]
[Authorize]
[ApiController]
public class ServiceContractController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServiceContractController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? name = null)
    {
        var serviceContracts = await _mediator.Send(new GetServiceContractsQuery(page, pageSize, name));

        return Ok(serviceContracts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var serviceContract = await _mediator.Send(new GetServiceContractByIdQuery(id));

        if (serviceContract is null)
        {
            return NotFound($"ServiceContract with id {id} is not found.");
        }
        
        return Ok(serviceContract);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromBody] ServiceContractForCreationDto? serviceContract)
    {
        if (serviceContract is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateServiceContractCommand(serviceContract));

        return CreatedAtAction(nameof(Create), serviceContract);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ServiceContractForUpdateDto? serviceContract)
    {
        if (serviceContract is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateServiceContractCommand(serviceContract));

        if (!isEntityFound)
        {
            return NotFound($"ServiceContract with id {id} is not found.");
        }

        return Ok(serviceContract);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteServiceContractCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"ServiceContract with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpGet("employees")]
    public async Task<IActionResult> GetEmployees([FromQuery] string? name = null)
    {
        var result = await _mediator.Send(new GetEmployeesAllQuery(name));

        return Ok(result);
    }
    [HttpGet("subscribers")]
    public async Task<IActionResult> GetSubscribers([FromQuery] string? name = null)
    {
        var result = await _mediator.Send(new GetSubscribersAllQuery(name));

        return Ok(result);
    }
}
