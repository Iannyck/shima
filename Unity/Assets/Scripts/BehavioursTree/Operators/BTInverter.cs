using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTInverter : EntityBehaviour {

	[SerializeField]
	private EntityBehaviour child;

	public new void EBStart() {
		child.EBStart ();
		State = BTState.RUNNING;
	}

	public override BTState EBUpdate ()
	{
		if (child.State == BTState.FAILED) {
			return BTState.SUCCEEDED;
		}
		else if (child.State == BTState.SUCCEEDED) {
			return BTState.FAILED;
		}
		return BTState.RUNNING;
	}
}
