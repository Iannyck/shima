using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSelector : EntityBehaviour {

	[SerializeField]
	private List<EntityBehaviour> children;

	private int current = 0;

	public new void EBStart() {
		children [current].EBStart ();
		State = BTState.RUNNING;
	}

	public override BTState EBUpdate ()
	{
		if (children [current].State == BTState.FAILED) {
			if (current < children.Count - 1) {
				current++;
				children [current].EBStart ();
			} else
				return BTState.FAILED;
		}
		else if (children [current].State == BTState.SUCCEEDED)
			return BTState.SUCCEEDED;
		return BTState.RUNNING;
	}

}
