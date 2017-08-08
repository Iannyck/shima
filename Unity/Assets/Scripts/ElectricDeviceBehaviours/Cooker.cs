using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooker : EntityBehaviour {

	private ElectricityConsumption electricity;

	[SerializeField]
	private float toastTimeP1 = 4;
	[SerializeField]
	private int stepConsumptionP1 = 10;
	[SerializeField]
	private float toastTimeP2 = 10;
	[SerializeField]
	private int stepConsumptionP2 = 5;
	[SerializeField]
	private float toastTimeP3 = 40;

	private int total;

	private int step;

	private float remaningTime;

	private bool launched = false;

	public override void EBUpdate ()
	{
		if (!launched) {
			remaningTime = toastTimeP1;
			launched = true;
			step = 1;
			total = 0;
			electricity = GetComponent<ElectricityConsumption> ();;
		}
		else {
			remaningTime -= Time.deltaTime;
			if (step == 1) {
				total += stepConsumptionP1;
				electricity.Consume (1, (int)stepConsumptionP1, 0);
			} else if (step == 2) {
				total += stepConsumptionP2;
				electricity.Consume (1, (int)stepConsumptionP2, 0);
			}
			if (remaningTime <= 0) {
				if (step == 1) {
					remaningTime = toastTimeP2;
					step = 2;
				}else if (step == 2) {
					remaningTime = toastTimeP3;
					step = 3;
				} else {
					launched = false;
					IsStarted = false;
					electricity.Release (1, (int)total, 0);
				}
			}
		}
	}
}
