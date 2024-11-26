namespace CourseProject.Application.Dtos;

public class ServiceStatisticForCreationDto 
{
	public Guid ServiceContractId { get; set; }
	public int CallDuration { get; set; }
	public int SmsCount { get; set; }
	public int MmsCount { get; set; }
	public int DataTransferAmount { get; set; }
}

