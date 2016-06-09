using UnityEngine;
using System.Collections;

public class ElectricityLogger : MonoBehaviour {

	private string phase1;
	private string phase2;
	private string phase3;

	void OnGUI() {
		GUI.Box(new Rect(10,10,100,50) , "test");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Phase 1: "+phase1);
		Debug.Log ("Phase 2: "+phase2);
		Debug.Log ("Phase 3: "+phase3);
		Debug.Log (" --- ");
	}

	public void PhasesStates(string phase1, string phase2, string phase3) {
		this.phase1 = phase1;
		this.phase2 = phase2;
		this.phase3 = phase3;
	}
}
