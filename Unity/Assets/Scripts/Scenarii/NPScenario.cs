using UnityEngine;
using System.Collections;

public class NPScenario : AbstractScript {

	public GameObject phone;
	public GameObject aStar;
	public Rigidbody playerRigidbody;
	public GameObject toilet;
	public GameObject washbasin;

	protected override AbstractBehaviour InitScript ()
	{
		this.BName = "NP Scenario";
		AbstractScript[] behaviours = new AbstractScript[3];

		PutStuffIntoWardRobe scenario1 = gameObject.AddComponent(typeof(PutStuffIntoWardRobe)) as PutStuffIntoWardRobe;
		scenario1.playerRigidbody = playerRigidbody;
		scenario1.aStar = aStar;
		behaviours [0] = scenario1;

		AnswerThePhone scenario2 = gameObject.AddComponent(typeof(AnswerThePhone)) as AnswerThePhone;
		scenario2.playerRigidbody = playerRigidbody;
		scenario2.aStar = aStar;
		scenario2.phone = phone;
		behaviours [1] = scenario2;

		CleanBathroom scenario3 = gameObject.AddComponent(typeof(CleanBathroom)) as CleanBathroom;
		scenario3.playerRigidbody = playerRigidbody;
		scenario3.aStar = aStar;
		scenario3.toilet = toilet;
		scenario3.washbasin = washbasin;
		behaviours [2] = scenario3;

		Sequence sequence = gameObject.AddComponent (typeof(Sequence)) as Sequence;
		sequence.Behaviours = behaviours;

		return sequence;

	}
}
