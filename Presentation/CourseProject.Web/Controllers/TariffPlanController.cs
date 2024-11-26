using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using CourseProject.Application.Dtos;
using CourseProject.Application.Requests.Queries;
using CourseProject.Application.Requests.Commands;

namespace CourseProject.Web.Controllers;

[Route("api/tariffPlans")]
[ApiController]
public class TariffPlanController : ControllerBase
{
    private readonly IMediator _mediator;

    public TariffPlanController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var tariffPlans = await _mediator.Send(new GetTariffPlansQuery());

        return Ok(tariffPlans);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var tariffPlan = await _mediator.Send(new GetTariffPlanByIdQuery(id));

        if (tariffPlan is null)
        {
            return NotFound($"TariffPlan with id {id} is not found.");
        }
        
        return Ok(tariffPlan);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TariffPlanForCreationDto? tariffPlan)
    {
        if (tariffPlan is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateTariffPlanCommand(tariffPlan));

        return CreatedAtAction(nameof(Create), tariffPlan);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] TariffPlanForUpdateDto? tariffPlan)
    {
        if (tariffPlan is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateTariffPlanCommand(tariffPlan));

        if (!isEntityFound)
        {
            return NotFound($"TariffPlan with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteTariffPlanCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"TariffPlan with id {id} is not found.");
        }

        return NoContent();
    }
}
