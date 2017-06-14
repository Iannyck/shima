using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFPS : MonoBehaviour {

	public float cameraSensitivity = 90;
	public float climbSpeed = 4;
	public float normalMoveSpeed = 10;
	public float slowMoveFactor = 0.25f;
	public float fastMoveFactor = 3;

	private float rotationX = 0.0f;
	private float rotationY = 0.0f;

	private Quaternion initRotation;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = false;
		initRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		rotationX += Input.GetAxis ("Mouse X") * cameraSensitivity * Time.deltaTime;
		rotationX = Mathf.Clamp (rotationX, -90, 90);
		rotationY += Input.GetAxis ("Mouse Y") * cameraSensitivity * Time.deltaTime;
		rotationY = Mathf.Clamp (rotationY, -90, 90);

		transform.localRotation = Quaternion.AngleAxis (rotationX, Vector3.up);
		transform.localRotation *= Quaternion.AngleAxis (rotationY, Vector3.left);

		if (Input.GetKeyDown (KeyCode.L)) {
			Screen.lockCursor = !Screen.lockCursor;
		}

	}
}
