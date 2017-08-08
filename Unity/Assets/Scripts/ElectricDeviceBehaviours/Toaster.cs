using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toaster : EntityBehaviour {

	private ElectricityConsumption electricity;

	[SerializeField]
	private AudioClip ToastReadySound;
	[SerializeField]
	private AudioClip ToastStartSound;
	private AudioSource audio;

	[SerializeField]
	private int phase = 1;

	[SerializeField]
	private float toastTimeP1 = 8;
	[SerializeField]
	private int stepConsumptionP1 = 2;
	[SerializeField]
	private float toastTimeP2 = 20;
	[SerializeField]
	private int stepConsumptionP2 = 1;
	[SerializeField]
	private float toastTimeP3 = 10;

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
			electricity = GetComponent<ElectricityConsumption> ();
		}
		else {
			remaningTime -= Time.deltaTime;
			if (step == 1) {
				total += stepConsumptionP1;
				electricity.Consume (phase, (int)stepConsumptionP1, 0);
			} else if (step == 2) {
				total += stepConsumptionP2;
				electricity.Consume (phase, (int)stepConsumptionP2, 0);
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
					PlayEndSound ();
					IsStarted = false;
					electricity.Release (phase, (int)total, 0);
				}
			}
		}
	}

	private void PlayStartSound() {
		if (audio == null) {
			audio = gameObject.AddComponent (typeof(AudioSource)) as AudioSource;
			audio.clip = ToastStartSound;
		}
		audio.Play ();
	}

	private void PlayEndSound() {
		if (audio == null) {
			audio = gameObject.AddComponent (typeof(AudioSource)) as AudioSource;
		}
		audio.clip = ToastReadySound;
		audio.Play ();
	}
}
