using UnityEngine;
using System.Collections;

public class MakeADish : AbstractScript {


	public float duration = 15.0f;
	public GameObject toilet;
	public GameObject washbasin;
	public GameObject aStar;
	public Rigidbody playerRigidbody;

	protected override AbstractBehaviour InitScript ()
	{
		this.BName = "Clean bathroom";

		AbstractBehaviour[] behaviours = new AbstractBehaviour[5];
		GoToBehaviour behaviour = gameObject.AddComponent(typeof(GoToBehaviour)) as GoToBehaviour;
		behaviour.avatarRigidbody = playerRigidbody;
		behaviour.roomToGo = "Kitchen";
		behaviour.aStarGameObject = aStar;
		behaviour.BName = "Move";
		behaviours [0] = behaviour;

		behaviour = gameObject.AddComponent(typeof(GoToBehaviour)) as GoToBehaviour;
		behaviour.avatarRigidbody = playerRigidbody;
		behaviour.roomToGo = "WC";
		behaviour.aStarGameObject = aStar;
		behaviour.BName = "Moveto WC";
		behaviours [1] = behaviour;

		UseDeviceBehaviour uDBehaviour = gameObject.AddComponent(typeof(UseDeviceBehaviour)) as UseDeviceBehaviour;
		uDBehaviour.DeviceToUse = toilet;
		uDBehaviour.BName = "WC use";
		behaviours [2] = uDBehaviour;

		behaviour = gameObject.AddComponent(typeof(GoToBehaviour)) as GoToBehaviour;
		behaviour.avatarRigidbody = playerRigidbody;
		behaviour.roomToGo = "Washbasin";
		behaviour.aStarGameObject = aStar;
		behaviour.BName = "Moveto ashbasin";
		behaviours [3] = behaviour;

		AtomicActivityBehaviour aABehaviour = gameObject.AddComponent(typeof(AtomicActivityBehaviour)) as AtomicActivityBehaviour;
		aABehaviour.duration = duration;
		aABehaviour.BName = "Clean";
		behaviours [4] = aABehaviour;

		//		this.Behaviours = behaviours;
		Sequence sequence = gameObject.AddComponent (typeof(Sequence)) as Sequence;
		sequence.Behaviours = behaviours;

		return sequence;
	}


}
