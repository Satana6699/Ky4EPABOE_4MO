using MediatR;
using CourseProject.Application.Dtos;

namespace CourseProject.Application.Requests.Commands;

public record CreateSubscriberCommand(SubscriberForCreationDto Subscriber) : IRequest;
