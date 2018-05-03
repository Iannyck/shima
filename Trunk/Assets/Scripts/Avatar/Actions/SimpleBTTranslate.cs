using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBTTranslate : SimpleBTAction {

	private GameObject itemToMove;
	private GameObject container;
	private GameObject positionObjToPut;

	public SimpleBTTranslate (GameObject itemToMove, GameObject container, GameObject positionObjToPut) : base (0)
	{
		this.itemToMove = itemToMove;
		this.container = container;
		this.positionObjToPut = positionObjToPut;
	}
	
	public GameObject ItemToMove {
		get {
			return this.itemToMove;
		}
	}

	public GameObject Container {
		get {
			return this.container;
		}
	}

	public GameObject PositionObjToPut {
		get {
			return this.positionObjToPut;
		}
	}

}
