using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EDDecreaseComsumption : EntityBehaviour {

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
		ElectricityConsumption electricity = GetComponent<ElectricityConsumption> ();
		electricity.Release (1, phase1ActiveDelta, phase1ReactiveDelta);
		electricity.Release (2, phase2ActiveDelta, phase2ReactiveDelta);
		electricity.Release (3, phase3ActiveDelta, phase3ReactiveDelta);

		return BTState.SUCCEEDED;
	}
}
