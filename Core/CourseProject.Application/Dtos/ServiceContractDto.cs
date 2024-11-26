namespace CourseProject.Application.Dtos;

public class ServiceContractDto 
{
	public Guid Id { get; set; }
	public Guid SubscriberId { get; set; }
	public SubscriberDto Subscriber { get; set; }
	public DateTime ContractDate { get; set; }
	public string TariffPlanName { get; set; }
	public string PhoneNumber { get; set; }
	public Guid EmployeeId { get; set; }
	public EmployeeDto Employee { get; set; }
}

