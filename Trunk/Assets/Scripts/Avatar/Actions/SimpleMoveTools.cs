using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveTools : SimpleBTAction {

	private GameObject toolToMove;
	private Vector3 positionToPut;

	public SimpleMoveTools (int forgetErrorRate, GameObject toolToMove, Vector3 positionToPut) : base (forgetErrorRate)
	{
		this.toolToMove = toolToMove;
		this.positionToPut = positionToPut;
	}

	public GameObject ToolToMove {
		get {
			return this.toolToMove;
		}
		set {
			toolToMove = value;
		}
	}

	public Vector3 PositionToPut {
		get {
			return this.positionToPut;
		}
		set {
			positionToPut = value;
		}
	}

}
