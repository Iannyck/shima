using UnityEngine;
using System.Collections;

public class AddObjectToSceneBehaviour : AbstractBehaviour {

	public float x;
	public float y;
	public float z;
	public GameObject ItemToAdd;

	public override void Init ()
	{
		Debug.Log (BName);
	}

	public override State Execute ()
	{
		UI_Object item = ItemToAdd.GetComponent<UI_Object> ();
		item.Action ();
		item.ChangeState ();
		item.PutItemAt (x, y, z);

		return State.Suceeded;
	}
}
