using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBTDestroy : SimpleBTAction {

	private GameObject itemToDestroy;

	public SimpleBTDestroy (GameObject itemToDestroy) : base (0)
	{
		this.itemToDestroy = itemToDestroy;
	}

	public GameObject ItemToDestroy {
		get {
			return this.itemToDestroy;
		}
		set {
			itemToDestroy = value;
		}
	}
}
