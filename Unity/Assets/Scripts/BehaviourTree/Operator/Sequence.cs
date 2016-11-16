using UnityEngine;
using System.Collections;

public class Sequence : MultipleBehaviourOperator {

	private int currentBehaviourIndex;

	public override void Init ()
	{
		currentBehaviourIndex = 0;
	}

	public override State Execute ()
	{
		if (currentBehaviourIndex >= Behaviours.Length)
			return State.Suceeded;
		else {
			if (Behaviours [currentBehaviourIndex].BehaviourState == State.Stopped)
				Behaviours [currentBehaviourIndex].Run ();
			State behaviourState = Behaviours [currentBehaviourIndex].BehaviourState;
			if (behaviourState == State.Suceeded) {
				currentBehaviourIndex++;
			} else if (behaviourState == State.Failed)
				return State.Failed;
		}
		return State.Running;
	}
}
