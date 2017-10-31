using UnityEngine;
using System.Collections;

public class UseDeviceBehaviour : AbstractBehaviour {

	public GameObject DeviceToUse;

	public override void Init ()
	{
		
	}

	public override State Execute ()
	{
		UsableDevice device = DeviceToUse.GetComponent<UsableDevice> ();
		device.ActOn ();
		return State.Suceeded;
	}
}
