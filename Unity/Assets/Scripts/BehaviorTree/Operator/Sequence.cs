using UnityEngine;
using System.Collections;

public class Sequence : BehaviourOperator {

	int currentBehaviourIndex;

	public Sequence() {
		currentBehaviourIndex = 0;
	}

	public override State Execute ()
	{
		if (currentBehaviourIndex >= behaviours.Length)
			return State.Suceeded;
		else {
			State behaviourState = behaviours [currentBehaviourIndex].Execute ();
			if (behaviourState == State.Suceeded) {
				currentBehaviourIndex++;
			} else if (behaviourState == State.Failed)
				return State.Failed;
		}
		return State.Running;
	}
}
