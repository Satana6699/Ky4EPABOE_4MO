using MediatR;
using AutoMapper;
using CourseProject.Application.Dtos;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Queries;

namespace CourseProject.Application.RequestHandlers.QueryHandlers;

public class GetTariffPlansQueryHandler : IRequestHandler<GetTariffPlansQuery, PageTable<TariffPlanDto>>
{
	private readonly ITariffPlanRepository _repository;
	private readonly IMapper _mapper;

	public GetTariffPlansQueryHandler(ITariffPlanRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<PageTable<TariffPlanDto>> Handle(GetTariffPlansQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _repository.CountAsync(request.Name);
        var tariffPlans = await _repository.GetPageAsync(request.Page, request.PageSize, request.Name);

        var items = _mapper.Map<IEnumerable<TariffPlanDto>>(tariffPlans);
        return new PageTable<TariffPlanDto>(items, totalCount, request.Page, request.PageSize);
    }
}
