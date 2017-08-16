using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSequence : EntityBehaviour {

	[SerializeField]
	private List<EntityBehaviour> children;

	private int current;

	public override void EBStart() {
		Debug.Log ("Start sequence");
		current = 0;
		children [current].EBStart ();
		State = BTState.RUNNING;
	}

	public override BTState EBUpdate ()
	{
//		Debug.Log ("Update sequence");
		if (children [current].State == BTState.SUCCEEDED) {
			if (current < children.Count - 1) {
				current++;
				children [current].EBStart ();
			} else {
				Debug.Log ("End sequence Succeeded");
				return BTState.SUCCEEDED;
			}
		} else if (children [current].State == BTState.FAILED) {
			Debug.Log ("End sequence Failed");
			return BTState.FAILED;
		}
		return BTState.RUNNING;
	}

}
