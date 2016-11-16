using UnityEngine;
using System.Collections;

public class ReleaseItemBehaviour : AbstractBehaviour {

	public GameObject DeviceToRelease;

	public override void Init ()
	{

	}

	public override State Execute ()
	{
		UI_Object item = DeviceToRelease.GetComponent<UI_Object> ();
		item.Action ();
		return State.Suceeded;
	}
}
