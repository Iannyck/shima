using SQLite4Unity3d;

public class RFIDData {

	[PrimaryKey]
	public string timestamp { get; set; }
	public string antenaId { get; set; }
	public int signalStrength { get; set; }
	public string tagId { get; set; }

	public RFIDData (string timestamp, string antenaId, int signalStrength, string tagId)
	{
		this.timestamp = timestamp;
		this.antenaId = antenaId;
		this.signalStrength = signalStrength;
		this.tagId = tagId;
	}
	

	public override string ToString ()
	{
		return string.Format ("[RFIDData: Timestamp={0}, AntenaId={1},  SignalStrength={2}, TagId={3}]", timestamp, antenaId, signalStrength, tagId);
	}

}
