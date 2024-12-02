using AutoMapper;
using CourseProject.Application.Dtos;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Queries;
using MediatR;

namespace CourseProject.Application.RequestHandlers.QueryHandlers
{
    public class GetUsersAllQueryHandler : IRequestHandler<GetUsersAllQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public GetUsersAllQueryHandler(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetUsersAllQuery request, CancellationToken cancellationToken)
        {
            var users = _mapper.Map<IEnumerable<UserDto>>(await _repository.Get(trackChanges: false, request.UserName));

            return users;
        }
    }
}
