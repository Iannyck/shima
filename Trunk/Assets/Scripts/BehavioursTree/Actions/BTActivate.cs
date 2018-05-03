using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTActivate : EntityBehaviour {

	[SerializeField]
	private GameObject deviceToTake;

	public override void EBStart() {
		Debug.Log ("Start " + Ebname + " sequence");
		State = BTState.RUNNING;
	}

	public override BTState EBUpdate ()
	{
		return BTState.RUNNING;
	}
}
