using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EDIncreaseComsumption : EntityBehaviour {

	[SerializeField]
	private int phase1ActiveDelta;
	[SerializeField]
	private int phase1ReactiveDelta;
	[SerializeField]
	private int phase2ActiveDelta;
	[SerializeField]
	private int phase2ReactiveDelta;
	[SerializeField]
	private int phase3ActiveDelta;
	[SerializeField]
	private int phase3ReactiveDelta;

	public override BTState EBUpdate ()
	{
		Debug.Log ("Start increase");
		ElectricityConsumption electricity = GetComponent<ElectricityConsumption> ();
		electricity.Consume (1, phase1ActiveDelta, phase1ReactiveDelta);
		electricity.Consume (2, phase2ActiveDelta, phase2ReactiveDelta);
		electricity.Consume (3, phase3ActiveDelta, phase3ReactiveDelta);

		return BTState.SUCCEEDED;
	}
}
