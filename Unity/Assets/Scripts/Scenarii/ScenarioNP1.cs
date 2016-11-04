using UnityEngine;
using System.Collections;

public class ScenarioNP1 : Sequence {

	public string roomToGo1;
	public string roomToGo2;
	public GameObject aStar;
	public Rigidbody playerRigidbody;

	public override void Init ()
	{
		this.BName = "Scenario1";

		MoveToBehaviour[] behaviours = new MoveToBehaviour[2];
		behaviours [0] = gameObject.AddComponent(typeof(MoveToBehaviour)) as MoveToBehaviour;
		behaviours [0].playerRigidbody = playerRigidbody;
		behaviours [0].roomToGo = roomToGo1;
		behaviours [0].aStarGameObject = aStar;
		behaviours [0].PathInit ();
		behaviours [0].BName = "Move1";

		behaviours [1] = gameObject.AddComponent(typeof(MoveToBehaviour)) as MoveToBehaviour;
		behaviours [1].playerRigidbody = playerRigidbody;
		behaviours [1].roomToGo = roomToGo2;
		behaviours [1].aStarGameObject = aStar;
		behaviours [1].PathInit ();
		behaviours [1].BName = "Move2";

		this.Behaviours = behaviours;

//		this.Run ();
	}
	
}
