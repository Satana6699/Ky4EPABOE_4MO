namespace CourseProject.Application.Dtos;

public class ServiceContractForCreationDto 
{
	public Guid SubscriberId { get; set; }
	public DateTime ContractDate { get; set; }
	public string TariffPlanName { get; set; }
	public string PhoneNumber { get; set; }
	public Guid EmployeeId { get; set; }
}

