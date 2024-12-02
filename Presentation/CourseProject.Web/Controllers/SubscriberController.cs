using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using CourseProject.Application.Dtos;
using CourseProject.Application.Requests.Queries;
using CourseProject.Application.Requests.Commands;
using Microsoft.AspNetCore.Authorization;

namespace CourseProject.Web.Controllers;

[Route("api/subscribers")]
[Authorize]
[ApiController]
public class SubscriberController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubscriberController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? name = null)
    {
        var subscribers = await _mediator.Send(new GetSubscribersQuery(page, pageSize, name));

        return Ok(subscribers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var subscriber = await _mediator.Send(new GetSubscriberByIdQuery(id));

        if (subscriber is null)
        {
            return NotFound($"Subscriber with id {id} is not found.");
        }
        
        return Ok(subscriber);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromBody] SubscriberForCreationDto? subscriber)
    {
        if (subscriber is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateSubscriberCommand(subscriber));

        return CreatedAtAction(nameof(Create), subscriber);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] SubscriberForUpdateDto? subscriber)
    {
        if (subscriber is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateSubscriberCommand(subscriber));

        if (!isEntityFound)
        {
            return NotFound($"Subscriber with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteSubscriberCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Subscriber with id {id} is not found.");
        }

        return NoContent();
    }
}
