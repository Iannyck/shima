using UnityEngine;
using System.Collections;

public class Delayed : SingleBehaviourOperator {

	public float delay;

	private float timeLeft;

	public override void Init ()
	{
		timeLeft = delay;
	}

	public override State Execute ()
	{
		if (timeLeft >= 0)
			timeLeft -= Time.deltaTime;
		else {
			if (Behaviour.BehaviourState == State.Stopped)
				Behaviour.Run ();
			return Behaviour.BehaviourState;
		}
		return State.Running;
	}
}
