using MediatR;
using AutoMapper;
using CourseProject.Application.Dtos;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Queries;

namespace CourseProject.Application.RequestHandlers.QueryHandlers;

public class GetEmployeesAllQueryHandler : IRequestHandler<GetEmployeesAllQuery, IEnumerable<EmployeeDto>>
{
    private readonly IEmployeeRepository _repository;
    private readonly IMapper _mapper;

    public GetEmployeesAllQueryHandler(IEmployeeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmployeeDto>> Handle(GetEmployeesAllQuery request, CancellationToken cancellationToken)
    {
        var items = _mapper.Map<IEnumerable<EmployeeDto>>(await _repository.Get(trackChanges: false, request.Name));
        return items;
    }
}
