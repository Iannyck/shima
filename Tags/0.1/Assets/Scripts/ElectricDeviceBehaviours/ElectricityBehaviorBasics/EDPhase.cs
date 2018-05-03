using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EDPhase : EntityBehaviour {

	[SerializeField]
	private float time = 3f;

	private float timeLeft;

	private bool interrupted = false;

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

	private int step = 0;

	[SerializeField]
	private int noiseRate = 30;

	private bool noiseAdded = false;


	private void Increase ()
	{
		ElectricityConsumption electricity = GetComponent<ElectricityConsumption> ();
		electricity.Consume (1, phase1ActiveDelta, phase1ReactiveDelta);
		electricity.Consume (2, phase2ActiveDelta, phase2ReactiveDelta);
		electricity.Consume (3, phase3ActiveDelta, phase3ReactiveDelta);
	}

	private void Decrease ()
	{
		ElectricityConsumption electricity = GetComponent<ElectricityConsumption> ();
		electricity.Release (1, phase1ActiveDelta, phase1ReactiveDelta);
		electricity.Release (2, phase2ActiveDelta, phase2ReactiveDelta);
		electricity.Release (3, phase3ActiveDelta, phase3ReactiveDelta);
	}

	public override BTState EBUpdate ()
	{
		if (step == 0) {
			Increase ();
			step++;
			timeLeft = time;
//			Debug.Log (step + ":" +timeLeft);
		}
		else if(step == 1) {
			timeLeft -= Time.deltaTime;
			if (noiseAdded || Random.Range (0, 100) < noiseRate)
				Noise ();
			if (timeLeft <= 0f || interrupted) {
				step++;
				if (noiseAdded)
					Noise ();
			}
		}
		else if(step == 2) {
			Decrease ();
			step = 0;
//			Debug.Log (step + " end");
			return BTState.SUCCEEDED;
		}
		return BTState.RUNNING;
	}

	public void Interrupt() {
		interrupted = !interrupted;
//		Debug.Log ("============ Interrup =============== " + interrupted);
	}

	public bool Interrupted {
		get {
			return this.interrupted;
		}
	}

	private void Noise() {
		int scale = 10;
		ElectricityConsumption electricity = GetComponent<ElectricityConsumption> ();
		if (!noiseAdded) {
			electricity.Consume (1, phase1ActiveDelta / scale, phase1ReactiveDelta / scale);
			electricity.Consume (2, phase2ActiveDelta / scale, phase2ReactiveDelta / scale);
			electricity.Consume (3, phase3ActiveDelta / scale, phase3ReactiveDelta / scale);
			noiseAdded = true;
		} else {
			electricity.Release (1, phase1ActiveDelta / scale, phase1ReactiveDelta / scale);
			electricity.Release (2, phase2ActiveDelta / scale, phase2ReactiveDelta / scale);
			electricity.Release (3, phase3ActiveDelta / scale, phase3ReactiveDelta / scale);
			noiseAdded = false;
		}
	}

}
