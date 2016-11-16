using UnityEngine;
using System.Collections;

public class Parallel : MultipleBehaviourOperator {

	public override void Init ()
	{
		
	}

	public override State Execute ()
	{
		int ended = 0;
		State behaviourState;
		foreach(AbstractBehaviour behaviour in Behaviours) {
			if (behaviour.BehaviourState == State.Stopped)
				behaviour.Run ();
			if (behaviour.BehaviourState == State.Failed)
				return State.Failed;
			else if (behaviour.BehaviourState == State.Suceeded)
				ended++;
		}
		if (ended == Behaviours.Length)
			return State.Suceeded;
		return State.Running;
	}
}
