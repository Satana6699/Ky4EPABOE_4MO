using AutoMapper;
using CourseProject.Application.Requests.Queries;
using CourseProject.Domain.Abstractions;
using CourseProject.Domain.Entities;
using MediatR;
using CourseProject.Application.Dtos;

namespace CourseProject.Application.RequestHandlers.QueryHandlers
{
    public class GetTariffPlansAllQueryHandler : IRequestHandler<GetTariffPlansAllQuery, IEnumerable<TariffPlanDto>>
    {
        private readonly ITariffPlanRepository _repository;
        private readonly IMapper _mapper;

        public GetTariffPlansAllQueryHandler(ITariffPlanRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TariffPlanDto>> Handle(GetTariffPlansAllQuery request, CancellationToken cancellationToken)
        {
            var items = _mapper.Map<IEnumerable<TariffPlanDto>>(await _repository.Get(trackChanges: false));

            return items;
        }
    }
}