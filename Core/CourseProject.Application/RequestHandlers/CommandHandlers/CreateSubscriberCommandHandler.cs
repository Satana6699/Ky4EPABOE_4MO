using MediatR;
using AutoMapper;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Commands;

namespace CourseProject.Application.RequestHandlers.CommandHandlers;

public class CreateSubscriberCommandHandler : IRequestHandler<CreateSubscriberCommand>
{
	private readonly ISubscriberRepository _repository;
	private readonly IMapper _mapper;

	public CreateSubscriberCommandHandler(ISubscriberRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateSubscriberCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<Subscriber>(request.Subscriber));
		await _repository.SaveChanges();
	}
}
