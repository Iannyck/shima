using UnityEngine;
using System.Collections;

public abstract class BehaviourOperator : AbstractBehaviour {

	protected AbstractBehaviour[] behaviours;

	public AbstractBehaviour[] Behaviours {
		get {
			return this.behaviours;
		}
	}

}
