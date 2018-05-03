using UnityEngine;
using System.Collections;

public class FlowMeasurement : UsableDevice {

	private SmartHomeServer shServer;

	public AudioClip audio;

	protected override bool Init ()
	{
		if (shServer == null) {
			GameObject smartHomeServer = GameObject.Find ("smarthomeserver");
			if(smartHomeServer == null)
				Debug.Log("SmartHomeServer Not Loaded");

			shServer = smartHomeServer.GetComponent<SmartHomeServer> ();
			return true;
		}
		return false;
	}

	protected override State OnOn ()
	{
		if (audio != null)
			AudioSource.PlayClipAtPoint (audio,transform.position);
		shServer.InsertBinarySensorData (name, "FlowMeasurement", 1);
		return State.Opening;
	}

	protected override State OnClose ()
	{
		shServer.InsertBinarySensorData (name, "FlowMeasurement", 0);
		return State.Closing;
	}

	protected override State OnOpening ()
	{
		return State.On;
	}

	protected override State OnClosing ()
	{
		return State.Off;
	}
}
