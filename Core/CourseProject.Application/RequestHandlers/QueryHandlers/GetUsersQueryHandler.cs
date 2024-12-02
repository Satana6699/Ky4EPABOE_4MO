using AutoMapper;
using CourseProject.Application.Dtos;
using CourseProject.Application.Requests.Queries;
using CourseProject.Domain.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.RequestHandlers.QueryHandlers
{
    internal class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PageTable<UserDto>>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PageTable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var totalCount = await _repository.CountAsync(request.Name);
            var users = await _repository.GetPageAsync(request.Page, request.PageSize, request.Name);

            var items = _mapper.Map<IEnumerable<UserDto>>(users);
            return new PageTable<UserDto>(items, totalCount, request.Page, request.PageSize);
        }
    }
}