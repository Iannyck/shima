using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTRepeat : EntityBehaviour {

	[SerializeField]
	private EntityBehaviour child;

	public new void EBStart() {
		child.EBStart ();
		State = BTState.RUNNING;
	}

	public override BTState EBUpdate ()
	{
		if (child.State != BTState.RUNNING) {
			child.EBStart ();
		}
		return BTState.RUNNING;
	}
}
