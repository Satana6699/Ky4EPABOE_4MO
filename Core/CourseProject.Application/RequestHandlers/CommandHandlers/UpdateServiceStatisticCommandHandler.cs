using MediatR;
using AutoMapper;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Commands;

namespace CourseProject.Application.RequestHandlers.CommandHandlers;

public class UpdateServiceStatisticCommandHandler : IRequestHandler<UpdateServiceStatisticCommand, bool>
{
	private readonly IServiceStatisticRepository _repository;
	private readonly IMapper _mapper;

	public UpdateServiceStatisticCommandHandler(IServiceStatisticRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateServiceStatisticCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.ServiceStatistic.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.ServiceStatistic, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
