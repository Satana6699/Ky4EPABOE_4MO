namespace CourseProject.Domain.Entities;

public class ServiceContract 
{
	public Guid Id { get; set; }
	public Guid SubscriberId { get; set; }
	public Subscriber Subscriber { get; set; }
	public DateTime ContractDate { get; set; }
	public string TariffPlanName { get; set; }
	public string PhoneNumber { get; set; }
	public Guid EmployeeId { get; set; }
	public Employee Employee { get; set; }
}
