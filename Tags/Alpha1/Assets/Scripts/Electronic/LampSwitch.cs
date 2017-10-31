using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSwitch : EntityBehaviour {

	private ElectricityConsumption electricity;

	private bool isInit= false;

	[SerializeField]
	private int phase1ActiveDelta = 0;
	[SerializeField]
	private int phase1ReactiveDelta = 0;
	[SerializeField]
	private int phase2ActiveDelta = 0;
	[SerializeField]
	private int phase2ReactiveDelta = 0;
	[SerializeField]
	private int phase3ActiveDelta = 30;
	[SerializeField]
	private int phase3ReactiveDelta = 3;

	[SerializeField]
	private List<Light> lights;

	public override BTState EBUpdate ()
	{
		if (AutoStart && !isInit) {
			return Init ();
		} else {
			if(electricity == null)
				electricity = GetComponent<ElectricityConsumption> ();
			foreach (Light light in lights) {
				light.gameObject.SetActive (!light.gameObject.activeInHierarchy);
				if (light.gameObject.activeInHierarchy) {
					electricity.Consume (1, phase1ActiveDelta, phase1ReactiveDelta);
					electricity.Consume (2, phase2ActiveDelta, phase2ReactiveDelta);
					electricity.Consume (3, phase3ActiveDelta, phase3ReactiveDelta);
				} else {
					electricity.Release (1, phase1ActiveDelta, phase1ReactiveDelta);
					electricity.Release (2, phase2ActiveDelta, phase2ReactiveDelta);
					electricity.Release (3, phase3ActiveDelta, phase3ReactiveDelta);
				}
			}
			return BTState.SUCCEEDED;
		}
	}

	private BTState Init() {
		if(electricity == null)
			electricity = GetComponent<ElectricityConsumption> ();
		foreach(Light light in lights) {
			if (light.gameObject.activeInHierarchy) {
				electricity.Consume (1, phase1ActiveDelta, phase1ReactiveDelta);
				electricity.Consume (2, phase2ActiveDelta, phase2ReactiveDelta);
				electricity.Consume (3, phase3ActiveDelta, phase3ReactiveDelta);
			}
		}
		isInit = true;
		return BTState.SUCCEEDED;
	}

}
