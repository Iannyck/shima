using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleActivate : MonoBehaviour {

	[SerializeField] private bool autoDesactivate = false;
	[SerializeField] private GameObject particleP = null;
	[SerializeField] private float deltaParticlePlay = 10;
	[SerializeField] private float deltaAutoDesactivate = 20;
	private float cdeltaParticlePlay;
	private float cdeltaAutoDesactivate;

	private bool start = false;
	private bool isOn = false;

	[SerializeField] private Activable activable;

	// Use this for initialization
	void Start () {
		if (autoDesactivate)
			cdeltaAutoDesactivate = deltaAutoDesactivate;
		if (particleP != null)
			cdeltaParticlePlay = deltaParticlePlay;
	}
	
	// Update is called once per frame
	void Update () {
		if (start) {
			if (particleP != null && isOn) {
				cdeltaParticlePlay -= Time.deltaTime;
				if(cdeltaParticlePlay < 0) {
					ParticleSystem pSystem = particleP.GetComponent<ParticleSystem> ();
					if(!pSystem.isPlaying)
						pSystem.Play ();
					if (activable != null)
						activable.Activate ();
				}
			}
			if (autoDesactivate) {
				cdeltaAutoDesactivate -= Time.deltaTime;
				if (cdeltaAutoDesactivate < 0) {
					if (particleP != null) {
						ParticleSystem pSystem = particleP.GetComponent<ParticleSystem> ();
						pSystem.Stop ();
					}
//					if (activable != null)
//						activable.Activate ();
					start = false;
					isOn = false;
				}
			}
		}
	}

	public void Act() {
		if (!isOn) {
			start = true;
			isOn = true;
		} else {
//			Debug.Log ("isOn");
			start = false;
			isOn = false;
			ParticleSystem pSystem = particleP.GetComponent<ParticleSystem> ();
//			if (pSystem.isPlaying)
//				Debug.Log ("emit");
			pSystem.Stop ();
//			if (activable != null)
//				activable.Activate ();
		}
	}

}
