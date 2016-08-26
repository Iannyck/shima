using System.Collections;

public class ElectricityData {

	public string timestamp { get; set; }
    public string phaseId { get; set; }
	public int activePower { get; set; }
	public int reactivePower { get; set; }

	public ElectricityData (string timestamp, string phaseId, int activePower, int reactivePower)
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

	public string ToJson() {
		return "{\"Timestamp\":\""+timestamp+"\", \"PhaseId\":\""+phaseId+"\",  \"ActivePower\":\""+activePower+", \"ItemId\":\""+reactivePower+"\"}";
	}

	public string ToSend() {
		return "t:" + timestamp + ",p:" + phaseId + ",a:" + activePower + ",r:" + reactivePower;
	}

	public static string ArrayToSend(ArrayList items) {
		string toSend = "";
		if (items.Count > 0) {
			toSend = toSend + ((ElectricityData)items [0]).ToSend ();
		}
		if (items.Count > 1) {
			for (int i = 1; i < items.Count; i++) {
				toSend = toSend + ";" + ((ElectricityData)items [i]).ToSend ();
			}
		}
		return toSend;
	}

}
