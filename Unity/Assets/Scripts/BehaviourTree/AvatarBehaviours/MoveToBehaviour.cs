using UnityEngine;
using System.Collections;

public class MoveToBehaviour : AbstractBehaviour {

	public string roomToGo;
	public float speed;

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
			Move (1,0);
		}
		return State.Running;
	}

	private string GetCurrentRoom() {
		return null;
	}

	private bool IsTargetRoomReached() {
		return false;
	}

	private void Move(float h, float v)
	{
		Debug.Log ("Move");
		movement.Set(h, 0f, v);

		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition(transform.position + movement);

//		Turning();
	}

//	private void Turning()
//	{
//		float faceDirection = Input.GetAxisRaw("Horizontal") * -1;
//		float faceOrientation = Input.GetAxisRaw("Vertical");
//		transform.forward = new Vector3(faceOrientation, 0, faceDirection);
//
//	}
}
