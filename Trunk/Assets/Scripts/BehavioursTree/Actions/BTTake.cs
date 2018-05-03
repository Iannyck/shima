using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTake : EntityBehaviour {

	[SerializeField]
	private GameObject objectToTake;

	public override void EBStart() {
		Debug.Log ("Start " + Ebname + " sequence");
		State = BTState.RUNNING;
	}

	public override BTState EBUpdate ()
	{
		return BTState.RUNNING;
	}
		
}
