using UnityEngine;
using System.Collections;

public abstract class AbstractScript : AbstractBehaviour {

	private AbstractBehaviour behaviour;

	public AbstractBehaviour Behaviour {
		get {
			return this.behaviour;
		}
		set {
			behaviour = value;
		}
	}

	public override void Init ()
	{
		behaviour = InitScript ();
	}

	public override State Execute ()
	{
		if (behaviour.BehaviourState == State.Stopped)
			behaviour.Run ();
		return behaviour.BehaviourState;
	}

	protected abstract AbstractBehaviour InitScript ();
}
