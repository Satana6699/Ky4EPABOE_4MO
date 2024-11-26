namespace CourseProject.Application.Dtos;

public class ServiceContractForUpdateDto 
{
	public Guid Id { get; set; }
	public Guid SubscriberId { get; set; }
	public DateTime ContractDate { get; set; }
	public string TariffPlanName { get; set; }
	public string PhoneNumber { get; set; }
	public Guid EmployeeId { get; set; }
}

