using MediatR;
using AutoMapper;
using CourseProject.Application.Dtos;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Queries;

namespace CourseProject.Application.RequestHandlers.QueryHandlers;

public class GetServiceContractsQueryHandler : IRequestHandler<GetServiceContractsQuery, PageTable<ServiceContractDto>>
{
	private readonly IServiceContractRepository _repository;
	private readonly IMapper _mapper;

	public GetServiceContractsQueryHandler(IServiceContractRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<PageTable<ServiceContractDto>> Handle(GetServiceContractsQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _repository.CountAsync(request.Name);
        var serviceContracts = await _repository.GetPageAsync(request.Page, request.PageSize, request.Name);

        var items = _mapper.Map<IEnumerable<ServiceContractDto>>(serviceContracts);
        return new PageTable<ServiceContractDto>(items, totalCount, request.Page, request.PageSize);
    }
}
