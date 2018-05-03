using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrigeratorOpenDoor : EntityBehaviour {

	[SerializeField]
	private EDPhase coolingPhase;

	public override BTState EBUpdate ()
	{
		coolingPhase.Interrupt ();
		Debug.Log ("===================== Door opened ===================== " + coolingPhase.Interrupted);
		return BTState.SUCCEEDED;
	}
}
