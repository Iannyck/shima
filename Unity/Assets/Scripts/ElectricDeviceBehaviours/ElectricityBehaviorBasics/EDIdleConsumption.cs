using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EDIdleConsumption : EntityBehaviour {

	[SerializeField]
	private float time = 3f;

	private float timeLeft;

	private bool interrupted = false;

	private bool isInit = false;
//
//	[SerializeField]
//	private int phase1ActiveDelta;
//	[SerializeField]
//	private int phase1ReactiveDelta;
//	[SerializeField]
//	private int phase2ActiveDelta;
//	[SerializeField]
//	private int phase2ReactiveDelta;
//	[SerializeField]
//	private int phase3ActiveDelta;
//	[SerializeField]
//	private int phase3ReactiveDelta;

	public override BTState EBUpdate ()
	{
		Init ();
		timeLeft -= Time.deltaTime;
		if (timeLeft <= 0f) {
			isInit = false;
			return BTState.SUCCEEDED;
		}
		if(interrupted) {
			isInit = false;
			return BTState.FAILED;
		}
		return BTState.RUNNING;
	}

	public void Interrupt() {
		interrupted = true;
	}

	private void Init() {
		if (!isInit) {
			timeLeft = time;
			isInit = true;
		}
	}

}
