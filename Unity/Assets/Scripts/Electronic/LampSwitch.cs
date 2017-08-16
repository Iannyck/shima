using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSwitch : EntityBehaviour {

	private ElectricityConsumption electricity;

	private bool isInit;

	[SerializeField]
	private int phase1 = 3;

	[SerializeField]
	private int consump = 30;
	[SerializeField]
	private int consumpR = 3;

	[SerializeField]
	private List<Light> lights;

	// Use this for initialization
	void Start () {
		State = BTState.STOP;
		isInit = false;
		electricity = GetComponent<ElectricityConsumption> ();
		foreach(Light light in lights) {
			if (light.gameObject.activeInHierarchy)
				State = BTState.RUNNING;
		}
	}

	public override BTState EBUpdate ()
	{
		if (!isInit) {
			return Init ();
		} else {
			foreach (Light light in lights) {
				light.gameObject.SetActive (!light.gameObject.activeInHierarchy);
				if(light.gameObject.activeInHierarchy)
					electricity.Consume (phase1, consump, consumpR);
				else
					electricity.Release (phase1, consump, consumpR);
			}
			return BTState.SUCCEEDED;
		}
	}

	private BTState Init() {
		foreach(Light light in lights) {
			if (light.gameObject.activeInHierarchy)
				electricity.Consume (phase1, consump, consumpR);
		}
		return BTState.SUCCEEDED;
		isInit = true;
	}

}
