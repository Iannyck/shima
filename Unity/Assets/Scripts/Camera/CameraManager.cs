using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	[SerializeField] private List<GameObject> cameras;
	[SerializeField] private GameObject current_camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.F1)) {
			SwitchCamera (0);
		} else if (Input.GetKey (KeyCode.F2)) {
			SwitchCamera (1);
		} else if (Input.GetKey (KeyCode.F3)) {
			SwitchCamera (2);
		}
	}

	private void SwitchCamera(int number) {
		current_camera.SetActive (false);
		current_camera = cameras [number];
		current_camera.SetActive (true);
	}
}
