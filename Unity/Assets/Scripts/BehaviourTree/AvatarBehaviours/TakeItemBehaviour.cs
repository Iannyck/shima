using UnityEngine;
using System.Collections;

public class TakeItemBehaviour : AbstractBehaviour {

	public GameObject DeviceToTake;

	public override void Init ()
	{

	}

	public override State Execute ()
	{
		UI_Object item = DeviceToTake.GetComponent<UI_Object> ();
		item.Action ();
		return State.Suceeded;
	}
}
