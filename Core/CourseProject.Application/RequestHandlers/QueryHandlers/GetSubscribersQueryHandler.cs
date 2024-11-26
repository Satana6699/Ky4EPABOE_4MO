using MediatR;
using AutoMapper;
using CourseProject.Application.Dtos;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Queries;

namespace CourseProject.Application.RequestHandlers.QueryHandlers;

public class GetSubscribersQueryHandler : IRequestHandler<GetSubscribersQuery, IEnumerable<SubscriberDto>>
{
	private readonly ISubscriberRepository _repository;
	private readonly IMapper _mapper;

	public GetSubscribersQueryHandler(ISubscriberRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<SubscriberDto>> Handle(GetSubscribersQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<SubscriberDto>>(await _repository.Get(trackChanges: false));
}
