using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : MonoBehaviour {

	private ElectricityConsumption electricity;

	[SerializeField]
	private AudioClip workingSound;
	[SerializeField]
	private AudioClip openSound;
	private AudioSource audio;

	[SerializeField]
	private int phase1 = 1;

	[SerializeField]
	private int phase2 = 2;

	[SerializeField]
	private int consumpP1 = 300;
	[SerializeField]
	private int consumpRP1 = 30;

	[SerializeField]
	private int consumpP2 = 100;
	[SerializeField]
	private int consumpRP2 = 10;

	private bool isClose = false;

	public void Act() {
		isClose = !isClose;
	}

	[SerializeField]
	private int overConsumpP1 = 50;
	[SerializeField]
	private int overConsumpRP1 = 40;

	[SerializeField]
	private int overConsumpP2 = 30;
	[SerializeField]
	private int overConsumpRP2 = 30;

	private float remaningTime;

	private bool overConsumpDone = false;

	private bool idleStarted1;
	private bool idleStarted2;

	void Start() {
		electricity = GetComponent<ElectricityConsumption> ();
		idleStarted1 = electricity.Consume (phase1, consumpP1, consumpRP1);
		idleStarted2 = electricity.Consume (phase2, consumpP2, consumpRP2);
	}

	void Update ()
	{
		if (!idleStarted1) {
			idleStarted1 = electricity.Consume (phase1, consumpP1, consumpRP1);
		}
		if (!idleStarted2) {
			idleStarted2 = electricity.Consume (phase2, consumpP2, consumpRP2);
		}
		if (!isClose) {
			remaningTime += Time.deltaTime;
		}
		else {
			if(remaningTime > 0) {
				remaningTime -= Time.deltaTime;
				if (!overConsumpDone) {
					electricity.Consume (phase1, overConsumpP1, overConsumpRP1);
					electricity.Consume (phase2, overConsumpP2, overConsumpRP2);
					overConsumpDone = true;
				}
			}
			if(overConsumpDone) {
				overConsumpDone = false;
				electricity.Release (phase1, overConsumpP1, overConsumpRP1);
				electricity.Release (phase2, overConsumpP2, overConsumpRP2);
			}
		}
	}

	private void PlayWorkingSound() {
		if (audio == null) {
			audio = gameObject.AddComponent (typeof(AudioSource)) as AudioSource;
			audio.clip = workingSound;
		}
		audio.Play ();
	}

	private void PlayOpenCloseSound() {
		if (audio == null) {
			audio = gameObject.AddComponent (typeof(AudioSource)) as AudioSource;
		}
		audio.clip = openSound;
		audio.Play ();
	}
}
