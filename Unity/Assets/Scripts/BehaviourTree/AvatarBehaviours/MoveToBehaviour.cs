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

	private float nodeTime;
	public float TimeOfNode = 5f;
	private Node oldNode;

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
			oldNode = aStar.GetCurrentNode (transform.position);
			nodeTime = TimeOfNode;
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
				//Debug.Log ("next step");
				oldNode = path[0];
				path.RemoveAt (0);
				nodeTime = TimeOfNode;
				if (path.Count > 0) {
					nextStep = path [0].position;

					h = nextStep.x - transform.position.x;
					v = nextStep.z - transform.position.z;
				} else {
					h = 0;
					v = 0;
				}
			} else {
				if(aStar.IsOnThisNode(transform.position,oldNode))
					nodeTime -= Time.deltaTime;
				if (IsStoped ()) {
					Node newNode = aStar.GetReplacementNode (oldNode, path [0]);
					nodeTime = TimeOfNode;
					//path.Insert (0, newNode);
					//Debug.Log ("stopped");
					path [0] = newNode;
					aStar.SetPath (path);
				}
				nextStep = path [0].position;

				h = nextStep.x - transform.position.x;
				v = nextStep.z - transform.position.z;
			}
			run = false;
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

	void Rotating(float horizontal, float vertical)
	{
		if((isMoving) )
		{
			//Debug.Log ("rotation");
			Vector3 direction = path [0].position - transform.position;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation (direction), turnSmoothing * Time.deltaTime);
		}
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

	private bool IsStoped() {
		if (nodeTime <= 0f)
			return true;
		return false;
	}
}
