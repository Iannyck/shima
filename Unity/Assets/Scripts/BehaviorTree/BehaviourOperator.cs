using UnityEngine;
using System.Collections;

public abstract class BehaviourOperator : AbstractBehaviour {

	private AbstractBehaviour[] behaviours;

	public AbstractBehaviour[] Behaviours {
		get {
			return this.behaviours;
		}
	}

}
