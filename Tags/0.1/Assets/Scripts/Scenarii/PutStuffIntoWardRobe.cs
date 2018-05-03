using UnityEngine;
using System.Collections;

public class PutStuffIntoWardRobe : AbstractScript {

	public float duration = 10.0f;
	public GameObject aStar;
	public Rigidbody playerRigidbody;

	protected override AbstractBehaviour InitScript ()
	{
		this.BName = "Put Stuff Into Ward Robe";

		AbstractBehaviour[] behaviours = new AbstractBehaviour[4];
		GoToBehaviour behaviour = gameObject.AddComponent(typeof(GoToBehaviour)) as GoToBehaviour;
		behaviour.avatarRigidbody = playerRigidbody;
		behaviour.roomToGo = "Entrance";
		behaviour.aStarGameObject = aStar;
		//		behaviour.PathInit ();
		behaviour.BName = "Move";
		behaviours [0] = behaviour;

		behaviour = gameObject.AddComponent(typeof(GoToBehaviour)) as GoToBehaviour;
		behaviour.avatarRigidbody = playerRigidbody;
		behaviour.roomToGo = "Wardrobe";
		behaviour.aStarGameObject = aStar;
		//		behaviour.PathInit ();
		behaviour.BName = "Move";
		behaviours [1] = behaviour;

		AtomicActivityBehaviour aABehaviour = gameObject.AddComponent(typeof(AtomicActivityBehaviour)) as AtomicActivityBehaviour;
		aABehaviour.duration = duration;
		aABehaviour.BName = "Clean";
		behaviours [2] = aABehaviour;

		behaviour = gameObject.AddComponent(typeof(GoToBehaviour)) as GoToBehaviour;
		behaviour.avatarRigidbody = playerRigidbody;
		behaviour.roomToGo = "Livingroom";
		behaviour.aStarGameObject = aStar;
		//		behaviour.PathInit ();
		behaviour.BName = "Move";
		behaviours [3] = behaviour;

//		this.Behaviours = behaviours;
		Sequence sequence = gameObject.AddComponent (typeof(Sequence)) as Sequence;
		sequence.Behaviours = behaviours;

		return sequence;
	}

}
