using SQLite4Unity3d;

public class RFIDData {

	[PrimaryKey]
	public long Timestamp { get; set; }
	public string AntenaId { get; set; }
	public int SignalStrength { get; set; }
	public string ItemId { get; set; }

	public override string ToString ()
	{
		return string.Format ("[RFIDData: Timestamp={0}, AntenaId={1},  SignalStrength={2}, ItemId={3}]", Timestamp, AntenaId, SignalStrength, ItemId);
	}

}
