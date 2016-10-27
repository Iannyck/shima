using UnityEngine;
using System.Collections;

public class ScenarioNP1 : Sequence {

	public GameObject roomToGo1;
	public GameObject roomToGo2;
	public GameObject aStar;
	public Rigidbody playerRigidbody;

	public override void Init ()
	{
		MoveToBehaviour[] behaviours = new MoveToBehaviour[2];
		behaviours [0] = new MoveToBehaviour ();
		behaviours [0].playerRigidbody = playerRigidbody;
		behaviours [0].target = roomToGo1;
		behaviours [0].aStarGameObject = aStar;

		behaviours [1] = new MoveToBehaviour ();
		behaviours [1].playerRigidbody = playerRigidbody;
		behaviours [1].target = roomToGo2;
		behaviours [1].aStarGameObject = aStar;
	}
	
}
