using MediatR;
using AutoMapper;
using CourseProject.Application.Dtos;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Queries;

namespace CourseProject.Application.RequestHandlers.QueryHandlers;

public class GetSubscriberByIdQueryHandler : IRequestHandler<GetSubscriberByIdQuery, SubscriberDto?>
{
	private readonly ISubscriberRepository _repository;
	private readonly IMapper _mapper;

	public GetSubscriberByIdQueryHandler(ISubscriberRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<SubscriberDto?> Handle(GetSubscriberByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<SubscriberDto>(await _repository.GetById(request.Id, trackChanges: false));
}
