using MediatR;
using AutoMapper;
using CourseProject.Application.Dtos;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Queries;

namespace CourseProject.Application.RequestHandlers.QueryHandlers;

public class GetServiceContractByIdQueryHandler : IRequestHandler<GetServiceContractByIdQuery, ServiceContractDto?>
{
	private readonly IServiceContractRepository _repository;
	private readonly IMapper _mapper;

	public GetServiceContractByIdQueryHandler(IServiceContractRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<ServiceContractDto?> Handle(GetServiceContractByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<ServiceContractDto>(await _repository.GetById(request.Id, trackChanges: false));
}
