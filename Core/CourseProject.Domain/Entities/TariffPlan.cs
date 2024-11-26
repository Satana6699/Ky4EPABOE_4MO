namespace CourseProject.Domain.Entities;

public class TariffPlan 
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public decimal SubscriptionFee { get; set; }
	public decimal LocalCallCost { get; set; }
	public decimal LongDistanceCallCost { get; set; }
	public decimal InternationalCallCost { get; set; }
	public string BillingType { get; set; }
	public decimal SmsCost { get; set; }
	public decimal MmsCost { get; set; }
	public decimal DataTransferCost { get; set; }
}
