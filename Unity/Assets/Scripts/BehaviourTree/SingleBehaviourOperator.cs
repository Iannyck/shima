using UnityEngine;
using System.Collections;

public abstract class SingleBehaviourOperator : AbstractBehaviour {

	private AbstractBehaviour behaviour;

	public AbstractBehaviour Behaviour {
		get {
			return this.behaviour;
		}
		set {
			behaviour = value;
		}
	}

}
