using SQLite4Unity3d;

public class ElectricityData {

	[PrimaryKey]
	public long Timestamp { get; set; }
	public string PhaseId { get; set; }
	public int ActivePower { get; set; }
	public int ReactivePower { get; set; }

	public override string ToString ()
	{
		return string.Format ("[ElectricityData: Timestamp={0}, PhaseId={1},  ActivePower={2}, ItemId={3}]", Timestamp, PhaseId, ActivePower, ReactivePower);
	}
}
