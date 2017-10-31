using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MultipleBehaviourOperator : AbstractBehaviour {

	private AbstractBehaviour[] behaviours;

	public AbstractBehaviour[] Behaviours {
		get {
			return this.behaviours;
		}
		set {
			behaviours = value;
		}
	}

	public abstract List<AbstractBehaviour> GetActiveBehaviours();

}
