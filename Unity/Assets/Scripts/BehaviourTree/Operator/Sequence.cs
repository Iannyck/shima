using UnityEngine;
using System.Collections;

public class Sequence : BehaviourOperator {

	private int currentBehaviourIndex;

	public override void Init ()
	{
		currentBehaviourIndex = 0;
	}

	public Sequence(AbstractBehaviour[] behaviours) {
		this.behaviours = behaviours;
	}

	public Sequence() {
		this.behaviours = null;
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
