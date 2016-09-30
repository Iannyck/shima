using UnityEngine;
using System.Collections;

public class MoveToBehaviour : AbstractBehaviour {

	public string roomToGo;
	public float avatarSpeed;

	private Vector3 movement;
	public Rigidbody playerRigidbody;

	public MoveToBehaviour (string roomToGo, float speed, Rigidbody playerRigidbody)
	{
		this.roomToGo = roomToGo;
		this.speed = speed;
		this.playerRigidbody = playerRigidbody;
	}

	public override State Execute ()
	{
		if (IsTargetRoomReached ())
			return State.Suceeded;
		else {
			GetHV ();
			anim.SetFloat(hFloat, h);
			anim.SetFloat(vFloat, v);

			anim.SetBool (groundedBool, true);
			MovementManagement(h,v,run);
		}
		return State.Running;
	}

	private string GetCurrentRoom() {
		return null;
	}

	private bool IsTargetRoomReached() {
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
	private Transform cameraTransform;

	private float h;
	private float v;

	private bool run;

	private bool isMoving;

	void Awake()
	{
		anim = GetComponent<Animator> ();
		cameraTransform = Camera.main.transform;

		speedFloat = Animator.StringToHash("Speed");
		hFloat = Animator.StringToHash("H");
		vFloat = Animator.StringToHash("V");

		groundedBool = Animator.StringToHash("Grounded");
	}

	void GetHV() {
		h = 1;
		v = 0;
		run = false;
		isMoving = Mathf.Abs(h) > 0.1 || Mathf.Abs(v) > 0.1;
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
		Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
		forward = forward.normalized;

		Vector3 right = new Vector3(forward.z, 0, -forward.x);

		Vector3 targetDirection;

		float finalTurnSmoothing;

		targetDirection = forward * vertical + right * horizontal;
		finalTurnSmoothing = turnSmoothing;

		if((isMoving && targetDirection != Vector3.zero) )
		{
			Quaternion targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);

			Quaternion newRotation = Quaternion.Slerp(GetComponent<Rigidbody>().rotation, targetRotation, finalTurnSmoothing * Time.deltaTime);
			GetComponent<Rigidbody>().MoveRotation (newRotation);
			lastDirection = targetDirection;
		}

		if(!(Mathf.Abs(h) > 0.9 || Mathf.Abs(v) > 0.9))
		{
			Repositioning();
		}

		return targetDirection;
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
