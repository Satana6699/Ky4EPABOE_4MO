using MediatR;
using AutoMapper;
using CourseProject.Application.Dtos;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Queries;

namespace CourseProject.Application.RequestHandlers.QueryHandlers;

public class GetServiceStatisticsQueryHandler : IRequestHandler<GetServiceStatisticsQuery, IEnumerable<ServiceStatisticDto>>
{
	private readonly IServiceStatisticRepository _repository;
	private readonly IMapper _mapper;

	public GetServiceStatisticsQueryHandler(IServiceStatisticRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<ServiceStatisticDto>> Handle(GetServiceStatisticsQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<ServiceStatisticDto>>(await _repository.Get(trackChanges: false));
}
