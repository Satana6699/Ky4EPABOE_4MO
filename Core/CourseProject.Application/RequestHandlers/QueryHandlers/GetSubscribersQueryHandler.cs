using MediatR;
using AutoMapper;
using CourseProject.Application.Dtos;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Queries;

namespace CourseProject.Application.RequestHandlers.QueryHandlers;

public class GetSubscribersQueryHandler : IRequestHandler<GetSubscribersQuery, PageTable<SubscriberDto>>
{
	private readonly ISubscriberRepository _repository;
	private readonly IMapper _mapper;

	public GetSubscribersQueryHandler(ISubscriberRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<PageTable<SubscriberDto>> Handle(GetSubscribersQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _repository.CountAsync(request.Name);
        var subscribers = await _repository.GetPageAsync(request.Page, request.PageSize, request.Name);

        var items = _mapper.Map<IEnumerable<SubscriberDto>>(subscribers);
        return new PageTable<SubscriberDto>(items, totalCount, request.Page, request.PageSize);
    }
}
