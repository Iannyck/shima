using UnityEngine;
using System.Collections;

public class PutItemAtBehaviour : AbstractBehaviour {

	public float x;
	public float y;
	public float z;
	public GameObject ItemToRelease;

	public override void Init ()
	{
//		Debug.Log (BName);
	}

	public override State Execute ()
	{
		UI_Object item = ItemToRelease.GetComponent<UI_Object> ();
		item.Action ();
		item.ChangeState ();
		item.PutItemAt (x, y, z);

		return State.Suceeded;
	}
}
