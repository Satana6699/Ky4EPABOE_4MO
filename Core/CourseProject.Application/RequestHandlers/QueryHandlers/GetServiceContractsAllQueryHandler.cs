﻿using MediatR;
using AutoMapper;
using CourseProject.Application.Dtos;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Queries;

namespace CourseProject.Application.RequestHandlers.QueryHandlers;

public class GetServiceContractsAllQueryHandler : IRequestHandler<GetServiceContractsAllQuery, IEnumerable<ServiceContractDto>>
{
    private readonly IServiceContractRepository _repository;
    private readonly IMapper _mapper;

    public GetServiceContractsAllQueryHandler(IServiceContractRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ServiceContractDto>> Handle(GetServiceContractsAllQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<IEnumerable<ServiceContractDto>>(await _repository.Get(false));
}
