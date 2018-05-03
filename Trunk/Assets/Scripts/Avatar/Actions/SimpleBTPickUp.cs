using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBTPickUp : SimpleBTAction {

	private GameObject itemToPickUp;

	public SimpleBTPickUp (int forgetErrorRate, GameObject itemToPickUp) : base (forgetErrorRate)
	{
		this.itemToPickUp = itemToPickUp;
	}

	public GameObject ItemToPickUp {
		get {
			return this.itemToPickUp;
		}
		set {
			itemToPickUp = value;
		}
	}
}
