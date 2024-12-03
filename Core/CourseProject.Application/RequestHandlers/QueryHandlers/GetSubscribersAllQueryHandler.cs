using MediatR;
using AutoMapper;
using CourseProject.Application.Dtos;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Queries;

namespace CourseProject.Application.RequestHandlers.QueryHandlers;

public class GetSubscribersAllQueryHandler : IRequestHandler<GetSubscribersAllQuery, IEnumerable<SubscriberDto>>
{
    private readonly ISubscriberRepository _repository;
    private readonly IMapper _mapper;

    public GetSubscribersAllQueryHandler(ISubscriberRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SubscriberDto>> Handle(GetSubscribersAllQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<IEnumerable<SubscriberDto>>(await _repository.Get(trackChanges: false, request.Name));
    }
}
