namespace CourseProject.Application.Dtos;

public class ServiceStatisticDto 
{
	public Guid Id { get; set; }
	public Guid ServiceContractId { get; set; }
	public ServiceContractDto ServiceContract { get; set; }
	public int CallDuration { get; set; }
	public int SmsCount { get; set; }
	public int MmsCount { get; set; }
	public int DataTransferAmount { get; set; }
}

