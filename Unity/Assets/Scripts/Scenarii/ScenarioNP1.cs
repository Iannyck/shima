using UnityEngine;
using System.Collections;

public class ScenarioNP1 : Sequence {

	public string roomToGo1;
	public string roomToGo2;
	public GameObject deviceToUse;
	public GameObject aStar;
	public Rigidbody playerRigidbody;

	public override void Init ()
	{
		this.BName = "Scenario1";

		AbstractBehaviour[] behaviours = new AbstractBehaviour[3];
		MoveToBehaviour behaviour = gameObject.AddComponent(typeof(MoveToBehaviour)) as MoveToBehaviour;
		behaviour.playerRigidbody = playerRigidbody;
		behaviour.roomToGo = roomToGo1;
		behaviour.aStarGameObject = aStar;
		behaviour.PathInit ();
		behaviour.BName = "Move1";
		behaviours [0] = behaviour;

		behaviour = gameObject.AddComponent(typeof(MoveToBehaviour)) as MoveToBehaviour;
		behaviour.playerRigidbody = playerRigidbody;
		behaviour.roomToGo = roomToGo2;
		behaviour.aStarGameObject = aStar;
		behaviour.PathInit ();
		behaviour.BName = "Move2";
		behaviours [1] = behaviour;

		UseDeviceBehaviour uDBehaviour = gameObject.AddComponent(typeof(UseDeviceBehaviour)) as UseDeviceBehaviour;
		uDBehaviour.DeviceToUse = deviceToUse;
		uDBehaviour.BName = "Act";
		behaviours [2] = uDBehaviour;

		this.Behaviours = behaviours;

//		this.Run ();
	}
	
}
