using AutoMapper;
using CourseProject.Domain.Entities;
using CourseProject.Application.Dtos;

namespace CourseProject.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
		CreateMap<Employee, EmployeeDto>();
		CreateMap<EmployeeForCreationDto, Employee>();
		CreateMap<EmployeeForUpdateDto, Employee>();

		CreateMap<Subscriber, SubscriberDto>();
		CreateMap<SubscriberForCreationDto, Subscriber>();
		CreateMap<SubscriberForUpdateDto, Subscriber>();

		CreateMap<TariffPlan, TariffPlanDto>();
		CreateMap<TariffPlanForCreationDto, TariffPlan>();
		CreateMap<TariffPlanForUpdateDto, TariffPlan>();

		CreateMap<ServiceContract, ServiceContractDto>();
		CreateMap<ServiceContractForCreationDto, ServiceContract>();
		CreateMap<ServiceContractForUpdateDto, ServiceContract>();

		CreateMap<ServiceStatistic, ServiceStatisticDto>();
		CreateMap<ServiceStatisticForCreationDto, ServiceStatistic>();
		CreateMap<ServiceStatisticForUpdateDto, ServiceStatistic>();

		CreateMap<User, UserDto>();
		CreateMap<UserForCreationDto, User>();
		CreateMap<UserDto, User>();
    }
}

