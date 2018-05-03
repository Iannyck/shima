using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioTest : MonoBehaviour {

	[SerializeField]
	private float duration = 2f;

	[SerializeField]
	private int delta = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		duration -= Time.deltaTime;
		if (duration < 0) {
			transform.Translate (new Vector3 (delta, 0, 0));
			duration = 2f;
		}
	}
}
