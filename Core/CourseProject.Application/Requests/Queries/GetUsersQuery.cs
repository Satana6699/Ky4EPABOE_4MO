using CourseProject.Application.Dtos;
using CourseProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.Requests.Queries
{
    public record class GetUsersQuery : IRequest<PageTable<UserDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Name { get; set; }

        public GetUsersQuery(int page, int pageSize, string? name)
        {
            Page = page;
            PageSize = pageSize;
            Name = name;
        }
    }
}
