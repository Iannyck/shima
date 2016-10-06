using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveToBehaviour : AbstractBehaviour {

	public string roomToGo;
	public GameObject target;
	public float avatarSpeed;

	private bool isInit = false;
	private List<Node> path;

	private Vector3 movement;
	public Rigidbody playerRigidbody;

	public GameObject aStarGameObject;
	private PathFinding aStar;

	public MoveToBehaviour (string roomToGo, float speed, Rigidbody playerRigidbody)
	{
		this.roomToGo = roomToGo;
		this.speed = speed;
		this.playerRigidbody = playerRigidbody;
	}

	public override State Execute ()
	{
		if (isInit) {
			if (IsTargetRoomReached ()) {
				h = 0;
				v = 0;
				anim.SetFloat (hFloat, h);
				anim.SetFloat (vFloat, v);

				anim.SetBool (groundedBool, true);
				MovementManagement (h, v, run);
				Debug.Log ("reached");
				return State.Suceeded;
			}
			else {
				GetHV ();
				anim.SetFloat (hFloat, h);
				anim.SetFloat (vFloat, v);

				anim.SetBool (groundedBool, true);
				MovementManagement (h, v, run);
			}
		} else {
			path = aStar.FindPath (transform.position, target.transform.position);
			aStar.SetPath (path);
			isInit = true;
		}
		return State.Running;
	}

	private bool IsTargetRoomReached() {
		if (path != null && path.Count == 0)
			return true;
		return false;
	}


	private Vector2 GetNextSet() {
		return new Vector2();
	}


	public float walkSpeed = 0.15f;
	public float runSpeed = 1.0f;

	public float turnSmoothing = 3.0f;
	public float speedDampTime = 0.1f;

	private float speed;

	private Vector3 lastDirection;

	private Animator anim;
	private int speedFloat;
	private int hFloat;
	private int vFloat;

	private int groundedBool;
//	private Transform cameraTransform;

	private float h;
	private float v;

	private bool run;

	private bool isMoving;

	void Awake()
	{
		aStar = aStarGameObject.GetComponent<PathFinding> ();

		anim = GetComponent<Animator> ();
//		cameraTransform = Camera.main.transform;

		speedFloat = Animator.StringToHash("Speed");
		hFloat = Animator.StringToHash("H");
		vFloat = Animator.StringToHash("V");

		groundedBool = Animator.StringToHash("Grounded");
	}

	void GetHV() {
		Vector3 nextStep;
		if (path [0] != null) {
			if (aStar.IsOnThisNode (transform.position, path [0])) {
				Debug.Log ("next step");
				path.RemoveAt (0);
				if (path.Count > 0) {
					nextStep = path [0].position;

					h = nextStep.x - transform.position.x;
					v = nextStep.z - transform.position.z;
				} else {
					h = 0;
					v = 0;
				}
//				Debug.Log ("position= "+nextStep.x + " "+nextStep.y+" "+nextStep.z);
//				Debug.Log ("my position= "+transform.position.x + " "+transform.position.y+" "+transform.position.z);
//				Debug.Log ("h= " + h + " v=" + v);
			} else {
				nextStep = path [0].position;

				h = nextStep.x - transform.position.x;
				v = nextStep.z - transform.position.z;
			}
			run = false;
//			Debug.Log ("h= " + h + " v=" + v);
			isMoving = Mathf.Abs (h) > 0.1 || Mathf.Abs (v) > 0.1;
		}
	}

	void MovementManagement(float horizontal, float vertical, bool running)
	{
		Rotating(horizontal, vertical);

		if(isMoving)
		{
			if (running)
			{
				speed = runSpeed;
			}
			else
			{
				speed = walkSpeed;
			}

			anim.SetFloat(speedFloat, speed, speedDampTime, Time.deltaTime);
		}
		else
		{
			speed = 0f;
			anim.SetFloat(speedFloat, 0f);
		}
		GetComponent<Rigidbody>().AddForce(Vector3.forward*speed);
	}

	Vector3 Rotating(float horizontal, float vertical)
	{
//		Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
//		Vector3 forward = target.transform.TransformDirection(Vector3.left);
//		forward = forward.normalized;
//
//		Vector3 right = new Vector3(forward.z, 0, -forward.x);
//
//		Vector3 targetDirection;
//
		float finalTurnSmoothing;
//
//		targetDirection = forward * vertical + right * horizontal;
		finalTurnSmoothing = turnSmoothing;
//
//		if((isMoving && targetDirection != Vector3.zero) )
//		{
//			Quaternion targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);
//
//			Quaternion newRotation = Quaternion.Slerp(GetComponent<Rigidbody>().rotation, targetRotation, finalTurnSmoothing * Time.deltaTime);
//
//			GetComponent<Rigidbody>().MoveRotation (newRotation);
//			lastDirection = targetDirection;
//		}
		if((isMoving) )
		{
			Debug.Log ("rotation");
			Vector3 direction = path [0].position - transform.position;
			transform.rotation = Quaternion.LookRotation (direction);
		}

//		if(!(Mathf.Abs(h) > 0.9 || Mathf.Abs(v) > 0.9))
//		{
//			Repositioning();
//		}
//
//		return targetDirection;
		return Vector3.up;
	}	

	private void Repositioning()
	{
		Vector3 repositioning = lastDirection;
		if(repositioning != Vector3.zero)
		{
			repositioning.y = 0;
			Quaternion targetRotation = Quaternion.LookRotation (repositioning, Vector3.up);
			Quaternion newRotation = Quaternion.Slerp(GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);
			GetComponent<Rigidbody>().MoveRotation (newRotation);
		}
	}
}
