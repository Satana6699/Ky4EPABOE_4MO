using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.Requests.Commands
{
    internal class DeleteUserCommand(Guid Id) : IRequest<bool>
    {
    }
}
