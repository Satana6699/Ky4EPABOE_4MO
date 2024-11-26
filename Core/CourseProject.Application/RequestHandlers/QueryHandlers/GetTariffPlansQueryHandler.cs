using MediatR;
using AutoMapper;
using CourseProject.Application.Dtos;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Queries;

namespace CourseProject.Application.RequestHandlers.QueryHandlers;

public class GetTariffPlansQueryHandler : IRequestHandler<GetTariffPlansQuery, IEnumerable<TariffPlanDto>>
{
	private readonly ITariffPlanRepository _repository;
	private readonly IMapper _mapper;

	public GetTariffPlansQueryHandler(ITariffPlanRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<TariffPlanDto>> Handle(GetTariffPlansQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<TariffPlanDto>>(await _repository.Get(trackChanges: false));
}
