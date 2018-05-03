using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour {

	public float cameraSensitivity = 90;
	public float climbSpeed = 4;
	public float normalMoveSpeed = 10;
	public float slowMoveFactor = 0.25f;
	public float fastMoveFactor = 3;

	private float rotationX = 0.0f;
	private float rotationY = 0.0f;

	private bool isEnable = false;

	private Quaternion initRotation;
	private Vector3 initPosition;

	void Start ()
	{
		Screen.lockCursor = false;
		initPosition = transform.position;
		initRotation = transform.rotation;
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown(2))
			isEnable = true;
		if (Input.GetMouseButtonUp(2))
			isEnable = false;
		if (isEnable) {
			rotationX += Input.GetAxis ("Mouse X") * cameraSensitivity * Time.deltaTime;
			rotationY += Input.GetAxis ("Mouse Y") * cameraSensitivity * Time.deltaTime;
			rotationY = Mathf.Clamp (rotationY, -90, 90);

			transform.localRotation = Quaternion.AngleAxis (rotationX, Vector3.up);
			transform.localRotation *= Quaternion.AngleAxis (rotationY, Vector3.left);

			if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
				transform.position += transform.forward * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis ("Vertical") * Time.deltaTime;
				transform.position += transform.right * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis ("Horizontal") * Time.deltaTime;
			} else if (Input.GetKey (KeyCode.LeftControl) || Input.GetKey (KeyCode.RightControl)) {
				transform.position += transform.forward * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis ("Vertical") * Time.deltaTime;
				transform.position += transform.right * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis ("Horizontal") * Time.deltaTime;
			} else {
				transform.position += transform.forward * normalMoveSpeed * Input.GetAxis ("Vertical") * Time.deltaTime;
				transform.position += transform.right * normalMoveSpeed * Input.GetAxis ("Horizontal") * Time.deltaTime;
			}


			if (Input.GetKey (KeyCode.Q)) {
				transform.position += transform.up * climbSpeed * Time.deltaTime;
			}
			if (Input.GetKey (KeyCode.E)) {
				transform.position -= transform.up * climbSpeed * Time.deltaTime;
			}

			if (Input.GetKeyDown (KeyCode.L)) {
				Screen.lockCursor = !Screen.lockCursor;
			}

			if (Input.GetKeyDown (KeyCode.I)) {
				isEnable = false;
				transform.position = initPosition;
				transform.rotation = initRotation;
//				transform.position = new Vector3 (0, 96, 0);
//			transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
//			transform.localRotation *= Quaternion.AngleAxis(0.7f, new Vector3(0, 0, 0.7f));
			}
		}
	}
}
