using UnityEngine;
using System.Collections;

public class MoveToBehaviour : AbstractBehaviour {

	private string roomToGo;
	private string timeToGo;
	private GameObject avatar;

	public MoveToBehaviour (string roomToGo, string timeToGo, GameObject avatar)
	{
		this.roomToGo = roomToGo;
		this.timeToGo = timeToGo;
		this.avatar = avatar;
	}

	public override State Execute ()
	{
		if (IsTargetRoomReached ())
			return State.Suceeded;
		return State.Running;
	}

	private string GetCurrentRoom() {
		return null;
	}

	private bool IsTargetRoomReached() {
		return false;
	}
}
