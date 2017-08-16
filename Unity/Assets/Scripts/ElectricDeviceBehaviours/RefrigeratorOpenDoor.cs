using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrigeratorOpenDoor : EntityBehaviour {

	[SerializeField]
	private EDPhase coolingPhase;

	public override BTState EBUpdate ()
	{
		if (coolingPhase.State == BTState.RUNNING)
			coolingPhase.Interrupt ();
		return BTState.SUCCEEDED;
	}
}
