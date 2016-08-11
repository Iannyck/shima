using SQLite4Unity3d;

public class ElectricityData {

	//[PrimaryKey]
	public string timestamp { get; set; }
    public short phaseId { get; set; }
	public int activePower { get; set; }
	public int reactivePower { get; set; }

	public ElectricityData (string timestamp, short phaseId, int activePower, int reactivePower)
	{
		this.timestamp = timestamp;
		this.phaseId = phaseId;
		this.activePower = activePower;
		this.reactivePower = reactivePower;
	}
	

	public override string ToString ()
	{
		return string.Format ("[ElectricityData: Timestamp={0}, PhaseId={1},  ActivePower={2}, ItemId={3}]", timestamp, phaseId, activePower, reactivePower);
	}
}
