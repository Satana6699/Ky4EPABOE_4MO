using MediatR;
using AutoMapper;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Commands;

namespace CourseProject.Application.RequestHandlers.CommandHandlers;

public class UpdateServiceContractCommandHandler : IRequestHandler<UpdateServiceContractCommand, bool>
{
	private readonly IServiceContractRepository _repository;
	private readonly IMapper _mapper;

	public UpdateServiceContractCommandHandler(IServiceContractRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateServiceContractCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.ServiceContract.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.ServiceContract, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
