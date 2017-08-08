using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine : EntityBehaviour {

	private ElectricityConsumption electricity;

	[SerializeField]
	private AudioClip CoffeeSound;
	private AudioSource audio;

	[SerializeField]
	private float timeP1 = 8;
	[SerializeField]
	private int stepConsumptionP1 = 3;
	[SerializeField]
	private int stepConsumptionRP1 = 1;
	[SerializeField]
	private float timeP2 = 5;
	[SerializeField]
	private int endStepConsumption = 10;

	private int total;

	private int step;

	private float remaningTime;

	private bool launched = false;

	public override void EBUpdate ()
	{
		if (!launched) {
			PlaySound ();
			remaningTime = timeP1;
			launched = true;
			step = 1;
			total = 0;
			electricity = GetComponent<ElectricityConsumption> ();;
		}
		else {
			remaningTime -= Time.deltaTime;
			if (step == 1) {
				total += stepConsumptionP1;
				electricity.Consume (1, stepConsumptionP1, 0);
			} else if (step == 2) {
				if (total > 0) {
					electricity.Release (1, total, 0);
					electricity.Consume (1, endStepConsumption, stepConsumptionRP1);
					total = 0;
				}
			}
			if (remaningTime <= 0) {
				if (step == 1) {
					remaningTime = timeP2;
					step = 2;
				} else {
					launched = false;
					IsStarted = false;
					electricity.Release (1, endStepConsumption, stepConsumptionRP1);
				}
			}
		}
	}

	private void PlaySound() {
		if (audio == null) {
			audio = gameObject.AddComponent (typeof(AudioSource)) as AudioSource;
			audio.clip = CoffeeSound;
		}
		audio.Play ();
	}
}
