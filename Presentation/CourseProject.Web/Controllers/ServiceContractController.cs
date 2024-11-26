using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using CourseProject.Application.Dtos;
using CourseProject.Application.Requests.Queries;
using CourseProject.Application.Requests.Commands;

namespace CourseProject.Web.Controllers;

[Route("api/serviceContracts")]
[ApiController]
public class ServiceContractController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServiceContractController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var serviceContracts = await _mediator.Send(new GetServiceContractsQuery());

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

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteServiceContractCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"ServiceContract with id {id} is not found.");
        }

        return NoContent();
    }
}
