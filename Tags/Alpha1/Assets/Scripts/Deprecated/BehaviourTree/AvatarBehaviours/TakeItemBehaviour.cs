using UnityEngine;
using System.Collections;

public class TakeItemBehaviour : AbstractBehaviour {

	public GameObject ItemToTake;

	public override void Init ()
	{

	}

	public override State Execute ()
	{
		UI_Object item = ItemToTake.GetComponent<UI_Object> ();
		item.Action ();
		item.ChangeState ();
		return State.Suceeded;
	}
}
