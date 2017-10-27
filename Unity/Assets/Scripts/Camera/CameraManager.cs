using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera manager. This script manages all cameras in the scene.
/// </summary>
public class CameraManager : MonoBehaviour {

	/// <summary>
	/// The list of cameras in the scene.
	/// </summary>
	[SerializeField] private List<GameObject> cameras;

	/// <summary>
	/// The current camera.
	/// </summary>
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

	/// <summary>
	/// Switchs the camera.
	/// </summary>
	/// <param name="number">Position of the camera in the list.</param>
	private void SwitchCamera(int number) {
		current_camera.SetActive (false);
		current_camera = cameras [number];
		current_camera.SetActive (true);
	}
}
