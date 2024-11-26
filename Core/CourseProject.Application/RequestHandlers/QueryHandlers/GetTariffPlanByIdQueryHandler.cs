using MediatR;
using AutoMapper;
using CourseProject.Application.Dtos;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Queries;

namespace CourseProject.Application.RequestHandlers.QueryHandlers;

public class GetTariffPlanByIdQueryHandler : IRequestHandler<GetTariffPlanByIdQuery, TariffPlanDto?>
{
	private readonly ITariffPlanRepository _repository;
	private readonly IMapper _mapper;

	public GetTariffPlanByIdQueryHandler(ITariffPlanRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<TariffPlanDto?> Handle(GetTariffPlanByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<TariffPlanDto>(await _repository.GetById(request.Id, trackChanges: false));
}
