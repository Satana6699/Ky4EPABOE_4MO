using MediatR;
using AutoMapper;
using CourseProject.Application.Dtos;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Queries;

namespace CourseProject.Application.RequestHandlers.QueryHandlers;

public class GetServiceStatisticsQueryHandler : IRequestHandler<GetServiceStatisticsQuery, PageTable<ServiceStatisticDto>>
{
	private readonly IServiceStatisticRepository _repository;
	private readonly IMapper _mapper;

	public GetServiceStatisticsQueryHandler(IServiceStatisticRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<PageTable<ServiceStatisticDto>> Handle(GetServiceStatisticsQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _repository.CountAsync(request.Name);
        var serviceStatistics = await _repository.GetPageAsync(request.Page, request.PageSize, request.Name);

        var items = _mapper.Map<IEnumerable<ServiceStatisticDto>>(serviceStatistics);
        return new PageTable<ServiceStatisticDto>(items, totalCount, request.Page, request.PageSize);
    }
}
