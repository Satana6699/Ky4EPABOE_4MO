using MediatR;
using AutoMapper;
using CourseProject.Application.Dtos;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Queries;

namespace CourseProject.Application.RequestHandlers.QueryHandlers;

public class GetServiceStatisticByIdQueryHandler : IRequestHandler<GetServiceStatisticByIdQuery, ServiceStatisticDto?>
{
	private readonly IServiceStatisticRepository _repository;
	private readonly IMapper _mapper;

	public GetServiceStatisticByIdQueryHandler(IServiceStatisticRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<ServiceStatisticDto?> Handle(GetServiceStatisticByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<ServiceStatisticDto>(await _repository.GetById(request.Id, trackChanges: false));
}
