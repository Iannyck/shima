using UnityEngine;
using System.Collections;

public class CallForBus : Sequence {

	public GameObject phone;
	public GameObject aStar;
	public Rigidbody playerRigidbody;

	public override void Init ()
	{

		this.BName = "Call for pieces of information";

		AbstractBehaviour[] behaviours = new AbstractBehaviour[2];
		MoveToBehaviour behaviour = gameObject.AddComponent(typeof(MoveToBehaviour)) as MoveToBehaviour;
		behaviour.playerRigidbody = playerRigidbody;
		behaviour.roomToGo = "Phone";
		behaviour.aStarGameObject = aStar;
		behaviour.PathInit ();
		behaviour.BName = "Move";
		behaviours [0] = behaviour;

		UseDeviceBehaviour uDBehaviour = gameObject.AddComponent(typeof(UseDeviceBehaviour)) as UseDeviceBehaviour;
		uDBehaviour.DeviceToUse = phone;
		uDBehaviour.BName = "Call";
		behaviours [1] = uDBehaviour;

		this.Behaviours = behaviours;
	}

}
