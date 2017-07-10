using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartElectricMeter : IESensor {

	[SerializeField]
	private int phasesNumber = 3;

	private Hashtable electricPhases;

	[SerializeField]
	private float updateFrequence = 0.5f;
	private float lastUpdate;

	protected override void IESensorInit ()
	{
		base.IESensorInit ();
		electricPhases = new Hashtable ();
		lastUpdate = updateFrequence;
		for (int i = 0; i < phasesNumber; i++) {
			electricPhases.Add (i, new ElectricPhase(true));
		}
	}

	protected override void IESensorUpdate ()
	{
		base.IESensorUpdate ();
		lastUpdate -= Time.deltaTime;
		if (lastUpdate <= 0) {
			string timestamp = System.DateTime.UtcNow.ToLongTimeString() + ":"+ System.DateTime.UtcNow.Millisecond;
			ElectricPhase _electricPhase;
			for (int i = 0; i < phasesNumber; i++) {
				_electricPhase = electricPhases [i] as ElectricPhase;
				SmartHomeServer.InsertElectricityData (timestamp, ""+i, _electricPhase.ActivePower, _electricPhase.ReactivePower);
			}
			lastUpdate = updateFrequence;
		}
	}

	protected override void IESensorStop ()
	{
		base.IESensorStop ();
	}

	public ElectricPhase GetPhase(int number) {
		return electricPhases[number] as ElectricPhase;
	}

	public List<ElectricPhase> GetPhases() {
		return electricPhases.Values as List<ElectricPhase>;
	}

	public bool Consume(int phase, int activePower, int reactivePower){
		if (((ElectricPhase)electricPhases [phase]).IsActive) {
			((ElectricPhase)electricPhases [phase]).AddActivePower (activePower);
			((ElectricPhase)electricPhases [phase]).AddReactivePower (reactivePower);
			return true;
		}
		return false;
	}

	public void Release(int phase, int activePower, int reactivePower){
		((ElectricPhase)electricPhases [phase]).AddActivePower (-activePower);
		((ElectricPhase)electricPhases [phase]).AddReactivePower (-reactivePower);
	}
}
