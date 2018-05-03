using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBTSpawn : SimpleBTAction {

	private GameObject itemToSpawn;

	public SimpleBTSpawn (int forgetErrorRate, GameObject itemToActivate) : base (forgetErrorRate)
	{
		this.itemToSpawn = itemToActivate;
	}

	public GameObject ItemToSpawn {
		get {
			return this.itemToSpawn;
		}
		set {
			itemToSpawn = value;
		}
	}
}
