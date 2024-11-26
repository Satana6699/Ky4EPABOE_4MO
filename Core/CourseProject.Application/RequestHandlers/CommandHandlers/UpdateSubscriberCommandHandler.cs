using MediatR;
using AutoMapper;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Commands;

namespace CourseProject.Application.RequestHandlers.CommandHandlers;

public class UpdateSubscriberCommandHandler : IRequestHandler<UpdateSubscriberCommand, bool>
{
	private readonly ISubscriberRepository _repository;
	private readonly IMapper _mapper;

	public UpdateSubscriberCommandHandler(ISubscriberRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateSubscriberCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.Subscriber.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.Subscriber, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
