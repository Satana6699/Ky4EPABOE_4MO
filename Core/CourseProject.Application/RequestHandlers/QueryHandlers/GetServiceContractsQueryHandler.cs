using MediatR;
using AutoMapper;
using CourseProject.Application.Dtos;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Queries;

namespace CourseProject.Application.RequestHandlers.QueryHandlers;

public class GetServiceContractsQueryHandler : IRequestHandler<GetServiceContractsQuery, IEnumerable<ServiceContractDto>>
{
	private readonly IServiceContractRepository _repository;
	private readonly IMapper _mapper;

	public GetServiceContractsQueryHandler(IServiceContractRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<ServiceContractDto>> Handle(GetServiceContractsQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<ServiceContractDto>>(await _repository.Get(trackChanges: false));
}
